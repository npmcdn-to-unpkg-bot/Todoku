using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Ionic.Zip;
using Todoku.Models;
using System.Data;
using System.Data.OleDb;
using System.Web.Security;

namespace Todoku.Areas.Merchants.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Merchants/Product/

        public ActionResult Index()
        {
            Int32 MerchantID = Convert.ToInt32(TempData.Peek("MerchantID"));
            BusinessLayer db = new BusinessLayer();
            List<Product> products = db.products.Where(x => x.MerchantID == MerchantID).ToList();
            return View(products);
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file, Int32 hdnProductID = 0)
        {
            if (file.ContentLength > 0)
            {
                String fileName = Path.GetFileName(file.FileName);
                String ext = Path.GetExtension(file.FileName);
                if (ext == ".zip")
                {
                    using (BusinessLayer db = new BusinessLayer())
                    {
                        try
                        {
                            #region Unzip File
                            Int32 MerchantID = Convert.ToInt32(TempData.Peek("MerchantID"));
                            Merchant merchant = db.merchants.FirstOrDefault(x => x.MerchantID == MerchantID);

                            String MerchantFolder = Path.Combine(Server.MapPath(SystemSetting.Default_Upload_Path), merchant.MerchantCode);

                            if (!Directory.Exists(MerchantFolder)) Directory.CreateDirectory(MerchantFolder);

                            var path = Path.Combine(Server.MapPath(SystemSetting.Default_Upload_Path), fileName);
                            file.SaveAs(path);

                            var options = new ReadOptions { StatusMessageWriter = System.Console.Out };
                            using (ZipFile zip = ZipFile.Read(path, options))
                            {
                                foreach (ZipEntry e in zip)
                                {
                                    if (!System.IO.File.Exists(MerchantFolder + "/" + e.FileName))
                                    {
                                        e.Extract(MerchantFolder);
                                    }
                                    else
                                    {
                                        System.IO.File.Delete(MerchantFolder + "/" + e.FileName);
                                        e.Extract(MerchantFolder);
                                    }
                                }
                            }
                            System.IO.File.Delete(path);
                            #endregion

                            if (hdnProductID == 0) 
                            {
                                #region Read List Produk
                                String source = MerchantFolder + @"\Produk\Produk.xlsx";
                                String con = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};" + @"Extended Properties='Excel 12.0;HDR=Yes;IMEX=1'", source);
                                using (OleDbConnection connection = new OleDbConnection(con))
                                {
                                    connection.Open();
                                    OleDbCommand command = new OleDbCommand("select * from [Sheet1$]", connection);
                                    using (OleDbDataReader dr = command.ExecuteReader())
                                    {
                                        while (dr.Read())
                                        {
                                            Product prd = new Product();
                                            ProductsDetails detail = new ProductsDetails();

                                            var Code = dr[0];
                                            var Name = dr[1];
                                            var Kategori = dr[3];
                                            var Price = dr[4];
                                            var Profit = dr[5];
                                            var discount1 = dr[6];
                                            var discount2 = dr[7];
                                            var weight = dr[10];
                                            var stock = dr[11];
                                            var description = dr[12];

                                            prd.ProductCode = Code.ToString();
                                            prd.ProductName = Name.ToString();
                                            prd.Category = Kategori.ToString();
                                            prd.MerchantID = MerchantID;
                                            if (new System.IO.FileInfo(String.Format(@"{0}\Produk\{1}\Produk.jpg", MerchantFolder, prd.ProductCode)).Length / 1024 > 100)
                                            {
                                                System.IO.File.Delete(String.Format(@"{0}\Produk\{1}\Produk.jpg", MerchantFolder, prd.ProductCode));
                                            }

                                            prd.ImgLink = String.Format(@"~\Uploads\{0}\Produk\{1}\Produk.jpg", merchant.MerchantCode, prd.ProductCode);

                                            prd.Description = description.ToString();
                                            prd.IsDeleted = false;
                                            prd.CreatedBy = Membership.GetUser().UserName;
                                            prd.CreatedDate = DateTime.Now;

                                            prd.detail = detail;
                                            detail.Quantity = 0;
                                            detail.Price = Convert.ToDecimal(Price);
                                            detail.DiscountAmount = Convert.ToInt32(discount1);
                                            detail.DiscountAmount2 = Convert.ToInt32(discount2);
                                            detail.DiscountAmount3 = 0;
                                            detail.Weight = Convert.ToInt32(weight);
                                            detail.Quantity = Convert.ToInt32(stock);
                                            detail.VATPercentage = SystemSetting.VATPercentage;
                                            Decimal total = (detail.Price * (1 - ((detail.DiscountInPercentage + detail.DiscountInPercentage2) / 100) + (detail.DiscountInPercentage * detail.DiscountInPercentage2 / 10000)));
                                            detail.VATAmount = (total * SystemSetting.VATPercentage / 100);
                                            detail.LineAmount = total * (100 + SystemSetting.VATPercentage) / 100;
                                            detail.IsDeleted = false;
                                            detail.CreatedBy = Membership.GetUser().UserName;
                                            detail.CreatedDate = DateTime.Now;

                                            db.products.Add(prd);
                                        }
                                        db.SaveChanges();
                                    }
                                }
                                #endregion
                            }

                            return RedirectToAction("Index");
                        }
                        catch (Exception ex)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create() 
        {
            Product product = new Product();
            ViewBag.Category = new SelectList(new BusinessLayer().standardcodes.Where(x => x.ParentID == SCConstant.Kategori_Produk).ToList(), "StandardCodeID", "StandardCodeName");
            product.detail = new ProductsDetails();
            product.detail.Quantity = 0;
            product.detail.Price = 0;
            product.detail.DiscountInPercentage = 0;
            product.detail.DiscountInPercentage2 = 0;
            product.detail.LineAmount = 0;
            return View(product);
        }

        [HttpPost]
        public JsonResult Create(Product product) 
        {
            try
            {
                BusinessLayer db = new BusinessLayer();
                Product entity = new Product();

                entity.ProductCode = product.ProductCode;
                entity.ProductName = product.ProductName;
                entity.Category = product.Category;
                entity.MerchantID = Convert.ToInt32(TempData.Peek("MerchantID"));
                Merchant merchant = db.merchants.Find(entity.MerchantID);
                //entity.ImgLink = String.Format(@"~\Uploads\{0}\Produk\{1}\Produk.jpg", merchant.MerchantCode, entity.ProductCode);
                entity.Description = product.Description;

                entity.detail = new ProductsDetails();
                entity.detail.Quantity = product.detail.Quantity;
                entity.detail.Price = Convert.ToDecimal(product.detail.Price);
                entity.detail.DiscountInPercentage = product.detail.DiscountInPercentage;
                entity.detail.DiscountInPercentage2 = product.detail.DiscountInPercentage2;
                entity.detail.DiscountInPercentage3 = product.detail.DiscountInPercentage3;
                entity.detail.VATPercentage = SystemSetting.VATPercentage;
                Decimal total = (product.detail.Price * (1 - (((Decimal)entity.detail.DiscountInPercentage + entity.detail.DiscountInPercentage2) / 100) + ((Decimal)entity.detail.DiscountInPercentage * entity.detail.DiscountInPercentage2 / 10000)));
                entity.detail.VATAmount = total * SystemSetting.VATPercentage / 100;
                entity.detail.LineAmount = total * (100 + SystemSetting.VATPercentage) / 100;

                db.products.Add(entity);
                db.SaveChanges();
                return Json(new { ok = true, Status = "Success" });
            }
            catch (Exception ex)
            {
                ViewBag.Category = new SelectList(new BusinessLayer().standardcodes.Where(x => x.ParentID == SCConstant.Kategori_Produk).ToList(), "StandardCodeID", "StandardCodeName");
                ModelState.AddModelError("", ex.Message);
                return Json(new { ok = false, Status = ex.Message });
            }
        }

        public ActionResult Edit(Int32 id) 
        {
            BusinessLayer db = new BusinessLayer();
            Product product = db.products.Find(id);
            ViewBag.Category = new SelectList(new BusinessLayer().standardcodes.Where(x => x.ParentID == SCConstant.Kategori_Produk).ToList(), "StandardCodeID", "StandardCodeName", product.Category);
            return View(product);
        }

        [HttpPost]
        public JsonResult Edit(Product product) 
        {
            if (TempData.Peek("MerchantID") != null)
            {
                using (BusinessLayer db = new BusinessLayer())
                {
                    try
                    {
                        Product entity = db.products.Find(product.ProductID);
                        entity.ProductName = product.ProductName;
                        entity.Category = product.Category;
                        entity.MerchantID = Convert.ToInt32(TempData.Peek("MerchantID"));
                        Merchant merchant = db.merchants.Find(entity.MerchantID);
                        entity.ImgLink = String.Format(@"~\Uploads\{0}\Produk\{1}\Produk.jpg", merchant.MerchantCode, entity.ProductCode);
                        entity.Description = product.Description;
                        entity.LastUpdatedBy = Membership.GetUser().UserName;
                        entity.LastUpdatedDate = DateTime.Now;
                        if (entity.detail == null)
                        {
                            entity.detail = new ProductsDetails();
                            entity.detail.Quantity = product.detail.Quantity;
                            entity.detail.Weight = product.detail.Weight;
                            entity.detail.Price = Convert.ToDecimal(product.detail.Price);
                            entity.detail.DiscountInPercentage = product.detail.DiscountInPercentage;
                            entity.detail.DiscountInPercentage2 = product.detail.DiscountInPercentage2;
                            entity.detail.DiscountInPercentage3 = product.detail.DiscountInPercentage3;
                            Decimal total = (product.detail.Price * (1 - (((Decimal)entity.detail.DiscountInPercentage + entity.detail.DiscountInPercentage2) / 100) + ((Decimal)entity.detail.DiscountInPercentage * entity.detail.DiscountInPercentage2 / 10000)));
                            entity.detail.VATAmount = total * SystemSetting.VATPercentage / 100;
                            entity.detail.LineAmount = total * (100 + SystemSetting.VATPercentage) / 100;
                        }
                        else 
                        {
                            entity.detail.Quantity = product.detail.Quantity;
                            entity.detail.Weight = product.detail.Weight;
                            entity.detail.Price = Convert.ToDecimal(product.detail.Price);
                            entity.detail.DiscountInPercentage = product.detail.DiscountInPercentage;
                            entity.detail.DiscountInPercentage2 = product.detail.DiscountInPercentage2;
                            entity.detail.DiscountInPercentage3 = product.detail.DiscountInPercentage3;
                            Decimal total = (product.detail.Price * (1 - (((Decimal)entity.detail.DiscountInPercentage + entity.detail.DiscountInPercentage2) / 100) + ((Decimal)entity.detail.DiscountInPercentage * entity.detail.DiscountInPercentage2 / 10000)));
                            entity.detail.VATAmount = total * SystemSetting.VATPercentage / 100;
                            entity.detail.LineAmount = total * (100 + SystemSetting.VATPercentage) / 100;
                        }
                        
                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges();
                        return Json(new { ok = true, status = "Success" });
                    }
                    catch (Exception ex)
                    {
                        return Json(new { ok = false, status = ex.Message });
                    }
                }
            }
            else 
            {
                return Json(new { ok = false, status = "MerchantID is null" });;
            }
        }

        public ActionResult Detail() 
        {
            return View();
        }

        public ActionResult Delete(int id)
        {
            BusinessLayer db = new BusinessLayer();
            Product product = db.products.Find(id);
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Int32 id) 
        {
            try
            {
                BusinessLayer db = new BusinessLayer();
                Product product = db.products.Find(id);
                db.Entry(product).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception ex) 
            {
                String errMessage = ex.Message;
                return View("Delete");
            }
        }
    }
}

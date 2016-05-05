using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Todoku.Models
{
    public class SampleData : DropCreateDatabaseIfModelChanges<BusinessLayer>
    {
        protected override void Seed(BusinessLayer context)
        {
            try
            {
                #region Bank
                new List<Bank>
                {
                    new Bank { BankID = 1, BankName = "Mandiri", AccountName = "PT. Todoku", AccountNo = "123-456-789-0", IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                    new Bank { BankID = 2, BankName = "BCA", AccountName = "PT. Todoku", AccountNo = "123-456-789-0", IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now}
                }.ForEach(x => context.banks.Add(x));
                #endregion

                #region MerchantsRegistration
                new List<MerchantRegistration>
                {
                    new MerchantRegistration { RegistrationCode = String.Format("{0}/{1}/0001", SystemSetting.RegisMerchantCode, DateTime.Now.ToString("yyyyMMdd")), 
                        MerchantName = "Ace Hardware", 
                        AddressCode = String.Format("{0}{1}{2}", SystemSetting.MerchantCode, DateTime.Now.ToString("yyyyMMdd"), "0001"), 
                        OwnerID = 2, StartPrice = 10000, EndPrice = 100000000, RegistrationDate = DateTime.Now, RegistrationStatus = RegistrationStatus.ConfirmedByManagement, 
                        Description = "Ace Hardware sell home appliance", IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now,
                        address = new Addresses {AddressCode = String.Format("{0}{1}{2}", SystemSetting.MerchantCode, DateTime.Now.ToString("yyyyMMdd"), "0001"),
                        Province = "SC0002.006", RajaOngkir_Province_ID = 6, City = "JAKARTA PUSAT", RajaOngkir_City_ID = 152, Email = "ace.hardware@yahoo.com", Address = "Jln. xxx", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, Handphone = "0812345679", Country = "SC0003.001", IsDeleted = false, ZipCode = "10110" }},
                    new MerchantRegistration { RegistrationCode = String.Format("{0}/{1}/0002", SystemSetting.RegisMerchantCode, DateTime.Now.ToString("yyyyMMdd")), MerchantName = "Electronic City", AddressCode = String.Format("{0}{1}{2}", SystemSetting.MerchantCode, DateTime.Now.ToString("yyyyMMdd"), "0002"), OwnerID = 2, StartPrice = 10000, EndPrice = 100000000, RegistrationDate = DateTime.Now, RegistrationStatus = RegistrationStatus.Request, Description = "Electronic City sell all type of electronic such as Television, Printer, Laptop, Air Conditioner, etc.", IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now,
                        address = new Addresses {AddressCode = String.Format("{0}{1}{2}", SystemSetting.MerchantCode, DateTime.Now.ToString("yyyyMMdd"), "0002"),
                        Province = "SC0002.006", RajaOngkir_Province_ID = 6, City = "JAKARTA PUSAT", RajaOngkir_City_ID = 152, Email = "electronic.city@yahoo.com", Address = "Jln. xxx", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, Handphone = "0812345679", Country = "SC0003.001", IsDeleted = false, ZipCode = "10110" }},
                }.ForEach(x => context.merchantRegistrations.Add(x));
                #endregion

                #region Merchants
                new List<Merchant>
                {
                    new Merchant { MerchantID = 1, MerchantCode = String.Format("{0}/{1}/0001", SystemSetting.RegisMerchantCode, DateTime.Now.ToString("yyyyMMdd")), MerchantName = "Ace Hardware", IsActive = true, OwnerID = 2, JoinDate = DateTime.Now, AddressCode = String.Format("{0}{1}{2}", SystemSetting.MerchantCode, DateTime.Now.ToString("yyyyMMdd"), "0001"), Description = "Ace Hardware sell home appliance", IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now },
                    //new Merchant { MerchantCode = "TDM000002", MerchantName = "Electronic City", IsActive = true, OwnerID = 1, JoinDate = DateTime.Now },
                }.ForEach(x => context.merchants.Add(x));
                #endregion

                #region Product Attribute Group
                new List<ProductAttributeGroup>()
                {
                    new ProductAttributeGroup { GroupName = "Ukuran", IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                    new ProductAttributeGroup { GroupName = "Warna", IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now}
                }.ForEach(x => context.productAttributeGroups.Add(x));
                #endregion

                #region Product
                new List<Product>
                {
                    new Product {ProductID = 1, MerchantID = 1, ProductCode = "CNSL0001", ProductName = "Item 1", ImgLink = @"~\Uploads\RMC/20160426/0001\Produk\item_1\Produk.jpg", Category="SC0010.017", IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now, detail = new ProductDt { Price = 100000, Quantity = 10, Weight = 1000, Profit = 0, DiscountAmount = 0, DiscountAmount2 = 0, DiscountAmount3 = 0, DiscountInPercentage = 0, DiscountInPercentage2 = 0, DiscountInPercentage3 = 0, VATAmount = 10000, VATPercentage = 10, LineAmount=110000, IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now } },
                    new Product {ProductID = 2, MerchantID = 1, ProductCode = "CNSL0002", ProductName = "Item 2", ImgLink = @"~\Uploads\RMC/20160426/0001\Produk\item_2\Produk.jpg", Category="SC0010.017", IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now, detail = new ProductDt { Price = 100000, Quantity = 10, Weight = 1000, Profit = 0, DiscountAmount = 0, DiscountAmount2 = 0, DiscountAmount3 = 0, DiscountInPercentage = 0, DiscountInPercentage2 = 0, DiscountInPercentage3 = 0, VATAmount = 10000, VATPercentage = 10, LineAmount=110000, IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now } },
                    new Product {ProductID = 3, MerchantID = 1, ProductCode = "CNSL0003", ProductName = "Item 3", ImgLink = @"~\Uploads\RMC/20160426/0001\Produk\item_3\Produk.jpg", Category="SC0010.017", IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now, detail = new ProductDt { Price = 100000, Quantity = 10, Weight = 1000, Profit = 0, DiscountAmount = 0, DiscountAmount2 = 0, DiscountAmount3 = 0, DiscountInPercentage = 0, DiscountInPercentage2 = 0, DiscountInPercentage3 = 0, VATAmount = 10000, VATPercentage = 10, LineAmount=110000, IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now } },
                    new Product {ProductID = 4, MerchantID = 1, ProductCode = "CNSL0004", ProductName = "Item 4", ImgLink = @"~\Uploads\RMC/20160426/0001\Produk\item_4\Produk.jpg", Category="SC0010.017", IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now, detail = new ProductDt { Price = 100000, Quantity = 10, Weight = 1000, Profit = 0, DiscountAmount = 0, DiscountAmount2 = 0, DiscountAmount3 = 0, DiscountInPercentage = 0, DiscountInPercentage2 = 0, DiscountInPercentage3 = 0, VATAmount = 10000, VATPercentage = 10, LineAmount=110000, IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now } },
                    new Product {ProductID = 5, MerchantID = 1, ProductCode = "CNSL0005", ProductName = "Item 5", ImgLink = @"~\Uploads\RMC/20160426/0001\Produk\item_5\Produk.jpg", Category="SC0010.017", IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now, detail = new ProductDt { Price = 100000, Quantity = 10, Weight = 1000, Profit = 0, DiscountAmount = 0, DiscountAmount2 = 0, DiscountAmount3 = 0, DiscountInPercentage = 0, DiscountInPercentage2 = 0, DiscountInPercentage3 = 0, VATAmount = 10000, VATPercentage = 10, LineAmount=110000, IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now } },
                    new Product {ProductID = 6, MerchantID = 1, ProductCode = "CNSL0006", ProductName = "Item 6", ImgLink = @"~\Uploads\RMC/20160426/0001\Produk\item_6\Produk.jpg", Category="SC0010.017", IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now, detail = new ProductDt { Price = 100000, Quantity = 10, Weight = 1000, Profit = 0, DiscountAmount = 0, DiscountAmount2 = 0, DiscountAmount3 = 0, DiscountInPercentage = 0, DiscountInPercentage2 = 0, DiscountInPercentage3 = 0, VATAmount = 10000, VATPercentage = 10, LineAmount=110000, IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now } },
                    new Product {ProductID = 7, MerchantID = 1, ProductCode = "CNSL0007", ProductName = "Item 7", ImgLink = @"~\Uploads\RMC/20160426/0001\Produk\item_7\Produk.jpg", Category="SC0010.017", IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now, detail = new ProductDt { Price = 100000, Quantity = 10, Weight = 1000, Profit = 0, DiscountAmount = 0, DiscountAmount2 = 0, DiscountAmount3 = 0, DiscountInPercentage = 0, DiscountInPercentage2 = 0, DiscountInPercentage3 = 0, VATAmount = 10000, VATPercentage = 10, LineAmount=110000, IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now } },
                    new Product {ProductID = 8, MerchantID = 1, ProductCode = "CNSL0008", ProductName = "Item 8", ImgLink = @"~\Uploads\RMC/20160426/0001\Produk\item_8\Produk.jpg", Category="SC0010.017", IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now, detail = new ProductDt { Price = 100000, Quantity = 10, Weight = 1000, Profit = 0, DiscountAmount = 0, DiscountAmount2 = 0, DiscountAmount3 = 0, DiscountInPercentage = 0, DiscountInPercentage2 = 0, DiscountInPercentage3 = 0, VATAmount = 10000, VATPercentage = 10, LineAmount=110000, IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now } },
                    new Product {ProductID = 9, MerchantID = 1, ProductCode = "CNSL0009", ProductName = "Item 9", ImgLink = @"~\Uploads\RMC/20160426/0001\Produk\item_9\Produk.jpg", Category="SC0010.017", IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now, detail = new ProductDt { Price = 100000, Quantity = 10, Weight = 1000, Profit = 0, DiscountAmount = 0, DiscountAmount2 = 0, DiscountAmount3 = 0, DiscountInPercentage = 0, DiscountInPercentage2 = 0, DiscountInPercentage3 = 0, VATAmount = 10000, VATPercentage = 10, LineAmount=110000, IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now } },
                }.ForEach(x => context.products.Add(x));
                #endregion

                #region Product Attributes
                new List<ProductAttribute>
                {
                    new ProductAttribute{ AttributeName = "S", ProductID = 1, GroupID = 1, IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now },
                    new ProductAttribute{ AttributeName = "M", ProductID = 1, GroupID = 1, IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now },
                    new ProductAttribute{ AttributeName = "L", ProductID = 1, GroupID = 1, IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now },
                    new ProductAttribute{ AttributeName = "XL", ProductID = 1, GroupID = 1, IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now },

                    new ProductAttribute{ AttributeName = "Merah", ProductID = 1, GroupID = 2, IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now },
                    new ProductAttribute{ AttributeName = "Kuning", ProductID = 1, GroupID = 2, IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now },
                    new ProductAttribute{ AttributeName = "Hijau", ProductID = 1, GroupID = 2, IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now },
                    new ProductAttribute{ AttributeName = "Biru", ProductID = 1, GroupID = 2, IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now },

                    new ProductAttribute{ AttributeName = "S", ProductID = 2, GroupID = 1, IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now },
                    new ProductAttribute{ AttributeName = "M", ProductID = 2, GroupID = 1, IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now },
                    new ProductAttribute{ AttributeName = "L", ProductID = 2, GroupID = 1, IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now },
                    new ProductAttribute{ AttributeName = "XL", ProductID = 2, GroupID = 1, IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now },

                    new ProductAttribute{ AttributeName = "Merah", ProductID = 2, GroupID = 2, IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now },
                    new ProductAttribute{ AttributeName = "Kuning", ProductID = 2, GroupID = 2, IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now },
                    new ProductAttribute{ AttributeName = "Hijau", ProductID = 2, GroupID = 2, IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now },
                    new ProductAttribute{ AttributeName = "Biru", ProductID = 2, GroupID = 2, IsDeleted = false, CreatedBy="sysadmin", CreatedDate = DateTime.Now },
                }.ForEach(x => context.productAttributes.Add(x));
                #endregion

                #region Dashboard
                new List<Dashboard>
                {
                    new Dashboard { DashboardID = 1, DashboardName = "Member", Action = "Index", Controller = "Home", Area = DashboardArea.Members, IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now },
                    new Dashboard { DashboardID = 2, DashboardName = "Administrator", Action = "Index", Controller = "Home", Area = DashboardArea.Admin, IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now },
                    new Dashboard { DashboardID = 3, DashboardName = "Toko", Action = "Index", Controller = "Home", Area = DashboardArea.Merchants, IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now },
                    new Dashboard { DashboardID = 4, DashboardName = "Agen", Action = "Index", Controller = "Home", Area = DashboardArea.Agents, IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now },
                    new Dashboard { DashboardID = 5, DashboardName = "Manajemen", Action = "Index", Controller = "Home", Area = DashboardArea.Management, IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now },
                }.ForEach(x => context.dashboards.Add(x));
                #endregion

                #region Dashboard in UserRole
                new List<DashboardInUserRole>
                {
                    new DashboardInUserRole {UserRole = "systemadministrator", DashboardID = 1, IsDefault = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                    new DashboardInUserRole {UserRole = "systemadministrator", DashboardID = 2, IsDefault = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                    new DashboardInUserRole {UserRole = "systemadministrator", DashboardID = 3, IsDefault = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                    new DashboardInUserRole {UserRole = "systemadministrator", DashboardID = 4, IsDefault = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                    new DashboardInUserRole {UserRole = "systemadministrator", DashboardID = 5, IsDefault = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                    new DashboardInUserRole {UserRole = "administrator", DashboardID = 1, IsDefault = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                    new DashboardInUserRole {UserRole = "administrator", DashboardID = 2, IsDefault = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                    new DashboardInUserRole {UserRole = "member", DashboardID = 1, IsDefault = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                    new DashboardInUserRole {UserRole = "merchantowner", DashboardID = 3, IsDefault = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                    new DashboardInUserRole {UserRole = "agen", DashboardID = 4, IsDefault = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                    new DashboardInUserRole {UserRole = "management", DashboardID = 5, IsDefault = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                }.ForEach(x => context.dashboardinuserroles.Add(x));
                #endregion

                #region Menu
                new List<Menu>
                {
                    new Menu{ MenuID = 1, DashboardID = 1, MenuName = "User", Area = "SC0014.002", Path = "", ParentID = null, IsChildMenu = false, IsParent = true, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                        new Menu{ MenuID = 2, DashboardID = 1, MenuName = "User Profil", Area = "SC0014.002", Path = "Members", ParentID = 1, IsChildMenu = false, IsParent = false, IsActive = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                        new Menu{ MenuID = 3, DashboardID = 1, MenuName = "Riwayat Pembelian", Area = "SC0014.002", Path = "#", Action = "Index", Controller = "Information", ParentID = 1, IsChildMenu = false, IsParent = false, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                        new Menu{ MenuID = 4, DashboardID = 1, MenuName = "Cart", Area = "SC0014.002", Path = "Cart", Action = "Index", Controller = "Cart", ParentID = 1, IsChildMenu = false, IsParent = false, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                        new Menu{ MenuID = 5, DashboardID = 1, MenuName = "Konfirmasi", Area = "SC0014.002", Path = "", ParentID = 1, IsChildMenu = false, IsParent = true, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                            new Menu{ MenuID = 6, DashboardID = 1, MenuName = "Pemesanan Barang", Area = "SC0014.002", Path = "Members/Home/OrderConfirmation", Action = "Index", Controller = "Order", ParentID = 5, IsChildMenu = false, IsParent = false, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                            new Menu{ MenuID = 7, DashboardID = 1, MenuName = "Pembayaran Barang", Area = "SC0014.002", Path = "Members/Home/PaymentConfirmation", Action = "Index", Controller = "Purchasing", ParentID = 5, IsChildMenu = false, IsParent = false, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                        new Menu{ MenuID = 8, DashboardID = 1, MenuName = "Informasi", Area = "SC0014.002", Path = "Members/Information/", Action = "Index", Controller = "Information", ParentID = 1, IsChildMenu = false, IsParent = false, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},

                    new Menu{ MenuID = 9, DashboardID = 3, MenuName = "Toko", Path = "", Area = "SC0014.003", ParentID = null, IsChildMenu = false, IsParent = true, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                        new Menu{ MenuID = 10, DashboardID = 1, MenuName = "Pendaftaran", Area = "SC0014.002", Path = "Merchants/Registration", Action = "Merchant", Controller= "Registration", ParentID = 9, IsChildMenu = false, IsParent = false, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                        new Menu{ MenuID = 11, DashboardID = 3, MenuName = "Overview", Area = "SC0014.003", Path = "Merchants/", ParentID = 9, IsChildMenu = false, IsParent = true, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                            new Menu{ MenuID = 12, DashboardID = 3, MenuName = "Profil", Area = "SC0014.003", Path = "Merchants/Profil/Index", Action = "Index", Controller= "Profil", ParentID = 11, IsChildMenu = true, IsParent = false, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                            new Menu{ MenuID = 13, DashboardID = 3, MenuName = "Produk", Area = "SC0014.003", Path = "Merchants/Product/Index", Action = "Index", Controller= "Product", ParentID = 11, IsChildMenu = true, IsParent = false, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                            new Menu{ MenuID = 14, DashboardID = 3, MenuName = "Order", Area = "SC0014.003", Path = "Merchants/CustomerOrder/Index", Action = "Index", Controller= "CustomerOrder", ParentID = 11, IsChildMenu = true, IsParent = false, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                            new Menu{ MenuID = 15, DashboardID = 3, MenuName = "Pengiriman", Area = "SC0014.003", Path = "Merchants/Shipping/Index", Action = "Index", Controller= "Shipping", ParentID = 11, IsChildMenu = true, IsParent = false, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},

                    new Menu{ MenuID = 16, DashboardID = 4, MenuName = "Agen", Area = "SC0014.005", Path = "", ParentID = null, IsChildMenu = false, IsParent = true, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                        new Menu{ MenuID = 17, DashboardID = 1, MenuName = "Pendaftaran", Area = "SC0014.002", Path = "#", Action="Agen", Controller= "Registration", ParentID = 16, IsChildMenu = false, IsParent = false, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},

                    new Menu{ MenuID = 18, DashboardID = 2, MenuName = "Admin", Area = "SC0014.006", Path = "/Admin/", Action= "Index", Controller = "Home", ParentID = null,  IsChildMenu = false, IsParent = true, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                        new Menu{ MenuID = 19, DashboardID = 2, MenuName = "Konfirmasi", Area = "SC0014.006", Path = "", ParentID = 18,  IsChildMenu = false, IsParent = true, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                            new Menu{ MenuID = 20, DashboardID = 2, MenuName = "Konfirmasi Pembayaran PO", Area = "SC0014.006", Path = "", Action = "CustomerPayment", Controller = "Confirmation", ParentID = 19,  IsChildMenu = false, IsParent = false, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                            new Menu{ MenuID = 21, DashboardID = 2, MenuName = "Pendaftaran Toko", Area = "SC0014.006", Path = "Members/Home/MerchantRegistration", Action = "MerchantRegistration", Controller = "Registration", ParentID = 19,  IsChildMenu = false, IsParent = false, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                            new Menu{ MenuID = 22, DashboardID = 2, MenuName = "Pendaftaran Agen", Area = "SC0014.006", Path = "", Action = "AgentRegistration", Controller = "Registration", ParentID = 19,  IsChildMenu = false, IsParent = false, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                        new Menu{ MenuID = 23, DashboardID = 2, MenuName = "Perubahan Data", Area = "SC0014.006", Path = "", ParentID = 18,  IsChildMenu = false, IsParent = true, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                            new Menu{ MenuID = 24, DashboardID = 2, MenuName = "Perubahan Data Toko", Area = "SC0014.006", Path = "", Action = "MerchantDataChanged", Controller = "Confirmation", ParentID = 23,  IsChildMenu = false, IsParent = false, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},

                    new Menu{ MenuID = 25, DashboardID = 5, MenuName = "Manajamen", Area = "SC0014.007", Path = "", ParentID = null,  IsChildMenu = false, IsParent = true, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                        new Menu{ MenuID = 26, DashboardID = 5, MenuName = "Konfirmasi", Area = "SC0014.007", Path = "", ParentID = 25,  IsChildMenu = false, IsParent = true, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                            new Menu{ MenuID = 27, DashboardID = 5, MenuName = "Pendaftaran Toko", Area = "SC0014.007", Path = "Management/MerchantRegistration", ParentID = 26,  IsChildMenu = false, IsParent = false, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},

                    new Menu{ MenuID = 28, DashboardID = 2, MenuName = "Sistem", Area = "SC0014.008", Path = "", ParentID = null,  IsChildMenu = false, IsParent = true, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                        new Menu{ MenuID = 29, DashboardID = 2, MenuName = "Pengaturan Sistem", Area = "SC0014.008", Path = "#", Action = "Index", Controller = "System", ParentID = 28,  IsChildMenu = false, IsParent = false, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                        new Menu{ MenuID = 30, DashboardID = 2, MenuName = "Bantuan", Area = "SC0014.001", Path = "#", Action = "Help", Controller = "System", ParentID = 28,  IsChildMenu = false, IsParent = false, IsActive = true, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                }.ForEach(x => context.menus.Add(x));
                #endregion

                #region Menu In UserRole
                new List<MenuInUserRole>
                {
                    #region System Administrator
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 1, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 2, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 3, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 4, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 5, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 6, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 7, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 8, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 9, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 10, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 11, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 12, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 13, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 14, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 15, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 16, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 17, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 18, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 19, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 20, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 21, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 22, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 23, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 24, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 25, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 26, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 27, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 28, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 29, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "systemadministrator", MenuID = 30, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    #endregion

                    #region administrator
                    new MenuInUserRole {UserRole = "administrator", MenuID = 1, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "administrator", MenuID = 2, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    
                    new MenuInUserRole {UserRole = "administrator", MenuID = 18, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "administrator", MenuID = 19, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "administrator", MenuID = 20, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "administrator", MenuID = 21, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "administrator", MenuID = 22, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "administrator", MenuID = 23, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "administrator", MenuID = 24, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    #endregion

                    #region Member
                    new MenuInUserRole {UserRole = "member", MenuID = 1, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "member", MenuID = 2, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "member", MenuID = 3, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "member", MenuID = 4, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "member", MenuID = 5, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "member", MenuID = 6, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "member", MenuID = 7, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "member", MenuID = 8, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "member", MenuID = 9, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "member", MenuID = 10, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    
                    new MenuInUserRole {UserRole = "member", MenuID = 16, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "member", MenuID = 17, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "member", MenuID = 28, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "member", MenuID = 30, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    #endregion

                    #region MerchantOwner
                    new MenuInUserRole {UserRole = "merchantowner", MenuID = 9, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "merchantowner", MenuID = 10, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "merchantowner", MenuID = 11, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "merchantowner", MenuID = 12, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "merchantowner", MenuID = 13, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "merchantowner", MenuID = 14, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    new MenuInUserRole {UserRole = "merchantowner", MenuID = 15, CreatedBy="sysadmin", CreatedDate = DateTime.Now},
                    #endregion

                }.ForEach(x => context.menuinuserrole.Add(x));
                #endregion

                #region UserProfile
                new List<UserProfile>
                {
                    new UserProfile { UserProfileID = 1, UserName = "sysadmin", Fullname="System Administrator", AddressCode = String.Format("{0}{1}00001", SystemSetting.MemberCode, DateTime.Now.ToString("yyyyMMdd")), Gender = Gender.Laki_Laki, DateOfBirth = new DateTime(1991,2,23), address = new Addresses { AddressCode = String.Format("{0}{1}00001", SystemSetting.MemberCode, DateTime.Now.ToString("yyyyMMdd")), Phone = "", Address = "", City = "Jakarta", Province = "SC0002.006", Country = "Ind", IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now  }, IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now },
                    new UserProfile { UserProfileID = 2, UserName = "agireza", Fullname = "Agi Reza Jasuma S.", AddressCode = String.Format("{0}{1}00002", SystemSetting.MemberCode, DateTime.Now.ToString("yyyyMMdd")), Gender = Gender.Laki_Laki, DateOfBirth = new DateTime(1991,2,23), address = new Addresses { AddressCode = String.Format("{0}{1}00002", SystemSetting.MemberCode, DateTime.Now.ToString("yyyyMMdd")), Phone = "", Address = "", City = "Jakarta", Province = "SC0002.006", Country = "Ind", IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now  }, IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now },
                    new UserProfile { UserProfileID = 3, UserName = "ibnu", Fullname = "Ibnu Vito", AddressCode = String.Format("{0}{1}00003", SystemSetting.MemberCode, DateTime.Now.ToString("yyyyMMdd")), Gender = Gender.Laki_Laki, DateOfBirth = new DateTime(1994,2,23), address = new Addresses { AddressCode = String.Format("{0}{1}00003", SystemSetting.MemberCode, DateTime.Now.ToString("yyyyMMdd")), Phone = "", Address = "", City = "Jakarta", Province = "SC0002.006", Country = "Ind", IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now }, IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now }
                }.ForEach(x => context.userprofiles.Add(x));
                #endregion

                #region ShippingAddresses
                new List <ShippingAddresses>
                {
                    new ShippingAddresses { AddressName = "Alamat Rumah", UserProfileID = 3, Address = "Jln. XXX", Handphone = "08521346789", Country= "SC0003.001", City ="JAKARTA PUSAT", RajaOngkir_City_ID = 152, Province = "SC0002.006", RajaOngkir_Province_ID = 6, ZipCode = "10110", Email = "ibnu.vito@yahoo.com", Email2 = "ibnu.vito@gmail.com", IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now },
                    new ShippingAddresses { AddressName = "Alamat Pacar", UserProfileID = 3, Address = "Jln. XXX", Handphone = "12345678901", Country= "SC0003.001", City ="JAKARTA PUSAT", RajaOngkir_City_ID = 152, Province = "SC0002.006", RajaOngkir_Province_ID = 6, ZipCode = "10110", Email = "ibnu.vito@yahoo.com", Email2 = "ibnu.vito@gmail.com", IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now },
                }.ForEach(x => context.ShippingAddresses.Add(x));
                #endregion

                #region Standard Code
                new List<StandardCode>
                {
                    #region Jenis Kelamin
                    new StandardCode{StandardCodeID = "SC0001", StandardCodeName = "Jenis_Kelamin", IsParent = true, ParentID = null, CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0001.001", StandardCodeName = "Laki-Laki", IsParent = false, ParentID = "SC0001", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0001.002", StandardCodeName = "Perempuan", IsParent = false, ParentID = "SC0001", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    #endregion

                    #region Provinsi
                    new StandardCode{StandardCodeID = "SC0002", StandardCodeName = "Provinsi", IsParent = true, ParentID = null, CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.001", StandardCodeName = "Aceh", Alias="Nanggroe Aceh Darussalam (NAD)", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.002", StandardCodeName = "Bali", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.003", StandardCodeName = "Banten", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.004", StandardCodeName = "Bengkulu", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.005", StandardCodeName = "Gorontalo", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.006", StandardCodeName = "Jakarta", Alias="DKI Jakarta", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.007", StandardCodeName = "Jambi", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.008", StandardCodeName = "Jawa Barat", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.009", StandardCodeName = "Jawa Tengah", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.010", StandardCodeName = "Jawa Timur", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.011", StandardCodeName = "Kalimantan Barat", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.012", StandardCodeName = "Kalimantan Selatan", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.013", StandardCodeName = "Kalimantan Tengah", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.014", StandardCodeName = "Kalimantan Timur", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.015", StandardCodeName = "Kalimantan Utara", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.016", StandardCodeName = "Kepulauan Bangka Belitung", Alias = "Bangka Belitung", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.017", StandardCodeName = "Kepulauan Riau", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.018", StandardCodeName = "Lampung", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.019", StandardCodeName = "Maluku", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.020", StandardCodeName = "Maluku Utara", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.021", StandardCodeName = "Nusa Tenggara Barat", Alias="Nusa Tenggara Barat (NTB)", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.022", StandardCodeName = "Papua", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.023", StandardCodeName = "Papua Barat", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.024", StandardCodeName = "Riau", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.025", StandardCodeName = "Sulawesi Barat", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.026", StandardCodeName = "Sulawesi Selatan", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.027", StandardCodeName = "Sulawesi Tengah", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.028", StandardCodeName = "Sulawesi Tenggara", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.029", StandardCodeName = "Sulawesi Utara", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.030", StandardCodeName = "Sumatera Barat", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.031", StandardCodeName = "Sumatera Selatan", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.032", StandardCodeName = "Sumatera Utara", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.033", StandardCodeName = "Daerah Istimewa Yogyakarta", Alias="DI Yogyakarta", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.034", StandardCodeName = "Nusa Tenggara Timur", Alias="Nusa Tenggara Timur (NTT)", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    #endregion
                    
                    #region Negara
                    new StandardCode{StandardCodeID = "SC0003", StandardCodeName = "Negara", IsParent = true, ParentID = null, CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0003.001", StandardCodeName = "Indonesia", IsParent = false, ParentID = "SC0003", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0003.002", StandardCodeName = "Malaysia", IsParent = false, ParentID = "SC0003", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0003.003", StandardCodeName = "Singapura", IsParent = false, ParentID = "SC0003", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0003.004", StandardCodeName = "Filipina", IsParent = false, ParentID = "SC0003", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    #endregion

                    #region Status Barang
                    new StandardCode{StandardCodeID = "SC0004", StandardCodeName = "Status Barang", IsParent = true, ParentID = null, CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0004.001", StandardCodeName = "Keranjang", IsParent = false, ParentID = "SC0004", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0004.002", StandardCodeName = "Dipesan", IsParent = false, ParentID = "SC0004", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0004.003", StandardCodeName = "Dibayar", IsParent = false, ParentID = "SC0004", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    #endregion
                    
                    #region Cara Pembayaran
                    new StandardCode{StandardCodeID = "SC0005", StandardCodeName = "Cara Pembayaran", IsParent = true, ParentID = null, CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0005.001", StandardCodeName = "ATM", IsParent = false, ParentID = "SC0005", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0005.002", StandardCodeName = "Transfer", IsParent = false, ParentID = "SC0005", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    #endregion
                    
                    #region Status Pemesanan
                    new StandardCode{StandardCodeID = "SC0006", StandardCodeName = "Status Pemesanan", IsParent = true, ParentID = null, CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0006.001", StandardCodeName = "Open", IsParent = false, ParentID = "SC0006", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0006.002", StandardCodeName = "Order", IsParent = false, ParentID = "SC0006", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0006.003", StandardCodeName = "Closed", IsParent = false, ParentID = "SC0006", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    //new StandardCode{StandardCodeID = "SC0006.004", StandardCodeName = "Dibayar", IsParent = false, ParentID = "SC0006", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    //new StandardCode{StandardCodeID = "SC0006.005", StandardCodeName = "Refund", IsParent = false, ParentID = "SC0006", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0006.999", StandardCodeName = "Void", IsParent = false, ParentID = "SC0006", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    #endregion
                    
                    #region Status Member
                    new StandardCode{StandardCodeID = "SC0007", StandardCodeName = "Status Member", IsParent = true, ParentID = null, CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0007.001", StandardCodeName = "Active", IsParent = false, ParentID = "SC0007", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0007.002", StandardCodeName = "Inactive", IsParent = false, ParentID = "SC0007", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    #endregion
                    
                    #region Status Permintaan
                    new StandardCode{StandardCodeID = "SC0008", StandardCodeName = "Status Permintaan", IsParent = true, ParentID = null, CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0008.001", StandardCodeName = "Booked", IsParent = false, ParentID = "SC0008", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0008.002", StandardCodeName = "ConfirmedByAdmin", IsParent = false, ParentID = "SC0008", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0008.003", StandardCodeName = "ConfirmedByMerchant", IsParent = false, ParentID = "SC0008", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0008.004", StandardCodeName = "Complete", IsParent = false, ParentID = "SC0008", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0008.999", StandardCodeName = "Void", IsParent = false, ParentID = "SC0008", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    #endregion

                    #region Status Pendaftaran
                    new StandardCode{StandardCodeID = "SC0009", StandardCodeName = "Status Permintaan", IsParent = true, ParentID = null, CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0009.001", StandardCodeName = "Open", IsParent = false, ParentID = "SC0009", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0009.002", StandardCodeName = "Request", IsParent = false, ParentID = "SC0009", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0009.003", StandardCodeName = "ConfirmedByAdmin", IsParent = false, ParentID = "SC0009", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0009.004", StandardCodeName = "ConfirmedByManagement", IsParent = false, ParentID = "SC0009", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0009.999", StandardCodeName = "Void", IsParent = false, ParentID = "SC0009", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    #endregion

                    #region Produk Kategori
                    new StandardCode{StandardCodeID = "SC0010", StandardCodeName = "Produk Kategori", IsParent = true, ParentID = null, CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0010.001", StandardCodeName = "Fashion & Aksesoris", IsParent = false, ParentID = "SC0010", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0010.002", StandardCodeName = "Pakaian", IsParent = false, ParentID = "SC0010", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0010.003", StandardCodeName = "Kecantikan", IsParent = false, ParentID = "SC0010", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0010.004", StandardCodeName = "Kesehatan", IsParent = false, ParentID = "SC0010", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0010.005", StandardCodeName = "Rumah Tangga", IsParent = false, ParentID = "SC0010", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0010.006", StandardCodeName = "Dapur", IsParent = false, ParentID = "SC0010", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0010.007", StandardCodeName = "Perawatan Bayi", IsParent = false, ParentID = "SC0010", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0010.008", StandardCodeName = "Handphone & Tablet", IsParent = false, ParentID = "SC0010", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0010.009", StandardCodeName = "Laptop & Aksesoris", IsParent = false, ParentID = "SC0010", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0010.010", StandardCodeName = "Komputer & Aksesoris", IsParent = false, ParentID = "SC0010", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0010.011", StandardCodeName = "Elektronik", IsParent = false, ParentID = "SC0010", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0010.012", StandardCodeName = "Kamera, Foto & Video", IsParent = false, ParentID = "SC0010", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0010.013", StandardCodeName = "Otomotif", IsParent = false, ParentID = "SC0010", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0010.014", StandardCodeName = "Olahraga", IsParent = false, ParentID = "SC0010", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0010.015", StandardCodeName = "Office & Stationery", IsParent = false, ParentID = "SC0010", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0010.016", StandardCodeName = "Souvenir, Kado & Hadiah", IsParent = false, ParentID = "SC0010", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0010.017", StandardCodeName = "Mainan & Hobi", IsParent = false, ParentID = "SC0010", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0010.018", StandardCodeName = "Makanan & Minuman", IsParent = false, ParentID = "SC0010", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0010.019", StandardCodeName = "Buku", IsParent = false, ParentID = "SC0010", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0010.020", StandardCodeName = "Software", IsParent = false, ParentID = "SC0010", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0010.021", StandardCodeName = "Film, Musik & Game", IsParent = false, ParentID = "SC0010", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    #endregion

                    #region Status Pengiriman
                    new StandardCode{StandardCodeID = "SC0011", StandardCodeName = "Status Pengiriman", IsParent = true, ParentID = null, CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0011.001", StandardCodeName = "Open", IsParent = false, ParentID = "SC0011", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0011.002", StandardCodeName = "Disiapkan", IsParent = false, ParentID = "SC0011", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0011.003", StandardCodeName = "Dikirim", IsParent = false, ParentID = "SC0011", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0011.999", StandardCodeName = "Void", IsParent = false, ParentID = "SC0011", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    #endregion

                    #region Prefix
                    new StandardCode{StandardCodeID = "SC0012", StandardCodeName = "Panggilan", IsParent = true, ParentID = null, CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0012.001", StandardCodeName = "Bpk.", IsParent = false, ParentID = "SC0012", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0012.002", StandardCodeName = "Ibu", IsParent = false, ParentID = "SC0012", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    #endregion

                    #region Regency Type
                    new StandardCode{StandardCodeID = "SC0013", StandardCodeName = "Kabupaten / Kota", IsParent = true, ParentID = null, CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0013.001", StandardCodeName = "Kabupaten", IsParent = false, ParentID = "SC0013", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0013.002", StandardCodeName = "Kota", IsParent = false, ParentID = "SC0013", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    #endregion

                    #region Area
                    new StandardCode{StandardCodeID = "SC0014", StandardCodeName = "Area Menu", IsParent = true, ParentID = null, CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0014.001", StandardCodeName = "General", IsParent = false, ParentID = "SC0014", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0014.002", StandardCodeName = "Members", IsParent = false, ParentID = "SC0014", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0014.003", StandardCodeName = "Merchants", IsParent = false, ParentID = "SC0014", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0014.004", StandardCodeName = "Store", IsParent = false, ParentID = "SC0014", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0014.005", StandardCodeName = "Agents", IsParent = false, ParentID = "SC0014", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0014.006", StandardCodeName = "Admin", IsParent = false, ParentID = "SC0014", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0014.007", StandardCodeName = "Management", IsParent = false, ParentID = "SC0014", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0014.008", StandardCodeName = "Control Panel", IsParent = false, ParentID = "SC0014", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    #endregion

                    #region Status Penerimaan
                    new StandardCode{StandardCodeID = "SC0015", StandardCodeName = "Area Menu", IsParent = true, ParentID = null, CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0015.001", StandardCodeName = "Open", IsParent = false, ParentID = "SC0015", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0015.002", StandardCodeName = "PayedByCustomer", IsParent = false, ParentID = "SC0015", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0015.003", StandardCodeName = "ConfirmedByAdmin", IsParent = false, ParentID = "SC0015", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0015.004", StandardCodeName = "Closed", IsParent = false, ParentID = "SC0015", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0015.999", StandardCodeName = "Void", IsParent = false, ParentID = "SC0015", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    #endregion

                }.ForEach(x => context.standardcodes.Add(x));
                #endregion

                #region Cart
                new List<Cart>
                {
                    new Cart{ CartID = 1, Username = "ibnu", DiscountAmount = 0, DiscountInPercentage = 0, TotalAmount = 110000, ProductID =  1, Quantity = 1, ItemStatus = ItemStatus.Ordered, CreatedDate = DateTime.Now },
                    new Cart{ CartID = 2, Username = "ibnu", DiscountAmount = 0, DiscountInPercentage = 0, TotalAmount = 110000, ProductID =  2, Quantity = 1, ItemStatus = ItemStatus.Ordered, CreatedDate = DateTime.Now },
                    new Cart{ CartID = 3, Username = "ibnu", DiscountAmount = 0, DiscountInPercentage = 0, TotalAmount = 110000, ProductID =  1, Quantity = 1, ItemStatus = ItemStatus.Ordered, CreatedDate = DateTime.Now },
                    new Cart{ CartID = 4, Username = "ibnu", DiscountAmount = 0, DiscountInPercentage = 0, TotalAmount = 110000, ProductID =  2, Quantity = 1, ItemStatus = ItemStatus.Ordered, CreatedDate = DateTime.Now },
                    new Cart{ CartID = 5, Username = "ibnu", DiscountAmount = 0, DiscountInPercentage = 0, TotalAmount = 110000, ProductID =  1, Quantity = 1, ItemStatus = ItemStatus.Ordered, CreatedDate = DateTime.Now },
                    new Cart{ CartID = 6, Username = "ibnu", DiscountAmount = 0, DiscountInPercentage = 0, TotalAmount = 110000, ProductID =  2, Quantity = 1, ItemStatus = ItemStatus.Requested, CreatedDate = DateTime.Now },
                    new Cart{ CartID = 7, Username = "ibnu", DiscountAmount = 0, DiscountInPercentage = 0, TotalAmount = 110000, ProductID =  1, Quantity = 1, ItemStatus = ItemStatus.Requested, CreatedDate = DateTime.Now }
                }.ForEach(x => context.carts.Add(x));
                #endregion

                #region PurchaseOrderHd
                new List<PurchaseOrderHd>
                {
                    new PurchaseOrderHd { OrderID = 1, CustomerID = 3, MerchantID = 1, OrderNo = "PO/20160501/000001", OrderDate = DateTime.Now, TotalAmount = 220000, AgentID = null, Address = "Provinsi : Jakarta Kota : JAKARTA PUSAT Alamat : Jln. XXX Kode Pos : 10110", ShippingCharges = 0, InsuranceCharges = 0, CreatedDate = DateTime.Now, OrderStatus = OrderStatus.Order, PaymentMehod = PaymentMethod.TRANSFER, ValidUntil = DateTime.Now.AddDays(SystemSetting.ValidUntil)},
                    new PurchaseOrderHd { OrderID = 2, CustomerID = 3, MerchantID = 1, OrderNo = "PO/20160501/000002", OrderDate = DateTime.Now, TotalAmount = 220000, AgentID = 1, Address = "Provinsi : Jakarta Kota : JAKARTA PUSAT Alamat : Jln. XXX Kode Pos : 10110", ShippingCharges = 0, InsuranceCharges = 0, CreatedDate = DateTime.Now, OrderStatus = OrderStatus.Order, PaymentMehod = PaymentMethod.TRANSFER, ValidUntil = DateTime.Now.AddDays(SystemSetting.ValidUntil)},
                    new PurchaseOrderHd { OrderID = 3, CustomerID = 3, MerchantID = 1, OrderNo = "PO/20160501/000003", OrderDate = DateTime.Now, TotalAmount = 110000, AgentID = null, Address = null, ShippingCharges = 0, InsuranceCharges = 0, CreatedDate = DateTime.Now, OrderStatus = OrderStatus.Open, PaymentMehod = PaymentMethod.TRANSFER, ValidUntil = DateTime.Now.AddDays(SystemSetting.ValidUntil)}
                }.ForEach(x => context.purchaseorderhds.Add(x));
                #endregion

                #region PurchaseOrderDt
                new List<PurchaseOrderDt>
                {
                    new PurchaseOrderDt { OrderID = 1, CartID = 1, OrderStatus = OrderStatus.Order, IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                    new PurchaseOrderDt { OrderID = 1, CartID = 2, OrderStatus = OrderStatus.Order, IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                    new PurchaseOrderDt { OrderID = 2, CartID = 3, OrderStatus = OrderStatus.Order, IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                    new PurchaseOrderDt { OrderID = 2, CartID = 4, OrderStatus = OrderStatus.Order, IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                    new PurchaseOrderDt { OrderID = 3, CartID = 5, OrderStatus = OrderStatus.Order, IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                }.ForEach(x => context.purchaseorderdts.Add(x));
                #endregion

                #region CustomerOrder
                new List<CustomerOrder>
                {
                    new CustomerOrder { OrderID = 1, ProductID = 1, MerchantID = 1, Quantity = 1, CustomerID = 2, RequestStatus = RequestStatus.Booked },
                    new CustomerOrder { OrderID = 1, ProductID = 2, MerchantID = 1, Quantity = 1, CustomerID = 2, RequestStatus = RequestStatus.Booked },
                    new CustomerOrder { OrderID = 2, ProductID = 1, MerchantID = 1, Quantity = 1, CustomerID = 2, RequestStatus = RequestStatus.Booked },
                    new CustomerOrder { OrderID = 2, ProductID = 2, MerchantID = 1, Quantity = 1, CustomerID = 2, RequestStatus = RequestStatus.Booked },
                }.ForEach(x => context.customerOrder.Add(x));
                #endregion

                #region PurchaseReceiveHd
                new List<PurchaseReceiveHd>
                {
                    new PurchaseReceiveHd { ReceiveNo = "PR/20160501/000001", ReceiveDate = DateTime.Now, CustomerID = 3, ShippingCharges = 0, InsuranceCharges = 0, TotalAmount = 220000, PayerName = "Ibnu Vito", Address = "Provinsi : Jakarta Kota : JAKARTA PUSAT Alamat : Jln. XXX Kode Pos : 10110", BankID = 1, TransferAmount = 220000, RefundAmount = 0, SenderAccountNo = "1234567890", ReceiveStatus = ReceiveStatus.PayedByCustomer, CreatedBy = "sysadmin", CreatedDate = DateTime.Now },
                    new PurchaseReceiveHd { ReceiveNo = "PR/20160501/000002", ReceiveDate = DateTime.Now, CustomerID = 3, ShippingCharges = 0, InsuranceCharges = 0, TotalAmount = 220000, PayerName = "Ibnu Vito", Address = "Provinsi : Jakarta Kota : JAKARTA PUSAT Alamat : Jln. XXX Kode Pos : 10110", BankID = 1, TransferAmount = 220000, RefundAmount = 0, SenderAccountNo = "1234567890", ReceiveStatus = ReceiveStatus.PayedByCustomer, CreatedBy = "sysadmin", CreatedDate = DateTime.Now },
                }.ForEach(x => context.purchasereceivehds.Add(x));
                #endregion

                #region PurchaseReceiveDt
                new List<PurchaseReceiveDt>
                {
                    new PurchaseReceiveDt {ReceiveID = 1, OrderID = 1, TotalAmount = 110000, IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now},
                    new PurchaseReceiveDt {ReceiveID = 2, OrderID = 2, TotalAmount = 110000, IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now}
                }.ForEach(x => context.purchasereceivedts.Add(x));
                #endregion

                #region ZipCode
                new List<ZipCode>
                {
                    new ZipCode { ZipNumber = "10110", Province = "SC0002.006", Regency = "JAKARTA PUSAT", RegencyType = "SC0013.002", District = "GAMBIR", SubDistrict = "GAMBIR", IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now },
                    new ZipCode { ZipNumber = "10120", Province = "SC0002.006", Regency = "JAKARTA PUSAT", RegencyType = "SC0013.002", District = "GAMBIR", SubDistrict = "KEBON KELAPA", IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now },
                    new ZipCode { ZipNumber = "10130", Province = "SC0002.006", Regency = "JAKARTA PUSAT", RegencyType = "SC0013.002", District = "GAMBIR", SubDistrict = "PETOJO UTARA", IsDeleted = false, CreatedBy = "sysadmin", CreatedDate = DateTime.Now },
                }.ForEach(x => context.zipcodes.Add(x));
                #endregion

                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}
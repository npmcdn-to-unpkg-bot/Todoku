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
                    new Bank { BankID = 1, BankName = "Mandiri", AccountName = "PT. Todoku", AccountNo = "123-456-789-0"},
                    new Bank { BankID = 2, BankName = "BCA", AccountName = "PT. Todoku", AccountNo = "123-456-789-0"}
                }.ForEach(x => context.banks.Add(x));
                #endregion

                #region Merchants
                //new List<Merchant>
                //{
                //    new Merchant { MerchantID = 1, MerchantCode = "TDM000001", MerchantName = "GS Shop", IsActive = true, OwnerID = 1, JoinDate = DateTime.Now },
                //    new Merchant { MerchantID = 2, MerchantCode = "TDM000002", MerchantName = "Electronic City", IsActive = true, OwnerID = 1, JoinDate = DateTime.Now },
                //}.ForEach(x => context.merchants.Add(x));
                #endregion

                #region ProductsDetails
                //new List<ProductsDetails>
                //{
                //    //new ProductsDetails { ProductID = 1, DiscountAmount = 0, DiscountAmount2 = 0, DiscountAmount3 = 0, DiscountInPercentage = 0, DiscountInPercentage2 = 0, DiscountInPercentage3 = 0, Price = 4750000, Quantity = 10 },
                //    //new ProductsDetails { ProductID = 2, DiscountAmount = 0, DiscountAmount2 = 0, DiscountAmount3 = 0, DiscountInPercentage = 0, DiscountInPercentage2 = 0, DiscountInPercentage3 = 0, Price = 3000000, Quantity = 10 },
                //    //new ProductsDetails { ProductID = 5, DiscountAmount = 0, DiscountAmount2 = 0, DiscountAmount3 = 0, DiscountInPercentage = 0, DiscountInPercentage2 = 0, DiscountInPercentage3 = 0, Price = 2000000, Quantity = 10 },
                //    //new ProductsDetails { ProductID = 6, DiscountAmount = 0, DiscountAmount2 = 0, DiscountAmount3 = 0, DiscountInPercentage = 0, DiscountInPercentage2 = 0, DiscountInPercentage3 = 0, Price = 3000000, Quantity = 10 },
                //    //new ProductsDetails { ProductID = 7, DiscountAmount = 0, DiscountAmount2 = 0, DiscountAmount3 = 0, DiscountInPercentage = 0, DiscountInPercentage2 = 0, DiscountInPercentage3 = 0, Price = 675000, Quantity = 10 },
                //    //new ProductsDetails { ProductID = 8, DiscountAmount = 0, DiscountAmount2 = 0, DiscountAmount3 = 0, DiscountInPercentage = 0, DiscountInPercentage2 = 0, DiscountInPercentage3 = 0, Price = 675000, Quantity = 10 },
                //    //new ProductsDetails { ProductID = 9, DiscountAmount = 0, DiscountAmount2 = 0, DiscountAmount3 = 0, DiscountInPercentage = 0, DiscountInPercentage2 = 0, DiscountInPercentage3 = 0, Price = 50000, Quantity = 10 },

                //    //new ProductsDetails { ProductID = 3, DiscountAmount = 0, DiscountAmount2 = 0, DiscountAmount3 = 0, DiscountInPercentage = 0, DiscountInPercentage2 = 0, DiscountInPercentage3 = 0, Price = 3450000, Quantity = 10 },
                //    //new ProductsDetails { ProductID = 4, DiscountAmount = 0, DiscountAmount2 = 0, DiscountAmount3 = 0, DiscountInPercentage = 0, DiscountInPercentage2 = 0, DiscountInPercentage3 = 0, Price = 10000000, Quantity = 10 },
                
                //    new ProductsDetails { ProductDetailID = 1, DiscountAmount = 0, DiscountAmount2 = 0, DiscountAmount3 = 0, DiscountInPercentage = 0, DiscountInPercentage2 = 0, DiscountInPercentage3 = 0, Price = 4750000, LineAmount = 4750000, Quantity = 10 },
                //    new ProductsDetails { ProductDetailID = 2, DiscountAmount = 0, DiscountAmount2 = 0, DiscountAmount3 = 0, DiscountInPercentage = 0, DiscountInPercentage2 = 0, DiscountInPercentage3 = 0, Price = 3000000, LineAmount = 3000000, Quantity = 10 },
                //    new ProductsDetails { ProductDetailID = 3, DiscountAmount = 0, DiscountAmount2 = 0, DiscountAmount3 = 0, DiscountInPercentage = 0, DiscountInPercentage2 = 0, DiscountInPercentage3 = 0, Price = 2000000, LineAmount = 2000000, Quantity = 10 },
                //    new ProductsDetails { ProductDetailID = 4, DiscountAmount = 0, DiscountAmount2 = 0, DiscountAmount3 = 0, DiscountInPercentage = 0, DiscountInPercentage2 = 0, DiscountInPercentage3 = 0, Price = 3000000, LineAmount = 3000000, Quantity = 10 },
                //    new ProductsDetails { ProductDetailID = 5, DiscountAmount = 0, DiscountAmount2 = 0, DiscountAmount3 = 0, DiscountInPercentage = 0, DiscountInPercentage2 = 0, DiscountInPercentage3 = 0, Price = 675000, LineAmount = 675000, Quantity = 10 },
                //    new ProductsDetails { ProductDetailID = 6, DiscountAmount = 0, DiscountAmount2 = 0, DiscountAmount3 = 0, DiscountInPercentage = 0, DiscountInPercentage2 = 0, DiscountInPercentage3 = 0, Price = 675000, LineAmount = 675000, Quantity = 10 },
                //    new ProductsDetails { ProductDetailID = 7, DiscountAmount = 0, DiscountAmount2 = 0, DiscountAmount3 = 0, DiscountInPercentage = 0, DiscountInPercentage2 = 0, DiscountInPercentage3 = 0, Price = 50000, LineAmount = 50000, Quantity = 10 },
                //    new ProductsDetails { ProductDetailID = 8, DiscountAmount = 0, DiscountAmount2 = 0, DiscountAmount3 = 0, DiscountInPercentage = 0, DiscountInPercentage2 = 0, DiscountInPercentage3 = 0, Price = 3450000, LineAmount = 3450000, Quantity = 10 },
                //    new ProductsDetails { ProductDetailID = 9, DiscountAmount = 0, DiscountAmount2 = 0, DiscountAmount3 = 0, DiscountInPercentage = 0, DiscountInPercentage2 = 0, DiscountInPercentage3 = 0, Price = 10000000, LineAmount = 10000000, Quantity = 10 },
                //}.ForEach(x => context.productsDetails.Add(x));
                #endregion

                #region Product
                //new List<Product>
                //{
                //    new Product {ProductID = 1, ProductDetailID=1, MerchantID = 1, ProductCode = "CNSL0001", ProductName = "Sony Playstation 4 Charcoal Black - 500 GB [ PS4 CUH 1106 ] Garansi", ImgLink = "#", Category="SC0010.011"},
                //    new Product {ProductID = 2, ProductDetailID=2, MerchantID = 1, ProductCode = "CNSL0002", ProductName = "Sony Playstation 3 Charcoal Black - 125 GB", ImgLink = "#", Category="SC0010.011"},
                //    new Product {ProductID = 3, ProductDetailID=8, MerchantID = 1, ProductCode = "CNSL0003", ProductName = "Sony Bravia 32 inch", ImgLink = "#", Category="SC0010.011"},
                //    new Product {ProductID = 4, ProductDetailID=9, MerchantID = 1, ProductCode = "CNSL0004", ProductName = "Sony Experia Z5", ImgLink = "#", Category="SC0010.008"},
                //    new Product {ProductID = 5, ProductDetailID=3, MerchantID = 1, ProductCode = "CNSL0005", ProductName = "Sony PSP", ImgLink = "#", Category="SC0010.011"},
                //    new Product {ProductID = 6, ProductDetailID=4, MerchantID = 1, ProductCode = "CNSL0006", ProductName = "Sony PS Vita", ImgLink = "#", Category="SC0010.011"},
                //    new Product {ProductID = 7, ProductDetailID=5, MerchantID = 1, ProductCode = "CNSL0007", ProductName = "Star Wars Battle Front", Category="SC0010.021", ImgLink = "#"},
                //    new Product {ProductID = 8, ProductDetailID=6, MerchantID = 1, ProductCode = "CNSL0008", ProductName = "Fallout 4", ImgLink = "#", Category="SC0010.021"},
                //    new Product {ProductID = 9, ProductDetailID=7, MerchantID = 1, ProductCode = "CNSL0009", ProductName = "Final Fantasy XV - Pre Order", Category="SC0010.021", ImgLink = "#"}
                //}.ForEach(x => context.products.Add(x));
                #endregion

                #region Menu
                new List<Menu>
                {
                    new Menu{ MenuID = 1, MenuName = "Home", Path="", ParentID = null},
                    new Menu{ MenuID = 2, MenuName = "Product", Path="/Product/", ParentID = 1},
                    new Menu{ MenuID = 3, MenuName = "Detail", Path="/Product/Detail/", ParentID = 2}
                }.ForEach(x => context.menus.Add(x));
                #endregion

                #region UserProfile
                new List<UserProfile>
                {
                    new UserProfile { UserProfileID = 1, UserName = "sysadmin", Fullname="System Administrator", AddressCode = String.Format("{0}{1}00001", SystemSetting.MemberCode, DateTime.Now.ToString("yyyyMMdd")), Gender = Gender.Laki_Laki, DateOfBirth = new DateTime(1991,2,23), address = new Addresses { AddressCode = String.Format("{0}{1}00001", SystemSetting.MemberCode, DateTime.Now.ToString("yyyyMMdd")), Phone = "", Address = "", City = "Jakarta", Province = "SC0002.006", Country = "Ind" }},
                    new UserProfile { UserProfileID = 2, UserName = "agireza", Fullname = "Agi Reza Jasuma S.", AddressCode = String.Format("{0}{1}00002", SystemSetting.MemberCode, DateTime.Now.ToString("yyyyMMdd")), Gender = Gender.Laki_Laki, DateOfBirth = new DateTime(1991,2,23), address = new Addresses { AddressCode = String.Format("{0}{1}00002", SystemSetting.MemberCode, DateTime.Now.ToString("yyyyMMdd")), Phone = "", Address = "", City = "Jakarta", Province = "SC0002.006", Country = "Ind" }}
                }.ForEach(x => context.userprofiles.Add(x));
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
                    new StandardCode{StandardCodeID = "SC0002.001", StandardCodeName = "Aceh", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.002", StandardCodeName = "Bali", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.003", StandardCodeName = "Banten", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.004", StandardCodeName = "Bengkulu", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.005", StandardCodeName = "Gorontalo", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.006", StandardCodeName = "Jakarta", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.007", StandardCodeName = "Jambi", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.008", StandardCodeName = "Jawa Barat", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.009", StandardCodeName = "Jawa Tengah", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.010", StandardCodeName = "Jawa Timur", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.011", StandardCodeName = "Kalimantan Barat", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.012", StandardCodeName = "Kalimantan Selatan", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.013", StandardCodeName = "Kalimantan Tengah", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.014", StandardCodeName = "Kalimantan Timur", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.015", StandardCodeName = "Kalimantan Utara", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.016", StandardCodeName = "Kepulauan Bangka Belitung", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.017", StandardCodeName = "Kepulauan Riau", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.018", StandardCodeName = "Lampung", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.019", StandardCodeName = "Maluku", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.020", StandardCodeName = "Maluku Utara", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.021", StandardCodeName = "Nusa Tenggara Barat", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
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
                    new StandardCode{StandardCodeID = "SC0002.033", StandardCodeName = "Daerah Istimewa Yogyakarta", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0002.034", StandardCodeName = "Nusa Tenggara Timur", IsParent = false, ParentID = "SC0002", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
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
                    new StandardCode{StandardCodeID = "SC0006.003", StandardCodeName = "Konfirmasi", IsParent = false, ParentID = "SC0006", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0006.004", StandardCodeName = "Dibayar", IsParent = false, ParentID = "SC0006", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0006.005", StandardCodeName = "Refund", IsParent = false, ParentID = "SC0006", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
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
                    new StandardCode{StandardCodeID = "SC0009.001", StandardCodeName = "Request", IsParent = false, ParentID = "SC0009", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
                    new StandardCode{StandardCodeID = "SC0009.002", StandardCodeName = "Approved", IsParent = false, ParentID = "SC0009", CreatedBy = "sysadmin", CreatedDate = DateTime.Now, LastUpdatedBy = null, LastUpdatedDate = null },
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

                }.ForEach(x => context.standardcodes.Add(x));
                #endregion

                #region Cart
                //new List<Cart>
                //{
                //    new Cart{ CartID = 1, UserName = "agireza", DiscountAmount = 0, DiscountInPercentage = 0, ProductID =  1, Quantity = 1, LineAmount = 4750000, ItemStatus = ItemStatus.Requested, CreatedDate = DateTime.Now },
                //    new Cart{ CartID = 2, UserName = "agireza", DiscountAmount = 0, DiscountInPercentage = 0, ProductID =  2, Quantity = 1, LineAmount = 3000000, ItemStatus = ItemStatus.Requested, CreatedDate = DateTime.Now }
                //}.ForEach(x => context.carts.Add(x));
                #endregion

                #region PurchaseOrderHd
                //new List<PurchaseOrderHd>
                //{
                //    new PurchaseOrderHd { OrderID = 1, CustomerID=2, OrderNo = "PO/20160122/0000001", TotalAmount = 7750000, AgentID = "", CreatedDate = DateTime.Now, OrderStatus = OrderStatus.Open, PayerName = "", PaymentMehod = PaymentMethod.TRANSFER, ValidUntil = DateTime.Now.AddDays(SystemSetting.ValidUntil)}
                //}.ForEach(x => context.purchaseorderhds.Add(x));
                #endregion

                #region PurchaseOrderDt
                //new List<PurchaseOrderDt>
                //{
                //    new PurchaseOrderDt { OrderID = 1, CartID = 1, OrderStatus = OrderStatus.Open},
                //    new PurchaseOrderDt { OrderID = 1, CartID = 2, OrderStatus = OrderStatus.Open}
                //}.ForEach(x => context.purchaseorderdts.Add(x));
                #endregion

                #region CustomerOrder
                //new List<CustomerOrder>
                //{
                //    new CustomerOrder { OrderID = 1, ProductID = 1, MerchantID = 1, Quantity = 1, CustomerID = 2, RequestStatus = RequestStatus.Booked },
                //    new CustomerOrder { OrderID = 1, ProductID = 2, MerchantID = 1, Quantity = 1, CustomerID = 2, RequestStatus = RequestStatus.Booked }
                //}.ForEach(x => context.customerOrder.Add(x));
                #endregion

                #region ZipCode
                new List<ZipCode>
                {
                    new ZipCode { ZipNumber = "10110", Province = "SC0002.006", Regency = "JAKARTA PUSAT", RegencyType = "SC0013.002", District = "GAMBIR", SubDistrict = "GAMBIR", CreatedBy = "sysadmin", CreatedDate = DateTime.Now },
                    new ZipCode { ZipNumber = "10120", Province = "SC0002.006", Regency = "JAKARTA PUSAT", RegencyType = "SC0013.002", District = "GAMBIR", SubDistrict = "KEBON KELAPA", CreatedBy = "sysadmin", CreatedDate = DateTime.Now },
                    new ZipCode { ZipNumber = "10130", Province = "SC0002.006", Regency = "JAKARTA PUSAT", RegencyType = "SC0013.002", District = "GAMBIR", SubDistrict = "PETOJO UTARA", CreatedBy = "sysadmin", CreatedDate = DateTime.Now },
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
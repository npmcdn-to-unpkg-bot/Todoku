using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Todoku.Models
{
    #region General Class
    public class StandardCode
    {
        [Key]
        public String StandardCodeID { get; set; }
        public String StandardCodeName { get; set; }
        public bool IsParent { get; set; }
        public String ParentID { get; set; }
        public String CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public String LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        List<StandardCode> childs { get; set; }
        StandardCode parent { get; set; }
    }

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Konfirmasi Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Pertanyaan")]
        public String PasswordQuestion { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Jawaban")]
        public String PasswordAnswer { get; set; }

        public UserProfile userprofile { get; set; }
    }
    #endregion

    #region Class

    public class ShippingAddresses 
    {
        [Key]
        public Int32 ShippingID { get; set; }

        [Display(Name = "Keterangan")]
        public String AddressName { get; set; }

        public Int32 UserProfileID { get; set; }
        
        [DataType(DataType.MultilineText)]
        [Display(Name = "Alamat")]
        public String Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "No. Telp.")]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "No. telp. tidak benar")]
        public String Phone { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "No. Hp.")]
        public String Handphone { get; set; }

        [Display(Name = "Negara")]
        public String Country { get; set; }

        [Display(Name = "Kota")]
        public String City { get; set; }

        [Display(Name = "Provinsi")]
        public String Province { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email is is not valid.")]
        public string Email { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Alternatif")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email is is not valid.")]
        public string Email2 { get; set; }

        [Display(Name = "Kode Pos")]
        public String ZipCode { get; set; }

        public String CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        public String LastUpdatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? LastUpdatedDate { get; set; }

        [ForeignKey("UserProfileID")]
        public virtual UserProfile userprofile { get; set; }
    }

    public class Addresses
    {
        [Key]
        public String AddressCode { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Alamat")]
        public String Address { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Alamat Sesuai KTP")]
        public String Address2 { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "No. Telp.")]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "No. telp. tidak benar")]
        public String Phone { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "No. Hp.")]
        public String Handphone { get; set; }

        [Display(Name = "Negara")]
        public String Country { get; set; }

        [Display(Name = "Kota")]
        public String City { get; set; }

        [Display(Name = "Provinsi")]
        public String Province { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email is is not valid.")]
        public string Email { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Alternative Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email is is not valid.")]
        public string Email2 { get; set; }

        [Display(Name="No. Fax")]
        public String FaxNo { get; set; }

        [Display(Name = "Kode Pos")]
        public String ZipCode { get; set; }
    }

    public class Bank
    {
        [Key]
        public Int32 BankID { get; set; }

        [Display(Name = "Nama")]
        public String BankName { get; set; }

        [Display(Name = "Pemilik Akun")]
        public String AccountName { get; set; }

        [Display(Name = "No. Rekening")]
        public String AccountNo { get; set; }

        [Display(Name = "Gambar")]
        [DataType(DataType.ImageUrl)]
        public String ImgLink { get; set; }
    }

    public class ProductsDetails
    {
        [Key]
        public Int32 ProductDetailID { get; set; }

        [Display(Name = "Stok")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.###}")]
        public Int32 Quantity { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Harga")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C}")]
        public Decimal Price { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Keuntungan")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public Decimal Profit { get; set; }

        [Display(Name = "Diskon (%)")]
        public Int32 DiscountInPercentage { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Diskon")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public Decimal DiscountAmount { get; set; }

        [Display(Name = "Diskon 2 (%)")]
        public Int32 DiscountInPercentage2 { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Diskon 2")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal DiscountAmount2 { get; set; }

        [Display(Name = "Diskon 3 (%)")]
        public Int32 DiscountInPercentage3 { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Diskon 3")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal DiscountAmount3 { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Pajak")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public Int32 VATAmount { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Total")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public Decimal LineAmount { get; set; }

        [Display(Name = "Min. Stok")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.###}")]
        public Int32 MinimumStock { get; set; }
    }

    public class Product
    {
        [Key]
        public Int32 ProductID { get; set; }

        public Int32 ProductDetailID { get; set; }

        [Display(Name = "Kode")]
        public String ProductCode { get; set; }
        
        [Display(Name="Produk")]
        public String ProductName { get; set; }
        
        [Display(Name = "Kategori")]
        public String Category { get; set; }

        [Display(Name = "Toko")]
        public Int32 MerchantID { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Link")]
        public String ImgLink { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Deskripsi")]
        public String Description { get; set; }

        [ForeignKey("MerchantID")]
        public virtual Merchant merchant { get; set; }

        [ForeignKey("ProductDetailID")]
        public virtual ProductsDetails detail { get; set; }

        [ForeignKey("Category")]
        public virtual StandardCode sccategory { get; set; }


        public String ShortDescription 
        {
            get 
            {
                if (Description != null)
                {
                    return Description.Length > 100 ? String.Format("{0}...", Description.Substring(0, 100)) : Description;
                }
                else { return ""; }
                
            }
        }
    }

    public class Menu
    {
        [Key]
        public Int32 MenuID { get; set; }
        public String MenuName { get; set; }
        public String MenuArea { get; set; }
        public String Path { get; set; }
        public Int32? ParentID { get; set; }
        public Boolean IsParent { get; set; }
        public bool IsActive { get; set; }
        public String CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public String LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        [ForeignKey("ParentID")]
        public Menu parent { get; set; }

        [ForeignKey("MenuArea")]
        public virtual StandardCode scmenuarea { get; set; }

        public virtual List<Menu> childs { get; set; }
    }

    public class MenuInUserRole 
    {
        [Key]
        [Column(Order=1)]
        public String UserRole { get; set; }
        [Key]
        [Column(Order = 2)]
        public Int32 MenuID { get; set; }
        public String CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public String LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }

    public class Merchant
    {
        [Key]
        public Int32 MerchantID { get; set; }
        
        [Display(Name = "Kode")]
        public String MerchantCode { get; set; }
        
        [Display(Name = "Nama")]
        public String MerchantName { get; set; }
        
        [Display(Name = "Status")]
        public Boolean IsActive { get; set; }

        [Display(Name = "Pemilik")]
        public Int32 OwnerID { get; set; }

        public String AddressCode { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Deskripsi")]
        public String Description { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Logo")]
        public String Logo { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Tgl. Bergabung")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime JoinDate { get; set; }

        public String RegistrationStatus { get; set; }

        [ForeignKey("AddressCode")]
        public virtual Addresses address { get; set; }

        [ForeignKey("OwnerID")]
        public virtual UserProfile userprofile { get; set; }
    }

    public class MerchantRegistration
    { 
        [Key]
        public Int32 RegistrationID { get; set;}

        public String RegistrationCode { get; set; }

        [Display(Name = "Nama")]
        public String MerchantName { get; set;}

        public String AddressCode { get; set; }

        [Display(Name = "Pemilik")]
        public Int32 OwnerID { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal StartPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal EndPrice { get; set; }

        [Display(Name = "Tgl. Registrasi")]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "Status")]
        public String RegistrationStatus { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Deskripsi")]
        public String Description { get; set; }

        public String CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public String LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        [ForeignKey("AddressCode")]
        public virtual Addresses address { get; set; }

        [ForeignKey("OwnerID")]
        public virtual UserProfile userprofile { get; set; }

        [ForeignKey("RegistrationStatus")]
        public virtual StandardCode ScRegisStatus { get; set; }

        public virtual List<MerchantRegistrationDetail> details { get; set; }
    }

    public class MerchantRegistrationDetail
    {
        [Key]
        public Int32 DetailID { get; set; }

        public Int32 RegistrationID { get; set; }

        public String ProductName { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Gambar")]
        public String ImgLink { get; set; }
        
        [DataType(DataType.MultilineText)]
        [Display(Name = "Deskripsi")]
        public String Description { get; set; }
        
        public String CreatedBy { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public String LastUpdatedBy { get; set; }
        
        public DateTime? LastUpdatedDate { get; set; }

        [ForeignKey("RegistrationID")]
        public virtual MerchantRegistration registration { get; set; }
    }

    public class UserProfile
    {
        [Key]
        public Int32 UserProfileID { get; set; }

        public String UserName { get; set; }

        public String Prefix { get; set; }

        [Display(Name="Nama Lengkap")]
        public String Fullname { get; set; }

        [Display(Name = "Jenis Kelamin")]
        public String Gender { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Tanggal Lahir")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime DateOfBirth { get; set; }

        public String AddressCode { get; set; }

        [ForeignKey("AddressCode")]
        public virtual Addresses address { get; set; }

        public virtual List<ShippingAddresses> shippings { get; set; }
    }

    public class Cart
    {
        [Key]
        public Int32 CartID { get; set; }
        
        public String UserName { get; set; }

        public Int32 ProductID { get; set; }
        
        [Display(Name="Jumlah")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.##}")]
        public Int32 Quantity { get; set; }

        public Int32 DiscountInPercentage { get; set; }

        public Decimal DiscountAmount { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal LineAmount { get; set; }

        public String ItemStatus { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product product { get; set; }
    }

    public class PurchaseOrderHd
    {
        [Key]
        public Int32 OrderID { get; set; }

        [Display(Name="No. Transaksi")]
        public String OrderNo { get; set; }
        
        public Int32 CustomerID { get; set; }
        
        [Display(Name="Agen")]
        public Int32? AgentID { get; set; }

        [DataType(DataType.Date)]
        public DateTime ValidUntil { get; set; }
        
        [Display(Name="Cara Pembayaran")]
        public String PaymentMehod { get; set; }
        
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C}")]
        [Display(Name = "Total Transaksi")]
        public Decimal TotalAmount { get; set; }
        
        public String OrderStatus { get; set; }

        public String DeliveryStatus { get; set; }

        [Display(Name="Nama Pembayar")]
        public String PayerName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Alamat")]
        public String Address { get; set; }

        public Int32? BankID { get; set; }

        [Display(Name="Jmlh. Transfer")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal TransferAmount { get; set; }

        [Display(Name = "Jmlh. Refund")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal RefundAmount { get; set; }

        [Display(Name="Rekening")]
        public String SenderAccountNo { get; set; }

        public String CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        public String LastUpdatedBy { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime? LastUpdatedDate { get; set; }

        [ForeignKey("BankID")]
        public virtual Bank bank { get; set; }

        [ForeignKey("CustomerID")]
        public virtual UserProfile customer { get; set; }

        public virtual List<PurchaseOrderDt> orderdetails { get; set; }

        [ForeignKey("AgentID")]
        public virtual UserProfile agent { get; set; }
    }

    public class PurchaseOrderDt
    { 
        [Key]
        public Int32 OrderDtId { get; set; }
        public Int32 OrderID { get; set; }
        public Int32 CartID { get; set; }
        public String OrderStatus { get; set; }

        [ForeignKey("OrderID")]
        public virtual PurchaseOrderHd order { get; set; }
        
        [ForeignKey("CartID")]
        public virtual Cart cart { get; set; }
    }

    public class CustomerOrder 
    {
        [Key]
        [Column(Order = 1)]
        public Int32 MerchantID { get; set; }
        [Key]
        [Column(Order = 2)]
        public Int32 OrderID { get; set; }
        [Key]
        [Column(Order = 3)]
        public Int32 CustomerID { get; set; }

        [Key]
        [Column(Order = 4)]
        public Int32 ProductID { get; set; }

        [Display(Name="Jumlah")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.##}")]
        public Int32 Quantity { get; set; }
        
        public String RequestStatus { get; set; }

        [ForeignKey("OrderID")]
        public virtual PurchaseOrderHd pohd { get; set; }

        [ForeignKey("CustomerID")]
        public virtual UserProfile customer { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product product { get; set; }
    }

    public class MemberHistory 
    {
        [Key]
        public Int32 HistoryID { get; set; }
        
        [Display(Name="Nama")]
        public String ProductName { get; set; }
        
        [Display(Name="Gambar")]
        [DataType(DataType.ImageUrl)]
        public String ImgLink { get; set; }

        [Display(Name="Jumlah")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.##}")]
        public Int32 Quantity { get; set; }
        
        [Display(Name = "Total")]
        [DisplayFormat(ApplyFormatInEditMode=true, DataFormatString="{0:c}")]
        public Decimal LineAmount { get; set; }
    }

    public class ItemDeliveryHd
    {
        [Key]
        public Int32 DeliveryID { get; set; }

        [Display(Name="No. Resi")]
        public String ReceiptNumber { get; set; }

        public Int32 OrderID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name="Tanggal")]
        public DateTime DeliveryDate { get; set; }
        
        public Int32 MerchantID { get; set; }
        
        public Int32 CustomerID { get; set; }
        
        [DataType(DataType.MultilineText)]
        [Display(Name = "Alamat")]
        public String Address { get; set; }

        public Decimal DeliveryCost { get; set; }

        public Decimal InsuranceCost { get; set; }

        public String DeliveryStatus { get; set; }

        public String CreatedBy { get; set; }
        
        public DateTime CreatedDate { get; set; }

        public String LastUpdatedBy { get; set; }
        
        public DateTime? LastUpdatedDate { get; set; }

        [ForeignKey("OrderID")]
        public virtual PurchaseOrderHd pohd { get; set; }

        [ForeignKey("MerchantID")]
        public virtual Merchant merchant { get; set; }

        [ForeignKey("CustomerID")]
        public virtual UserProfile customer { get; set; }

        public virtual List<ItemDeliveryDt> itemdeliverydts { get; set; }
    }

    public class ItemDeliveryDt
    {
        [Key]
        public Int32 DeliveryDtID { get; set; }
        
        public Int32 DeliveryID { get; set; }
        
        public Int32 ProductID { get; set; }
        
        [Display(Name="Jumlah")]
        public Int32 Quantity { get; set; }

        [ForeignKey("DeliveryID")]
        public virtual ItemDeliveryHd itemdeliveryhd { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product product { get; set; }
    }

    public class ZipCode
    {
        [Key]
        public Int32 ZipID { get; set; }

        public String ZipNumber { get; set; }

        public String SubDistrict { get; set; }

        public String District { get; set; }

        public String RegencyType { get; set; }

        public String Regency { get; set; }

        public String Province { get; set; }

        public String CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public String LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        [ForeignKey("RegencyType")]
        public virtual StandardCode ScRegency { get; set; }

        [ForeignKey("Province")]
        public virtual StandardCode ScProvince { get; set; }
    }

    #endregion

    #region Custom Class
    public class SampleProduct 
    {
        public Int32 RegistrationID { get; set; }
        public String ProductName { get; set; }
        public String ProductDescription { get; set; }
        public HttpPostedFileBase file { get; set; }
        public Decimal StartPrice { get; set; }
        public Decimal EndPrice { get; set; }
    }
    #endregion
}
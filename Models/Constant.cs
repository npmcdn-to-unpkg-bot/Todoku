using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Todoku.Models
{

    #region SC Detail
    public class Gender 
    { 
        public const String Laki_Laki = "SC0001.001";
        public const String Perempuan = "SC0001.002";
    }

    public class ItemStatus 
    {
        public const String Requested = "SC0004.001";
        public const String Ordered = "SC0004.002";
        public const String Payed = "SC0004.003";
        public const String Void = "SC0004.999";
    }

    public class PaymentMethod 
    {
        public const String TRANSFER = "SC0005.001";
        public const String INTERNET_BANKING = "SC0005.002";
    }

    public class OrderStatus 
    {
        public const String Open = "SC0006.001";
        public const String Order = "SC0006.002";
        public const String Konfimasi = "SC0006.003";
        public const String Dibayar = "SC0006.004";
        public const String Refund = "SC0006.005";
        public const String Void = "SC0006.999";
    }

    public class MemberStatus 
    {
        public const String Active = "SC0007.001";
        public const String Inactive = "SC0007.002";
    }

    public class RequestStatus 
    {
        public const String Booked = "SC0008.001";
        public const String ConfirmedByAdmin = "SC0008.002";
        public const String ConfirmedByMerchant = "SC0008.003";
        public const String Complete = "SC0008.004";
        public const String Void = "SC0008.999";
    }

    public class TransactionNoPrefix 
    { 
        public const String Purchase_Order = "PO";
        public const String Purchase_Receive = "PR";
    }

    public class RegistrationStatus 
    {
        public const String Open = "SC0009.001";
        public const String Request = "SC0009.002";
        public const String ConfirmedByAdmin = "SC0009.003";
        public const String ConfirmedByManagement = "SC0009.004";
        public const String Void = "SC0009.999";
    }

    public class ProductCategories 
    {
        public const String Fashion = "SC0010.001";
        public const String Pakaian = "SC0010.002";
        public const String Kecantikan = "SC0010.003";
        public const String Kesehatan = "SC0010.004";
        public const String Rumah_Tangga = "SC0010.005";
        public const String Dapur = "SC0010.006";
        public const String Perawatan_Bayi = "SC0010.007";
        public const String Handphone = "SC0010.008";
        public const String Laptop = "SC0010.009";
        public const String Komputer = "SC0010.010";
        public const String Elektronik = "SC0010.011";
        public const String Kamera = "SC0010.012";
        public const String Otomotif = "SC0010.013";
        public const String Olahraga = "SC0010.014";
        public const String Office = "SC0010.015";
        public const String Souvenir = "SC0010.016";
        public const String Mainan = "SC0010.017";
        public const String Makanan = "SC0010.018";
        public const String Buku = "SC0010.019";
        public const String Software = "SC0010.020";
        public const String Film_Musik_Game = "SC0010.021";
    }

    public class DeliveryStatus
    {
        public const String Open = "SC0011.001";
        public const String Prepared = "SC0011.002";
        public const String Delivery = "SC0011.003";
        public const String Void = "SC0011.999";
    }

    public class UserRole 
    {
        public const String Guest = "Guest";
        public const String Admin = "Administrator";
        public const String MerchantOwner = "MerchantOwner";
        public const String Member = "Member";
        public const String Agen = "Agen";
        public const String SystemAdministrator = "SystemAdministrator";
    }

    public class ReceiveStatus 
    {
        public const String Open = "SC0015.001";
        public const String PayedByCustomer = "SC0015.002";
        public const String ConfirmedByAdmin = "SC0015.003";
        public const String Closed = "SC0015.004";
        public const String Void = "SC0015.999";
    }
    #endregion

    public class SCConstant
    {
        public const String Jenis_Kelamin = "SC0001";
        public const String Provinsi = "SC0002";
        public const String Negara = "SC0003";
        public const String Status_Barang = "SC0004";
        public const String Cara_Pembayaran = "SC0005";
        public const String Status_Pemesanan = "SC0006";
        public const String MemberStatus = "SC0007";
        public const String RequestStatus = "SC0008";
        public const String RegistrationStatus = "SC0009";
        public const String Kategori_Produk = "SC0010";
        public const String Status_Pengiriman = "SC0011";
        public const String Panggilan = "SC0012";
        public const String Status_Penerimaan = "SC0013";
    }

    #region System Setting
    public class SystemSetting 
    {
        public const Int32 ValidUntil = 3;
        public const Int32 VATAmount = 10;
        public const String Default_Upload_Path = @"~\Uploads\";
        public const String Default_Upload_Registration = @"~\Uploads\Registration\";
        public const String Default_URL = @"http://localhost/Todoku/";
        public const Int32 MinimumStock = 1;
        public const Int32 Default_Lock_Time = 5;

        public const String MerchantCode = "MCH";
        public const String MemberCode = "MBR";
        public const String AgentCode = "AGN";
        public const String RegisMerchantCode = "RMC";
        public const String RegisAgenCode = "RAG";
    }
    #endregion
}
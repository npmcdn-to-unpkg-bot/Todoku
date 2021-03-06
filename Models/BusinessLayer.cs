﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Linq.Dynamic;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;

namespace Todoku.Models
{
    public class BusinessLayer : DbContext
    {
        #region Database
        public DbSet<Addresses> addresses { get; set; }
        public DbSet<Agent> agents { get; set; }
        public DbSet<AgentRegistration> agentRegistrations { get; set; }
        public DbSet<AgentDiscountSetting> agentDiscountSettings { get; set; }
        public DbSet<Bank> banks { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<CustomerOrder> customerOrder { get; set; }
        public DbSet<Dashboard> dashboards { get; set; }
        public DbSet<DashboardInUserRole> dashboardinuserroles { get; set; }
        public DbSet<ItemDeliveryDt> itemdeliverydts { get; set; }
        public DbSet<ItemDeliveryHd> itemdeliveryhds { get; set; }
        public DbSet<MenuInUserRole> menuinuserrole { get; set; }
        public DbSet<Menu> menus { get; set; }
        public DbSet<MerchantRegistrationDetail> merchantRegistrationDetails { get; set; }
        public DbSet<MemberHistory> memberHistory { get; set; }
        public DbSet<MerchantRegistration> merchantRegistrations { get; set; }
        public DbSet<Merchant> merchants { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<ProductAttributeGroup> productAttributeGroups { get; set; }
        public DbSet<ProductAttribute> productAttributes { get; set; }
        public DbSet<ProductDt> productDetails { get; set; }
        public DbSet<ProductReview> productReviews { get; set; }
        public DbSet<PurchaseOrderDt> purchaseorderdts { get; set; }
        public DbSet<PurchaseOrderHd> purchaseorderhds { get; set; }
        public DbSet<PurchaseReceiveDt> purchasereceivedts { get; set; }
        public DbSet<PurchaseReceiveHd> purchasereceivehds { get; set; }
        public DbSet<ShippingAddresses> ShippingAddresses { get; set; }
        public DbSet<StandardCode> standardcodes { get; set; }
        public DbSet<TransactionHistory> transactionHistories { get; set; }
        public DbSet<UserProfile> userprofiles { get; set; }
        public DbSet<ZipCode> zipcodes { get; set; }
        #endregion

        #region Ajax
        #region Agent
        public List<Agent> GetAgentList(String filterExpression)
        {
            return this.agents.Where(filterExpression).ToList().Select(x => new Agent 
            { 
                AgentID = x.AgentID,
                AgentCode = x.AgentCode,
                AgentType = x.AgentType,
                AddressCode = x.AddressCode,
                IsActive = x.IsActive,
                IsDeleted = x.IsDeleted
            }).ToList();
        }

        public List<Agent> GetAgentList(String filterExpression, String OrderBy)
        {
            IQueryable<Agent> temp = this.agents.Where(filterExpression);
            if (OrderBy != null && OrderBy != "")
            {
                temp = temp.OrderBy(OrderBy);
            }
            return temp.ToList();
        }

        public IQueryable GetAgentList(String filterExpression = "", String GroupBy = "", String OrderBy = "")
        {
            if (filterExpression == "") filterExpression = "1 = 1";
            if (OrderBy == "") OrderBy = GroupBy + "ASC";
            return this.agents.Where(filterExpression).GroupBy(GroupBy, GroupBy).Select(String.Format("new (Key as {0})", GroupBy));
        }
        #endregion

        #region Shipping Address
        public List<ShippingAddresses> GetShippingAddressList(String filterExpression)
        {
            return this.ShippingAddresses.Where(filterExpression).ToList().Select(x => new ShippingAddresses
            {
                ShippingID = x.ShippingID,
                AddressName = x.AddressName,
                Address = x.Address,
                Phone = x.Phone,
                Handphone = x.Handphone,
                Email = x.Email,
                Email2 = x.Email2,
                ZipCode = x.ZipCode,
                City = x.City,
                RajaOngkir_City_ID = x.RajaOngkir_City_ID,
                ScProvince = x.ScProvince,
                RajaOngkir_Province_ID = x.RajaOngkir_Province_ID
            }).ToList();
        }

        public List<ShippingAddresses> GetShippingAddressList(String filterExpression, String OrderBy)
        {
            IQueryable<ShippingAddresses> temp = this.ShippingAddresses.Where(filterExpression);
            if (OrderBy != null && OrderBy != "")
            {
                temp = temp.OrderBy(OrderBy);
            }
            return temp.ToList();
        }

        public IQueryable GetShippingAddressList(String filterExpression = "", String GroupBy = "", String OrderBy = "")
        {
            if (filterExpression == "") filterExpression = "1 = 1";
            if (OrderBy == "") OrderBy = GroupBy + "ASC";
            return this.ShippingAddresses.Where(filterExpression).GroupBy(GroupBy, GroupBy).Select(String.Format("new (Key as {0})", GroupBy));
        }
        #endregion

        #region ZipCode
        public List<ZipCode> GetZipCodeList(String filterExpression) 
        {
            return this.zipcodes.Where(filterExpression).ToList();
        }

        public List<ZipCode> GetZipCodeList(String filterExpression, String OrderBy)
        {
            IQueryable<ZipCode> temp = this.zipcodes.Where(filterExpression);
            if (OrderBy != null && OrderBy != "") 
            {
                temp = temp.OrderBy(OrderBy);
            }
            return temp.ToList();
        }

        public IQueryable GetZipCodeList(String filterExpression = "", String GroupBy = "", String OrderBy = "") 
        {
            if (filterExpression == "") filterExpression = "1 = 1";
            if (OrderBy == "") OrderBy = GroupBy + "ASC";
            return this.zipcodes.Where(filterExpression).GroupBy(GroupBy, GroupBy).Select(String.Format("new (Key as {0})", GroupBy));
        }
        #endregion

        #region StandardCode
        public List<StandardCode> GetStandardCodeList(String filterExpression)
        {
            return this.standardcodes.Where(filterExpression).ToList();
        }

        public List<StandardCode> GetStandardCodeList(String filterExpression, String OrderBy)
        {
            IQueryable<StandardCode> temp = this.standardcodes.Where(filterExpression);
            if (OrderBy != null && OrderBy != "")
            {
                temp = temp.OrderBy(OrderBy);
            }
            return temp.ToList();
        }

        public List<StandardCode> GetStandardCodeList(String filterExpression, String GroupBy, String OrderBy)
        {
            IQueryable<StandardCode> temp = this.standardcodes.Where(filterExpression);
            if (GroupBy != null && GroupBy != "")
            {
                temp = (IQueryable<StandardCode>)temp.GroupBy(GroupBy, GroupBy);
            }
            if (OrderBy != null && OrderBy != "")
            {
                temp = temp.OrderBy(OrderBy);
            }
            return temp.ToList();
        }
        #endregion
        #endregion

        #region Custom
        public Int32 GetUserProfileID(){
            String Username = Membership.GetUser().UserName;
            return this.userprofiles.FirstOrDefault(x => x.UserName == Username && !x.IsDeleted).UserProfileID;
        }
        public Int32 GetAgentID() {
            Int32 UserID = this.GetUserProfileID();
            return this.agents.FirstOrDefault(x => x.UserID == UserID && x.IsActive && !x.IsDeleted).AgentID;
        }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerOrder>()
                        .HasRequired(m => m.pohd)
                        .WithMany()
                        .HasForeignKey(m => m.OrderID)
                        .WillCascadeOnDelete(false);
            modelBuilder.Entity<ItemDeliveryHd>()
                        .HasRequired(m => m.customer)
                        .WithMany()
                        .HasForeignKey(m => m.CustomerID)
                        .WillCascadeOnDelete(false);
            modelBuilder.Entity<ItemDeliveryDt>()
                        .HasRequired(m => m.product)
                        .WithMany()
                        .HasForeignKey(m => m.ProductID)
                        .WillCascadeOnDelete(false);
            modelBuilder.Entity<PurchaseOrderDt>()
                        .HasRequired(m => m.cart)
                        .WithMany()
                        .HasForeignKey(m => m.CartID)
                        .WillCascadeOnDelete(false);
            modelBuilder.Entity<CustomerOrder>()
                        .HasRequired(m => m.product)
                        .WithMany()
                        .HasForeignKey(m => m.ProductID)
                        .WillCascadeOnDelete(false);
            modelBuilder.Entity<ItemDeliveryHd>()
                        .HasRequired(m => m.merchant)
                        .WithMany()
                        .HasForeignKey(m => m.MerchantID)
                        .WillCascadeOnDelete(false);
            modelBuilder.Entity<PurchaseOrderHd>()
                        .HasRequired(m => m.merchant)
                        .WithMany()
                        .HasForeignKey(m => m.MerchantID)
                        .WillCascadeOnDelete(false);
            modelBuilder.Entity<CustomerOrder>()
                        .HasRequired(m => m.merchant)
                        .WithMany()
                        .HasForeignKey(m => m.MerchantID)
                        .WillCascadeOnDelete(false);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Reflection;

namespace Todoku.Models
{
    public class AppSession
    {
        public const string GuestSessionKey = "GuestId";
        public const string GuestCartSessionKey = "CartId";

        static public string GetGuestId(HttpContextBase context)
        {
            if (context.Session[GuestSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[GuestSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempGuestId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[GuestSessionKey] = tempGuestId.ToString();
                }
            }
            return context.Session[GuestSessionKey].ToString();
        }

        //static public void SetCart(HttpContextBase context, List<Cart> carts) 
        //{
        //    context.Session[GuestCartSessionKey] = carts;
        //}

        //static public List<Cart> GetCart(HttpContextBase context) 
        //{
        //    if (context.Session[GuestCartSessionKey] != null)
        //    {
                
        //        return (List<Cart>) context.Session[GuestCartSessionKey];
        //    }
        //    return null;
        //}

        static public string GetGuestIdUsingCookie(HttpContextBase context) 
        {
            if (context.Request.Cookies.AllKeys.Contains(GuestSessionKey))
            {
                return context.Request.Cookies[GuestSessionKey].Value;
            }
            else 
            {
                // Generate a new random GUID using System.Guid class
                Guid tempGuestId = Guid.NewGuid();
                // Send tempCartId back to client as a cookie
                HttpCookie cookie = new HttpCookie(GuestSessionKey);
                cookie.Value = tempGuestId.ToString();
                cookie.Expires = DateTime.Now.AddDays(1);
                context.Response.Cookies.Add(cookie);
                return tempGuestId.ToString();
            }
        }

        static public void SetCartUsingCookie(HttpContextBase context, List<Cart> carts) 
        {
            String cartsinstring = new JavaScriptSerializer().Serialize(carts);
            HttpCookie cookie = new HttpCookie(String.Format("{0}/{1}", GetGuestIdUsingCookie(context), GuestCartSessionKey));
            cookie.Value = cartsinstring;
            cookie.Expires = DateTime.Now.AddDays(1);
            context.Response.Cookies.Add(cookie);
        }

        static public List<Cart> GetCartUsingCookie(HttpContextBase context) 
        {
            String Key = String.Format("{0}/{1}", GetGuestIdUsingCookie(context), GuestCartSessionKey);
            if (context.Request.Cookies.AllKeys.Contains(Key))
            {
                String cartsinstring = context.Request.Cookies[Key].Value;
                var obj = new JavaScriptSerializer().DeserializeObject(cartsinstring);
                return Method.ConvertJsonObject<Cart>((Object[]) obj);
            }
            return new List<Cart>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using Todoku.Models;

namespace Todoku.Controllers
{
    public class AjaxController : Controller
    {
        [HttpPost]
        public JsonResult GetListObject(String method, String filterExpression = "", String GroupBy = "", String OrderBy = "")
        {
            Type elementType = Type.GetType("Todoku.Models.BusinessLayer");
            Dictionary<String, String> param = new Dictionary<String, String>();
            List<Type> types = new List<Type>();
            if (filterExpression != "") {
                param.Add("filterExpression", filterExpression);
                types.Add(typeof(String));
            }

            if (GroupBy != "") 
            {
                param.Add("GroupBy", GroupBy);
                types.Add(typeof(String));

                if (OrderBy == "") 
                {
                    param.Add("OrderBy", OrderBy);
                    types.Add(typeof(String));
                }
            }

            if (OrderBy != "") {
                types.Add(typeof(String));
                param.Add("OrderBy", OrderBy);
            }

            MethodInfo mi = typeof(BusinessLayer).GetMethod(method, BindingFlags.Instance | BindingFlags.Public, null, types.ToArray(), null);

            if (mi != null)
            {
                try
                {
                    ParameterInfo[] parameters = mi.GetParameters();
                    List<Object> obj = new List<Object>();
                    foreach (ParameterInfo pi in parameters)
                    {
                        var data = param.FirstOrDefault(x => x.Key == pi.Name);
                        switch (pi.ParameterType.ToString())
                        {
                            case "System.Int32":
                                {
                                    obj.Add(Convert.ToInt32(data.Value != null && data.Value != "" ? data.Value : "0"));
                                    break;
                                }
                            case "System.Decimal":
                                {
                                    obj.Add(Convert.ToDecimal(data.Value != null && data.Value != "" ? data.Value : "0"));
                                    break;
                                }
                            default:
                                {
                                    obj.Add(data.Value);
                                    break;
                                }
                        }
                    }
                    var lst = mi.Invoke(new BusinessLayer(), obj.ToArray());
                    if (lst != null)
                    {
                        return Json(lst);
                    }
                }
                catch (Exception ex)
                {
                    String errMessage = ex.Message;
                }
                return null;
            }
            else return null;
        }
    }
}

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
            if(filterExpression != "")
                param.Add("filterExpression", filterExpression);
            if (GroupBy != "")
                param.Add("GroupBy", GroupBy);
            if (OrderBy != "")
                param.Add("OrderBy", OrderBy);

            List<MethodInfo> lstMi = elementType.GetMethods().Where(x => x.Name == String.Format("{0}", method)).ToList();
            
            foreach (KeyValuePair<String, String> obj in param) 
            {
                lstMi = lstMi.Where(x => x.GetParameters().Any(s => s.Name.Contains(obj.Key))).ToList();
            }

            if (lstMi != null)
            {
                try
                {
                    MethodInfo mi = lstMi[0];
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

using DonorAppVersion2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DonorAppVersion2.Controllers
{
    public class PartnerController : Controller
    {
        // GET: Partner
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Partner partner)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            using (sampleEntities dbModel = new sampleEntities())
            {
                try
                {
                    var success = dbModel.Partners.Where(p => p.BusinessEmail == partner.BusinessEmail && p.Password == partner.Password).FirstOrDefault();
                    if (success != null)
                    {
                        Session["PartnerId"] = success.PartnerId;
                        Session["PartnerName"] = success.Name;

                        return RedirectToAction("Dashboard");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Invalid Credentials, Please try again!";
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return View();
                }
            }       
        }


    }
}
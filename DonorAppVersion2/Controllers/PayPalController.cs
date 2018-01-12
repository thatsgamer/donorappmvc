using DonorAppVersion2.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DonorAppVersion2.Controllers
{
    public class PayPalController : Controller
    {
        public ActionResult HomePaypal()
        {
            return View();
        }


        [HttpPost]
        public ActionResult RedirectFromPaypal()
        {
            return View();
        }
        

        public ActionResult CancelFromPaypal()
        {
            return View();
        }

        public ActionResult NotifyFromPaypal()
        {
            return View();
        }
    }
}
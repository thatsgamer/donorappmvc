using DonorAppVersion2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DonorAppVersion2.Controllers
{
    public class AdminController : Controller
    {
        ////////////////////////////////////////////////////////////////////////////////  Admin Login
        public ActionResult Login()
        {
            if (Session["AdminId"] != null)
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Login(AdminLoginViewModel alvm)
        {
            if (ModelState.IsValid)
            {
                using (sampleEntities dbModel = new sampleEntities())
                {
                    var adminuser = dbModel.AdminDetails.Where(x => x.Email == alvm.Email && x.Password == alvm.Password).FirstOrDefault();
                    if(adminuser != null)
                    {
                        Session["AdminId"] = adminuser.AdminId;
                        Session["AdminName"] = adminuser.Name;


                        return RedirectToAction("Dashboard");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Invalid Username or Password";
                        return View();
                    }
                    
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Validation Errors";
                return View();
            }
        }










        ////////////////////////////////////////////////////////////////////////////////// Dashboard
        public ActionResult Dashboard()
        {
            if (Session["AdminId"] != null)
            {
                using (sampleEntities dbModel = new sampleEntities())
                {
                    var parentlist = dbModel.Parents.ToList();
                    var donorlist = dbModel.Donors.ToList();
                    var donorCyclelist = dbModel.DonorCycleEgg.ToList();
                    var partnerList = dbModel.Partners.ToList();
                    var parentpaymentList = dbModel.ParentPayments.ToList();

                    var viewModel = new AdminDashboardViewModel()
                        {
                            Parent = parentlist,
                            Donor = donorlist,
                            Partner = partnerList,
                            DonorCycleEgg = donorCyclelist,
                            ParentPayments = parentpaymentList
                        };
                    

                    return View(viewModel);
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }













        ////////////////////////////////////////////////////////////////////////////////// Session Timeout
        public ActionResult SessionTimeout()
        {
            
            Session.Clear();
            //return RedirectToAction("Login");
            return View();            
        }











        ////////////////////////////////////////////////////////////////////////////////// Add Remove Agency
        public ActionResult AddAgency()
        {
            if(Session["AdminId"]!= null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }
        [HttpPost]
        public ActionResult AddAgency(Partner partner)
        {
             var errors = ModelState.Values.SelectMany(v => v.Errors);
             if (ModelState.IsValid)
             {
                 if (Session["AdminId"] != null)
                 {
                     using (sampleEntities dbModel = new sampleEntities())
                     {
                         try
                         {
                             dbModel.Partners.Add(partner);
                             dbModel.SaveChanges();

                             ViewBag.SuccessMessage = "Partner details saved successfully!";
                             return View();
                         }
                         catch (Exception ex)
                         {
                             ViewBag.ErrorMessage = ex.Message;
                             return View(partner);
                         }
                     }
                 }
                 else
                 {
                     return RedirectToAction("SessionTimeout");
                 }
             }
            else
             {
                 int i = 0;
                 foreach (ModelError error in errors)
                 {
                     i = i + 1;
                     ViewBag.WarningMessage += i.ToString() + ") " + error.ErrorMessage.ToString() + ". ";
                 }

                 return View(partner);
             }
        }
        public ActionResult ViewAllAgencies()
        {
            if(Session["AdminId"]!= null)
            {
                using(sampleEntities dbModel = new sampleEntities())
                {
                    var partnerList = dbModel.Partners.ToList();
                    return View(partnerList);
                }
                
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }











        ////////////////////////////////////////////////////////////////////////////////// Add Remove Agency
        public ActionResult Settings()
        {
            if (Session["AdminId"] != null)
            {
                using (sampleEntities dbModel = new sampleEntities())
                {
                    var adminsettings = dbModel.AdminSettings.FirstOrDefault();
                    return View(adminsettings);
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }

        [HttpPost]
        public ActionResult Settings(AdminSettings asett)
        {
        if (Session["AdminId"] != null)
            {
                using (sampleEntities dbModel = new sampleEntities())
                {
                    var settingsfromdb = dbModel.AdminSettings.FirstOrDefault();
                    //settingsfromdb.ParentNewDonorCycleCharges = asett.ParentNewDonorCycleCharges;
                    //settingsfromdb.ParentRegistrationCharges = asett.ParentRegistrationCharges;
                    if (settingsfromdb != null)
                    {
                        dbModel.AdminSettings.Remove(settingsfromdb);
                    }

                    try
                    {
                        dbModel.AdminSettings.Add(asett);
                        dbModel.SaveChanges();

                        ViewBag.SuccessMessage = "Settings Saved!";
                        
                    }
                    catch(Exception ex)
                    {
                        ViewBag.ErrorMessage = ex.Message;
                    }
                    return View(asett);
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
            
        }







        ////////////////////////////////////////////////////////////////////////////////// View All Parents

        public ActionResult ParentList()
        {
            if (Session["AdminId"] != null)
            {
                using (sampleEntities dbModel = new sampleEntities())
                {
                    var parentlist = dbModel.Parents.ToList();
                    return View(parentlist);
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }
    }
}
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



        ////////////////////////////////////////////////////////////////////////////////////////////////  DASHBOARD
        public ActionResult Dashboard()
        {
            if (Session["PartnerId"] != null)
            {
                sampleEntities dbModel = new sampleEntities();
                
                    int partnerid = int.Parse(Session["PartnerId"].ToString());
                    //var agencyDonorCycle = dbModel.ParentDonorCycleAgencies.Include("DonorCycleEgg").Include("Parent").Where(x => x.PartnerAndTheirContacts.PartnerId == partnerid).ToList();
                    var staffList = dbModel.PartnerAndTheirContacts.Where(x => x.PartnerId == partnerid).ToList();
                    List<ParentDonorCycleAgencies> pdca = new List<ParentDonorCycleAgencies>();
                    foreach(var item in staffList)
                    {
                        pdca.AddRange(dbModel.ParentDonorCycleAgencies.Include("DonorCycleEgg").Include("PartnerAndTheirContacts").Where(a => a.PartnerContactsId == item.PartnerContactsId).ToList());
                    }

                    return View(pdca);
                
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
            
        }





        ////////////////////////////////////////////////////////////////////////////////////////////////  Add Staff
        

        public ActionResult ViewStaff()
        {
            if (Session["PartnerId"] != null)
            {
                using(sampleEntities dbModel = new sampleEntities())
                {
                    int partnerid = int.Parse(Session["PartnerId"].ToString());
                    var staff = dbModel.PartnerAndTheirContacts.Where(x => x.PartnerId == partnerid).ToList();
                    return View(staff);
                }
                
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }


        public ActionResult AddStaff()
        {
            if (Session["PartnerId"] != null)
            {
                using (sampleEntities dbModel = new sampleEntities())
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }

        [HttpPost]
        public ActionResult AddStaff(PartnerAndTheirContacts patc)
        {
            if (Session["PartnerId"] != null)
            {
                using (sampleEntities dbModel = new sampleEntities())
                {
                    try
                    {
                        patc.PartnerId = int.Parse(Session["PartnerId"].ToString());
                        patc.CreatedDate = System.DateTime.Today;
                        dbModel.PartnerAndTheirContacts.Add(patc);
                        dbModel.SaveChanges();
                        ViewBag.SuccessMessage = "Details Saved Successfully!";
                        return View();
                    }

                    catch(Exception ex)
                    {
                        ViewBag.ErrorMessage = ex.Message;
                        return View(patc);
                    }                    
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////  Approve Donor Cycle

        public ActionResult ApproveDonorCycle(int id)
        {
            if (Session["PartnerId"] != null)
            {
                using(sampleEntities dbModel = new sampleEntities())
                {
                    var getParentDonorCycleAgency = dbModel.ParentDonorCycleAgencies.Where(x => x.PDCAID == id).FirstOrDefault();
                    getParentDonorCycleAgency.isApprovedByPartner = true;
                    //dbModel.ParentDonorCycleAgencies.Remove(getParentDonorCycleAgency);
                    dbModel.SaveChanges();

                    return RedirectToAction("Dashboard");
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }


        public ActionResult RejectDonorCycle(int id)
        {
            if (Session["PartnerId"] != null)
            {
                using (sampleEntities dbModel = new sampleEntities())
                {
                    var getParentDonorCycleAgency = dbModel.ParentDonorCycleAgencies.Where(x => x.PDCAID == id).FirstOrDefault();
                    //getParentDonorCycleAgency.isApprovedByPartner = false;
                    //dbModel.ParentDonorCycleAgencies.Remove(getParentDonorCycleAgency);
                    //dbModel.SaveChanges();

                    return View(getParentDonorCycleAgency);
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }

        [HttpPost]
        public ActionResult RejectDonorCycle(ParentDonorCycleAgencies pdca)
        {
            if (Session["PartnerId"] != null)
            {
                using (sampleEntities dbModel = new sampleEntities())
                {
                    var getParentDonorCycleAgency = dbModel.ParentDonorCycleAgencies.Where(x => x.PDCAID == pdca.PDCAID).FirstOrDefault();
                    getParentDonorCycleAgency.isApprovedByPartner = false;
                    getParentDonorCycleAgency.Reason = pdca.Reason;
                    dbModel.SaveChanges();
                    return RedirectToAction("Dashboard");
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////  Single Actions and Methods
        public ActionResult SessionTimeout()
        {
            Session.Clear();
            return View();
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////  Single Actions and Methods
        public ActionResult MakeNewMatch()
        {
            if(Session["PartnerId"]!= null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }
        [HttpPost]
        public ActionResult MakeNewMatch(PotentialMatches pm)
        {
            if (Session["PartnerId"] != null)
            {
                using (sampleEntities dbModel = new sampleEntities())
                {
                    pm.PartnerId = int.Parse(Session["PartnerId"].ToString());
                    pm.MatchStatus = "Pending";
                    dbModel.PotentialMatches.Add(pm);
                    ViewBag.SuccessMessage = "Match Details Saved!";
                    ModelState.Clear();
                    dbModel.SaveChanges();
                    return View();
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }


        public ActionResult PotentialMatchDetails()
        {
            if(Session["PartnerId"]!= null)
            {
                using (sampleEntities dbModel = new sampleEntities())
                {
                    int partnerid = int.Parse(Session["PartnerId"].ToString());
                    var listofmatches = dbModel.PotentialMatches.Where(x=>x.PartnerId == partnerid).ToList();
                    return View(listofmatches);
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }
    }
}
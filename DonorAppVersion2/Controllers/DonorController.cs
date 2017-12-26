﻿using DonorAppVersion2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DonorAppVersion2.Controllers
{
    public class DonorController : Controller
    {
        // GET: Donor
        public ActionResult Index()
        {
            return View();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////// Donor Registration
        
        public ActionResult Registration()
        {
            Donor donorModel = new Donor();
            return View(donorModel);
        }
        [HttpPost]
        public ActionResult Registration(Donor donorModel, HttpPostedFileBase imagefile)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                using (sampleEntities dbModel = new sampleEntities())
                {
                    if (dbModel.Parents.Any(x => x.Email == donorModel.Email))
                    {
                        ViewBag.ErrorMessage = "This email id is previously registered, Please try to login if you have already registered!";
                        return View("Registration", donorModel);
                    }
                    else
                    {
                        try
                        {
                            if (donorModel.Password != donorModel.ConfirmPassword)
                            {
                                ViewBag.ErrorMessage = "Password & Confirm Password doesn't Match!";
                                return View();
                            }
                            else
                            {
                                string newfilename = "";
                                //string oldfilename = "";
                                string oldfileextn = "";


                                if (imagefile != null && imagefile.ContentLength > 0)
                                {
                                    //oldfilename = Path.GetFileNameWithoutExtension(imagefile.FileName);
                                    oldfileextn = Path.GetExtension(imagefile.FileName);
                                    newfilename = DateTime.Now.ToString("ddMMyyyyhhmmss") + oldfileextn;

                                    var path = Path.Combine(Server.MapPath("~/Files"), newfilename);
                                    imagefile.SaveAs(path);
                                }
                                else
                                {
                                    ViewBag.ErrorMessage = "Please select a Photo";
                                    return View(donorModel);
                                }

                        
                                Random randomnumber = new Random();
                                int phoneCode = randomnumber.Next(1000, 9999);
                                int emailCode = randomnumber.Next(1000, 9999);

                                donorModel.ContactVerificationCode = phoneCode.ToString();
                                donorModel.EmailVerificationCode = emailCode.ToString();
                                donorModel.isContactVerified = false;
                                donorModel.isEmailVerified = false;
                                donorModel.CreationDate = DateTime.Today;
                                donorModel.Photo = newfilename;
                                donorModel.Salt = ComputeHash(donorModel.Password);

                                //Save Data to Database
                                dbModel.Donors.Add(donorModel);
                                dbModel.SaveChanges();
                                ModelState.Clear();

                                //Create User Session
                                Session.Clear();
                                Session["DonorId"] = donorModel.DonorId;
                                Session["DonorName"] = donorModel.Salutation + " " + donorModel.FirstName + " " + donorModel.LastName;


                                return RedirectToAction("Verification", "Donor", new { @id = donorModel.DonorId });
                            }
                        }
                        catch (Exception ex)
                        {
                            ViewBag.ErrorMessage = "Something Went Wrong! Error-" + ex.Message.ToString();
                            return View("Registration", donorModel);
                        }
                    }
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

                return View(donorModel);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////// Donor Login
        
        public ActionResult Login()
        {
            if (Session["DonorId"] != null)
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Login(DonorLoginViewModel donor)
        {
            if (Session["DonorId"] != null)
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                using (sampleEntities dbModel = new sampleEntities())
                {
                    //var salt = ComputeHash(donor.Password.ToString());
                    try
                    {
                        var success = dbModel.Donors.Where(p => p.Email == donor.Email && p.Password == donor.Password).FirstOrDefault();
                        if (success != null)
                        {
                            Session["DonorId"] = success.DonorId;
                            Session["DonorName"] = success.Salutation + " " + success.FirstName + " " + success.LastName;

                            if (success.isEmailVerified == false)
                            {
                                return RedirectToAction("Verification", "Donor", new { @id = success.DonorId });
                            }
                            else
                            {
                                return RedirectToAction("Dashboard");
                            }
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

        ///////////////////////////////////////////////////////////////////////////////////////////// Donor Profile
        
        public ActionResult DonorProfile()
        {
            if (Session["DonorId"] != null)
            {
                int sessiondonorid = int.Parse(Session["DonorId"].ToString());

                using (sampleEntities dbModel = new sampleEntities())
                {
                    var donor = dbModel.Donors.Where(p => p.DonorId == sessiondonorid).FirstOrDefault();
                    if (donor != null)
                    {
                        return View(donor);
                    }
                    else
                    {
                        return View();
                    }
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////// Donor Dashboard
        
        [HttpGet]
        public ActionResult Dashboard()
        {
            if (Session["DonorId"] != null)
            {
                int sessiondonorid = int.Parse(Session["DonorId"].ToString());
                
                using (sampleEntities dbModel = new sampleEntities())
                {
                    var donor = dbModel.Donors.Where(p => p.DonorId == sessiondonorid).FirstOrDefault();
                    var donorcycles = dbModel.DonorCycles.Where(p => p.DonorId == sessiondonorid).ToList();


                    var viewModel = new DonorAndDonorCycleViewModel
                    {
                        Donor = donor,
                        DonorCycle = donorcycles
                    };

                    if (donor != null && donorcycles != null)
                    {
                        return View(viewModel);                
                    }
                    else
                    {
                        return View();
                    }                    
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////// Donor Verification
        
        [HttpGet]
        public ActionResult Verification(int id)
        {
            using (sampleEntities dbModel = new sampleEntities())
            {
                var donor = dbModel.Donors.Where(p => p.DonorId == id).FirstOrDefault();
                var isEmailVerified = donor.isEmailVerified;
                var isContactVerified = donor.isContactVerified;

                if (isEmailVerified == true && isContactVerified == true)
                {
                    if (Session["DonorId"] != null)
                    {
                        return RedirectToAction("Dashboard");
                    }
                    else
                    {
                        return RedirectToAction("Login");
                    }
                }
                else
                {
                    return View(donor);
                }
            }
        }
        [HttpPost]
        public ActionResult Verification(Donor donor)
        {
                using (sampleEntities dbModel = new sampleEntities())
                {

                    var success = dbModel.Donors.Where(p => p.EmailVerificationCode == donor.EmailVerificationCode && p.ContactVerificationCode == donor.ContactVerificationCode).FirstOrDefault();
                    if (success != null)
                    {
                        Donor updateDonor = (from p in dbModel.Donors
                                               where p.DonorId == donor.DonorId
                                               select p).FirstOrDefault();

                        updateDonor.isContactVerified = true;
                        updateDonor.isEmailVerified = true;
                        dbModel.SaveChanges();

                        //ViewBag.SuccessMessage = "Validation Complete";
                        return RedirectToAction("Dashboard");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "OTP Entered are Invalid, Please verify and try again!";
                        return View();
                    }

                }
            
        }

        ///////////////////////////////////////////////////////////////////////////////////////////// Edit Donor

        [HttpGet]
        public ActionResult EditDonorProfile(int id)
        {
            if(Session["DonorId"]!= null)
            {
                using (sampleEntities dbModel = new sampleEntities())
                {
                     var donor = dbModel.Donors.Where(p => p.DonorId == id).FirstOrDefault();
                    if(donor != null)
                    {
                        return View(donor);
                    }
                    else
                    {
                        return RedirectToAction("Dashboard");
                    }
                }

            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
            
        }
        [HttpPost]
        public ActionResult EditDonorProfile(Donor donorModel, HttpPostedFileBase imagefile)
        {
            if (ModelState.IsValid)
            {
                using (sampleEntities dbModel = new sampleEntities())
                {
                   
                    string newfilename = donorModel.Photo;
                    //string oldfilename = "";
                    string oldfileextn = "";


                    if (imagefile != null && imagefile.ContentLength > 0)
                    {
                        //oldfilename = Path.GetFileNameWithoutExtension(imagefile.FileName);
                        oldfileextn = Path.GetExtension(imagefile.FileName);
                        newfilename = DateTime.Now.ToString("ddMMyyyyhhmmss") + oldfileextn;

                        var path = Path.Combine(Server.MapPath("~/Files"), newfilename);
                        imagefile.SaveAs(path);

                        ViewBag.WarningMessage = "Image Updated";
                    }                   

                    try
                    {

                        var donorFromDatabase = dbModel.Donors.Where(p => p.DonorId == donorModel.DonorId).FirstOrDefault();
                        Donor updateDonor = (from p in dbModel.Donors
                                             where p.DonorId == donorModel.DonorId
                                             select p).FirstOrDefault();
                        
                        Random randomnumber = new Random();
                        int phoneCode = randomnumber.Next(1000, 9999);
                        int emailCode = randomnumber.Next(1000, 9999);
                        
                        if(donorFromDatabase.ContactNumber != donorModel.ContactNumber)
                        {
                            updateDonor.ContactVerificationCode = phoneCode.ToString();
                            updateDonor.isContactVerified = false;
                        }

                        if(donorFromDatabase.Email != donorModel.Email)
                        {
                            updateDonor.EmailVerificationCode = emailCode.ToString();
                            updateDonor.isEmailVerified = false;

                        }

                        dbModel.SaveChanges();

                        ViewBag.ErrorMessage = "Changes Saved";

                        //return RedirectToAction("DonorProfile");
                        ModelState.Clear();
                        return View(donorFromDatabase);

                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = "Something Went Wrong! Error-" + ex.Message.ToString();
                        return View("Registration", donorModel);
                    }
                   

                }

            }
            else
            {
                ViewBag.ErrorMessage = "Validation Error";
                return View(donorModel);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////// Single Operations and Methods
        
        public ActionResult SessionTimeout()
        {
            Session.Clear();
            return View();
        }
        public string ComputeHash(string input)
        {
            HashAlgorithm algorithm = new SHA256CryptoServiceProvider();
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }

        public ActionResult ApproveDonorCycle(int id)
        {
            if(Session["DonorId"]!= null)
            {
                using (sampleEntities dbModel = new sampleEntities())
                {
                    var donorCycle = dbModel.DonorCycles.Where(dc => dc.DonorCycleId == id).FirstOrDefault();
                    if (donorCycle != null)
                    {
                        donorCycle.isApprovedByDonor = true;

                        try
                        {
                            var donorUpdate = dbModel.DonorCycleUpdates.FirstOrDefault();

                            donorUpdate.DonorCycleId = id;
                            donorUpdate.UpdateHeading = "Approved!";
                            donorUpdate.UpdateDescription = "Donor Has Approved this Donor Cycle.";
                            donorUpdate.UpdateDate = System.DateTime.Today;
                            donorUpdate.isSubmitted = true;
                            donorUpdate.isCompleted = false;
                            donorUpdate.CompletionDate = null;

                            dbModel.DonorCycleUpdates.Add(donorUpdate);
                            dbModel.SaveChanges();

                            ViewBag.SuccessMessage = "Donor Cycle Approved!";


                            var sessiondonorid = int.Parse(Session["DonorId"].ToString());
                            var donor = dbModel.Donors.Where(p => p.DonorId == sessiondonorid).FirstOrDefault();
                            var donorcycles = dbModel.DonorCycles.Where(p => p.DonorId == sessiondonorid).ToList();

                            var viewModel = new DonorAndDonorCycleViewModel
                            {
                                Donor = donor,
                                DonorCycle = donorcycles
                            };
                            return RedirectToAction("Dashboard", viewModel);
                        }
                        catch (Exception ex)
                        {
                            ViewBag.ErrorMessage = ex.Message;
                            return RedirectToAction("Dashboard");
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Invalid Donor Cycle";
                        return RedirectToAction("Dashboard");
                    }
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }
        public ActionResult RejectDonorCycle(int id)
        {
            if (Session["DonorId"] != null)
            {
                using (sampleEntities dbModel = new sampleEntities())
                {
                    var donorCycle = dbModel.DonorCycles.Where(dc => dc.DonorCycleId == id).FirstOrDefault();
                    if (donorCycle != null)
                    {
                        //donorCycle.isApprovedByDonor = true;
                        try
                        {
                            var donorUpdate = dbModel.DonorCycleUpdates.Where(dc => dc.DonorCycleId == id).FirstOrDefault();
                            
                            donorUpdate.DonorCycleId = id;
                            donorUpdate.UpdateDate = System.DateTime.Today;
                            donorUpdate.UpdateDescription = "Donor Has Rejected this Donor Cycle.";
                            donorUpdate.UpdateHeading = "Rejected!";
                            donorUpdate.CompletionDate = System.DateTime.Today;
                            donorUpdate.isCompleted = true;
                            donorUpdate.isSubmitted = true;

                            dbModel.SaveChanges();
                           

                            var sessiondonorid = int.Parse(Session["DonorId"].ToString());
                            var donor = dbModel.Donors.Where(p => p.DonorId == sessiondonorid).FirstOrDefault();
                            var donorcycles = dbModel.DonorCycles.Where(p => p.DonorId == sessiondonorid).ToList();

                            var viewModel = new DonorAndDonorCycleViewModel
                            {
                                Donor = donor,
                                DonorCycle = donorcycles
                            };
                            ViewBag.SuccessMessage = "Donor Cycle Rejected!";
                            return RedirectToAction("Dashboard", viewModel);
                        }
                        catch (Exception ex)
                        {
                            ViewBag.ErrorMessage = ex.Message;
                            return RedirectToAction("Dashboard");
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Invalid Donor Cycle";
                        return RedirectToAction("Dashboard");
                    }
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }

        public ActionResult SubmitUpdate()
        {
            if (Session["DonorId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }

        [HttpPost]
        public ActionResult SubmitUpdate(DonorCycleUpdate dcu)
        {
            if (Session["DonorId"] != null)
            {
                using(sampleEntities dbModel = new sampleEntities())
                {
                    try
                    {
                        dbModel.DonorCycleUpdates.Add(dcu);
                        dbModel.SaveChanges();

                        ViewBag.SuccessMessage("Update Submitted Successfully!");
                        return View();
                    }
                    catch(Exception ex)
                    {
                        ViewBag.ErrorMessage("Error - " + ex.Message);
                        return View(dcu);
                    }
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }
    }
}

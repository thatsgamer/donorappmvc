﻿using DonorAppVersion2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net.Mail;
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

                                SendEmailOTP(donorModel.EmailVerificationCode, donorModel.ContactVerificationCode, donorModel.Email);

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
                    var donorcycles = dbModel.DonorCycleEgg.Where(p => p.DonorId == sessiondonorid).ToList();


                    var viewModel = new DonorAndDonorCycleViewModel
                    {
                        Donor = donor,
                        DonorCycleEgg = donorcycles
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
                    DonorVerificationViewModel dvvm = new DonorVerificationViewModel();
                    dvvm.DonorId = donor.DonorId;
                    dvvm.ContactNumber = donor.ContactNumber;
                    dvvm.ContactVerificationCode = donor.ContactVerificationCode;
                    dvvm.Email = donor.Email;
                    dvvm.EmailVerificationCode = donor.EmailVerificationCode;
                    return View(dvvm);
                  
                    // return View(donor);
                }
            }
        }
        [HttpPost]
        public ActionResult Verification(DonorVerificationViewModel donor)
        {
            using (sampleEntities dbModel = new sampleEntities())
            {
                var success = dbModel.Donors.Where(p => p.EmailVerificationCode == donor.EmailVerificationCode && p.ContactVerificationCode == donor.ContactVerificationCode).FirstOrDefault();
                if (success != null)
                {
                    try
                    {
                        var updatedonor = dbModel.Donors.Where(did => did.DonorId == success.DonorId).FirstOrDefault();

                        updatedonor.isContactVerified = true;
                        updatedonor.isEmailVerified = true;
                        updatedonor.ConfirmPassword = success.Password;
                        updatedonor.Height = success.Height;

                        dbModel.SaveChanges();

                        ViewBag.SuccessMessage = "Validation Complete";
                        //return View(donor);
                        return RedirectToAction("Dashboard");
                    }
                    catch (DbEntityValidationException dbEx)
                    {
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                ViewBag.ErrorMessage += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                            }
                        }
                        return View(donor);
                    }
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
        public string SendEmailOTP(string emailotp, string smsotp, string emailto)
        {
            try
            {
                string smtpserver = "smtp.gmail.com";
                string emailfrom = "donor21updates@gmail.com";
                string password = "Donor212121";
                string subject = "OTP for Donor21 Registration";
                string body = "Your EMAIL OTP for Verification of Donor 21 Registration is : " + emailotp + " <br> & Your SMS OTP for verification is : " + smsotp;

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(smtpserver);
                mail.IsBodyHtml = true;
                mail.From = new MailAddress(emailfrom);
                mail.To.Add(emailto);
                mail.Subject = subject;
                mail.Body = body;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(emailfrom, password);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                return "SUCCESS";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////// Approve Reject Donor Cycle

        public ActionResult ApproveDonorCycle(int id)
        {
            if(Session["DonorId"]!= null)
            {
                using (sampleEntities dbModel = new sampleEntities())
                {
                    var donorCycle = dbModel.DonorCycleEgg.Where(dcid => dcid.DonorCycleId == id).FirstOrDefault();
                    donorCycle.isApprovedByDonor = true;
                    dbModel.SaveChanges();
                    ViewBag.SuccessMessage = "Cycle Approved!";

                }

                using (sampleEntities dbModel2 = new sampleEntities())
                {
                    DonorCycleUpdate donorCycleUpdate = new DonorCycleUpdate();
                    donorCycleUpdate.DonorCycleId = id;
                    donorCycleUpdate.UpdateDate = System.DateTime.Today;
                    donorCycleUpdate.UpdateDescription = "Donor Has Approved this Donor Cycle.";
                    donorCycleUpdate.UpdateHeading = "Approved!";
                    donorCycleUpdate.CompletionDate = null;
                    donorCycleUpdate.isCompleted = false;
                    donorCycleUpdate.isSubmitted = true;

                    dbModel2.DonorCycleUpdates.Add(donorCycleUpdate);
                    dbModel2.SaveChanges();
                    
                    ViewBag.SuccessMessage += "First donor cycle update was published";   
                }

                using (sampleEntities dbModel = new sampleEntities())
                {
                    var sessiondonorid = int.Parse(Session["DonorId"].ToString());
                    var donor = dbModel.Donors.Where(p => p.DonorId == sessiondonorid).FirstOrDefault();
                    var donorcycles = dbModel.DonorCycleEgg.Where(p => p.DonorId == sessiondonorid).ToList();

                    var viewModel = new DonorAndDonorCycleViewModel
                    {
                        Donor = donor,
                        DonorCycleEgg = donorcycles
                    };
                    return RedirectToAction("Dashboard", viewModel);                    
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
                    var donorCycle = dbModel.DonorCycleEgg.Where(dcid => dcid.DonorCycleId == id).FirstOrDefault();
                    donorCycle.isApprovedByDonor = false;
                    dbModel.SaveChanges();
                    ViewBag.SuccessMessage = "Cycle Rejected!";

                }

                using (sampleEntities dbModel2 = new sampleEntities())
                {
                    DonorCycleUpdate donorCycleUpdate = new DonorCycleUpdate();
                    donorCycleUpdate.DonorCycleId = id;
                    donorCycleUpdate.UpdateDate = System.DateTime.Today;
                    donorCycleUpdate.UpdateDescription = "Donor Has Rejected this Donor Cycle.";
                    donorCycleUpdate.UpdateHeading = "Rejected!";
                    donorCycleUpdate.CompletionDate = System.DateTime.Today;
                    donorCycleUpdate.isCompleted = true;
                    donorCycleUpdate.isSubmitted = true;

                    dbModel2.DonorCycleUpdates.Add(donorCycleUpdate);
                    dbModel2.SaveChanges();

                    ViewBag.SuccessMessage += "First donor cycle update was published";
                }

                using (sampleEntities dbModel = new sampleEntities())
                {
                    var sessiondonorid = int.Parse(Session["DonorId"].ToString());
                    var donor = dbModel.Donors.Where(p => p.DonorId == sessiondonorid).FirstOrDefault();
                    var donorcycles = dbModel.DonorCycleEgg.Where(p => p.DonorId == sessiondonorid).ToList();

                    var viewModel = new DonorAndDonorCycleViewModel
                    {
                        Donor = donor,
                        DonorCycleEgg = donorcycles
                    };
                    return RedirectToAction("Dashboard", viewModel);
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////// Donor Update

        public ActionResult SubmitUpdate(int id)
        {
            if (Session["DonorId"] != null)
            {
                using (sampleEntities dbModel = new sampleEntities())
                {
                    var donorCycle = dbModel.DonorCycleEgg.Where(dcid => dcid.DonorCycleId == id).FirstOrDefault();
                    var donorcycleUpdate = dbModel.DonorCycleUpdates.Where(dcuid => dcuid.DonorCycleId == donorCycle.DonorCycleId).ToList();

                    var viewModel = new DonorCycleAndUpdates
                    {
                        DonorCycleEgg = donorCycle,
                        DonorCycleUpdate = donorcycleUpdate

                    };

                    return View(viewModel);
                }
               
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }
        public ActionResult SubmitNewUpdate(int id)
        {
            if(Session["DonorId"] != null)
            {
                ViewBag.donorCycleId = id;
                return View();
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }       
        [HttpPost]
        public ActionResult SubmitNewUpdate(MedicalReportUpdate dcu)
        {
            if (Session["DonorId"] != null)
            {
                using(sampleEntities dbModel = new sampleEntities())
                {
                    try
                    {
                        ViewBag.donorCycleId = dcu.DonorCycleId;
                        dcu.DateOfSubmission = System.DateTime.Today;                        
                        dcu.Status = "Pending";
                        dbModel.MedicalReportUpdate.Add(dcu);
                        dbModel.SaveChanges();

                        //ViewBag.SuccessMessage = "Record Updated";                        
                        //return View(dcu);
                        return RedirectToAction("MedicalUpdateStepTwo", new { @id = dcu.MedicalReportId });

                    }
                    catch(Exception ex)
                    {
                        ViewBag.ErrorMessage = "Error - " + ex.Message;
                        return View();
                    }
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }

        public ActionResult MedicalUpdateStepTwo(int id)
        {
            if(Session["DonorId"]!= null)
            {
                using (sampleEntities dbModel = new sampleEntities())
                {
                    var report = dbModel.MedicalReportUpdate.Where(x => x.MedicalReportId == id).FirstOrDefault();
                    var listquestion = dbModel.MedicalReportsQuestions.Include("MedicalQuestions").Where(x => x.MedicalReportId == report.MedicalReportId).ToList();
                    var questions = dbModel.MedicalQuestions.ToList();

                    var viewModel = new MedicalUpdateStepTwoViewModel()
                    {
                        MedicalReportUpdate = report,
                        MedicalReportsQuestions = listquestion,
                        MedicalQuestions = questions
                    };

                    
                    return View(viewModel);
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }

        [HttpPost]
        public ActionResult SaveAnswer(MedicalReportsQuestions mrq)
        {
            if (Session["DonorId"] != null)
            {
                using(sampleEntities dbModel = new sampleEntities())
                {
                    dbModel.MedicalReportsQuestions.Add(mrq);
                    dbModel.SaveChanges();

                    return RedirectToAction("MedicalUpdateStepTwo", new { id = mrq.MedicalReportId });
                    //return View(mrq.MedicalReportId);
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }




        public ActionResult RemoveUpdate(int id)
        {
            if(Session["DonorId"]!=null)
            {
                using (sampleEntities dbModel = new sampleEntities())
                {
                    var donorupdate = dbModel.DonorCycleUpdates.Where(dcuid => dcuid.DonorCycleUpdateId == id).FirstOrDefault();
                    try
                    {
                        dbModel.DonorCycleUpdates.Remove(donorupdate);
                        dbModel.SaveChanges();
                        ViewBag.SuccessMessage = "Update Removed!";
                        return RedirectToAction("SubmitUpdate", new { @id = donorupdate.DonorCycleId });
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = ex.Message;
                        return RedirectToAction("SubmitUpdate", new { @id = donorupdate.DonorCycleId });
                    }
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }
        
        ///////////////////////////////////////////////////////////////////////////////////////////// My Donor Cycle

        public ActionResult MyDonorCycles()
        {
            if(Session["DonorId"]!= null)
            {
                using(sampleEntities dbMOdel = new sampleEntities())
                {
                    int donorid = int.Parse(Session["DonorId"].ToString());
                    var donorCycles = dbMOdel.DonorCycleEgg.Where(x => x.DonorId == donorid).ToList();
                    return View(donorCycles);
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////// My Match Request

        public ActionResult MatchDetails()
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
             
    }
}

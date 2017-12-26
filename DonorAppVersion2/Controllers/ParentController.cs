using DonorAppVersion2.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DonorAppVersion2.Controllers
{
    public class ParentController : Controller
    {
        // GET: Parent
        public ActionResult Index()
        {
            return View();
        }

        ////////////////////////////////////////////////////////////////// Parent Login

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["ParentId"] != null)
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Login([Bind(Include="Email, Password")]Parent parent)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            
                 using (sampleEntities dbModel = new sampleEntities())
                {
                    try
                    {
                        var salt = ComputeHash(parent.Password.ToString());
                        var success = dbModel.Parents.Where(p => p.Email == parent.Email && p.Password == parent.Password).FirstOrDefault();
                        if (success != null)
                        {
                            Session["ParentId"] = success.ParentId;
                            Session["ParentName"] = success.Salutation + " " + success.FirstName + " " + success.LastName;

                            if (success.isEmailVerified == false)
                            {
                                return RedirectToAction("Verification", "Parent", new { @id = success.ParentId });
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
        
        ////////////////////////////////////////////////////////////////// Parent Registrations
        
        [HttpGet]
        public ActionResult Registration()
        {
            Parent parentModel = new Parent();
            return View(parentModel);
        }
        [HttpPost]
        public ActionResult Registration(Parent parentModel)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                using (sampleEntities dbModel = new sampleEntities())
                {
                    if (dbModel.Parents.Any(x => x.Email == parentModel.Email))
                    {
                        ViewBag.DuplicateMessage = "Existing Email, Please Login if you already have an account!";
                        return View("Registration", parentModel);
                    }

                    try
                    {

                        if (parentModel.Password != parentModel.ConfirmPassword)
                        {
                            ViewBag.ErrorMessage = "Password & Confirm password does not match";
                            return View();
                        }
                        else
                        {                       
                            Random randomnumber = new Random();
                            int phoneCode = randomnumber.Next(1000, 9999);
                            int emailCode = randomnumber.Next(1000, 9999);

                            parentModel.EmailVerificationCode = emailCode.ToString();
                            parentModel.ContactVerificationCode = phoneCode.ToString();
                            parentModel.isContactVerified = false;
                            parentModel.isEmailVerified = false;
                            parentModel.CreatinDate = System.DateTime.Today;
                            parentModel.isPaid = false;
                            parentModel.Status = false;
                            parentModel.Salt = ComputeHash(parentModel.Password.ToString());
                            parentModel.Note = "Payment Pending";
                            
                            //Save Data to Database
                            dbModel.Parents.Add(parentModel);
                            dbModel.SaveChanges();
                            ModelState.Clear();

                            //Create User Session
                            Session.Clear();
                            Session["ParentId"] = parentModel.ParentId;
                            Session["ParentName"] = parentModel.Salutation + " " + parentModel.FirstName + " " + parentModel.LastName;

                            //ViewBag.SuccessMessage = "Account Created Successfully! Please login to complete the registration!";

                            //Redirect to Verification Page
                            return RedirectToAction("Verification", "Parent", new { @id = parentModel.ParentId });

                            //return View("Registration", new Parent());

                        }
                    }
                    catch (DbEntityValidationException ex)
                    {
                        // Retrieve the error messages as a list of strings.
                        var errorMessages = ex.EntityValidationErrors
                                .SelectMany(x => x.ValidationErrors)
                                .Select(x => x.ErrorMessage);

                        // Join the list to a single string.
                        var fullErrorMessage = string.Join("; ", errorMessages);

                        // Combine the original exception message with the new one.
                        var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                        // Throw a new DbEntityValidationException with the improved exception message.
                        //throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                        
                        ViewBag.ErrorMessage = exceptionMessage + ex.EntityValidationErrors;
                        return View("Registration", parentModel);
                    }
                }
            }
            else
            {
                int i = 0;
                foreach(ModelError error in errors)
                {
                    i = i + 1;
                    ViewBag.WarningMessage += i.ToString() + ") " + error.ErrorMessage.ToString() + ". ";
                }
                
                return View(parentModel);
            }
        }

        ////////////////////////////////////////////////////////////////// Parent Verification

        [HttpGet]
        public ActionResult Verification(int id)
        {
            using (sampleEntities dbModel = new sampleEntities())
            {
                var parent = dbModel.Parents.Where(p => p.ParentId == id).FirstOrDefault();
                var isVerified = parent.isEmailVerified;
                    if(isVerified == true)
                    {
                        if (Session["ParentId"] != null)
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
                        return View(parent);
                    }
            }
        }
        [HttpPost]
        public ActionResult Verification(Parent parent)
        {
            using (sampleEntities dbModel = new sampleEntities())
            {
                var success = dbModel.Parents.Where(p => p.EmailVerificationCode == parent.EmailVerificationCode && p.ContactVerificationCode == parent.ContactVerificationCode).FirstOrDefault();
                try
                {
                    if (success != null)
                    {
                        parent.isContactVerified = true;
                        parent.isEmailVerified = true;
                        parent.Note = "Contact Information Verified, Waiting for Payment";
                        dbModel.SaveChanges();                         
                        ViewBag.SuccessMessage = "Validation Complete";
                        return RedirectToAction("Dashboard");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "OTP Entered are Invalid, Please verify and try again!";
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

        ////////////////////////////////////////////////////////////////// Parent Dashboard

        [HttpGet]
        public ActionResult Dashboard()
        {
            if (Session["ParentId"] != null)
            {
                int sessionparentid = int.Parse(Session["ParentId"].ToString());
                //var isPaid = false;
                using (sampleEntities dbModel = new sampleEntities())
                {
                    var parent = dbModel.Parents.Where(p => p.ParentId == sessionparentid).FirstOrDefault();
                    var donorcycles = dbModel.DonorCycles.Where(p => p.ParentId == sessionparentid).ToList();

                    var viewModel = new ParentAndDonorCyclesViewModel
                    {
                        Parent = parent,
                        DonorCycle = donorcycles
                    };

                    if (parent != null)
                    {
                        return View(viewModel);
                    }
                    else
                    {
                        return RedirectToAction("SessionTimeout");
                    }   
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }

        ////////////////////////////////////////////////////////////////// Add Donor Cycles
        
        public ActionResult AddDonorCycle()
        {
            if (Session["ParentId"] != null)
            {
                 
                DonorCycle donorCycleModel = new DonorCycle();
                return View(donorCycleModel);
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
            
        }
        [HttpPost]
        public ActionResult AddDonorCycle(DonorCycle donorCycle)
        {
            if (Session["ParentId"] != null)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                string parentid = Session["ParentId"].ToString();
                if (ModelState.IsValid)
                {

                    using (sampleEntities dbModel = new sampleEntities())
                    {
                        try
                        {
                            var donorid = dbModel.Donors.Where(x => x.DonorId == donorCycle.DonorId).FirstOrDefault();
                            if (donorid == null)
                            {
                                ViewBag.ErrorMessage = "This Donor Id does not exist in our database, Please recheck with your Clinic/Agency";
                                return View();
                            }
                            else
                            {
                                donorCycle.ParentId = int.Parse(parentid);
                                donorCycle.isApprovedByAgency = false;
                                donorCycle.isApprovedByClinic = false;
                                donorCycle.isApprovedByDonor = false;
                                donorCycle.isApprovedByParent = true;

                                dbModel.DonorCycles.Add(donorCycle);
                                dbModel.SaveChanges();
                                ModelState.Clear();

                                ViewBag.SuccessMessage= "Donor Cycle Added Successfully!";
                                return View();
                            }
                        }
                        catch (Exception ex)
                        {
                            ViewBag.ErrorMessage = ex.Message;
                            ViewBag.WarningMessage = ex.ToString();
                            return View(donorCycle);
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

                    return View(donorCycle);
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }

        }
        
        ////////////////////////////////////////////////////////////////// Single Actions and Methods
        
        public string ComputeHash(string input)
        {
            HashAlgorithm algorithm = new SHA256CryptoServiceProvider();
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
        public ActionResult SessionTimeout()
        {
            Session.Clear();
            return View();
        }
        public void SendEmailOTP(string otp, string emailto)
        {
            try
            {
                string smtpserver = "smtp.gmail.com";
                string emailfrom = "abc@gmail.com";
                string password = "password";
                string subject = "OTP for Donor21 Registration";
                string body = "Your OTP for Verification of Donor 21 Registration is : <b> " + otp + " </b>";

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(smtpserver);

                mail.From = new MailAddress(emailfrom);
                mail.To.Add(emailto);
                mail.Subject = subject;
                mail.Body = body;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(emailfrom, password);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        ////////////////////////////////////////////////////////////////// Edit Parent Profile
        
        public ActionResult ParentProfile()
        {
            if(Session["ParentId"]!= null)
            {
                int parentid = int.Parse(Session["ParentId"].ToString());
                using(sampleEntities dbModel = new sampleEntities())
                {
                    var parent = dbModel.Parents.Where(x=> x.ParentId == parentid).FirstOrDefault();
                    if(parent != null)
                    {
                        return View(parent);
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
        [HttpPost]
        public ActionResult ParentProfile(Parent parentModel)
        {
            if(Session["ParentId"]!=null)
            {
                using (sampleEntities dbModel = new sampleEntities())
                {
                    var parentFromdb = dbModel.Parents.Where(x => x.ParentId == parentModel.ParentId).FirstOrDefault();

                    parentFromdb = parentModel;
                    try
                    {
                        parentFromdb = parentModel;
                        dbModel.SaveChanges();
                        ViewBag.SuccessMessage = "Updates Saved!";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = ex.Message;
                    }
                    return View(parentModel);
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }
        
        ////////////////////////////////////////////////////////////////// Add Clinic of Agency

        public ActionResult AddClinic()
        {
            if (Session["ParentId"] != null)
            {
                 return View();
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }
        [HttpPost]
        public ActionResult AddClinic(ParentsRegisteredWithPartner prwp)
        {
            if (Session["ParentId"] != null)
            {
                var parentid = Session["ParentId"].ToString();
                prwp.ParentId = int.Parse(parentid);
                prwp.isApproved = false;
                prwp.DateOfApproval = System.DateTime.Today;
                prwp.Status = "Pending";
                prwp.PartnerContactsId = prwp.PartnerContactsId;
                
                using (sampleEntities dbModel = new sampleEntities())
                {
                    ViewBag.ErrorMessage =
                        " Partner ID : " + prwp.ParentPartnerId +
                        ", ParentId ID : " + prwp.ParentId +
                        ", isApproved : " + prwp.isApproved +
                        ", DateOfApproval : " + prwp.DateOfApproval +
                        ", Status : " + prwp.Status +
                        ", Partner Contact ID : " + prwp.PartnerContactsId;

                    /*
                    try
                    {
                        dbModel.ParentsRegisteredWithPartners.Add(prwp);
                        dbModel.SaveChanges();
                        ViewBag.SuccessMessage = "Saved Successfully!";
                    }
                    catch(Exception ex)
                    {
                        ViewBag.ErrorMessage = ex.Message;

                    }
                     * */
                    return View();
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }

        }

        //[HttpPost]
        public ActionResult GetPartners()
        {
            List<SelectListItem> partners = new List<SelectListItem>();
            using (sampleEntities dbModel = new sampleEntities())
            {
                var partnerlistfromdb = dbModel.Partners.ToList();
                for (int i = 0; i < partnerlistfromdb.Count; i++)
                {
                    partners.Add(new SelectListItem
                        {
                            Value = partnerlistfromdb[i].PartnerId.ToString(),
                            Text = partnerlistfromdb[i].Name
                        });
                }


                return Json(partners, JsonRequestBehavior.AllowGet);
            }
        }
        //[HttpPost]
        public ActionResult GetPartnerContacts(int id)
        {
            List<SelectListItem> contacts = new List<SelectListItem>();
            using (sampleEntities dbModel = new sampleEntities())
            {
                var contactlistfromdb = dbModel.PartnerAndTheirContacts.Where(p=>p.PartnerId == id).ToList();
                for (int i = 0; i < contactlistfromdb.Count; i++)
                {
                    contacts.Add(new SelectListItem
                    {
                        Value = contactlistfromdb[i].PartnerContactsId.ToString(),
                        Text = contactlistfromdb[i].ContactName
                    });
                }


                return Json(contacts, JsonRequestBehavior.AllowGet);
            }
        }


        ////////////////////////////////////////////////////////////////// JSON OUTPUT

        [HttpPost]
        public JsonResult GetDonorsList(string prefix)
        {
             using (sampleEntities dbModel = new sampleEntities())
            {
                var donors = (from Donors in dbModel.Donors
                                 where Donors.DonorId.ToString().StartsWith(prefix)
                                 select new
                                 {
                                     Value = Donors.DonorId,
                                     Text = Donors.DonorId
                                 }).ToList();

                return Json(donors);
             }
        }

        ////////////////////////////////////////////////////////////////// Registration Fees

        public ActionResult PayRegistrationFees()
        {
            if (Session["ParentId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }
       

        ////////////////////////////////////////////////////////////////// Request a Match

        public ActionResult RequestMatch()
        {
            if (Session["ParentId"] != null)
            {
                int sessionparentid = int.Parse(Session["ParentId"].ToString());
                //var isPaid = false;
                using (sampleEntities dbModel = new sampleEntities())
                {
                    var matchrequests = dbModel.MatchRequestedByParents.Where(p => p.ParentId == sessionparentid && p.Status != "Canceled").ToList();
                    
                    var viewModel = new ParentMatchRequestViewModel
                    {
                        ParentMatchRequest = matchrequests
                    };

                    return View(viewModel);
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }
        public ActionResult CancelRequest(int id)
        {
            if(Session["ParentId"] != null)
            { 
                using(sampleEntities DbModel = new sampleEntities())
                {
                    var matchrequests = DbModel.MatchRequestedByParents.Where(p => p.ParentMatchRequestId == id).FirstOrDefault();

                    if (matchrequests != null)
                    {
                        matchrequests.Status = "Canceled";
                        DbModel.SaveChanges();
                        return RedirectToAction("RequestMatch");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Invalid Match Request Id";
                        return RedirectToAction("RequestMatch");

                    }
                
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }

            
        }
        public ActionResult NewRequest()
        {
            if (Session["ParentId"] != null)
            {
                return View();
            }
            else
            {
               return RedirectToAction("SessionTimeout");
            }
    
        }
        [HttpPost]
        public ActionResult NewRequest(MatchRequestedByParent mrbp)
        {
            if (Session["ParentId"] != null)
            {
                mrbp.ParentId = Convert.ToInt32(Session["ParentId"].ToString());
                mrbp.RequestDate = System.DateTime.Today;
                mrbp.isApproved = false;
                mrbp.Status = "Pending";
                mrbp.isPaidByParent = false;
                mrbp.ParentsPaymentId = null;

                using (sampleEntities DbModel = new sampleEntities())
                {
                    try
                    {
                        DbModel.MatchRequestedByParents.Add(mrbp);
                        DbModel.SaveChanges();

                        ViewBag.SuccessMessage = "Your Request has been submitted successfully!";
                        return View();
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = "There were errors";
                        return View(mrbp);
                    }
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }


        ////////////////////////////////////////////////////////////////// View Donor Cycle Updates
        public ActionResult ViewDonorCycle(int id)
        {
            if (Session["ParentId"] != null)
            {
                if (id > 0)
                {
                    using(sampleEntities dbModel = new sampleEntities())
                    {
                        var donorCycle = dbModel.DonorCycles.Where(dcid =>  dcid.DonorCycleId == id).FirstOrDefault();
                        var donorcycleUpdate = dbModel.DonorCycleUpdates.Where(dcuid => dcuid.DonorCycleId == donorCycle.DonorCycleId).ToList();

                        var viewModel = new DonorCycleAndUpdates
                        {
                            DonorCycle = donorCycle,
                            DonorCycleUpdate = donorcycleUpdate

                        };

                        return View(viewModel);
                    }
                }
                else
                {
                    return RedirectToAction("Dashboard");
                }
            }
            else
            {
                return RedirectToAction("SessionTimeout");
            }
        }

        //[HttpPost]
        public ActionResult RemoveDonorCycle(int id)
        {
            if (Session["ParentId"] != null)
            {
                using (sampleEntities DbModel = new sampleEntities())
                {
                    var donorcycles = DbModel.DonorCycles.Where(p => p.DonorCycleId == id).FirstOrDefault();

                    if (donorcycles != null)
                    {
                        DbModel.DonorCycles.Remove(donorcycles);
                        DbModel.SaveChanges();
                        ViewBag.SuccessMessage = "Donor Cycle Removed!";
                        return RedirectToAction("Dashboard");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Invalid Donor Cycle Id";
                        return RedirectToAction("Dashboard");
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
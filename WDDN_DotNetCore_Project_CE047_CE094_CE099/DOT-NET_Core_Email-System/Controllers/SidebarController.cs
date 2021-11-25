using DOT_NET_Core_Email_System.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;
using DOT_NET_Core_Email_System.ViewModels;

namespace DOT_NET_Core_Email_System.Controllers
{
    public class SidebarController : Controller
    {
        private readonly string UserEmailSession = "_UserEmail";
        private readonly string UserEmailViewSession = "_UserEmailView";
        private readonly AppDbContext context;
        private readonly IUserRepo userRepo;
        public SidebarController(IUserRepo userRepo, AppDbContext context)
        {
            this.userRepo = userRepo;
            this.context = context;
        }
        public IActionResult Compose()
        {
            return View();
        }
        [HttpPost, ActionName("Compose")]
        public IActionResult Compose(ComposeViewModel modelEmail)
        {
            string CurrentSentUserMail = HttpContext.Session.GetString(UserEmailSession);
            if (CurrentSentUserMail != null)
            {
                if (modelEmail.ToUserEmailId == null)
                {
                    ViewBag.Message = string.Format("You have to add email where you want to send");
                }
                else if (modelEmail.ToUserEmailId == CurrentSentUserMail)
                {
                    ViewBag.Message = string.Format("You can not mail your self");
                }
                else if (modelEmail.EmailText == null && modelEmail.Attachment == null)
                {
                    ViewBag.Message = string.Format("You have to write or attach something");
                    return View();
                }
                else
                {
                    DbUser ToMailUser = userRepo.GetUserEmail(modelEmail.ToUserEmailId);
                    if (ToMailUser == null)
                    {
                        ViewBag.Message = string.Format("Email you want to send is not found");
                    }
                    else
                    {
                        string FromUserId, ToUserId;
                        ToUserId = ToMailUser.UserEmailId;
                        DbUser FromUser = userRepo.GetUserEmail(CurrentSentUserMail);
                        FromUserId = FromUser.UserEmailId;
                        DbEmail email = new DbEmail { };
                        email.FromUserEmailId = FromUserId;
                        email.ToUserEmailId = ToUserId;
                        email.EmailSubject = modelEmail.EmailSubject == null ? "(no subject)" : modelEmail.EmailSubject;
                        email.EmailText = modelEmail.EmailText == null ? "(no text)" : modelEmail.EmailText;
                        email.Is_Inbox = true;
                        email.Is_Sent = true;
                        email.Is_FromUser_Starred = false;
                        email.Is_ToUser_Starred = false;
                        email.Is_FromUser_Delete = false;
                        email.Is_ToUser_Delete = false;
                        if (modelEmail.Attachment != null)
                        {
                            string FileType = modelEmail.Attachment.ContentType;
                            ViewBag.Message = string.Format("has attachment.");
                            Byte[] FileBytes = null;
                            using (MemoryStream ms = new MemoryStream())
                            {
                                modelEmail.Attachment.OpenReadStream().CopyTo(ms);
                                FileBytes = ms.ToArray();
                            }
                            email.AttachmentData = FileBytes;
                            email.AttachmentName = modelEmail.Attachment.FileName;
                            email.AttachmentType = FileType;
                        }
                        context.Emails.Add(email);
                        context.SaveChanges();
                    }
                }
                return RedirectToAction("sent", "sidebar");
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Inbox()
        {
            string CurrentSentUserMail = HttpContext.Session.GetString(UserEmailSession);
            if (CurrentSentUserMail != null)
            {
                var model = (IEnumerable<DbEmail>)context.Emails.Where(m => m.ToUserEmailId == CurrentSentUserMail && m.Is_Inbox == true);
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Starred()
        {
            string CurrentSentUserMail = HttpContext.Session.GetString(UserEmailSession);
            if (CurrentSentUserMail != null) 
            { 
                var model = (IEnumerable<DbEmail>)context.Emails.Where(m => (m.ToUserEmailId == CurrentSentUserMail && m.Is_ToUser_Starred == true) || (m.FromUserEmailId == CurrentSentUserMail && m.Is_FromUser_Starred == true));
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Sent()
        {
            string CurrentSentUserMail = HttpContext.Session.GetString(UserEmailSession);
            if (CurrentSentUserMail != null)
            {
                var model = (IEnumerable<DbEmail>)context.Emails.Where(m => m.FromUserEmailId == CurrentSentUserMail && m.Is_Sent == true);
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Trash()
        {
            string CurrentSentUserMail = HttpContext.Session.GetString(UserEmailSession);
            if (CurrentSentUserMail != null)
            {
                var model = (IEnumerable<DbEmail>)context.Emails.Where(m => (m.ToUserEmailId == CurrentSentUserMail && m.Is_ToUser_Delete == true) || (m.FromUserEmailId == CurrentSentUserMail && m.Is_FromUser_Delete == true));
                return View(model);
            }
            return RedirectToAction("Index", "Home");
            }
        public IActionResult ViewPassMail(int id)
        {
            HttpContext.Session.SetInt32(UserEmailViewSession, id);            
            return RedirectToAction("ViewMail", "Sidebar");
        }
        public IActionResult ViewMail()
        {
            int id = (int)HttpContext.Session.GetInt32(UserEmailViewSession);
            if (id>0)
            {
                var model = context.Emails.FirstOrDefault(m => m.Id == id);
                return View(model);
            }
            return RedirectToAction("ViewMail", "Sidebar");
        }
        public IActionResult DownloadAttachment(int id)
        {
            var model = context.Emails.FirstOrDefault(m => m.Id == id);
            return File(model.AttachmentData, model.AttachmentType, model.AttachmentName);
        }
        public IActionResult StarMail(int id)
        {
            string CurrentSentUserMail = HttpContext.Session.GetString(UserEmailSession);
            var emailUpdate = context.Emails.FirstOrDefault(m => m.Id == id);
            if (emailUpdate.FromUserEmailId == CurrentSentUserMail)
            {
                if (emailUpdate.Is_FromUser_Starred == true)
                {
                    emailUpdate.Is_FromUser_Starred = false;
                }
                else
                {
                    emailUpdate.Is_FromUser_Starred = true;
                }
            }
            else if (emailUpdate.ToUserEmailId == CurrentSentUserMail)
            {
                if (emailUpdate.Is_ToUser_Starred == true)
                {
                    emailUpdate.Is_ToUser_Starred = false;
                }
                else
                {
                    emailUpdate.Is_ToUser_Starred = true;
                }
            }
            context.Emails.Attach(emailUpdate);
            context.SaveChanges();
            return RedirectToAction("Starred", "Sidebar");
        }
        public IActionResult DeleteMail(int id)
        {
            string CurrentSentUserMail = HttpContext.Session.GetString(UserEmailSession);
            var emailUpdate = context.Emails.FirstOrDefault(m => m.Id == id);
            if (emailUpdate.FromUserEmailId == CurrentSentUserMail)
            {
                emailUpdate.Is_Sent = false;                
                emailUpdate.Is_FromUser_Delete = true;
            }
            else if (emailUpdate.ToUserEmailId == CurrentSentUserMail)
            {
                emailUpdate.Is_Inbox = false;
                emailUpdate.Is_ToUser_Delete = true;
            }
            context.Emails.Attach(emailUpdate);
            context.SaveChanges();
            return RedirectToAction("Trash", "Sidebar");
        }
        public IActionResult RestoreMail(int id)
        {
            string CurrentSentUserMail = HttpContext.Session.GetString(UserEmailSession);
            var emailUpdate = context.Emails.FirstOrDefault(m => m.Id == id);
            if (emailUpdate.FromUserEmailId == CurrentSentUserMail)
            {
                emailUpdate.Is_Sent = true;
                emailUpdate.Is_FromUser_Delete = false;
            }
            else if (emailUpdate.ToUserEmailId == CurrentSentUserMail)
            {
                emailUpdate.Is_Inbox = true;
                emailUpdate.Is_ToUser_Delete = false;
            }
            context.Emails.Attach(emailUpdate);
            context.SaveChanges();
            return RedirectToAction("Inbox", "Sidebar");
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}

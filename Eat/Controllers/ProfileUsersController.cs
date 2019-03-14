using Eat.Models;
using Eat.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Eat.Controllers
{
    public class ProfileUsersController : Controller
    {
        private ApplicationDbContext db { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public ProfileUsersController()
        {
            this.db = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
        }
        // GET: ApplicationUsers
        public ActionResult Index()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            return View(user);
        }

        // GET: ApplicationUsers/Details/5
        public ActionResult Details()
        {
            
            return View();
        }


        // GET: ApplicationUsers/Edit/5
        public ActionResult EditProfile()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            UserProfileUpdateViewModel UserModelUpdate = new UserProfileUpdateViewModel();
            UserModelUpdate.Avatar = user.Avatar;
            UserModelUpdate.AvatarImageFile = user.AvatarImageFile;
            UserModelUpdate.Id = user.Id;
            UserModelUpdate.Number = user.PhoneNumber;
            UserModelUpdate.UserName = user.UserName;
            return View(UserModelUpdate);
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(UserProfileUpdateViewModel model)
        {
            ApplicationUser applicationUser = db.Users.Find(model.Id);
            if (db.Users.Where(x => x.Id!= model.Id).Any(n => n.UserName == model.UserName))
            {
                ModelState.AddModelError("UserName", "Name is busy");
            }
            if (model.AvatarImageFile != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(model.AvatarImageFile.FileName);
                string extention = Path.GetExtension(model.AvatarImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                model.Avatar = "~/Images/Avatars/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/Avatars/"), fileName);
                model.AvatarImageFile.SaveAs(fileName);
            }

            if (ModelState.IsValid)
            {
                
                if (model.Avatar != null && model.AvatarImageFile != null)
                {
                    applicationUser.Avatar = model.Avatar;
                    applicationUser.AvatarImageFile = model.AvatarImageFile;
                }
                applicationUser.Id = model.Id;
                applicationUser.PhoneNumber = model.Number;
                applicationUser.UserName = model.UserName;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

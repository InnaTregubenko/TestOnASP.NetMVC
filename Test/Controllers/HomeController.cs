using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Test.Models;
using Microsoft.VisualBasic.FileIO;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        private UserContext db = new UserContext();
        public ActionResult Index()
        {
            return View(db.Users.OrderBy(u => u.Id).ToList());
        }

        public ActionResult Fun()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Upload()
        {
            foreach (string file in Request.Files)
            {
                var upload = Request.Files[file];
                if (upload != null)
                {
                        // получаем имя файла
                        string fileName = System.IO.Path.GetFileName(upload.FileName);
                    //// сохраняем файл в папку Files в проекте
                    if (fileName != null)
                    {
                        upload.SaveAs(Server.MapPath("~/UploadFiles/" + fileName));
                        var FilePath = Request.MapPath("~/UploadFiles")+ "\\" + fileName;
                        using (TextFieldParser parser = new TextFieldParser(FilePath))
                        {
                            parser.TextFieldType = FieldType.Delimited;
                            parser.SetDelimiters(";");
                            while (!parser.EndOfData)
                            {
                                //Processing row
                                string[] fields = parser.ReadFields();
                                DateTime dateTime = Convert.ToDateTime(fields[1]);
                                decimal sal = Convert.ToDecimal(fields[4]);
                                User us = new User {Name = fields[0], Phone = fields[3], DateofBirth = dateTime, Married = Convert.ToBoolean(fields[2]), Salary = sal};
                                db.Users.Add(us);
                                db.SaveChanges();
                            }
                        }
                    }
                }
            }
            return Json("файл загружен");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Edit(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(id);
                
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            
        }


        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                User users = db.Users.Find(user.Id);
                users.Name = user.Name;
                users.Phone = user.Phone;
                users.DateofBirth = Convert.ToDateTime(user.DateofBirth);
                //users.Salary = user.Salary;
                users.Married = user.Married;
                db.Users.Attach(users);
                //db.Entry(users).Property(c => c.Active).IsModified = true;
                //db.Entry(users).Property(c => c.LastName).IsModified = true;
                //db.Entry(users).Property(c => c.FirstName).IsModified = true;
                //db.Entry(users).Property(c => c.Login).IsModified = true;
                db.Entry(users).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }


        public ActionResult Delete(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User users = db.Users.Find(id);
                if (users == null)
                {
                    return HttpNotFound();
                }
                return View(users);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedUser(int id)
        {
            User users = db.Users.Find(id);
            db.Users.Remove(users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
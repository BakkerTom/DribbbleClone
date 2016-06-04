using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dribbble.Models;

namespace Dribbble.Controllers
{
    public class ShotController : Controller
    {
        private ShotRepository shotRepo = new ShotRepository();
        // GET: Shot
        public ActionResult Index()
        {
            return View(shotRepo.GetAll());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shot s = shotRepo.GetByID((int) id);
            if (s == null)
            {
                return HttpNotFound();
            }
            return View(s);
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Models.Shot shot)
        {
            if (Request != null)
            {
                if (shot.file != null)
                {
                    string fileName = Path.GetFileName(shot.file.FileName);
                    if (fileName != null)
                    {
                        var path = Path.Combine(Server.MapPath("~/Content/uploads"), fileName);
                        shot.file.SaveAs(path);

                        shot.ImageURL = fileName;
                        shot.AccountID = Convert.ToInt32(User.Identity.Name);

                        shotRepo.Insert(shot);

                        return RedirectToAction("Index");
                    }
                }
                return View();
            }
            return View();
        }
    }
}
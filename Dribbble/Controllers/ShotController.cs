using System;
using System.Collections.Generic;
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
    }
}
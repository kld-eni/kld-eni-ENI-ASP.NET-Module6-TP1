using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Module6.Data;
using Module6.Models;
using Module6.ViewModels;

namespace Module6.Controllers
{
    public class SamouraisController : Controller
    {
        private Module6Context db = new Module6Context();

        // GET: Samourais
        public ActionResult Index()
        {
            return View(db.Samourais.ToList());
        }

        // GET: Samourais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(samourai);
        }

        // GET: Samourais/Create
        public ActionResult Create()
        {
            SamouraiVM vm = new SamouraiVM { WeaponsListItems = db.Armes.Select(a => new SelectListItem { Text = a.Nom, Value = a.Id.ToString() }).ToList() };
            return View(vm);
        }

        // POST: Samourais/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SamouraiVM samouraiVM)
        {
            try
            {
                ModelState.Remove("Samourai.Id");
                if (ModelState.IsValid)
                {
                    Samourai newSamourai = new Samourai
                    {
                        Nom = samouraiVM.Samourai.Nom,
                        Force = samouraiVM.Samourai.Force,
                        Arme = db.Armes.Find(samouraiVM.SelectedWeapon)
                    };

                    db.Samourais.Add(newSamourai);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(samouraiVM);
            }
            catch
            {
                return View(samouraiVM);
            }
        }

        // GET: Samourais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            SamouraiVM vm = new SamouraiVM
            {
                Samourai = samourai,
                SelectedWeapon = samourai.Arme != null ? samourai.Arme.Id : 0,
                WeaponsListItems = db.Armes.Select(a => new SelectListItem { Text = a.Nom, Value = a.Id.ToString() }).ToList()
            };
            return View(vm);
        }

        // POST: Samourais/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SamouraiVM samouraiVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Samourai samourai = db.Samourais.Find(samouraiVM.Samourai.Id);
                    if (samourai == null)
                    {
                        return RedirectToAction("Index");
                    }
                    samourai.Nom = samouraiVM.Samourai.Nom;
                    samourai.Force = samouraiVM.Samourai.Force;
                    samourai.Arme = db.Armes.Find(samouraiVM.SelectedWeapon);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(samouraiVM);
            }
            catch
            {
                return View(samouraiVM);
            }
        }

        // GET: Samourais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(samourai);
        }

        // POST: Samourais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Samourai samourai = db.Samourais.Find(id);
            db.Samourais.Remove(samourai);
            db.SaveChanges();
            return RedirectToAction("Index");
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

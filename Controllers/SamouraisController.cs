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
            return View(db.Samourais.Include("Arme").Include("ArtsMartiaux").ToList());
        }

        // GET: Samourais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Include("Arme").Include("ArtsMartiaux").Where(s => s.Id == id).SingleOrDefault();
            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(samourai);
        }

        // GET: Samourais/Create
        public ActionResult Create()
        {
            SamouraiVM vm = new SamouraiVM {
                WeaponsListItems = getAvailableWeapons(),
                MartialArtsListItems = db.ArtMartiaux.Select(am => new SelectListItem { Text = am.Nom, Value = am.Id.ToString() }).ToList()
            };
            return View(vm);
        }

        // POST: Samourais/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SamouraiVM samouraiVM)
        {
            ModelState.Remove("Samourai.Id");
            if (ModelState.IsValid)
            {
                Samourai newSamourai = new Samourai
                {
                    Nom = samouraiVM.Samourai.Nom,
                    Force = samouraiVM.Samourai.Force,
                    Arme = db.Armes.Find(samouraiVM.SelectedWeapon),
                    ArtsMartiaux = db.ArtMartiaux.Where(am => samouraiVM.SelectedMartialArts.Contains(am.Id)).ToList()
            };

                db.Samourais.Add(newSamourai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(samouraiVM);
        }

        // GET: Samourais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Include("Arme").Include("ArtsMartiaux").Where(s => s.Id == id).SingleOrDefault();
            if (samourai == null)
            {
                return HttpNotFound();
            }

            SamouraiVM vm = new SamouraiVM
            {
                Samourai = samourai,
                SelectedWeapon = samourai.Arme?.Id,
                SelectedMartialArts = samourai.ArtsMartiaux.Select(am => am.Id).ToList(),
                WeaponsListItems = getAvailableWeapons(samourai.Id),
                MartialArtsListItems = db.ArtMartiaux.Select(am => new SelectListItem { Text = am.Nom, Value = am.Id.ToString() }).ToList()
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
                    Samourai samourai = db.Samourais.Include("Arme").Include("ArtsMartiaux").Where(s => s.Id == samouraiVM.Samourai.Id).SingleOrDefault();
                    if (samourai == null)
                    {
                        return RedirectToAction("Index");
                    }
                    samourai.Nom = samouraiVM.Samourai.Nom;
                    samourai.Force = samouraiVM.Samourai.Force;
                    samourai.Arme = db.Armes.Find(samouraiVM.SelectedWeapon);
                    samourai.ArtsMartiaux = db.ArtMartiaux.Where(am => samouraiVM.SelectedMartialArts.Contains(am.Id)).ToList();
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

        private List<SelectListItem> getAvailableWeapons(int? samouraiId = null)
        {
            List<SelectListItem> availableWeapons = db.Armes.Where(a => db.Samourais.All(s => s.Arme.Id != a.Id)).Select(a => new SelectListItem { Text = a.Nom, Value = a.Id.ToString() }).ToList();
            if (samouraiId != null)
            {
                Arme samouraisWeapon = db.Samourais.Find(samouraiId).Arme;
                if (samouraisWeapon != null)
                {
                    availableWeapons.Add(new SelectListItem { Text = samouraisWeapon.Nom, Value = samouraisWeapon.Id.ToString() });
                }
            }
            return availableWeapons;
        }
    }
}

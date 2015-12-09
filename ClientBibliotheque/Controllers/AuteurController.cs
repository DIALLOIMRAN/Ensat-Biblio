using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClientBibliotheque.ServiceReferenceBibliotheque;

namespace ClientBibliotheque.Controllers
{
    public class AuteurController : Controller
    {
        ServiceBibliothequeClient client = new ServiceBibliothequeClient();
        // GET: Auteur
        public ActionResult Index()
        {
            if (!this.isLogged())
            {
                return Redirect("/Login/Index");
            }
            Utilisateur user = (Utilisateur)Session["UtilisateurConnecte"];
            if (user == null)
            {
                return Redirect("/Home/Index");
            }
            ViewBag.auteurs = client.getAuteurs();
            return View();
        }

        private bool isLogged()
        {
            Utilisateur user = (Utilisateur)Session["UtilisateurConnecte"];
            if (user == null)
            {
                return false;
            }
            return true;
        }

        // GET: filieres/Create
        public ActionResult Create()
        {
            if (!this.isLogged())
            {
                return Redirect("/Login/Index");
            }
            return View();
        }

        // POST: filieres/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Auteur f)
        {
            if (ModelState.IsValid)
            {
                client.addAuteur(f);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        // GET: Auteur/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!this.isLogged())
            {
                return Redirect("/Login/Index");
            }
            return View(client.getAuteur(id.Value));
        }

        // POST: Auteur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            client.deleteAuteur(id);
            return RedirectToAction("Index");
        }

        // GET: Auteur/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (!this.isLogged())
            {
                return Redirect("/Login/Index");
            }
            if (id.HasValue)
            {
                using (client)
                {
                    Auteur f = client.getAuteur(id.Value);

                    if (f == null)
                    {
                        return View("Error");
                    }
                    return View(f);
                }
            }
            else
            {
                return View("Error");
            }

        }

        // POST: Auteur/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Auteur f)
        {
            if (!this.isLogged())
            {
                return Redirect("/Login/Index");
            }
            if (ModelState.IsValid)
            {
                client.updateAuteur(f);
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        
        // GET: Auteur/Details/5
        public ActionResult Details(int? id)
        {
            if (!this.isLogged())
            {
                return Redirect("/Login/Index");
            }
            Auteur f = client.getAuteur(id.Value);
            if (f == null)
            {
                return HttpNotFound();
            }
            return View(f);
        }
    }
}
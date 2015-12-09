using ClientBibliotheque.ServiceReferenceBibliotheque;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientBibliotheque.Controllers
{
    public class MembreController : Controller
    {
        // GET: Membre
        public ActionResult Index()
        {
            Utilisateur user = (Utilisateur)Session["UtilisateurConnecte"];
            if (user == null)
            {
                return Redirect("/Home/Index");
            }

            ViewBag.Title = "Bibliothèque : Membre";
            return View();
        }
    }
}
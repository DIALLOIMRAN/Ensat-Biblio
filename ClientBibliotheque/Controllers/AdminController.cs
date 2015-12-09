using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClientBibliotheque.ServiceReferenceBibliotheque;

namespace ClientBibliotheque.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
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
            ViewBag.Title = "Bibliothèque : Admin";
            return View(user);
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


    }
}
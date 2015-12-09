using ClientBibliotheque.ServiceReferenceBibliotheque;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientBibliotheque.Controllers
{
    public class UserController : Controller
    {
        ServiceBibliothequeClient client = new ServiceBibliothequeClient();
        
        // GET: User     
        public ActionResult Index()
        {
            if (!this.isLogged())
            {
                return Redirect("/Login/Index");
            }
                       
            ViewBag.user = (Utilisateur) Session["UtilisateurConnecte"];
            ViewBag.Title = "Bibliothèque : Admin";
            return View ();
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
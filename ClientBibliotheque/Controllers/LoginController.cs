using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClientBibliotheque.ServiceReferenceBibliotheque;

namespace ClientBibliotheque.Controllers
{
    public class LoginController : Controller
    {
        ServiceBibliothequeClient client = new ServiceBibliothequeClient();
        // GET: Login
        public ActionResult Index(Utilisateur user = null)
        {
            ViewBag.Title = "Bibliothèque : Connexion";
            ViewBag.msgErreur = "";
            return View(user);
        }

        [HttpPost]
        public ActionResult Connect(Utilisateur user = null)
        {
            try
            {
                if (user != null)
                {
                    Utilisateur utilisateur = client.getUser(user.nomUser, user.motdepasse);
                    //Utilisateur user1 = client.getUser("dialloimran", "lelouma");

                    if (utilisateur != null)
                    {
                        Session["UtilisateurConnecte"] = utilisateur;
                        if (utilisateur.typeUser == "admin")
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        return RedirectToAction("Index", "Membre");
                    }
                    else
                    {
                        ViewBag.msgErreur = "erreur !!!! ";
                        return RedirectToAction("Index");
                    }

                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.msgErreur = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Disconnect()
        {
            Session["UtilisateurConnecte"] = null;
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        // GET: Login/Register
        [HttpGet]
        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Utilisateur user = null)
        {
            if (ModelState.IsValid)
            {
                if (user.email != null && user.nomUser != null && user.motdepasse != null)
                {
                    client.s_inscrire(user);
                    return Redirect("/Login/Index");

                }
                return View();
            }

            return View("error");
        }



    }
}
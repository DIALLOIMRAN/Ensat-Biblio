using ClientBibliotheque.ServiceReferenceBibliotheque;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientBibliotheque.Controllers
{
    public class LivreController : Controller
    {
        ServiceBibliothequeClient client = new ServiceBibliothequeClient();
        // GET: Livre
        public ActionResult Index(livreDeails [] livres = null)
        {
             ViewBag.Title = "Bibliothèque : livres";
            if (livres == null)
            {
                ViewBag.livres = client.getLivres(); 
            }
            ViewBag.categories = client.getCategories();
            ViewBag.auteurs = client.getAuteurs();

            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                using (client)
                {
                    
                    Livre c = client.getDetailsLivre(id.Value);
                    if (c == null)
                    {
                        return View("error");
                    }
                    ViewBag.livre = c;
                    ViewBag.categories = client.getCategories();
                    ViewBag.auteurs = client.getAuteurs();

                    return View(c);
                }
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult Categorie(int? id)
        {
            if (id.HasValue)
            {
                using (client)
                {
                    ViewBag.Title = "Bibliothèque : livres";
                    ViewBag.livres = client.getLivresByCategorie(id.Value);
                    ViewBag.categories = client.getCategories();
                    ViewBag.auteurs = client.getAuteurs();
                    ViewBag.categorie = client.getCategorie(id.Value).nom;
                    
                    return View();
                }
            }
            else
            {
                return View("Error");
            }
            
        }

        public ActionResult Auteur(int? id)
        {
            if (id.HasValue)
            {
                using (client)
                {
                    ViewBag.Title = "Bibliothèque : livres";
                    ViewBag.livres = client.getLivresByAuteur(id.Value);
                    ViewBag.categories = client.getCategories();
                    ViewBag.auteurs = client.getAuteurs();
                    Auteur auteur = client.getAuteur(id.Value);
                    ViewBag.auteur = auteur.nom+ " "+ auteur.prenom;
                    
                    return View();
                }
            }
            else
            {
                return View("Error");
            }
        }

    }
}
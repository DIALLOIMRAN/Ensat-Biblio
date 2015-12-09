using ClientBibliotheque.ServiceReferenceBibliotheque;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Web;
using System.IO;
using System;

namespace ClientBibliotheque.Controllers
{
    public class CategorieController : Controller
    {
        ServiceBibliothequeClient client = new ServiceBibliothequeClient();
        // GET: Categorie
        public ActionResult Index()
        {
            
            
            if (!this.isLogged())
            {
                return Redirect("/Login/Index");
            }
            ViewBag.categories = client.getCategories();
            ViewBag.Title = "Bibliothèque : Categorie";
            ViewBag.nombre = client.nomvreLivreCategori(1);
            return View();
        }

        private int nombreExemplaires(int id)
        {
            int nbr = 0;
            return nbr;
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
        public ActionResult Create(Categorie f=null)
        {
            if (ModelState.IsValid)
            {
                client.addCategorie(f);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        // GET: filiere/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!this.isLogged())
            {
                return Redirect("/Login/Index");
            }

            if (id.HasValue)
            {
                using (client)
                {
                    Categorie c = client.getCategorie(id.Value);
                    if (c == null)
                    {
                        return View("error");
                    }
                    
                    return View(c);
                }
            }
            else
            {
                return View("Error");
            }
        }

        // POST: filiere/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            client.deleteCategorie(id);
            return RedirectToAction("Index");
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
        // GET: filieres/Edit/5
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
                    Categorie f = client.getCategorie(id.Value);

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

        // POST: filieres1/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categorie f)
        {
            if (!this.isLogged())
            {
                return Redirect("/Login/Index");
            }
            if (ModelState.IsValid)
            {
                client.updateCategorie(f);
                return RedirectToAction("Index");
            }
            
            return View("Index");
        }

        // GET: filieres/PersonnesOntVus/5
        //public ActionResult PersonnesOntVus(int? id)
        //{
        //    //Personne[] = client.getPersonneOntVu(id.Value);
        //    //if (f == null)
        //    //{
        //    //    return HttpNotFound();
        //    //}
        //    //return View(client.getPersonneOntVu(id.Value));
        //}

        // GET: filieres/Details/5
        
        public ActionResult Details(int? id)
        {
            if (!this.isLogged())
            {
                return Redirect("/Login/Index");
            }
            Categorie f = client.getCategorie(id.Value);
            if (f == null)
            {
                return HttpNotFound();
            }
            
            return View(f);
        }

        public ActionResult Livres(int? id) 
        {
            if (id.HasValue)
            {
                using (client)
                {
                    
                    if (!this.isLogged())
                    {
                        return Redirect("/Login/Index");
                    }
                    ViewBag.Title = "Bibliothèque : livres";
                    ViewBag.livres = client.getLivresByCategorie(id.Value);
                    ViewBag.categorie = client.getCategorie(id.Value).nom;
                    return View();
                }
            }
            else
            {
                return View("Error");
            }
        }

        private List<SelectListItem> setItemsCombo(Categorie[] objets)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (Categorie objet in objets)
            {
                listItems.Add(new SelectListItem
                {
                    Text = objet.id+"",
                    Value = objet.nom
                });
            }
            return listItems;
        }

        [HttpGet]
        public ActionResult CreateLivre()
        {
                           
               if (!this.isLogged())
               {
                   return Redirect("/Login/Index");
               }
               ViewBag.Title = "Bibliothèque : Céer un livre";

               //ViewBag.categories = this.setItemsCombo(client.getCategories());
               ViewBag.categories = client.getCategories();
               ViewBag.auteurs = client.getAuteurs();               
               return View();                
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLivre(Livre livre = null)
        {
            
            //if (l.livrePath.ContentLength > 0 && l.livrePath.ContentLength > 0)
            // {
            //     string livrePathName = Path.GetFileName(l.livrePath.FileName);
            //     livre.image = livrePathName;
            //     var path = Path.Combine(Server.MapPath("~/Content/images/upload"), livrePathName);
            //     l.livrePath.SaveAs(path); 
            // }

            
            client.addLivre(livre);
            return Redirect("/Categorie/livres/" + livre.categorieid);            
        } 

    }
}
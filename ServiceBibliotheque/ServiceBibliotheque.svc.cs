using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceBibliotheque
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "ServiceBibliotheque" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez ServiceBibliotheque.svc ou ServiceBibliotheque.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class ServiceBibliotheque : IServiceBibliotheque
    {
        biblioEntities DB = new biblioEntities() 
        { 
            Configuration = { 
                                LazyLoadingEnabled = false, 
                                ProxyCreationEnabled = false 
                            } 
        };

        public void s_inscrire(Utilisateur user)
        {
            // ajouter cet utilisateur puis rafraichier le model pour l'appliquée
            // dans la base de données
            
            DB.Utilisateur.Add(user);
            DB.SaveChanges();
        }

        public Utilisateur getUser(string username, string password)
        {
            try
            {
                return DB.Utilisateur.Single(
                                            user => user.nomUser == username &&
                                                     user.motdepasse == password
                                        );
            } catch  {

                return null;
            }        
        }       

        public void addLivre(Livre newLivre)
        {
            DB.Livre.Add(newLivre);
            DB.SaveChanges();
        }

        public void updateLivre(Livre newLivre)
        {
            Livre livre = DB.Livre.Single(l => l.id==newLivre.id);

            livre.Titre = newLivre.Titre;
            livre.description = newLivre.description;
            livre.resume = newLivre.resume;
            livre.quantite = newLivre.quantite;
            livre.isbn = newLivre.isbn;
            livre.image = newLivre.image;
            
            DB.SaveChanges();
        }

        public void deleteLivre(int id)
        {
            Livre livre = DB.Livre.Single(l => l.id == id);
            DB.Livre.Remove(livre);
            DB.SaveChanges();
        }

        public void addAuteur(Auteur newAuteur)
        {
            DB.Auteur.Add(newAuteur);
            DB.SaveChanges();
        }

        public void updateAuteur(Auteur newAuteur)
        {
            Auteur auteur = DB.Auteur.Single(l => l.id == newAuteur.id);

            auteur.nom = newAuteur.nom;
            auteur.prenom = newAuteur.prenom;
            auteur.sexe = newAuteur.sexe;
            auteur.photo = newAuteur.photo;
            auteur.email = newAuteur.email;

            auteur.date_nais = newAuteur.date_nais;
            auteur.apropos = newAuteur.apropos;
            auteur.nationalite = newAuteur.nationalite;
            auteur.typeAuteur = newAuteur.typeAuteur;
            
            DB.SaveChanges();
        }

        public void deleteAuteur(int id)
        {
            Auteur auteur = DB.Auteur.Single(l => l.id == id);
            DB.Auteur.Remove(auteur);
            DB.SaveChanges();
        }

        public void addCategorie(Categorie newCategorie)
        {
            DB.Categorie.Add(newCategorie);
            DB.SaveChanges();
        }

        public void updateCategorie(Categorie newCategorie)
        {
            Categorie categorie = DB.Categorie.Single(l => l.id == newCategorie.id);

            categorie.nom = newCategorie.nom;
            categorie.description = newCategorie.description;
            
            DB.SaveChanges();
        }

        public void deleteCategorie(int id)
        {
            Categorie categorie = DB.Categorie.Single(l => l.id == id);
            DB.Categorie.Remove(categorie);
            DB.SaveChanges();
        }


        public void addPret(Pret newPret)
        {
            DB.Pret.Add(newPret);
            DB.SaveChanges();
        }

        public void updatePret(Pret newPret)
        {
            Pret pret = DB.Pret.Single(l => l.id == newPret.id);

            pret.datedebut = newPret.datedebut;
            pret.datefin = newPret.datefin;
            
            DB.SaveChanges();
        }

        public void deletePret(int id)
        {
            Pret pret = DB.Pret.Single(l => l.id == id);
            DB.Pret.Remove(pret);
            DB.SaveChanges();
        }




        public List<Utilisateur> getUsers()
        {
            return (from user in DB.Utilisateur select user).ToList();
        }

        public List<Categorie> getCategories()
        {
            return (from categorie in DB.Categorie select categorie).ToList();
        }

        public List<Pret> getPrets() 
        {
            return (from pret in DB.Pret select pret).ToList();
        }

        public IEnumerable <livreDeails> getLivres()
        {
            var query = (from a in DB.Auteur
                         join l in DB.Livre on a.id equals l.auteurid 
                         join c in DB.Categorie on l.categorieid equals c.id
                         
                         select new
                         {
                             id = l.id,
                             titre = l.Titre,
                             resume = l.resume,
                             isbn = l.isbn,
                             categorie = c.nom,
                             categorieid = c.id,
                             auteur = a.nom + " "+ a.prenom,
                             auteurid = a.id,
                             image = l.image
                         }).ToList();

            
            return query.Select(livre => new livreDeails
            {
                id = livre.id,
                titre = livre.titre,
                resume = livre.resume,
                image = livre.image,
                isbn = livre.isbn,
                categorie = livre.categorie,
                categorieid = livre.categorieid,
                auteur = livre.auteur,
                auteurid = livre.auteurid
            }); 
        }

        public IEnumerable<livreDeails> getLivresByCategorie(int categorie)
        {
            var query = (from a in DB.Auteur
                         join l in DB.Livre on a.id equals l.auteurid
                         join c in DB.Categorie on l.categorieid equals c.id
                         where l.categorieid == categorie
                         select new
                         {
                             id = l.id,
                             titre = l.Titre,
                             resume = l.resume,
                             isbn = l.isbn,
                             categorie = c.nom,
                             categorieid = c.id,
                             auteur = a.nom + " " + a.prenom,
                             auteurid = a.id,
                             image = l.image
                         }).ToList();


            return query.Select(livre => new livreDeails
            {
                id = livre.id,
                titre = livre.titre,
                resume = livre.resume,
                image = livre.image,
                isbn = livre.isbn,
                categorie = livre.categorie,
                categorieid = livre.categorieid,
                auteur = livre.auteur,
                auteurid = livre.auteurid
            }); 
        }

        public IEnumerable<livreDeails> getLivresByAuteur(int auteur)
        {
            var query = (from a in DB.Auteur
                         join l in DB.Livre on a.id equals l.auteurid
                         join c in DB.Categorie on l.categorieid equals c.id
                         where l.auteurid == auteur
                         select new
                         {
                             id = l.id,
                             titre = l.Titre,
                             resume = l.resume,
                             isbn = l.isbn,
                             categorie = c.nom,
                             categorieid = c.id,
                             auteur = a.nom + " " + a.prenom,
                             auteurid = a.id,
                             image = l.image
                         }).ToList();


            return query.Select(livre => new livreDeails
            {
                id = livre.id,
                titre = livre.titre,
                resume = livre.resume,
                image = livre.image,
                isbn = livre.isbn,
                categorie = livre.categorie,
                categorieid = livre.categorieid,
                auteur = livre.auteur,
                auteurid = livre.auteurid
            });
        }

        public int nomvreLivreCategori(int id)
        {
            List<Livre> query = (from l in DB.Livre where l.categorieid == id select l).ToList<Livre>() ;
            return query.Count;
        }


        public Livre getDetailsLivre(int id)
        {
            //var livre = (from a in DB.Auteur
            //             join l in DB.Livre on a.id equals l.auteurid
            //             join c in DB.Categorie on l.categorieid equals c.id
            //             where l.id== id
            //             select new
            //             {
            //                 id = l.id,
            //                 titre = l.Titre,
            //                 resume = l.resume,
            //                 isbn = l.isbn,
            //                 categorie = c.nom,
            //                 categorieid = c.id,
            //                 auteur = a.nom + " " + a.prenom,
            //                 auteurid = a.id,
            //                 image = l.image
            //             }).Single();

            //livreDeails details = new livreDeails()
            //{
            //    id = livre.id,
            //    titre = livre.titre,
            //    resume = livre.resume,
            //    image = livre.image,
            //    isbn = livre.isbn,
            //    categorie = livre.categorie,
            //    categorieid = livre.categorieid,
            //    auteur = livre.auteur,
            //    auteurid = livre.auteurid
            //};
            return DB.Livre.Single(l => l.id == id);
        }
        public List<Auteur> getAuteurs()
        {
            return (from auteur in DB.Auteur select auteur).ToList();
        }

        public Auteur getAuteur(int id)
        {
            return DB.Auteur.Single(auteur => auteur.id == id);
        }

        public Categorie getCategorie(int id)
        {
            return DB.Categorie.Single(categorie => categorie.id == id);
        }

        public Livre getLivre(int id)
        {
            return DB.Livre.Single(livre => livre.id == id);
        }

        public Pret getPret(int id)
        {
            return DB.Pret.Single(pret => pret.id == id);
        }

        public void updateRoleUser(int id, string role)
        {
            Utilisateur user = DB.Utilisateur.Single(l => l.id == id);
            user.typeUser = role;
            DB.SaveChanges();
        }
    }
}

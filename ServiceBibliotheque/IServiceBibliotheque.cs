using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceBibliotheque
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IServiceBibliotheque" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IServiceBibliotheque
    {
        /***************************************************************************/
        /****************** methodes necessaires pour gerer son compte *****************/
        /***************************************************************************/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        [OperationContract]
        void s_inscrire(Utilisateur user);

        /***************************************************************************/
        /****************** methodes necessaires pour gerer les livres *************/
        /***************************************************************************/
        [OperationContract]
        void addAuteur(Auteur newAuteur);

        [OperationContract]
        void updateAuteur(Auteur newAuteur);

        [OperationContract]
        void deleteAuteur(int id);
        
        /***************************************************************************/
        /****************** methodes necessaires pour gerer les livres *************/
        /***************************************************************************/
        [OperationContract]
        void addLivre(Livre newLivre);

        [OperationContract]
        void updateLivre(Livre newLivre);

        [OperationContract]
        void deleteLivre(int id);

        /***************************************************************************/
        /************** methodes necessaires pour gerer les categories *************/
        /***************************************************************************/

        [OperationContract]
        void addCategorie(Categorie newCategorie);

        [OperationContract]
        void updateCategorie(Categorie newCategorie);

        [OperationContract]
        void deleteCategorie(int id);

        [OperationContract]
        int nomvreLivreCategori(int id);

        /***************************************************************************/
        /*************** methodes necessaires pour gerer les les prets *************/
        /***************************************************************************/
        [OperationContract]
        void addPret(Pret newPret);

        [OperationContract]
        void updatePret(Pret newPret);

        [OperationContract]
        void deletePret(int id);

        /***************************************************************************/
        /*************** methodes necessaires pour les accès en public *************/
        /***************************************************************************/

        //[OperationContract]
        //IEnumerable <Livre> getLivres();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        Utilisateur getUser(string username, string password);

        [OperationContract]
        List<Utilisateur> getUsers();

        [OperationContract]
        List<Categorie> getCategories();

        [OperationContract]
        IEnumerable<livreDeails> getLivres();

        [OperationContract]
        IEnumerable<livreDeails> getLivresByCategorie(int categorie);

        [OperationContract]
        IEnumerable<livreDeails> getLivresByAuteur(int auteur);

        [OperationContract]
        Livre getDetailsLivre(int id);

        [OperationContract]
        List<Pret> getPrets(); 

        [OperationContract]
        List<Auteur> getAuteurs();

        [OperationContract]
        Auteur getAuteur(int id);

        [OperationContract]
        Categorie getCategorie(int id);

        [OperationContract]
        Livre getLivre(int id);

        [OperationContract]
        Pret getPret(int id);

        [OperationContract]
        void updateRoleUser(int id, string role);
    }
}

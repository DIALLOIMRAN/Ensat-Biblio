using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using testBlio.ServiceReferenceBibliotheque;

namespace testBlio
{
    public partial class Form1 : Form
    {
        ServiceBibliothequeClient client = new ServiceBibliothequeClient();

        public Form1()
        {
            InitializeComponent();

            try
            {
                
                Utilisateur user = client.getUser("dialloimran", "lelouma");

                label2.Text = ""+user.email + " " + user.nomUser;

                user.nom = "diallo";
                user.prenom = "lamine";
                user.email = "lamine@gmail.com";
                user.motdepasse = "secret";
                client.s_inscrire(user);
                dataGridView1.DataSource = client.getUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

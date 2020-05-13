using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace CreeCompteNetUser
{
    public partial class Form1 : Form
    {

        //private Compte[] client = new Compte[5000];
        private List<Compte> client = new List<Compte>();
        private int nbCompte;
        private string Charger;
        private string Sauvegarder;
        DataSet DS_List;
        List<string> Ids;
        public Form1()
        {
            InitializeComponent();
            nbCompte = 0;
            DS_List = new DataSet();
            initialisationCompte();            
            Ids = new List<string>();

        }
        private void initialisationCompte()
        {
            DS_List.Tables.Add("table");
            DS_List.Tables["table"].Columns.Add("Nom");
            DS_List.Tables["table"].Columns.Add("Prenom");
            DS_List.Tables["table"].Columns.Add("Date de naissance");
            DS_List.Tables["table"].Columns.Add("Class");
        }
        private void ajoutCompte()
        {
            DataRow ligne = DS_List.Tables["table"].NewRow();
            ligne["Nom"] = client.ElementAt(nbCompte).getnom();
            ligne["Prenom"] = client.ElementAt(nbCompte).getPrenom();
            ligne["Date de naissance"] = client.ElementAt(nbCompte).getDate();
            ligne["Class"] = client.ElementAt(nbCompte).getClass();
            DS_List.Tables["table"].Rows.Add(ligne);
            nbCompte++;
        }
        private void ActualisationDS()
        {
            DV_List.DataSource = DS_List;
            DV_List.DataMember = "table";
            DV_List.Update();
            DV_List.Refresh();
        }
        private void charger()
        {
            using (StreamReader monFichier = new StreamReader(Charger))
            {
                string line;
                char[] n_espace = { ' ' };
                while ((line = monFichier.ReadLine()) != null)
                {
                    string[] lesInfo = line.Split(';');
                    lesInfo[0] = lesInfo[0].Replace(" ", "");
                    lesInfo[1] = lesInfo[1].Replace(" ", "");
                    Compte Client = new Compte(lesInfo[0], lesInfo[1], lesInfo[2], lesInfo[3]);
                    client.Add(Client);
                    ajoutCompte();
                }
                ActualisationDS();
            }
        }
        private void CreeFichierCompte()
        {
            using (StreamWriter monFichier = new StreamWriter(Sauvegarder))
            {
                for (int i = 0; i < nbCompte; i++)
                {
                    monFichier.WriteLine("net user " + Id(i) + " " + MDP_Alea() + " /ADD /FULLNAME:'" + client[i].getPrenom() + " " + client[i].getnom() + "' /DOMAIN");
                }
            }
            MessageBox.Show("Fichier Crée Avec succès .");
        }
        private string Id(int compte)
        {
            
            string id = client[compte].getPrenom().Substring(0, 1) + client[compte].getnom();
            id = id.ToLower();
            string idb = id;
            int doublon = 1;
            bool existe = false;
            do
            {
                existe = false;
                foreach (string login in Ids)
                {
                    if (login == id)
                    existe = true;
                }
                if (existe)
                {
                    id = idb;
                    id += doublon;
                    doublon++;
                }
            } while (existe);
            {
                Ids.Add(id);
            }
            return id;
        }

        private static char[] Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        private static Random alea = new Random();
        private string MDP_Alea()
        {
            char[] str = new char[8];
            for (int i = 0; i < 8; i++)
                str[i] = Alphabet[alea.Next(Alphabet.Length)];
            return new string(str);
        }
        public void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            Charger = openFileDialog.FileName;
            charger();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.ShowDialog();
            Sauvegarder = saveFileDialog.FileName;
            CreeFichierCompte();
        }
    }
}
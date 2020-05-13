using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreeCompteNetUser
{
    class Compte
    {
        public string Nom;
        public string Prenom;
        public string DateNaissance;
        public string Class;

        public Compte(string n, string p, string dt, string S)
        {
            this.Nom = n;
            this.Prenom = p;
            this.DateNaissance = dt;
            this.Class = S;
        }
        public string getnom()
        {
            return this.Nom;
        }
        public void setnom(string n)
        {
            this.Nom = n;
        }
        public string getPrenom()
        {
            return this.Prenom;
        }
        public void setPrenom(string p)
        {
            this.Prenom = p;
        }
        public string getDate()
        {
            return this.DateNaissance;
        }
        public void setDate(string o)
        {
            this.DateNaissance = o;
        }
        public string getClass()
        {
            return this.Class;
        }
        public void setClass(string s)
        {
            this.Class = s;
        }
    }
}

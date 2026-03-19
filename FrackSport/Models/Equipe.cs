using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrackSport.Models
{
    public class Equipe
    {

        private string _nom;
        private string _ville; 
        
        private string _image;

        private string _entraineur;

       

        /// <summary>
        /// recois en paramètre le nom de l'équipe 
        /// </summary>
        public string Nom
        {
            get { return _nom; }
            set {
                if (value == null)
                    throw new ArgumentNullException(nameof(Nom),"Le nom ne doit pas être null");
                _nom = value; }
        }

        /// <summary>
        /// Recoit en paramètre l'image de l'équipe qui est destiné 
        /// </summary>
        public string ImagePath
        {
            get { return _image; }
            set
            {
                string[] extensions = { ".png", ".jpg", ".jpeg" };
                string ext = Path.GetExtension(value)?.ToLower();
                if (string.IsNullOrWhiteSpace(value) || !extensions.Contains(ext))
                    throw new ArgumentException(nameof(ImagePath), "Le fichier image ne doit pas être invalide ou inexistnt ");
                _image = value;

            }
        }

        public string Ville
        {
            get
            {
                return _ville;

            }
            set
            {
                _ville = value; 
            }
        }

        public string Entraineur
        {
            get { return _entraineur; }
            set { _entraineur = value; }
        }


        /// <summary>
        /// Constructeur de la classe équipe 
        /// </summary>
        /// <param name="pNom"></param>
        /// <param name="pImage"></param>
        public Equipe(string pNom , string pImage , string pVille , string pEntraineur)
        {
            Nom = pNom;
            ImagePath = pImage;
            Ville = pVille;
            Entraineur = pVille; 
        }

    }
}

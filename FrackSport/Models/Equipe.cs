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
                if (value.Length == 0 || !extensions.Contains(ext))
                    throw new ArgumentException(nameof(ImagePath), "Le fichier image ne doit pas être invalide ou inexistnt ");
                _image = value;

            }
        }



        /// <summary>
        /// Constructeur de la classe équipe 
        /// </summary>
        /// <param name="pNom"></param>
        /// <param name="pImage"></param>
        public Equipe(string pNom , string pImage)
        {
            Nom = pNom;
            ImagePath = pImage;
        }

    }
}

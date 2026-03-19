using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrackSport.Models
{
    public class Ligue
    {
		private int _id;

        /// <summary>
        /// Nom de la ligue 
        /// </summary>

		private string _nom;

		private string _pays;

		private string _organisation;

        private int _nbEquipe;


        private string _imagePath;

        /// <summary>
        /// Liste des équipes qui composent la ligue 
        /// </summary>

        private List<Equipe> _equipes;

        public List<Equipe> Equipes
        {
            get { return _equipes; }
            set { _equipes = value; }
        }



        public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        public string Pays
        {
            get { return _pays; }
            set { _pays = value; }
        }

        public string Organisation
        {
            get { return _organisation; }
            set { _organisation = value; }
        }

        public int NbEquipe
        {
            get { return _nbEquipe; }
            set { _nbEquipe = value; }
        }
        /// <summary>
        /// Iamge attribué à la ligue 
        /// </summary>
        public string ImagePath
        {
            get { return _imagePath; }
            set {
                string[] extensions = { ".png", ".jpg", ".jpeg" };
                string ext = Path.GetExtension(value)?.ToLower();
                if (string.IsNullOrWhiteSpace(value) || !extensions.Contains(ext))
                    throw new ArgumentException(nameof(ImagePath), "Le fichier image ne doit pas être invalide ou inexistnt ");
                _imagePath = value;

            }
        }
        public Ligue(int pId, string pNom, string pPays, string pOrganisation, int pNbEquipe, string pImage, List<Equipe> pEquipes)

        {
            Id = pId;
            Nom = pNom;
            Pays = pPays;
            Organisation = pOrganisation;
            NbEquipe = pNbEquipe;
            ImagePath = pImage;
            Equipes = pEquipes;
        }
    }
}

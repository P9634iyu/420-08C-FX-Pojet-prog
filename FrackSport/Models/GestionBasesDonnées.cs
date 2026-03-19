using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Xceed.Wpf.Toolkit;
using Microsoft.Extensions.Logging.Abstractions;

namespace FrackSport.Models
{
    public  class GestionBasesDonnées
    {
     
        private const string APPSETTINGS_FILE = "appsettings.json";
        private const string CONNECTION_STRING = "DefaultConnection";
        private const string IMAGE_PATH = "Images:Path";

        private static readonly IConfiguration _config = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory).AddJsonFile(APPSETTINGS_FILE, optional: false, reloadOnChange: true).Build();
       
        private static readonly string _imageDirectoryPath = _config.GetSection(IMAGE_PATH).Value ?? "images";

        public static string ObtenirCheminDossierImages()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Images");
        }

        private static SqliteConnection CreerConnection()
        {
            string connectionString = _config.GetConnectionString(CONNECTION_STRING); 

            if(connectionString == null)
            {
                throw new InvalidOperationException("Chaîne de connexion 'DefaultConnection' non trouvée."); 

            }
            if (!Directory.Exists(_imageDirectoryPath))
            {
                Directory.CreateDirectory(_imageDirectoryPath); 
            }
            return new  SqliteConnection(connectionString); 
        }
        private static void FermerConnection(SqliteConnection cn)
        {
            if (cn != null && cn.State == ConnectionState.Open)
            {
                cn.Close();
                cn.Dispose();
            }
        }


        public static List<Ligue> ObtenirListeLigue(string nom = "") 
        {
            List<Ligue> ligues = new List<Ligue>();
            SqliteConnection cn = CreerConnection();
            SqliteCommand cmd = null; 
            SqliteDataReader dr = null; 
            try
            {
                cn.Open();
                cmd = new SqliteCommand();
                cmd.Connection = cn;
                string requete = "SELECT Id, Nom, PaysRegion, Organisation, NombreEquipes, Logo FROM ligue ";



                requete += "ORDER BY Nom ASC ";
                cmd.CommandText = requete;
                dr = cmd.ExecuteReader(); 

                while (dr.Read())
                {
                    Ligue ligue = new Ligue(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetString(3) ,dr.GetInt32(4), dr.GetString(5) ,
                        new List<Equipe>());
                    ligues.Add(ligue);            
                 }

               

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erreur lors de la récupération des informations dans les ligues " , ex);
            }
            finally
            {
                if (dr !=  null)
                    dr.Close();

                if (cmd !=  null)
                    cmd.Dispose(); 

                FermerConnection(cn);
            }
            return ligues; 
        }


        public static List<Equipe> ObtenirEquipesParLigue(int ligueId)
        {
            List<Equipe> equipes = new List<Equipe>();
            SqliteConnection cn = CreerConnection();
            SqliteCommand cmd = null;
            SqliteDataReader dr = null;
            try
            {
                cn.Open(); // ✅ Cette ligne est obligatoire — vérifiez qu'elle est là !
                cmd = new SqliteCommand();
                cmd.Connection = cn;
                cmd.CommandText = "SELECT Nom, Logo, Ville, Entraineur FROM Equipe WHERE ligue_id = @ligueId ORDER BY Nom ASC";
                cmd.Parameters.AddWithValue("@ligueId", ligueId);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Equipe equipe = new Equipe(
                        dr.GetString(0),
                        dr.GetString(1),
                        dr.IsDBNull(2) ? "" : dr.GetString(2),
                        dr.IsDBNull(3) ? "" : dr.GetString(3)
                    );
                    equipes.Add(equipe);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erreur lors de la récupération des équipes", ex);
            }
            finally
            {
                if (dr != null) 
                    dr.Close();
                if (cmd != null) 
                    cmd.Dispose();
                FermerConnection(cn);
            }
            return equipes;
        }
    }
}

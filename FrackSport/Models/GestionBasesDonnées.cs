using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Xceed.Wpf.Toolkit;

namespace FrackSport.Models
{
    public  class GestionBasesDonnées
    {
        private const string APPSETTINGS_FILE = "appsettings.json";
        private const string CONNECTION_STRING = "DefaultConnection";
        private const string IMAGE_PATH = "Images:Path";

        private static readonly IConfiguration _config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(APPSETTINGS_FILE, optional: true, reloadOnChange: true).Build();
       
        private static readonly string _imageDirectoryPath = _config.GetSection(IMAGE_PATH).Value ?? "images";

        public static string ObtenirCheminDossierImages()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");
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


        public void CreerLigue() 
        {
           SqliteConnection cn = CreerConnection();

            try
            {
                cn.Open();

                SqliteCommand cmd = cn.CreateCommand();
                cmd.CommandText =
                    "INSERT INTO Ligue (Nom, Pays, Organisation, NombreEquipes, Logo VALUES" +
                  "('Premier League , Angleterre , FA , 20), " +
                  "('La Liga', 'Espagne', 'RFEF', 20) , " +
                  "('Ligue des champions', 'Europe','UEFA',36)"+
                   "('Serie A',Italie','FIGC','20')";

                cmd.ExecuteNonQuery(); 


            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erreur lors de la récupération des informations dans les ligues ");
            }
            finally
            {
              

                FermerConnection(cn);
            }

        }

    }
}

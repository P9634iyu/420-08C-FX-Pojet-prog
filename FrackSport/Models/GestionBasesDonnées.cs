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
using System.Windows.Navigation;

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

           
            

            if (connectionString == null)
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
                cn.Open();
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

        public static List<Match> ObtenirMatchsParEquipe(int equipeId)
        {
            List<Match> matchs = new List<Match>();
            SqliteConnection cn = CreerConnection();
            SqliteCommand cmd = null;
            SqliteDataReader dr = null;
            try
            {
                cn.Open();
                cmd = new SqliteCommand();
                cmd.Connection = cn;
               
                cmd.CommandText = @"
            SELECT 
    m.id, ed.Nom AS DomicileNom, ee.Nom AS ExterieurNom,  m.date_match, m.score_domicile, m.score_exterieur,m.statut,
    sd.possession AS possession_domicile, sd.tirs_total AS tirs_domicile, sd.tirs_cadres AS tirs_cadres_domicile, sd.corners AS corners_domicile, sd.fautes AS fautes_domicile,
    sd.cartons_jaunes AS jaunes_domicile, sd.cartons_rouges AS rouges_domicile, se.possession AS possession_exterieur,se.tirs_total AS tirs_exterieur,
    se.tirs_cadres AS tirs_cadres_exterieur,se.corners AS corners_exterieur, se.fautes AS fautes_exterieur, se.cartons_jaunes AS jaunes_exterieur, se.cartons_rouges AS rouges_exterieur
    FROM Match m  JOIN Equipe ed ON m.equipe_domicile_id = ed.id JOIN Equipe ee ON m.equipe_exterieur_id = ee.id
    LEFT JOIN StatistiqueMatch sd ON sd.match_id = m.id AND sd.equipe_id = ed.id
     LEFT JOIN StatistiqueMatch se  ON se.match_id = m.id AND se.equipe_id = ee.id";
                
                cmd.Parameters.AddWithValue("@equipeId", equipeId);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Match match = new Match(dr.GetInt32(0), dr.GetString(1),dr.GetString(2),DateTime.Parse(dr.GetString(3)),
                        dr.IsDBNull(4) ? (int?)null : dr.GetInt32(4), dr.IsDBNull(5) ? (int?)null : dr.GetInt32(5),
                        dr.GetString(6) );
                    matchs.Add(match);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erreur lors de la récupération des matchs", ex);
            }
            finally
            {
                if (dr != null) dr.Close();
                if (cmd != null) cmd.Dispose();
                FermerConnection(cn);
            }

            return matchs;
        }

        public static List<statistique> ObtenirStatistiqueParMatch (int matchId)
        {
            List<statistique> stats = new List<statistique>();
            SqliteConnection cn = CreerConnection();
            SqliteCommand cmd = null;
            SqliteDataReader dr = null;
            try
            {
                cn.Open();
                cmd = new SqliteCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"    SELECT s.id, s.match_id, e.Nom, s.possession, s.tirs_total, s.tirs_cadres," +
                    " s.corners, s.fautes, s.cartons_jaunes, s.cartons_rouges FROM StatistiqueMatch s JOIN Equipe e ON s.equipe_id = e.id WHERE s.match_id = @matchId";

                cmd.Parameters.AddWithValue("@matchId", matchId);

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    statistique stat = new statistique(
                        dr.GetInt32(0), dr.GetInt32(1), dr.GetString(2), dr.GetInt32(3), dr.GetInt32(4), dr.GetInt32(5),
                        dr.GetInt32(6), dr.GetInt32(7), dr.GetInt32(8), dr.GetInt32(9)) ;
                    stats.Add(stat);
                }
              
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erreur lors de la récupération des statistiques ", ex); 
            }
            finally
            {
                if(dr != null)
                {
                    dr.Close() ;
                }
                if(cmd != null)
                {
                    cmd.Dispose();

                }
                FermerConnection(cn);
            }
            return stats; 
        }
    }
}

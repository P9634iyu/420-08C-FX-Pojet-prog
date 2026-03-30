using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrackSport.Models
{
    public class statistique
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public string NomEquipe { get; set; }
        public int Possession { get; set; }
        public int TirsTotal { get; set; }
        public int TirsCadres { get; set; }
        public int Corners { get; set; }
        public int Fautes { get; set; }
        public int CartonRouges { get; set; }
        public int CartonJaunes { get; set; }


        public statistique(int pId , int pMatchId , string pNomEquipe , int pPossession , int pTirsTotal , int pTirsCadres , int pCorners , int pFautes, int pCartonJaunes, int pCartonRouges )
        {
            Id = pId;
            MatchId = pMatchId;
            NomEquipe = pNomEquipe;
            Possession = pPossession; 
            TirsTotal = pTirsTotal;
            TirsCadres = pTirsCadres;
            Corners = pCorners;
            Fautes = pFautes;
            CartonJaunes = pCartonJaunes;
            CartonRouges = pCartonRouges;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrackSport.Models
{
    public class Match
    {
        public int Id { get; set; }
        public string EquipeDomicile { get; set; }
        public string EquipeExterieur { get; set; }
        public DateTime DateMatch { get; set; }
        public int? ScoreDomicile { get; set; }
        public int? ScoreExterieur { get; set; }
        public string Statut { get; set; }

        public Match(int pId, string pEquipeDomicile, string pEquipeExterieur, DateTime pDateMatch, int? pScoreDomicile, int? pScoreExterieur, string pStatut)
        {
            Id = pId;
            EquipeDomicile = pEquipeDomicile;
            EquipeExterieur = pEquipeExterieur;
            DateMatch = pDateMatch;
            ScoreDomicile = pScoreDomicile;
            ScoreExterieur = pScoreExterieur;
            Statut = pStatut;
        }

        // Indique si le match est passé ou à venir
        public bool EstPasse => DateTime.Now > DateMatch;
    }

}

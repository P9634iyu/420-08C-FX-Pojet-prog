using FrackSport.Models;
using FrackSport.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace FrackSport
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Ligue> _ligues = new List<Ligue>();

        public List<Ligue> Ligues
        {
            get { return _ligues; }
            set { _ligues = value; }
        }



        public MainWindow()
        {
            InitializeComponent();
            ChargerDonnees(); 
        }


        /// <summary>
        /// Permet de charger les informations de la ligue  
        /// </summary>
        private void ChargerDonnees()
        
            {

            ///Ligue 1 
            Ligue ligue1 = new Ligue("Ligue 1", "Ressources/images/ligue1.png" , new List<Equipe>());
            ligue1.Equipes.Add(new Equipe("Paris Saint-Germain", "Ressources/images/PSG.png"));
            ligue1.Equipes.Add(new Equipe("RC Lens", "Ressources/images/lens.png"));
            ligue1.Equipes.Add(new Equipe("Olympique Lyonnais", "Ressources/images/Lyon.png"));
            ligue1.Equipes.Add(new Equipe("Olympique Marseille", "Ressources/images/Marseille.png"));
            ligue1.Equipes.Add(new Equipe("Lille OSC", "Ressources/images/Lille.png"));
            ligue1.Equipes.Add(new Equipe("Rennes", "Ressources/images/Rennes.png"));
            ligue1.Equipes.Add(new Equipe("Strasbourg", "Ressources/images/Strasbourg.png"));
            ligue1.Equipes.Add(new Equipe("AS Monaco", "Ressources/images/As-monaco.png"));
            ligue1.Equipes.Add(new Equipe("FC Lorient", "Ressources/images/Lorient.png"));
            ligue1.Equipes.Add(new Equipe("Toulouse FC", "Ressources/images/Toulouse.png"));
            ligue1.Equipes.Add(new Equipe("Brestois FC", "Ressources/images/Brestois.png"));
            ligue1.Equipes.Add(new Equipe("Angers SCO", "Ressources/images/Angers.png"));
            ligue1.Equipes.Add(new Equipe("Havre AC", "Ressources/images/Havre-ac.png"));
            ligue1.Equipes.Add(new Equipe("OGC Nice", "Ressources/images/Nice.png"));
            ligue1.Equipes.Add(new Equipe("Paris FC", "Ressources/images/Paris-fc.png"));
            ligue1.Equipes.Add(new Equipe("AJ Auxerre", "Ressources/images/Auxerre.png"));
            ligue1.Equipes.Add(new Equipe("FC Nantes", "Ressources/images/Nantes.png"));
            ligue1.Equipes.Add(new Equipe("FC Metz", "Ressources/images/Fc-metz.png"));

            //Pre

            Ligue premierLeague = new Ligue("Premier League", "Ressources/images/premierleague.png" ,new List<Equipe>());
            premierLeague.Equipes.Add(new Equipe("Manchester City", "Ressources/images/Manchester-city.png"));
            premierLeague.Equipes.Add(new Equipe("Arsenal", "Ressources/images/Arsenal.png"));
            premierLeague.Equipes.Add(new Equipe("Ashton Villa", "Ressources/images/Ashton-villa.png"));
            premierLeague.Equipes.Add(new Equipe("Manchester United", "Ressources/images/Manchester-united.png"));
            premierLeague.Equipes.Add(new Equipe("Chelsea", "Ressources/images/Chelsea.png"));
            premierLeague.Equipes.Add(new Equipe("Liverpool", "Ressources/images/Liverpool.png"));
            premierLeague.Equipes.Add(new Equipe("Brentford", "Ressources/images/Brentford.png"));
            premierLeague.Equipes.Add(new Equipe("AFC Bournemouth", "Ressources/images/Bournemouth.png"));
            premierLeague.Equipes.Add(new Equipe("Everton", "Ressources/images/Everton.png"));
            premierLeague.Equipes.Add(new Equipe("Fulham FC", "Ressources/images/Fulham.png"));
            premierLeague.Equipes.Add(new Equipe("Newcastle United FC", "Ressources/images/Newcastle.png"));
            premierLeague.Equipes.Add(new Equipe("Sunderland AFC", "Ressources/images/Sunderland.png"));
            premierLeague.Equipes.Add(new Equipe("Crystal Palace FC", "Ressources/images/Crystal-palace.png"));
            premierLeague.Equipes.Add(new Equipe("Leeds United", "Ressources/images/Leeds-united.png"));
            premierLeague.Equipes.Add(new Equipe("Tottenham", "Ressources/images/Tottenham.png"));
            premierLeague.Equipes.Add(new Equipe("Nottingham", "Ressources/images/Nottingham-forest.png"));
            premierLeague.Equipes.Add(new Equipe("West Ham United", "Ressources/images/West-ham.png"));
            premierLeague.Equipes.Add(new Equipe("Burnley", "Ressources/images/West-ham.png"));
            premierLeague.Equipes.Add(new Equipe("Wolverhampton", "Ressources/images/Wolves.png"));



            Ligue LaLiGa = new Ligue("LaLiGa", "Ressources/images/LaLiga.png", new List<Equipe>());
            premierLeague.Equipes.Add(new Equipe("BC Barcelone", "Ressources/images/Barcelona.png"));
            premierLeague.Equipes.Add(new Equipe("Real Madrid", "Ressources/images/Real-madrid.png"));
            premierLeague.Equipes.Add(new Equipe("Villarreal CF", "Ressources/images/Spain_villarreal.png"));
            premierLeague.Equipes.Add(new Equipe("Atlético Madrid", "Ressources/images/Atletico-madrid.png"));
            premierLeague.Equipes.Add(new Equipe("Real Betis", "Ressources/images/Real-betis.png"));
            premierLeague.Equipes.Add(new Equipe("Celta Vigo", "Ressources/images/Celta.png"));
            premierLeague.Equipes.Add(new Equipe("Athletic BIlbao", "Ressources/images/Athletic-club.png"));
            premierLeague.Equipes.Add(new Equipe("CA Osasuna", "Ressources/images/Osasuna.png"));
            premierLeague.Equipes.Add(new Equipe("Sociedad", "Ressources/images/Real-sociedad.png"));
            premierLeague.Equipes.Add(new Equipe("FC Séville", "Ressources/images/Sevilla.png"));
            premierLeague.Equipes.Add(new Equipe("Getafe CF", "Ressources/images/Getafe.png"));
            premierLeague.Equipes.Add(new Equipe("Deportivo Alaves", "Ressources/images/Deportivo.png"));
            premierLeague.Equipes.Add(new Equipe("Vallecano", "Ressources/images/Rayo-vallecano.png"));
            premierLeague.Equipes.Add(new Equipe("Valence CF", "Ressources/images/Rayo-vallecano.png"));
            premierLeague.Equipes.Add(new Equipe("Elche", "Ressources/images/Elche.png"));
            premierLeague.Equipes.Add(new Equipe("Majorque", "Ressources/images/Mallorca.png"));
            premierLeague.Equipes.Add(new Equipe("Levante UD", "Ressources/images/Levante.png"));
            premierLeague.Equipes.Add(new Equipe("Oviedo", "Ressources/images/Oviedo.png"));


            Ligues.Add(ligue1);
            Ligues.Add(premierLeague);
            Ligues.Add(LaLiGa);

            LiguesControl.ItemsSource = Ligues;
        }




        private void Ligue_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.Controls.Image image = sender as System.Windows.Controls.Image;
            Ligue ligue = image.DataContext as Ligue;

            if (ligue != null)
            {
                SelectionEquipe window = new SelectionEquipe(ligue.Equipes);
                window.Show();
            }
        }
    }
}

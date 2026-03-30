using FrackSport.Models;
using FrackSport.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FrackSport
{
    /// <summary>
    /// Logique d'interaction pour CalendrierEquipe.xaml
    /// </summary>
    public partial class CalendrierEquipe : Window
    {
        private Equipe _equipe;
        private int _equipeId;
        private List<Match> _tousLesMatchs;
        private bool _afficherPassés = false;

        public CalendrierEquipe(Equipe equipe, int equipeId)
        {
            InitializeComponent();
            _equipe = equipe;
            _equipeId = equipeId;
            txbTitreEquipe.Text = equipe.Nom;
            _tousLesMatchs = GestionBasesDonnées.ObtenirMatchsParEquipe(equipeId);
            AfficherMatchs(false); // À venir par défaut
        }

        private void AfficherMatchs(bool passés)
        {
            spMatchs.Children.Clear();
            _afficherPassés = passés;

            List<Match> liste = passés
                ? _tousLesMatchs.Where(m => m.EstPasse).OrderByDescending(m => m.DateMatch).ToList()
                : _tousLesMatchs.Where(m => !m.EstPasse).OrderBy(m => m.DateMatch).ToList();

            if (liste.Count == 0)
            {
                spMatchs.Children.Add(new TextBlock
                {
                    Text = passés ? "Aucun match passé." : "Aucun match à venir.",
                    Foreground = Brushes.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 20, 0, 0),
                    FontSize = 14
                });
                return;
            }

            foreach (Match m in liste)
            {
                Border carte = new Border
                {
                    Background = m.EstPasse ? new SolidColorBrush(Color.FromRgb(230, 230, 230))
                                            : Brushes.White,
                    BorderBrush = Brushes.Gray,
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(8),
                    Margin = new Thickness(4, 4, 4, 4),
                    Padding = new Thickness(12, 8, 12, 8)
                };

                Grid grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                // Équipe domicile
                TextBlock domicile = new TextBlock
                {
                    Text = m.EquipeDomicile,
                    FontWeight = m.EquipeDomicile == _equipe.Nom ? FontWeights.Bold : FontWeights.Normal,
                    TextAlignment = TextAlignment.Right,
                    TextWrapping = TextWrapping.Wrap,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetColumn(domicile, 0);

                // Score ou VS
                string scoreTexte = m.EstPasse && m.ScoreDomicile.HasValue
                    ? $"{m.ScoreDomicile} - {m.ScoreExterieur}"
                    : m.DateMatch.ToString("dd MMM yyyy\nHH:mm");
                TextBlock score = new TextBlock
                {
                    Text = scoreTexte,
                    FontWeight = FontWeights.Bold,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(12, 0, 12, 0),
                    Foreground = m.EstPasse ? Brushes.DarkGreen : Brushes.DarkBlue
                };
                Grid.SetColumn(score, 1);

                // Équipe extérieur
                TextBlock exterieur = new TextBlock
                {
                    Text = m.EquipeExterieur,
                    FontWeight = m.EquipeExterieur == _equipe.Nom ? FontWeights.Bold : FontWeights.Normal,
                    TextWrapping = TextWrapping.Wrap,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetColumn(exterieur, 2);

                // Statut
                TextBlock statut = new TextBlock
                {
                    Text = m.Statut,
                    FontSize = 10,
                    Foreground = Brushes.Gray,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(8, 0, 0, 0)
                };
                Grid.SetColumn(statut, 3);

                grid.Children.Add(domicile);
                grid.Children.Add(score);
                grid.Children.Add(exterieur);
                grid.Children.Add(statut);
                carte.Child = grid;
                spMatchs.Children.Add(carte);

                Match matchCourant = m;
                carte.MouseLeftButtonUp += (object sender, MouseButtonEventArgs args) =>
                {
                  statistiquesMatch  fenStat = new statistiquesMatch(matchCourant);
                    fenStat.ShowDialog();
                };
                carte.Cursor = Cursors.Hand;
            }
        }

        private void btnPassés_Click(object sender, RoutedEventArgs e) => AfficherMatchs(true);
        private void btnAvenir_Click(object sender, RoutedEventArgs e) => AfficherMatchs(false);
        private void btnFermer_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}

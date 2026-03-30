using FrackSport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Logique d'interaction pour statistiquesMatch.xaml
    /// </summary>
    public partial class statistiquesMatch : Window
    {
        public statistiquesMatch(FrackSport.Models.Match match)
        {
            InitializeComponent();
            txbTitreMatch.Text = match.EquipeDomicile + " vs " + match.EquipeExterieur;
            txbDateMatch.Text = match.DateMatch.ToString("dd MMMM yyyy — HH:mm");
            AfficherStats(match);
        }
        private void AfficherStats(FrackSport.Models.Match matchId)
        {
            spStats.Children.Clear();
            List<statistique> stats = GestionBasesDonnées.ObtenirStatistiqueParMatch(matchId.Id);

            if (stats.Count < 2)
            {
                spStats.Children.Add(new TextBlock
                {
                    Text = "Aucune statistique disponible pour ce match.",
                    Foreground = Brushes.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 30, 0, 0),
                    FontSize = 14
                });
                return;
            }

            statistique statDom = stats[0];
            statistique statExt = stats[1];

            // En-tête équipes
            Grid entete = CreerLigneEntete(statDom.NomEquipe, statExt.NomEquipe);
            spStats.Children.Add(entete);

            // Séparateur
            spStats.Children.Add(new Separator { Margin = new Thickness(0, 6, 0, 6) });

            // Lignes de stats
            AjouterLigneStat("Possession (%)", statDom.Possession, statExt.Possession, 100);
            AjouterLigneStat("Tirs total", statDom.TirsTotal, statExt.TirsTotal, 30);
            AjouterLigneStat("Tirs cadrés", statDom.TirsCadres, statExt.TirsCadres, 20);
            AjouterLigneStat("Corners", statDom.Corners, statExt.Corners, 15);
            AjouterLigneStat("Fautes", statDom.Fautes, statExt.Fautes, 25);
            AjouterLigneCarton("🟡 Cartons jaunes", statDom.CartonJaunes, statExt.CartonJaunes);
            AjouterLigneCarton("🔴 Cartons rouges", statDom.CartonRouges, statExt.CartonRouges);
        }

        private Grid CreerLigneEntete(string nomDom, string nomExt)
        {
            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            TextBlock dom = new TextBlock
            {
                Text = nomDom,
                FontWeight = FontWeights.Bold,
                FontSize = 13,
                Foreground = Brushes.White,
                TextAlignment = TextAlignment.Center,
                TextWrapping = TextWrapping.Wrap
            };
            TextBlock milieu = new TextBlock
            {
                Text = "VS",
                FontWeight = FontWeights.Bold,
                FontSize = 13,
                Foreground = Brushes.LightGray,
                TextAlignment = TextAlignment.Center
            };
            TextBlock ext = new TextBlock
            {
                Text = nomExt,
                FontWeight = FontWeights.Bold,
                FontSize = 13,
                Foreground = Brushes.White,
                TextAlignment = TextAlignment.Center,
                TextWrapping = TextWrapping.Wrap
            };

            Grid.SetColumn(dom, 0);
            Grid.SetColumn(milieu, 1);
            Grid.SetColumn(ext, 2);
            grid.Children.Add(dom);
            grid.Children.Add(milieu);
            grid.Children.Add(ext);
            return grid;
        }

        private void AjouterLigneStat(string label, int valDom, int valExt, int max)
        {
            Border carte = new Border
            {
                Background = Brushes.White,
                CornerRadius = new CornerRadius(8),
                Margin = new Thickness(0, 4, 0, 4),
                Padding = new Thickness(10, 8, 10, 8)
            };

            StackPanel sp = new StackPanel();

            // Label centré
            sp.Children.Add(new TextBlock
            {
                Text = label,
                FontSize = 11,
                Foreground = Brushes.Gray,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 4)
            });

            // Valeurs + barres
            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(40) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(40) });

            // Valeur domicile
            grid.Children.Add(new TextBlock
            {
                Text = valDom.ToString(),
                FontWeight = FontWeights.Bold,
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            });

            // Barre domicile
            double pctDom = max > 0 ? (double)valDom / max : 0;
            Border barreDom = new Border
            {
                Height = 10,
                CornerRadius = new CornerRadius(4),
                Background = new SolidColorBrush(Color.FromRgb(30, 100, 200)),
                HorizontalAlignment = HorizontalAlignment.Right,
                Width = pctDom * 150,
                Margin = new Thickness(4, 0, 4, 0)
            };
            Grid.SetColumn(barreDom, 1);

            // Barre extérieur
            double pctExt = max > 0 ? (double)valExt / max : 0;
            Border barreExt = new Border
            {
                Height = 10,
                CornerRadius = new CornerRadius(4),
                Background = new SolidColorBrush(Color.FromRgb(200, 50, 50)),
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = pctExt * 150,
                Margin = new Thickness(4, 0, 4, 0)
            };
            Grid.SetColumn(barreExt, 2);

            // Valeur extérieur
            TextBlock txbExt = new TextBlock
            {
                Text = valExt.ToString(),
                FontWeight = FontWeights.Bold,
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetColumn(txbExt, 3);

            grid.Children.Add(barreDom);
            grid.Children.Add(barreExt);
            grid.Children.Add(txbExt);

            sp.Children.Add(grid);
            carte.Child = sp;
            spStats.Children.Add(carte);
        }

        private void AjouterLigneCarton(string label, int valDom, int valExt)
        {
            Border carte = new Border
            {
                Background = Brushes.White,
                CornerRadius = new CornerRadius(8),
                Margin = new Thickness(0, 4, 0, 4),
                Padding = new Thickness(10, 8, 10, 8)
            };

            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            TextBlock dom = new TextBlock
            {
                Text = valDom.ToString(),
                FontWeight = FontWeights.Bold,
                TextAlignment = TextAlignment.Center
            };
            TextBlock mil = new TextBlock
            {
                Text = label,
                FontSize = 11,
                Foreground = Brushes.Gray,
                TextAlignment = TextAlignment.Center
            };
            TextBlock ext = new TextBlock
            {
                Text = valExt.ToString(),
                FontWeight = FontWeights.Bold,
                TextAlignment = TextAlignment.Center
            };

            Grid.SetColumn(dom, 0);
            Grid.SetColumn(mil, 1);
            Grid.SetColumn(ext, 2);
            grid.Children.Add(dom);
            grid.Children.Add(mil);
            grid.Children.Add(ext);

            carte.Child = grid;
            spStats.Children.Add(carte);
        }


        private void btnFermer_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}


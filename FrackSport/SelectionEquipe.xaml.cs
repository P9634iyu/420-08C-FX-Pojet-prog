using FrackSport.Models;
using FrackSport.Views;
using System;
using System.IO;
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

namespace FrackSport.Views
{
    /// <summary>
    /// Logique d'interaction pour SelectionEquipe.xaml
    /// </summary>
    public partial class SelectionEquipe : Window
    {
        private Ligue _ligue;

        public Ligue Ligue
        {
            get
            {
                return _ligue; 
            }
            set
            {
                _ligue = value;
            }
        }

        public SelectionEquipe(Ligue ligue)
        {
            InitializeComponent();
            _ligue = ligue;
            txbTitreLigue.Text = "⚽ " + ligue.Nom;
            AfficherEquipes();
        }

        private void AfficherEquipes()
        {
            wpEquipes.Children.Clear();
            List<Equipe> equipes = GestionBasesDonnées.ObtenirEquipesParLigue(Ligue.Id);

            foreach (Equipe e in equipes)
            {
                Border border = new Border
                {
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(5),
                    Margin = new Thickness(8),
                    Padding = new Thickness(5),
                    Width = 130,
                    Height = 160,
                    Background = Brushes.White,
                    Cursor = Cursors.Hand
                };

                StackPanel panel = new StackPanel
                {
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                border.Child = panel;

                // Image de l'équipe
                string cheminImage = Path.Combine(
                    GestionBasesDonnées.ObtenirCheminDossierImages(),
                    e.ImagePath ?? "");
                cheminImage = Path.GetFullPath(cheminImage);

                if (!string.IsNullOrWhiteSpace(e.ImagePath) && File.Exists(cheminImage))
                {
                    BitmapImage bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.UriSource = new Uri(cheminImage, UriKind.Absolute);
                    bmp.CacheOption = BitmapCacheOption.OnLoad;
                    bmp.EndInit();
                    Image img = new Image
                    {
                        Source = bmp,
                        Height = 80,
                        Stretch = Stretch.Uniform,
                        Margin = new Thickness(0, 5, 0, 5)
                    };
                    panel.Children.Add(img);
                }

                // Nom de l'équipe
                TextBlock nomEquipe = new TextBlock
                {
                    Text = e.Nom,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontWeight = FontWeights.Bold,
                    FontSize = 11,
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Center,
                    Margin = new Thickness(3)
                };
                panel.Children.Add(nomEquipe);

                // Ville
                TextBlock ville = new TextBlock
                {
                    Text = e.Ville,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontSize = 10,
                    Foreground = Brushes.Gray,
                    TextAlignment = TextAlignment.Center
                };
                panel.Children.Add(ville);

                wpEquipes.Children.Add(border);
            }
        }

        private void btnFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

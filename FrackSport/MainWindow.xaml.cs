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
            AfficherLesLigues(); 
        }


        /// <summary>
        /// Permet de charger les informations de la ligue  
        /// </summary>
     
        private void AfficherLesLigues (string nom = "")
        {
            wpLigues.Children.Clear();
            List<Ligue> lst = GestionBasesDonnées.ObtenirListeLigue(nom);
            foreach(Ligue l in lst)
            {
                Border border = new Border
                {
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(5),
                    Margin = new Thickness(8),
                    Padding = new Thickness(5),
                    Width = 160,
                    Height = 240,
                    Background = Brushes.White,
                    Cursor = Cursors.Hand
                };

                StackPanel panel = new StackPanel { HorizontalAlignment = HorizontalAlignment.Center };
                border.Child = panel; 
                // ImagePath
            
                string cheminImage = Path.Combine(GestionBasesDonnées.ObtenirCheminDossierImages(), l.ImagePath ?? "");
                cheminImage = Path.GetFullPath(cheminImage); 

              
                if(!string.IsNullOrWhiteSpace(l.ImagePath)&& File.Exists(cheminImage))
                {

                    BitmapImage bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.UriSource = new Uri(cheminImage, UriKind.Absolute);
                    bmp.CacheOption = BitmapCacheOption.OnLoad;
                    bmp.EndInit();
                    Image img = new Image
                    {
                        Source = bmp,
                        Height = 140,
                        Stretch = Stretch.UniformToFill
                    };
                    panel.Children.Add(img);
                }

                // Permet d'afficher un nom de la ligue 

                TextBlock nomlg = new TextBlock
                {
                    Text = l.Nom,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(5),
                    TextWrapping = TextWrapping.Wrap
                };
                panel.Children.Add(nomlg);

                Button btnEquipe = new Button { Background = Brushes.White, BorderThickness = new Thickness(0), Padding = new Thickness(2) };
                Image imageEdit = new Image
                {

                };
                btnEquipe.Content = imageEdit;
                btnEquipe.Click += (object sender, RoutedEventArgs args) =>
                {
                    SelectionEquipe slcEquipe = new SelectionEquipe();
                    bool? result = slcEquipe.ShowDialog();
                    if (result.HasValue && result.Value)
                    {

                    }
                };
              
                panel.Children.Add(btnEquipe);
                wpLigues.Children.Add(border); 
            }
        }
        
    }
}

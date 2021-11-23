﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sokoban
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Jeu jeu;

        public MainWindow()
        {
            InitializeComponent();
            jeu = new Jeu();

            this.KeyDown += MainWindow_KeyDown;

            Dessiner();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Right) || e.Key.Equals(Key.Left) || e.Key.Equals(Key.Down)|| e.Key.Equals(Key.Up))
            {
                jeu.ToucheAppuyée(e.Key);
                Redessiner();
                if (jeu.fini())
                {
                    MessageBoxResult msg = MessageBox.Show("Bravo, vous avez gagné en " + jeu.NBDeplacement + " mouvements. ", " recommencer ? ", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    if (msg == MessageBoxResult.Yes)
                    { 
                        jeu.Restart();
                        Redessiner();
                            }
                }
            }
               
        }

        private void Redessiner()
        {
            cnvMobiles.Children.Clear();
            DessinerCaisses();
            DessinerPersonnage();
            AfficherNbDeplacements();
        }
      

           private void  AfficherNbDeplacements()
        {

            txtNbDeplacements.Text = jeu.NBDeplacement.ToString();
        }   
       

        
        
            


        

        private void Dessiner()
        {
            DessinerCarte();
            Redessiner();
        }

        private void DessinerPersonnage()
        {
            Rectangle r = new Rectangle();
            r.Width = 30;
            r.Height = 30;
            r.Margin = new Thickness(jeu.Personnage.y * 50 + 10, jeu.Personnage.x * 50 + 10, 0, 0);
            r.Fill = new ImageBrush(new BitmapImage(new Uri("Images/perso.png", UriKind.Relative)));
            cnvMobiles.Children.Add(r);
        }

        private void DessinerCaisses()
        {
            foreach(Position pos in jeu.Caisses)
            {
                Rectangle r = new Rectangle();
                r.Width = 42;
                r.Height = 42;
                r.Margin = new Thickness(pos.y * 50 + 4 , pos.x * 50 + 4, 0, 0);
                r.Fill = new ImageBrush(new BitmapImage(new Uri("Images/caisse.png", UriKind.Relative)));
                cnvMobiles.Children.Add(r);

            }
             
        }

        private void DessinerCarte()
        {
            for (int ligne = 0; ligne < 10; ligne++)
            {
                for (int colonne = 0; colonne < 10; colonne++)
                {
                    Rectangle r = new Rectangle();
                    r.Width = 50;
                    r.Height = 50;
                    r.Margin = new Thickness(colonne * 50, ligne * 50, 0, 0);

                    switch (jeu.Case(ligne,colonne))
                    {
                        case Jeu.Etat.Vide:
                            r.Fill = new ImageBrush(new BitmapImage(new Uri("Images/sol.png", UriKind.Relative)));
                            break;
                        case Jeu.Etat.Mur:
                            r.Fill = new ImageBrush(new BitmapImage(new Uri("Images/mur.png", UriKind.Relative)));
                            break;
                        case Jeu.Etat.Cible:
                            r.Fill = new ImageBrush(new BitmapImage(new Uri("Images/cible.png", UriKind.Relative)));
                            break;




                    }

                    //r.Fill = new ImageBrush(new BitmapImage(new Uri("Images/mur.png", UriKind.Relative)));



                    cnvGrille.Children.Add(r);


                }


            }
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void BtnRecommencer_Click(object sender, RoutedEventArgs e)
        {
            jeu.Restart();
            Redessiner();
        }
    }
}

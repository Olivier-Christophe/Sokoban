using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sokoban
{
    public class Jeu
    {
        public enum Etat
        {
            Vide,
            Mur,
            Cible
        }

        private Etat[,] grille;

        private List<Position> caisses;

        public List<Position> Caisses { get => caisses; }

        private Position personnage;
        public Position Personnage { get => personnage; }
        private int nbDeplacements;

        public int NBDeplacement
        {
            get {return nbDeplacements;}
        }






        static String grilleTxt =

            "..XXXXXX..XXX.oo.XXXX..o..o..XX........XXXX....XXX..XX.CXX...XXXC.XXX..X.CP.C.X..X......X..XXXXXXXX.";





















        public Jeu()
        {
            grille = new Etat[10, 10];
            InitCarte();
            nbDeplacements=0;


        }

        private void InitCarte()
        {
            // Créer une liste vide de caisses
            caisses = new List<Position>();

            // Pour chaque case, initialise la bonne valeur
            // Ajoute les caisses si besoin
            // Détermine la position de départ du personnage
            for (int ligne = 0; ligne < 10; ligne++)
            {
                for (int colonne = 0; colonne < 10; colonne++)
                {

                    switch (grilleTxt[ligne * 10 + colonne])

                    {
                        case '.':
                            grille[ligne, colonne] = Etat.Vide;
                            break;
                        case 'X':
                            grille[ligne, colonne] = Etat.Mur;
                            break;
                        case 'o':
                            grille[ligne, colonne] = Etat.Cible;
                            break;

                        case 'C':
                            Caisses.Add(new Position(ligne, colonne));
                            grille[ligne, colonne] = Etat.Vide;
                            break;

                        case 'P':
                            personnage = new Position(ligne, colonne);
                            grille[ligne, colonne] = Etat.Vide;
                            break;


                    }
                }

            }


        }

        public bool fini()
        {
           foreach (Position caisse in caisses)
            {
                if (grille[caisse.x, caisse.y] != Etat.Cible)
                    return false;
            }
            return true;
        }

        public Etat Case(int ligne, int colonne)
        {
            return grille[ligne, colonne];

        }

        public void ToucheAppuyée(Key key)
        {


            Position newPos = new Position(Personnage.x, Personnage.y);

            CalculNewPos(newPos, key);

            if (CaseOK(newPos, key))
            {
                personnage = newPos;
                nbDeplacements++;


            }

        }





        private static void CalculNewPos(Position newPos, Key key)
        {


            switch (key)

            {
                case Key.Down:
                    newPos.x++;

                    break;

                case Key.Up:

                    newPos.x--;

                    break;

                case Key.Right:
                    newPos.y++;

                    break;

                case Key.Left:

                    newPos.y--;

                    break;
            }

        }



        private bool CaseOK(Position newPos, Key key)
        {
            //presence d'un mur
            if (grille[newPos.x, newPos.y] == Etat.Mur)
            {

                return false;
            }


            //présence d'une caisse

            Position caisse = CaisseInPos(newPos);
            if (caisse != null)

            {

                //déplacement caisse possible ?

                Position newPosCaisse = new Position(caisse.x, caisse.y);
                CalculNewPos(newPosCaisse, key);
                if (grille[newPosCaisse.x, newPosCaisse.y] == Etat.Mur)
                {
                    return false;
                }
                else if (CaisseInPos(newPosCaisse) != null)
                {
                    return false;
                }
                else
                {
                    caisse.x = newPosCaisse.x;
                    caisse.y = newPosCaisse.y;
                    return true;
                }
            }

            //pas d'obstacle 
            return true;
        }

        public void Restart()
        {
            InitCarte();
            nbDeplacements = 0;
        }

        private Position CaisseInPos(Position newPosCaisse)
        {
            foreach (Position caisse in caisses)
                if (caisse.x == newPosCaisse.x && caisse.y == newPosCaisse.y)
                {
                    return caisse;
                }

            return null;
        }
    }
}
        











    
            
                      

                    



               
               




      










         


            

    




        
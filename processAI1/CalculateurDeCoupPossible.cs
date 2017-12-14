using System;
using System.Collections.Generic;

namespace processAI1
{
    
    /**
    *
    * Classe chargée de renvoyer la liste de tout les coups possible (admisibles) pour un joueur donné
    * en fonction d'un état de l'environement doné
    *
    **/
    
    public class CalculateurDeCoupPossible
    {
        
        private List<Piece> _pieceList;
        private List<Piece> _pieceConcurentList;

        /* getListCoupsPossibles: Détermine la liste de toutl les coups admisibles
        * 
        * @param pieceList: les pièces du joueur pour lequel on veut connaitre les coups possibles
        * @param pieceConcurentList: les pièces de l'autre joueur
        * @return Liste d'ActionAgent modélisant les coups possibles
        */
        
        public List<ActionAgent> getListCoupsPossibles(List<Piece> pieceList, List<Piece> pieceConcurentList)
        {

            _pieceList = pieceList;
            _pieceConcurentList = pieceConcurentList;
            
            List<ActionAgent> actionAgentList = new List<ActionAgent>();

            // On va parcourir toutes les piéces et enregistrer les coups possibles en fonction de son type
            
            foreach (Piece piece in _pieceList)
            {
                // On récupére les coordonnées de la piéce
                
                int colonne = charToInt(piece.Position[0]);
                int ligne = (int)Char.GetNumericValue(piece.Position[1]);
                
                // On gére les différents cas qui dépendent du type de la piéce
                // On pense à controler qu'on est bien dans les limites du plateau
                
                // @todo un pion ne peut pas aller sur une case ou il y a deja un élement de sa couleur
                // @todo il faut gérer le cas selon que le pion vienne d'en haut ou d'en bas
                
                //***********************************************************//
                //***********************************************************//
                //                          LE PION                          //
                //***********************************************************//
                //***********************************************************//
                
                if (piece.TypePiece == TypesPieces.PION)
                {
                    if (ligne == 2)
                    {
                        // On peut déplacer le pion de 1 ou 2 cases en avant lorsqu'il est a sa position initiale
                        actionAgentList.Add(new ActionAgent(piece.Position, piece.Position[0] + "" + (ligne+1)));
                        actionAgentList.Add(new ActionAgent(piece.Position, piece.Position[0] + "" + (ligne+2)));
                    }
                    else if (ligne > 0 && ligne < 8)
                    {
                        // On peut déplacer le pion d'une case en avant tant qu'il n'attein pas la dernière
                        actionAgentList.Add(new ActionAgent(piece.Position, piece.Position[0] + "" + (ligne+1)));   
                    }
                }
                
                //***********************************************************//
                //***********************************************************//
                //     LA TOUR ET LES MOUVEMENTS HORIZONTAUX DE LA REINE     //
                //***********************************************************//
                //***********************************************************//
                
                else if (piece.TypePiece == TypesPieces.TOUR || piece.TypePiece == TypesPieces.REINE)
                {
                    // On peut déplacer la tour horizontalement ou verticalement dans tout les sens d'autant de case souhaité
                    // Mais elle ne peut pas sauter par dessus une autre piéce
                    
                    // On gére la reine ici aussi car elle à les mêmes proriétés que la tour mais elle peut en plus sauter par dessus une piéce
                    
                    // On commence par gérer les mouvement verticaux possibles dans les deux sens
                    
                    for (int i = ligne++; i <= 8; i++)
                    {
                        if (piece.TypePiece == TypesPieces.TOUR)
                        {
                            // si case déjà occupé par une piéce aliée: stop immédiat
                            if (IsCaseDejaOccupee(i, colonne, true, false)) break;
                            // si case non occupée ou déjà occupé par une piéce adverse: on ajoute le coup puis stop
                            actionAgentList.Add(new ActionAgent(piece.Position, colonne + "" + i));
                            if (IsCaseDejaOccupee(i, colonne, false, true)) break;

                        }
                        else if (piece.TypePiece == TypesPieces.REINE)
                        {
                            // si case non occupée par une piéce aliée: on ajoute le coup
                            if (!IsCaseDejaOccupee(i, colonne, true, false)) actionAgentList.Add(new ActionAgent(piece.Position, colonne + "" + i));
                        }
                    }
                    
                    for (int i = ligne--; i >= 1; i--)
                    {
                        if (piece.TypePiece == TypesPieces.TOUR)
                        {
                            // si case déjà occupé par une piéce aliée: stop immédiat
                            if (IsCaseDejaOccupee(i, colonne, true, false)) break;
                            // si case non occupée ou déjà occupé par une piéce adverse: on ajoute le coup puis stop
                            actionAgentList.Add(new ActionAgent(piece.Position, colonne + "" + i));
                            if (IsCaseDejaOccupee(i, colonne, false, true)) break;

                        }
                        else if (piece.TypePiece == TypesPieces.REINE)
                        {
                            // si case non occupée par une piéce aliée: on ajoute le coup
                            if (!IsCaseDejaOccupee(i, colonne, true, false)) actionAgentList.Add(new ActionAgent(piece.Position, colonne + "" + i));
                        }
                    }
                    
                    // Maintenant on gére les mouvements horizontaux dans les deux sens
                    
                    for (int i = colonne++; i <= 8; i++)
                    {
                        if (piece.TypePiece == TypesPieces.TOUR)
                        {
                            // si case déjà occupé par une piéce aliée: stop immédiat
                            if (IsCaseDejaOccupee(i, colonne, true, false)) break;
                            // si case non occupée ou déjà occupé par une piéce adverse: on ajoute le coup puis stop
                            actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + ligne));
                            if (IsCaseDejaOccupee(i, colonne, false, true)) break;

                        }
                        else if (piece.TypePiece == TypesPieces.REINE)
                        {
                            // si case non occupée par une piéce aliée: on ajoute le coup
                            if (!IsCaseDejaOccupee(i, colonne, true, false)) actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + ligne));
                        }
                    }
                    for (int i = colonne--; i >= 1; i--)
                    {
                        if (piece.TypePiece == TypesPieces.TOUR)
                        {
                            // si case déjà occupé par une piéce aliée: stop immédiat
                            if (IsCaseDejaOccupee(i, colonne, true, false)) break;
                            // si case non occupée ou déjà occupé par une piéce adverse: on ajoute le coup puis stop
                            actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + ligne));
                            if (IsCaseDejaOccupee(i, colonne, false, true)) break;

                        }
                        else if (piece.TypePiece == TypesPieces.REINE)
                        {
                            // si case non occupée par une piéce aliée: on ajoute le coup
                            if (!IsCaseDejaOccupee(i, colonne, true, false)) actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + ligne));
                        }
                    }
                }
                
                //***********************************************************//
                //***********************************************************//
                //     LE FOU ET LES MOUVEMENTS EN DIAGONALE DE LA REINE     //
                //***********************************************************//
                //***********************************************************//
                
                else if (piece.TypePiece == TypesPieces.FOU || piece.TypePiece == TypesPieces.REINE)
                {
                    // On peut déplacer le fou en diagonale dans tout les sens d'autant de case souhaité (4 directions possibles)
                    // Mais il ne peut pas sauter par dessus une autre piéce
                    
                    // On gére la reine ici aussi car elle à les mêmes proriétés que la tour mais elle peut en plus sauter par dessus une pièce

                    // CAS 1 de diagonale
                    
                    int i = colonne++;
                    int j = ligne--;

                    while (i <= 8 && j > 0)
                    {
                        if (piece.TypePiece == TypesPieces.FOU)
                        {
                            // si case déjà occupé par une piéce aliée: stop immédiat
                            if (IsCaseDejaOccupee(j, i, true, false)) break;
                            // si case non occupée ou déjà occupé par une piéce adverse: on ajoute le coup puis stop
                            actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + j));
                            if (IsCaseDejaOccupee(j, i, false, true)) break;
                        }
                        else if (piece.TypePiece == TypesPieces.REINE)
                        {
                            // si case non occupée par une piéce aliée: on ajoute le coup
                            if (!IsCaseDejaOccupee(j, i, true, false)) actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + j));
                        }
                        
                        i++;
                        j--;
                    }
                    
                    // CAS 2 de diagonale
                    
                    i = colonne++;
                    j = ligne++;

                    while (i <= 8 && j <= 8)
                    {
                        if (piece.TypePiece == TypesPieces.FOU)
                        {
                            // si case déjà occupé par une piéce aliée: stop immédiat
                            if (IsCaseDejaOccupee(j, i, true, false)) break;
                            // si case non occupée ou déjà occupé par une piéce adverse: on ajoute le coup puis stop
                            actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + j));
                            if (IsCaseDejaOccupee(j, i, false, true)) break;
                        }
                        else if (piece.TypePiece == TypesPieces.REINE)
                        {
                            // si case non occupée par une piéce aliée: on ajoute le coup
                            if (!IsCaseDejaOccupee(j, i, true, false)) actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + j));
                        }
                       
                        i++;
                        j++;
                    }
                    
                    // CAS 3 de diagonale
                    
                    i = colonne--;
                    j = ligne--;

                    while (i > 0 && j > 0)
                    {
                        if (piece.TypePiece == TypesPieces.FOU)
                        {
                            // si case déjà occupé par une piéce aliée: stop immédiat
                            if (IsCaseDejaOccupee(j, i, true, false)) break;
                            // si case non occupée ou déjà occupé par une piéce adverse: on ajoute le coup puis stop
                            actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + j));
                            if (IsCaseDejaOccupee(j, i, false, true)) break;
                        }
                        else if (piece.TypePiece == TypesPieces.REINE)
                        {
                            // si case non occupée par une piéce aliée: on ajoute le coup
                            if (!IsCaseDejaOccupee(j, i, true, false)) actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + j));
                        }
                        
                        i--;
                        j--;
                    }
                    
                    // CAS 4 de diagonale
                    
                    i = colonne--;
                    j = ligne++;

                    while (i > 0 && j <= 8)
                    {
                        if (piece.TypePiece == TypesPieces.FOU)
                        {
                            // si case déjà occupé par une piéce aliée: stop immédiat
                            if (IsCaseDejaOccupee(j, i, true, false)) break;
                            // si case non occupée ou déjà occupé par une piéce adverse: on ajoute le coup puis stop
                            actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + j));
                            if (IsCaseDejaOccupee(j, i, false, true)) break;
                        }
                        else if (piece.TypePiece == TypesPieces.REINE)
                        {
                            // si case non occupée par une piéce aliée: on ajoute le coup
                            if (!IsCaseDejaOccupee(j, i, true, false)) actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + j));
                        }
                        
                        i--;
                        j++;
                    }

                }
                
                //***********************************************************//
                //***********************************************************//
                //                     LE CAVALIER                           //
                //***********************************************************//
                //***********************************************************//
                
                else if (piece.TypePiece == TypesPieces.CAVALIER)
                {
                    // Le cavalier se déplace de 2 cases dans une direction suivi d'1 case perpendiculairement
                    // Il peut sauter par dessus une autre piéce
                    // Il existe donc 8 cas possible à gérer
                    
                    // CAS 1 

                    int i = colonne + 2;
                    int j = ligne + 1;

                    if (i <= 8 && i > 0 && j <= 8 && j > 0 && !IsCaseDejaOccupee(j, i, true, false))
                    {
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + j));
                    }
                    
                    // CAS 2
                    
                    i = colonne + 2;
                    j = ligne - 1;

                    if (i <= 8 && i > 0 && j <= 8 && j > 0 && !IsCaseDejaOccupee(j, i, true, false))
                    {
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + j));
                    }
                    
                    // CAS 3

                    i = colonne - 2;
                    j = ligne + 1;

                    if (i <= 8 && i > 0 && j <= 8 && j > 0 && !IsCaseDejaOccupee(j, i, true, false))
                    {
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + j));
                    }
                    
                    // CAS 4
                    
                    i = colonne - 2;
                    j = ligne - 1;

                    if (i <= 8 && i > 0 && j <= 8 && j > 0 && !IsCaseDejaOccupee(j, i, true, false))
                    {
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + j));
                    }
                    
                    // CAS 5
                    
                    i = colonne - 1;
                    j = ligne - 2;

                    if (i <= 8 && i > 0 && j <= 8 && j > 0 && !IsCaseDejaOccupee(j, i, true, false))
                    {
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + j));
                    }
                    
                    // CAS 6
                    
                    i = colonne + 1;
                    j = ligne - 2;

                    if (i <= 8 && i > 0 && j <= 8 && j > 0 && !IsCaseDejaOccupee(j, i, true, false))
                    {
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + j));
                    }
                    
                    // CAS 7
                    
                    i = colonne - 1;
                    j = ligne + 2;

                    if (i <= 8 && i > 0 && j <= 8 && j > 0 && !IsCaseDejaOccupee(j, i, true, false))
                    {
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + j));
                    }
                    
                    // CAS 8
                    
                    i = colonne + 1;
                    j = ligne + 2;

                    if (i <= 8 && i > 0 && j <= 8 && j > 0 && !IsCaseDejaOccupee(j, i, true, false))
                    {
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + j));
                    }
                    
                    
                }
                
                //***********************************************************//
                //***********************************************************//
                //                       LE ROI                              //
                //***********************************************************//
                //***********************************************************//
                
                else if (piece.TypePiece == TypesPieces.ROI)
                {
                    // Le roi peut se déplacer sur n'importe quelle piéce adjacente
                    // Sauf si une piéce alliée y est déjà 
                    // On gére donc les 8 positions possibles
                    
                    if((colonne + 1) <= 8 && !IsCaseDejaOccupee(ligne, colonne+1, true, false))
                    {
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(colonne + 1) + "" + ligne));
                    }
                    if ((colonne - 1) > 0 && !IsCaseDejaOccupee(ligne, colonne-1, true, false))
                    {
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(colonne - 1) + "" + ligne));
                    }
                    if ((ligne + 1) <= 8 && !IsCaseDejaOccupee(ligne+1, colonne, true, false))
                    {
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(colonne) + "" + (ligne + 1)));
                    }
                    if ((ligne - 1) > 0 && !IsCaseDejaOccupee(ligne-1, colonne, true, false))
                    {
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(colonne) + "" + (ligne - 1)));
                    }
                    if ((ligne + 1) <= 8 && (colonne + 1) <= 8 && !IsCaseDejaOccupee(ligne + 1, colonne + 1, true, false))
                    {
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(colonne + 1) + "" + (ligne + 1)));
                    }
                    if ((ligne - 1) > 0 && (colonne + 1) <= 8 && !IsCaseDejaOccupee(ligne - 1, colonne + 1, true, false))
                    {
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(colonne + 1) + "" + (ligne - 1)));
                    }
                    if ((ligne + 1) <= 8 && (colonne - 1) > 0 && !IsCaseDejaOccupee(ligne + 1, colonne - 1, true, false))
                    {
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(colonne - 1) + "" + (ligne + 1)));
                    }
                    if ((ligne - 1) > 0 && (colonne - 1) > 0 && !IsCaseDejaOccupee(ligne - 1, colonne - 1, true, false))
                    {
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(colonne - 1) + "" + (ligne - 1)));
                    }
                }
                
            }
            
            return actionAgentList;
        }
        
        /* isCaseDejaOccupee: Contrôle la présence d'une piéce à la case demandée
        * 
        * @param verifierPresenceMesPieces: true si l'on contrôle la présence des pièces aliées
        * @param verifierPresencePiecesConcurent: true si l'on contrôle la présence des pièces énemies
        * @return true si la case est déjà occupée selon les critères, false sinon
        */
        
        // @todo verifier que i et j sont entre 1 et 8 sinon return false direct

        private Boolean IsCaseDejaOccupee(int i, int j, bool verifierPresenceMesPieces, bool verifierPresencePiecesConcurent)
        {
            if (verifierPresencePiecesConcurent)
            {
                // Controle des piéces du concurent
                foreach (Piece piece in _pieceConcurentList)
                {
                    // Si la piéce concurente se trouve déjà sur la case
                    if (j == charToInt(piece.Position[0]) && i == (int)Char.GetNumericValue(piece.Position[1]))
                    {
                        return true;
                    }
                }
            }
            if (verifierPresenceMesPieces)
            {
                // Controle de mes piéces
                foreach (Piece piece in _pieceList)
                {
                    // Si une piéce qui m'appartien se trouve déjà sur la case
                    if (j == charToInt(piece.Position[0]) && i == (int) Char.GetNumericValue(piece.Position[1]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /* charToInt: Transforme les lettres des colonnes de a à h en un entier pour simplifier les inscrménations, comparaisons
        * 
        * @param c: un char entre a et h
        * @return un entier entre 1 et 8
        */
        
        private int charToInt(char c)
        {
            int colonne = 0;
            
            switch (c)
            {
                    case 'a':
                        colonne = 1;
                        break;
                    case 'b':
                        colonne = 2;
                        break;
                    case 'c':
                        colonne = 3;
                        break;
                    case 'd':
                        colonne = 4;
                        break;
                    case 'e':
                        colonne = 5;
                        break;
                    case 'f':
                        colonne = 6;
                        break;
                    case 'g':
                        colonne = 7;
                        break;
                    case 'h':
                        colonne = 8;
                        break;
            }
            
            return colonne;
        }

        /* intToChar: Transforme les entier en lettre de a à h pour les colonnes
        * 
        * @param i: un int entre 1 et 8
        * @return un char entre a et h
        */
        
        private char intToChar(int i)
        {
            char colonne = 'z';
            
            switch (i)
            {
                case 1:
                    colonne = 'a';
                    break;
                case 2:
                    colonne = 'b';
                    break;
                case 3:
                    colonne = 'c';
                    break;
                case 4:
                    colonne = 'd';
                    break;
                case 5:
                    colonne = 'e';
                    break;
                case 6:
                    colonne = 'f';
                    break;
                case 7:
                    colonne = 'g';
                    break;
                case 8:
                    colonne = 'h';
                    break;
            }
            
            return colonne;
        }
    }
}
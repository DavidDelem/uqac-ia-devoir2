using System;
using System.Collections.Generic;

namespace processAI1
{
    public class CalculateurDeCoupPossible
    {
        // Renvoie une liste de tout les coups valides (tout les endroits ou l'on peut déplacer un de nos pion)

        private List<Piece> _pieceList;
        private List<Piece> _pieceConcurentList;

        public List<ActionAgent> getListCoupsPossibles(List<Piece> pieceList, List<Piece> pieceConcurentList)
        {

            _pieceList = pieceList;
            _pieceConcurentList = pieceConcurentList;
            
            List<ActionAgent> actionAgentList = new List<ActionAgent>();

            foreach (Piece piece in _pieceList)
            {
                int colonne = charToInt(piece.Position[0]);
                int ligne = (int)Char.GetNumericValue(piece.Position[1]);
                
                if (piece.TypePiece == TypesPieces.PION)
                {
                    if (ligne == 2)
                    {
                        // On peut déplacer le pion de 1 ou 2 cases en avant lorsqu'il est a sa position initiale
                        actionAgentList.Add(new ActionAgent(piece.Position, piece.Position[0] + "" + (ligne+1)));
                        actionAgentList.Add(new ActionAgent(piece.Position, piece.Position[0] + "" + (ligne+2)));
                    }
                    else if (ligne < 8)
                    {
                        // On peut déplacer le pion d'une case en avant tant qu'il n'attein pas la dernière
                        actionAgentList.Add(new ActionAgent(piece.Position, piece.Position[0] + "" + (ligne+1)));   
                    }
                }
                else if (piece.TypePiece == TypesPieces.TOUR || piece.TypePiece == TypesPieces.REINE)
                {
                    // On peut déplacer la tour horizontalement ou verticalement dans tout les sens d'autant de case souhaité
                    // Mais elle ne peut pas sauter par dessus une autre piéce
                    
                    // On gére la reine ici aussi car elle à les mêmes proriétés que la tour (et plus)
                    
                    // On commence par gérer les mouvement verticaux possibles
                    for (int i = ligne++; i <= 8; i++)
                    {
                        if (isCaseDejaOccupee(i, colonne, true)) break;
                        actionAgentList.Add(new ActionAgent(piece.Position, colonne + "" + i));   
                    }
                    
                    for (int i = ligne--; i >= 1; i--)
                    {
                        if (isCaseDejaOccupee(i, colonne, true)) break;
                        actionAgentList.Add(new ActionAgent(piece.Position, colonne + "" + i));   
                    }
                    
                    // Maintenant on gére les mouvements horizontaux
                    for (int i = colonne++; i <= 8; i++)
                    {
                        if (isCaseDejaOccupee(ligne, i, true)) break;
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + ligne));   
                    }
                    for (int i = colonne--; i >= 1; i--)
                    {
                        if (isCaseDejaOccupee(ligne, i, true)) break;
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + ligne));   
                    }
                }
                else if (piece.TypePiece == TypesPieces.FOU || piece.TypePiece == TypesPieces.REINE)
                {
                    // On peut déplacer le fou en diagonale dans tout les sens d'autant de case souhaité (4 directions possibles)
                    // Mais il ne peut pas sauter par dessus une autre piéce
                    
                    // On gére la reine ici aussi car elle à les mêmes proriétés que la tour (et plus)

                    int i = colonne++;
                    int j = ligne--;

                    while (i <= 8 && j > 0)
                    {
                        if (isCaseDejaOccupee(j, i, true)) break;
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + j));   
                        i++;
                        j--;
                    }
                    
                    i = colonne++;
                    j = ligne++;

                    while (i <= 8 && j <= 8)
                    {
                        if (isCaseDejaOccupee(j, i, true)) break;
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + j));   
                        i++;
                        j++;
                    }
                    
                    i = colonne--;
                    j = ligne--;

                    while (i > 0 && j > 0)
                    {
                        if (isCaseDejaOccupee(j, i, true)) break;
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + j));   
                        i--;
                        j--;
                    }
                    
                    i = colonne--;
                    j = ligne++;

                    while (i > 0 && j <= 8)
                    {
                        if (isCaseDejaOccupee(j, i, true)) break;
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + j));   
                        i--;
                        j++;
                    }

                }
                else if (piece.TypePiece == TypesPieces.CAVALIER)
                {
                    // Le cavalier se déplace de 2 cases dans une direction suivi d'1 case perpendiculairement
                    // Il peut sauter par dessus une autre piéce
                    // Il existe donc 8 cas possible à gérer

                    int i = colonne + 2;
                    int j = ligne + 1;

                    if (i <= 8 && i > 0 && j <= 8 && j > 0)
                    {
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + j));
                    }
                    
                    i = colonne + 2;
                    j = ligne - 1;

                    if (i <= 8 && i > 0 && j <= 8 && j > 0)
                    {
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + j));
                    }

                    i = colonne - 2;
                    j = ligne + 1;

                    if (i <= 8 && i > 0 && j <= 8 && j > 0)
                    {
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + j));
                    }
                    
                    i = colonne - 2;
                    j = ligne - 1;

                    if (i <= 8 && i > 0 && j <= 8 && j > 0)
                    {
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + j));
                    }
                    
                    i = colonne - 1;
                    j = ligne - 2;

                    if (i <= 8 && i > 0 && j <= 8 && j > 0)
                    {
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + j));
                    }
                    
                    i = colonne + 1;
                    j = ligne - 2;

                    if (i <= 8 && i > 0 && j <= 8 && j > 0)
                    {
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + j));
                    }
                    
                    i = colonne - 1;
                    j = ligne + 2;

                    if (i <= 8 && i > 0 && j <= 8 && j > 0)
                    {
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + j));
                    }
                    
                    i = colonne + 1;
                    j = ligne + 2;

                    if (i <= 8 && i > 0 && j <= 8 && j > 0)
                    {
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(i) + "" + j));
                    }
                    
                    
                }
                else if (piece.TypePiece == TypesPieces.ROI)
                {
                    // Le roi peut se déplacer sur n'importe quelle piéce adjacente
                    // Sauf si une piéce alliée y est déjà 
                    // Il ne peut pas se mettre en position d'echec
                    
                    // if pas de piece allié a ajouter + if dans le plateau
                    if((colonne + 1) < 8 && !isCaseDejaOccupee(ligne, colonne, false))
                    {
                        actionAgentList.Add(new ActionAgent(piece.Position, intToChar(colonne + 1) + "" + ligne));
                    }
                    actionAgentList.Add(new ActionAgent(piece.Position, intToChar(colonne + 1) + "" + ligne));
                    actionAgentList.Add(new ActionAgent(piece.Position, intToChar(colonne - 1) + "" + ligne));
                    actionAgentList.Add(new ActionAgent(piece.Position, intToChar(colonne) + "" + (ligne+1)));
                    actionAgentList.Add(new ActionAgent(piece.Position, intToChar(colonne) + "" + (ligne-1)));

                    actionAgentList.Add(new ActionAgent(piece.Position, intToChar(colonne + 1) + "" + (ligne+1)));
                    actionAgentList.Add(new ActionAgent(piece.Position, intToChar(colonne + 1) + "" + (ligne-1)));
                    actionAgentList.Add(new ActionAgent(piece.Position, intToChar(colonne - 1) + "" + (ligne+1)));
                    actionAgentList.Add(new ActionAgent(piece.Position, intToChar(colonne - 1) + "" + (ligne-1)));
                }
                
            }
            
            return actionAgentList;
        }
        
        // Contrôle la présence d'une piéce à la case demandée
        // Si totuesLesPieces est à true: on contrôle la présence de toutes les piéces
        // Sinon, on controle uniquement la présence de nos piéces

        private Boolean isCaseDejaOccupee(int i, int j, Boolean toutesLesPieces)
        {
            if (toutesLesPieces)
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

            // Controle de mes piéces
            foreach (Piece piece in _pieceList)
            {
                // Si une piéce qui m'appartien se trouve déjà sur la case
                if (j == charToInt(piece.Position[0]) && i == (int) Char.GetNumericValue(piece.Position[1]))
                {
                    return true;
                }
            }
            return false;
        }

        // Transforme les lettres des colonnes de a à h en un entier pour simplifier les inscrménations, comparaisons
        
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
        
        // Transforme les lettres des colonnes de a à h en un entier pour simplifier les inscrménations, comparaisons
        
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
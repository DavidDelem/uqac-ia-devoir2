using System;
using System.Collections.Generic;

namespace processAI1
{
    /**
    *
    * Permet de déterminer le meilleur coup possible parmis une liste de coup
    *
    **/
    
    public class SearchBestMove
    {
        private int score;
        private List<ActionAgent> coupsPossibles;
        private List<Piece> mesPieces, piecesConcuList, mesPiecesTmp, piecesConcuListTmp;
        
        public SearchBestMove(List<ActionAgent> coupsPossibles, List<Piece> mesPieces, List<Piece> piecesConcuList)
        {
            score = 0;
            this.coupsPossibles = coupsPossibles;
            this.mesPieces = mesPieces;
            this.piecesConcuList = piecesConcuList;
        }
        
        /* bestMove: Renvoi la meilleur action possible parmis la liste coupsPossibles
        * 
        * @param aucun
        * @return ActionAgent: la meilleur action
        */
        
        public ActionAgent bestMove()
        {
            
            // On parcours tout les coups possibles
            for (int i = 0; i < coupsPossibles.Count; i++)
            {
                // On stocke l'état du plateau initial dans une variable temporaire
                // afin de le réinitialiser après chaque évaluation de coup possible
                mesPiecesTmp = mesPieces;
                piecesConcuListTmp = piecesConcuList;
                
                // On simmule le coup temporairement
                doMove(coupsPossibles[i]);
                
                // On évalue la qualité du coup et met à jour le nombre de points pour ce coup
                coupsPossibles[i].NbPoints = evaluate(coupsPossibles[i]);
            }

            // On parcours tout les coups possibles et on prends celui ayant la plus grande valeur
            // C'est le meilleur coup
            ActionAgent bestMove = coupsPossibles[0];
            for (int i = 0; i < coupsPossibles.Count; i++)
            {
                if (coupsPossibles[i].NbPoints > bestMove.NbPoints)
                {
                    bestMove = coupsPossibles[i];
                }
            }
            return bestMove;
        }
        
        /* doMove: Simmule un mouvement pour voir l'état du plateau résultant
        * 
        * @param move: le mouvement à réaliser
        * @return void
        */
        
        private void doMove(ActionAgent move)
        {
            // On regarde si notre coup touche un enemi
            // Si c'est le cas, on le supprime de la liste des piéces enemies
            for (int i = 0; i < piecesConcuListTmp.Count; i++)
            {
                if (move.PositionFinale == piecesConcuListTmp[i].Position)
                {
                    piecesConcuListTmp.Remove(piecesConcuListTmp[i]);
                }
            }
            
            // On met à jours la piéce déplacée dans la liste des piéces aliées avec sa nouvelle position
            for (int j = 0; j < mesPiecesTmp.Count; j++)
            {
                if (move.PositionInitiale == mesPiecesTmp[j].Position)
                {
                    mesPiecesTmp[j].Position = move.PositionFinale;
                }
            }
        }
        
        /* evaluate: Evalue la qualité d'un coup possible
        * 
        * @param move: le mouvement courant
        * @return void
        */

        private int evaluate(ActionAgent move)
        {
            
            int score = 0;
            
            
            // L'état du plateau suite à la simmulation du coup est stocké dans mesPiecesTmp et piecesConcuListTmp
            // On les parcours pour déterminer les points
            // Pour les piéces aliées, on attribue des points en fonction de l'avancée sur le plateau

            for (int i = 0; i < mesPiecesTmp.Count; i++)
            {
                    switch ((int) Char.GetNumericValue(mesPiecesTmp[i].Position[1]))
                    {
                        case 2:
                            score += 10;
                            break;
                        case 3:
                            score += 15;
                            break;
                        case 4:
                            score += 20;
                            break;
                        case 5:
                            score += 25;
                            break;
                        case 6:
                            score += 30;
                            break;
                        case 7:
                            score += 35;
                            break;
                        case 8:
                            score += 40;
                            break;
                    }

                score += mesPiecesTmp[i].Poids;
            }

            for (int j = 0; j < piecesConcuListTmp.Count; j++)
            {
                score += piecesConcuList[j].Poids;
            }

            return score;
        }
    }
}
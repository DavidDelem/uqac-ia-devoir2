using System;
using System.Collections.Generic;

namespace processAI1
{
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
        
        public ActionAgent bestMove()
        {
            for (int i = 0; i < coupsPossibles.Count; i++)
            {
                mesPiecesTmp = mesPieces;
                piecesConcuListTmp = piecesConcuList;
                doMove(coupsPossibles[i]);
                coupsPossibles[i].NbPoints = evaluate();
            }

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

        private void doMove(ActionAgent move)
        {
            // Check if we hit an ennemy
            for (int i = 0; i < piecesConcuListTmp.Count; i++)
            {
                if (move.PositionFinale == piecesConcuListTmp[i].Position)
                {
                    piecesConcuListTmp.Remove(piecesConcuListTmp[i]);
                }
            }
            // Do the move
            for (int j = 0; j < mesPiecesTmp.Count; j++)
            {
                if (move.PositionInitiale == mesPiecesTmp[j].Position)
                {
                    mesPiecesTmp[j].Position = move.PositionFinale;
                }
            }
        }

        private int evaluate()
        {
            int score = 0;

            for (int i = 0; i < mesPiecesTmp.Count; i++)
            {
                switch ((int)Char.GetNumericValue(mesPiecesTmp[i].Position[1]))
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
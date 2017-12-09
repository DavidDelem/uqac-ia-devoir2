using System;
using System.Collections.Generic;

namespace processAI1
{
    public class Environement
    {
        private List<Piece> mesPiecesList;
        private List<Piece> piecesConcurentList;

        public List<Piece> MesPiecesList
        {
            get { return mesPiecesList; }
            set { mesPiecesList = value; }
        }

        public List<Piece> PiecesConcurentList
        {
            get { return piecesConcurentList; }
            set { piecesConcurentList = value; }
        }
    }
}
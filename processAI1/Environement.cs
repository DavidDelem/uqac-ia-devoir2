using System;
using System.Collections.Generic;

namespace processAI1
{
    
    /**
    *
    * Modélise l'environement tel que perçu par les capteurs de l'agent
    *
    **/
    
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
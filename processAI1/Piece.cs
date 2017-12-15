using System;

namespace processAI1
{
    
    /**
    *
    * Modélise une pièce sur l'échiquier
    *
    **/
    
    public class Piece
    {
        private TypesPieces typePiece;
        private string position;
        private int poids;

        public Piece(string position, TypesPieces typePiece, bool concurrent)
        {
            this.typePiece = typePiece;
            this.position = position;
            switch (typePiece)
            {
                case  TypesPieces.PION:
                    switch (concurrent)
                    {
                    case true:
                        poids = -10;
                        break;
                    case false:
                        poids = 10;
                        break;
                    }
                    break;
                
                case  TypesPieces.CAVALIER:
                    switch (concurrent)
                    {
                        case true:
                            poids = -30;
                            break;
                        case false:
                            poids = 30;
                            break;
                    }
                    break;
                case  TypesPieces.FOU:
                    switch (concurrent)
                    {
                        case true:
                            poids = -30;
                            break;
                        case false:
                            poids = 30;
                            break;
                    }
                    break;
                case  TypesPieces.TOUR:
                    switch (concurrent)
                    {
                        case true:
                            poids = -50;
                            break;
                        case false:
                            poids = 50;
                            break;
                    }
                    break;
                case  TypesPieces.REINE:
                    switch (concurrent)
                    {
                        case true:
                            poids = -90;
                            break;
                        case false:
                            poids = 90;
                            break;
                    }
                    break;
                case  TypesPieces.ROI:
                    switch (concurrent)
                    {
                        case true:
                            poids = -900;
                            break;
                        case false:
                            poids = 900;
                            break;
                    }
                    break;
            }
        }

        public TypesPieces TypePiece
        {
            get { return typePiece; }
            set { typePiece = value; }
        }

        public string Position
        {
            get { return position; }
            set { position = value; }
        }
        
        public int Poids
        {
            get { return poids; }
            set { poids = value; }
        }
    }
}
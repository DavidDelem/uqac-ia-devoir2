using System;

namespace processAI1
{
    public class Piece
    {
        private TypesPieces typePiece;
        private string position;

        public Piece(string position, TypesPieces typePiece)
        {
            this.typePiece = typePiece;
            this.position = position;
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
    }
}
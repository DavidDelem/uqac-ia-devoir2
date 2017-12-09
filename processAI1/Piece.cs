using System;

namespace processAI1
{
    public class Piece
    {
        private int type;
        private string position;

        public Piece(string position, int type)
        {
            this.type = type;
            this.position = position;
        }
    }
}
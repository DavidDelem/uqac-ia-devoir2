using System;
using System.Runtime.CompilerServices;

namespace processAI1
{
    public class ActionAgent
    {
        private String positionInitiale;
        private String positionFinale;
        private int nbPoints;

        public ActionAgent(string positionInitiale, string positionFinale)
        {
            this.positionInitiale = positionInitiale;
            this.positionFinale = positionFinale;
        }

        public string PositionInitiale
        {
            get { return positionInitiale; }
            set { positionInitiale = value; }
        }

        public string PositionFinale
        {
            get { return positionFinale; }
            set { positionFinale = value; }
        }

        public int NbPoints
        {
            get { return nbPoints; }
            set { nbPoints = value; }
        }
    }
}
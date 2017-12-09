using System;
using System.Collections.Generic;

namespace processAI1
{
    public class Agent
    {

        private Environement environement;
        
        public Agent()
        {
            environement = new Environement();
        }

        public void observerEnvironement(int[] tabVal, String[] tabCoord)
        {
            
//            List<String> mesPiecesStringList = new List<String>();
//            List<String> piecesConcurentStringList = new List<String>();
//            
//            for (int i = 0; i < tabVal.Length; i++)
//            {
//                if (tabVal[i] > 0) mesPiecesStringList.Add(tabCoord[i]);
//            }
//
//            for (int i = 0; i < tabVal.Length; i++)
//            {
//                if (tabVal[i] <= 0) piecesConcurentStringList.Add(tabCoord[i]);
//            }

            // Création de deux listes: une pour mes piéces, l'autre pour celles du concurent
     
            List<Piece> mesPiecesList = new List<Piece>();
            List<Piece> piecesConcurentList = new List<Piece>();
            
            // Construction des listes
            
            for (int i = 0; i < tabVal.Length; i++)
            {
                // il faut trouver comment savoir quel est le type de la piéce et l'ajouter dans piece
                Piece piece = new Piece(tabCoord[i], tabVal[i]);
                
                if (tabVal[i] > 0)
                {
                    // Si la piéce m'appartient, je l'ajoute a ma liste
                    mesPiecesList.Add(piece);
                }
                else
                {
                   
                    // Si la piéce ne m'appartient pas, je l'ajoute a la liste de celles de mon concurent
                    piecesConcurentList.Add(piece);
                }
            }
            
            // Mise à jours de l'état de l'environement tel qu'il est perçu par les capteurs de l'agent
            
            environement.MesPiecesList = mesPiecesList;
            environement.PiecesConcurentList = piecesConcurentList;

        }

        public void choisirAction()
        {
            // déterminer coups possible
            // faire min max
            // trouver le meilleur
        }
        
    }
}
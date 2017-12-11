using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

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

            // Création de deux listes: une pour mes piéces, l'autre pour celles du concurent
            List<Piece> mesPiecesList = new List<Piece>();
            List<Piece> piecesConcurentList = new List<Piece>();
            
            // Construction des listes selon l'état de l'échiquier actuel perçu par les capteurs
            for (int i = 0; i < tabVal.Length; i++)
            {
                // Détermination du type de la piéce
                TypesPieces typesPieces = intToTypePiece(tabVal[i]);
                Piece piece = new Piece(tabCoord[i], typesPieces);
                
                if (tabVal[i] > 0)
                {
                    // Si la piéce m'appartient, je l'ajoute a ma liste
                    mesPiecesList.Add(piece);
                    Console.WriteLine("J'ai détecté un " + piece.TypePiece + " qui m'appartien en position "  + piece.Position);
                }
                else
                {
                    // Si la piéce ne m'appartient pas, je l'ajoute a la liste de celles de mon concurent
                    piecesConcurentList.Add(piece);
                    Console.WriteLine("J'ai détecté un " + piece.TypePiece + " concurent en position "  + piece.Position);
                }
            }
            
            // Mise à jours de l'état de l'environement tel qu'il est perçu par les capteurs de l'agent
            environement.MesPiecesList = mesPiecesList;
            environement.PiecesConcurentList = piecesConcurentList;
        }

        public ActionAgent choisirAction()
        {
            // On détermine tout les coups possibles (ou alors on fait ça direcement dans le min max ?)

            CalculateurDeCoupPossible calculateurDeCoupPossible = new CalculateurDeCoupPossible();
            List<ActionAgent> coupsPossibles = new List<ActionAgent>();
            
            coupsPossibles = calculateurDeCoupPossible.getListCoupsPossibles(environement.MesPiecesList, environement.PiecesConcurentList);
            
            // faire min max:
            // Wilfried: En gros t'as pas besoin de stocker tous les coups possibles, il faut juste retourner
            // la max ou min value en fct de la prédiction de tes coups ou celui de ton adversaire et lorsque
            // ça remonte à ta profondeur initiale, si la bestValue est meilleur alors tu retiens le coup correspondant
            // à cette bestValue ^^ Je sais pas trop si c'est clair... ^^
            
            // Recherche du meilleur coup avec MinMax amélioré grâce au Alpha Beta Pruning
            MinMaxAlphaBeta minMaxAlphaBeta = new MinMaxAlphaBeta();
            
            // min max
            
            ActionAgent action = new ActionAgent("a2", "a3"); // Contiendra l'action définie suite au résultat de min max
            return action;
        }

        private TypesPieces intToTypePiece(int valeur)
        {
            TypesPieces typesPieces;
            
            switch (valeur)
            {
                case 1:
                    typesPieces = TypesPieces.PION;
                    break;
                case 21:
                    typesPieces = TypesPieces.TOUR;
                    break;
                case 22:
                    typesPieces = TypesPieces.TOUR;
                    break;
                case 31:
                    typesPieces = TypesPieces.CAVALIER;
                    break;
                case 32:
                    typesPieces = TypesPieces.CAVALIER;
                    break;
                case 4:
                    typesPieces = TypesPieces.FOU;
                    break;
                case 5:
                    typesPieces = TypesPieces.ROI;
                    break;
                case 6:
                    typesPieces = TypesPieces.REINE;
                    break;
                default:
                    typesPieces = TypesPieces.NONE;
                    break;
            }

            return typesPieces;
        }
        
    }
}
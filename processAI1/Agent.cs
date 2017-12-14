using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

namespace processAI1
{
    
    /**
    *
    * Représente l'agent intelligent
    *
    **/
    
public class Agent
{

    private Environement environement;
    
    public Agent()
    {
        environement = new Environement();
    }

    /* observerEnvironement: Capteurs de l'agent lui permettant de mettre à jour son état interne
    * 
    * @param tabVal: permet de savoir à qui appartient chaque pièce et son type
    * @param tabCoord: coordonnée sur le plateau
    * @return void
    */
    
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
                Console.WriteLine("J'ai détecté un " + piece.TypePiece + " qui m'appartient en position "  + piece.Position);
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

    
    /* choisirAction: Prise de décision de l'agent intelligent
    * 
    * @param aucun
    * @return ActionAgent: l'action à réaliser
    */
    
    public ActionAgent choisirAction()
    {
        // On détermine tout les coups possibles (ou alors on fait ça direcement dans le min max ?)

        CalculateurDeCoupPossible calculateurDeCoupPossible = new CalculateurDeCoupPossible();
        List<ActionAgent> coupsPossibles = new List<ActionAgent>();
        
        coupsPossibles = calculateurDeCoupPossible.getListCoupsPossibles(environement.MesPiecesList, environement.PiecesConcurentList);
        
        // Recherche du meilleur coup avec MinMax amélioré grâce au Alpha Beta Pruning
        MinMaxAlphaBeta minMaxAlphaBeta = new MinMaxAlphaBeta();
        
        // min max
        
        // Envoi du meilleur coup sélectionné
        ActionAgent action = new ActionAgent("a2", "a3"); // Contiendra l'action définie suite au résultat de min max
        return action;
    }
    
        
    /* intToTypePiece: Permet à l'agent de savoir quel est le type de la pièce en fonction de sa valeur
    * 
    * @param valeur: int positif ou négatif
    * @return ActionAgent: l'action à réaliser
    */

    private TypesPieces intToTypePiece(int valeur)
    {
        TypesPieces typesPieces;
        
        switch (Math.Abs(valeur))
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
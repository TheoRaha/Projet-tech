using UnityEngine;
using UnityEngine.UI;
using Heuristique.cs;
using System;

public class AlpBet : MonoBehaviour
{
    private int maxDepth = 3; // Profondeur maximale de l'algorithme Alpha-Beta

    public GameObject IA;

    public GameObject autreJoueur;
    // Méthode principale de l'IA
    public float AlphaBeta(Noeud n, float alpha, float beta, int profondeur)
    {
        if(profondeur == 0 || estFinJeu()){
            n.evaluer();
            // System.out.println(n.getH());
            // System.out.println(n);
            return new Coup(n.getH(), -1);
        }
        if(n.isMax()){
            int bestY = 0;
            for(int y = 0; y < n.getPositions().Length; y++){
                copiePositions(n.getPositions(), );
                if(jouer(n.isMax(), y, mBuffer)){ //n
                    Noeud successeur = new Noeud(IA, autreJoueur, !n.isMax(), );
                    Coup coup = alpha_beta(successeur, alpha, beta, profondeur-1);
                    if(coup.getEval() > alpha){
                        alpha = coup.getEval();
                        bestY = y;
                    }
                    if(alpha >= beta){
                        return new Coup(alpha, bestY);
                    }
                }
            }
            return new Coup(alpha, bestY);
        } else {
            int worstY = 0;
            for(int y = 0; y < n.getPositions().Length; y++){
                Vector3[] mBuffer = new Vector3[2];
                copiePositions(n.getPositions(), mBuffer);
                if(jouer(n.isMax(), y, mBuffer)){ //n
                    Noeud successeur = new Noeud(!n.isMax(), mBuffer);
                    Coup coup = alpha_beta(successeur, alpha, beta, profondeur-1);
                    if(coup.getEval() < beta){
                        beta = coup.getEval();
                        worstY = y;
                    }
                    if(alpha >= beta){
                        return new Coup(beta, worstY);
                    }
                }
            }
            return new Coup(beta, worstY);
        }
    }

    private Boolean estFinJeu(){
        return !(IA != null && autreJoueur != null);
    }

    private void copiePositions(Vector3[] positionsOG, Vector3[] newPositions){
        if(positionsOG.Length != newPositions.Length)
            return;
        for(int i = 0; i < positionsOG.Length; i++){
            newPositions[i] = positionsOG[i];
        }
    }

    private Boolean jouer(Boolean typeJoueur, Vector3 position){
        // TODO
        return true;
    }
}
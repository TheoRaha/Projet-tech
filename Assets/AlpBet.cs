using UnityEngine;
using UnityEngine.UI;
using System;

public class AlpBet : MonoBehaviour
{
    public int maxDepth = 3; // Profondeur maximale de l'algorithme Alpha-Beta

    public GameObject IA;

    public GameObject autreJoueur;
    // Méthode principale de l'IA
    public Coup AlphaBeta(Noeud n, double alpha, double beta, int profondeur)
    {
        if(profondeur == 0 || estFinJeu()){
            n.evaluer();
            // System.out.println(n.getH());
            // System.out.println(n);
            return new Coup(n.getH(), new Vector3(), new Vector3(), false);
        }
        if(n.isMax()){
            int bestY = 0;
            for(int y = 0; y < 4; y++){
                Vector3[] mBuffer = new Vector3[2];
                copiePositions(n.getPositions(), mBuffer);
                if(jouer(n.isMax(), y, mBuffer)){ //n
                    Noeud successeur = new Noeud(IA, autreJoueur, !n.isMax(), mBuffer);
                    Coup coup = AlphaBeta(successeur, alpha, beta, profondeur-1);
                    if(coup.getEval() > alpha){
                        alpha = coup.getEval();
                        bestY = y;
                    }
                    if(alpha >= beta){
                        if(n.isMax()){}
                            return new Coup(alpha, vecteurDeplacement(n.getPositions()[1], bestY), new Vector3(), false);
                    }
                }
            }
            return new Coup(alpha, vecteurDeplacement(n.getPositions()[1], bestY), new Vector3(), false);
        } else {
            int worstY = 0;
            for(int y = 0; y < 4; y++){
                Vector3[] mBuffer = new Vector3[2];
                copiePositions(n.getPositions(), mBuffer);
                if(jouer(n.isMax(), y, mBuffer)){ //n
                    Noeud successeur = new Noeud(IA, autreJoueur, !n.isMax(), mBuffer);
                    Coup coup = AlphaBeta(successeur, alpha, beta, profondeur-1);
                    if(coup.getEval() < beta){
                        beta = coup.getEval();
                        worstY = y;
                    }
                    if(alpha >= beta){
                        return new Coup(beta, vecteurDeplacement(n.getPositions()[1], worstY), new Vector3(), false);
                    }
                }
            }
            return new Coup(beta, vecteurDeplacement(n.getPositions()[1], worstY), new Vector3(), false);
        }
    }

    private bool estFinJeu(){
        return !(IA != null && autreJoueur != null);
    }

    private void copiePositions(Vector3[] positionsOG, Vector3[] newPositions){
        if(positionsOG.Length != newPositions.Length)
            return;
        for(int i = 0; i < positionsOG.Length; i++){
            newPositions[i] = positionsOG[i];
        }
    }

    private bool jouer(bool typeJoueur, int deplacement, Vector3[] positions){
        if(typeJoueur)
            positions[1] = vecteurDeplacement(positions[1],deplacement);
        else
            positions[0] = vecteurDeplacement(positions[0],deplacement);
        return true;
    }

    private Vector3 vecteurDeplacement(Vector3 origine, int y){
        switch(y){
            case 0:
                return new Vector3(origine.x+1, origine.y, origine.z);
            case 1:
                return new Vector3(origine.x-1, origine.y, origine.z);
            case 2:
                return new Vector3(origine.x, origine.y, origine.z+1);
            case 3:
                return new Vector3(origine.x, origine.y, origine.z-1);
        }
        return origine;
    }

    void Update()
    {
        Vector3[] pos = new Vector3[2];
        pos[1] = IA.transform.position;
        pos[0] = autreJoueur.transform.position;
        Noeud n = new Noeud(IA, autreJoueur, true, pos);
        Coup buffer = AlphaBeta(n, 0, 0, maxDepth);
        IA.transform.position.Set(buffer.getCoordonnee().x, buffer.getCoordonnee().y, buffer.getCoordonnee().z);
        UnityEngine.Debug.Log(buffer.getCoordonnee());
    }
}
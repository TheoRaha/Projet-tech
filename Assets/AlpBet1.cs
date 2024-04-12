using UnityEngine;
using System;

public class AlpBet1 : MonoBehaviour
{
    public int maxDepth = 3; // Profondeur maximale de l'algorithme Alpha-Beta
    public float speed = 5f;
    public GameObject IA;

    private float test = 0.6f;

    private double triggerShoot = 1020;

    public GameObject autreJoueur;

    CharacterController Cac;

    public Player player;

    public GameObject munition;

    public int balleChargeur = 3;

    public int vitesseBalle = 10;


    void Start(){
        Cac = GetComponent<CharacterController>();
        player = GetComponent<Player>();
    }
    // Méthode principale de l'IA
    public Coup AlphaBeta(Noeud1 n, double alpha, double beta, int profondeur)
    {
        if(profondeur == 0 || estFinJeu()){
            n.evaluer();
            // System.out.println(n.getH());
            // System.out.println(n);
            return new Coup(n.getH(), new Vector3(), new Vector3(), false);
        }
        if(n.isMax()){
            int bestY = 0;
            for(int y = 0; y < 8; y++){
                Vector3[] mBuffer = new Vector3[2];
                copiePositions(n.getPositions(), mBuffer);
                if(jouer(n.isMax(), y, mBuffer)){ //n
                    Noeud1 successeur = new Noeud1(IA, autreJoueur, !n.isMax(), mBuffer);
                    Coup coup = AlphaBeta(successeur, alpha, beta, profondeur-1);
                    if(coup.getEval() > alpha){
                        alpha = coup.getEval();
                        bestY = y;
                    }
                    //UnityEngine.Debug.Log(y + " : "+coup.getEval() + " / " + alpha + " & " + beta);
                    if(alpha >= beta){
                        return new Coup(alpha, vecteurDeplacement(n.getPositions()[1], bestY), new Vector3(), alpha>triggerShoot);
                    }
                }
            }
            return new Coup(alpha, vecteurDeplacement(n.getPositions()[1], bestY), new Vector3(), alpha>triggerShoot);
        } else {
            int worstY = 0;
            for(int y = 0; y < 8; y++){
                Vector3[] mBuffer = new Vector3[2];
                copiePositions(n.getPositions(), mBuffer);
                if(jouer(n.isMax(), y, mBuffer)){ //n
                    Noeud1 successeur = new Noeud1(IA, autreJoueur, !n.isMax(), mBuffer);
                    Coup coup = AlphaBeta(successeur, alpha, beta, profondeur-1);
                    if(coup.getEval() < beta){
                        beta = coup.getEval();
                        worstY = y;
                    }
                    //UnityEngine.Debug.Log(y + ":"+coup.getEval() + "/" + alpha + "&" + beta);
                    if(alpha >= beta){
                        return new Coup(beta, vecteurDeplacement(n.getPositions()[0], worstY), new Vector3(), beta>triggerShoot);
                    }
                }
            }
            return new Coup(beta, vecteurDeplacement(n.getPositions()[0], worstY), new Vector3(), beta>triggerShoot);
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
        Noeud1 debug = new Noeud1(IA, autreJoueur, false, positions);
        if(typeJoueur){
            if(debug.detectCollision(positions[1],vecteurDeplacement(positions[1],deplacement)))
                return false;
            positions[1] = vecteurDeplacement(positions[1],deplacement);
        }
        else{
            if(debug.detectCollision(positions[0],vecteurDeplacement(positions[0],deplacement)))
                return false;
            positions[0] = vecteurDeplacement(positions[0],deplacement);
        }
        return true;
    }

    private Vector3 vecteurDeplacement(Vector3 origine, int y){
        switch(y){
            case 0:
                return new Vector3(origine.x+this.test, origine.y, origine.z);
            case 1:
                return new Vector3(origine.x-this.test, origine.y, origine.z);
            case 2:
                return new Vector3(origine.x, origine.y, origine.z+this.test);
            case 3:
                return new Vector3(origine.x, origine.y, origine.z-this.test);
            case 4:
                return new Vector3(origine.x+this.test, origine.y, origine.z-this.test);
            case 5:
                return new Vector3(origine.x-this.test, origine.y, origine.z-this.test);
            case 6:
                return new Vector3(origine.x+this.test, origine.y, origine.z+this.test);
            case 7:
                return new Vector3(origine.x-this.test, origine.y, origine.z+this.test);
        }
        return origine;
    }

    void Update()
    {
        Vector3[] pos = new Vector3[2];
        if(IA != null && autreJoueur != null){
            pos[1] = IA.transform.position;
            pos[0] = autreJoueur.transform.position;
            Noeud1 n = new Noeud1(IA, autreJoueur, true, pos);
            Coup buffer = AlphaBeta(n, Double.MinValue, Double.MaxValue, maxDepth);
            Vector3 volonte = buffer.getCoordonnee()-IA.transform.position;
            //UnityEngine.Debug.Log(volonte);
            Vector3 mouvZ = new Vector3();
            if(Cac.isGrounded){
                mouvZ = new Vector3 (volonte.x, 0 ,volonte.z);
                
                //mouvZ = transform.TransformDirection(mouvZ);
                mouvZ *= speed;
            }
            IA.transform.LookAt(pos[0]);
            //transform.Rotate(Vector3.up * Input.GetAxis("Rotation"+playerID) * Time.deltaTime * speed * 100);
            Cac.Move(mouvZ*Time.deltaTime);
            if(buffer.getTir() && player.balleChargeur > 0)
            {
                Vector3 spawn = transform.position;
                spawn += transform.TransformDirection(0,0,1);
                GameObject balle = Instantiate(munition, spawn, Quaternion.identity) as GameObject;
                balle.GetComponent<Bullet>().owner = player.gameObject;
                player.balleChargeur--;
                balle.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward) * vitesseBalle;
                balle.GetComponent<Rigidbody>().rotation = Quaternion.LookRotation(Vector3.forward);
            }
        }
    }
}
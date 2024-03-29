using System;
using UnityEngine;

public class Noeud
{
    private Vector3[] positions;
    private Boolean max;

    private double h;

    public GameObject IA;

    public GameObject autreJoueur;

    public Noeud(GameObject IA, GameObject autreJoueur, Boolean max, Vector3[] positions){
        this.IA = IA;
        this.autreJoueur = autreJoueur;
        this.max = max;
        this.positions = positions;
    }

    public void evaluer(){
        Vector3 JA = positions[0];
        Vector3 JB = positions[1];
        double value = 0;
        if (!detectCollision(JA, JB))
            value += 1000;
        if(IA != null && autreJoueur != null){
            value += 1/Math.Sqrt((JA.x-JB.x)*(JA.x-JB.x)+(JA.z - JB.z)*(JA.z - JB.z))*100;
        }
        h = value;
    }

    public double getH(){
        return h;
    }

    public Boolean isMax(){
        return max;
    }

    public Vector3[] getPositions(){
        return positions;
    }

    public Boolean detectCollision(Vector3 JA, Vector3 JB){
        JA.y = 1;
        JB.y = 1;
        Vector3 direction = JB - JA;

        RaycastHit[] hits = Physics.RaycastAll(JA, direction, Vector3.Distance(JA, JB));
        //Debug.DrawRay(JA, direction, Color.green);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject != autreJoueur && hit.collider.gameObject != IA)
            {
                //Debug.DrawRay(JA, direction, Color.red);
                return true;
            }
        }
        return false;
    }
}
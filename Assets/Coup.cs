using UnityEngine;

public class Coup
{
    private double eval;
    private Vector3 coordonnee;

    private Vector3 rotation;

    private bool tir;

    public Coup(double h, Vector3 coord, Vector3 rotation, bool tir){
        eval = h;
        coordonnee = coord;
        this.rotation = rotation;
        this.tir = tir;
    }

    public Vector3 getCoordonnee() {
        return coordonnee;
    }
    
    public double getEval() {
        return eval;
    }

    public Vector3 getRotation(){
        return rotation;
    }

    public bool getTir(){
        return tir;
    }
}
using System.Numerics;

class Coup
{
    private int eval;
    private Vector3 coordonnee;

    private Vector3 rotation;

    private bool tir;

    public Coup(int val, Vector3 c, Vector3 rotation, bool tir){
        eval = val;
        coordonnee = c;
        this.rotation = rotation;
        this.tir = tir;
    }

    public Vector3 getCoordonnee() {
        return coordonnee;
    }
    
    public int getEval() {
        return eval;
    }
}
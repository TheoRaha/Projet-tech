using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Image Barre;

    private float max;

    public GameObject joueur;
    private float Valeur;
    public float valeur
    {
        get { return Valeur; }

        set
        {
            Valeur = Mathf.Clamp(value, 0, max);

            Barre.fillAmount = (1 / max) * Valeur;
        }
    }

    void Start()
    {
        Barre = GetComponent<Image>();
        max = joueur.GetComponent<Player>().vieJoueur;
    }
    void Update(){
        valeur = joueur.GetComponent<Player>().vieJoueur;
    }
}

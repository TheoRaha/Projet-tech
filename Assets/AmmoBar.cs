using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    Image Ammo;

    private float max;

    public GameObject joueur;
    private float Valeur;
    public float valeur
    {
        get { return Valeur; }

        set
        {
            Valeur = Mathf.Clamp(value, 0, max);

            Ammo.fillAmount = (1 / max) * Valeur;
        }
    }

    void Start()
    {
        Ammo = GetComponent<Image>();
        max = joueur.GetComponent<Player>().balleChargeur;
    }
    void Update(){
        valeur = joueur.GetComponent<Player>().balleChargeur;
    }
}
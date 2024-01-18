using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    Image Ammo;

    public string Id_Joueur;

    public float max;

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
        Barre = GetComponent<Image>();
        
    }
}
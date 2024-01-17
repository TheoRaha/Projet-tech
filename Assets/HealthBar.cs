using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Image Barre;

    public float max;

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
    }
}

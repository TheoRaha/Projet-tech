using UnityEngine;
using System.Collections;
//using UnityEngine.Windows;



public class Test : MonoBehaviour
{

    HealthBar barredevie;

    void Start()
    {
        barredevie = this.gameObject.GetComponent<HealthBar>();
        barredevie.max = 3;
        barredevie.valeur = 2;
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            barredevie.valeur -= 1;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            barredevie.valeur += 1;
        }
    }
}

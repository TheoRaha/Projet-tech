using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Heuristique : MonoBehaviour
{

    public GameObject joueur;

    public GameObject joueurAutre;

    public GameObject boiteTexte;

    private double value;
    // Start is called before the first frame update
    void Start()
    {
        boiteTexte.GetComponent<TMPro.TextMeshProUGUI>().text = "Setup Complete";
    }


    // Update is called once per frame
    void Update()
    {
        value = 0;
        if (!detectCollision(joueur, joueurAutre))
            value += 1000;
        if(joueur != null && joueurAutre != null){
            value += (1/Math.Sqrt((joueur.transform.position.x-joueurAutre.transform.position.x)*(joueur.transform.position.x-joueurAutre.transform.position.x)+(joueur.transform.position.z - joueurAutre.transform.position.z)*(joueur.transform.position.z - joueurAutre.transform.position.z)))*100;
        }
        boiteTexte.GetComponent<TMPro.TextMeshProUGUI>().text = value.ToString();
    }

    Boolean detectCollision(GameObject joueur, GameObject autreJoueur){
        Vector3 direction = autreJoueur.transform.position - joueur.transform.position;

        RaycastHit[] hits = Physics.RaycastAll(joueur.transform.position, direction, Vector3.Distance(joueur.transform.position, autreJoueur.transform.position));
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject != autreJoueur)
            {
                return true;
            }
        }
        return false;
    }
}

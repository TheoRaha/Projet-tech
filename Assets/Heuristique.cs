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
        if(joueur != null && joueurAutre != null){
            value = 0;
            if (!detectCollision(joueur, joueurAutre))
                value += 1000;
            if(joueur != null && joueurAutre != null){
                value += 1/Math.Sqrt((joueur.transform.position.x-joueurAutre.transform.position.x)*(joueur.transform.position.x-joueurAutre.transform.position.x)+(joueur.transform.position.z - joueurAutre.transform.position.z)*(joueur.transform.position.z - joueurAutre.transform.position.z))*100;
            }
            boiteTexte.GetComponent<TMPro.TextMeshProUGUI>().text = value.ToString();
        }
    }

    Boolean detectCollision(GameObject joueur, GameObject autreJoueur){
        Vector3 JA = joueur.transform.position;
        Vector3 JB = autreJoueur.transform.position;  
        JA.y = 1;
        JB.y = 1;
        Vector3 direction = JB - JA;

        RaycastHit[] hits = Physics.RaycastAll(JA, direction, Vector3.Distance(JA, JB));
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tire : MonoBehaviour
{
    public GameObject projectile;
    public int force = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Vector3 spawn = transform.position;
            spawn.z += 1;
            
            GameObject Bullet = Instantiate(projectile, spawn, Quaternion.identity) as GameObject;
            Bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward) * force;
        }
    }
}
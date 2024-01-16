using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public string playerID = "1";
    public float speed = 5f;
    private float gravity = 20f;
    private Vector3 mouvZ = Vector3.zero;
    CharacterController Cac;
    public GameObject projectile;
    public int force = 1;
    public int health = 3;
    public int bullet = 3;

    // Start is called before the first frame update
    void Start()
    {
        Cac = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Cac.isGrounded){
            mouvZ = new Vector3 (Input.GetAxis("Horizontal"+playerID), 0 ,Input.GetAxis("Vertical"+playerID));
            mouvZ = transform.TransformDirection(mouvZ);
            mouvZ *= speed;
        }
        mouvZ.y -= gravity * Time.deltaTime;
        transform.Rotate(Vector3.up * Input.GetAxis("Rotation"+playerID) * Time.deltaTime * speed * 100);
        Cac.Move(mouvZ*Time.deltaTime);
        if(Input.GetButtonDown("Fire"+playerID) && bullet > 0)
        {
            Vector3 spawn = transform.position;
            spawn += transform.TransformDirection(0,0,1);
            
            GameObject balle = Instantiate(projectile, spawn, Quaternion.identity) as GameObject;
            balle.GetComponent<Bullet>().owner = this.gameObject;
            bullet--;
            balle.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward) * force;
        }
        if(health == 0){
            Destroy(this.gameObject);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject bullet;
    public GameObject owner;
    private Vector3 startVelocity;
    public int health = 1;
    // Start is called before the first frame update
    void Start()
    {
        bullet = this.gameObject;
        while(bullet.GetComponent<Rigidbody>().velocity.magnitude == 0){}
        startVelocity = bullet.GetComponent<Rigidbody>().velocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision){
        UnityEngine.Debug.Log(collision.collider.tag);
        if(collision.collider.tag == "Wall")
            health--;
        if(collision.collider.tag == "Player"){
            health = 0;
            collision.collider.GetComponent<Player>().health--;
        }
        if(health == 0){
            Destroy(bullet);
            owner.GetComponent<Player>().bullet++;
        }
    }
}

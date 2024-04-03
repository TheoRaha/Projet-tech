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
    void FixedUpdate()
    {
        startVelocity = bullet.GetComponent<Rigidbody>().velocity;
    }

    void OnCollisionEnter(Collision collision){
        //UnityEngine.Debug.Log(collision.contacts[0].normal);
        UnityEngine.Debug.Log(collision.contacts[0]);
        health--;
        if(collision.collider.tag == "Wall"){
            GetComponent<Rigidbody>().velocity = Vector3.Reflect(startVelocity, collision.contacts[0].normal);
        } else if(collision.collider.tag == "Player"){
            health = 0;
            collision.collider.GetComponent<Player>().vieJoueur--;
        } else {
        }
        if(health <= 0){
            Destroy(bullet);
            owner.GetComponent<Player>().balleChargeur++;
        }
    }
}

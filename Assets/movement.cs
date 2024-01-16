using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class mouvement : MonoBehaviour
{
    public float speed = 5f;
    private float gravity = 20f;
    private Vector3 mouvZ = Vector3.zero;
    CharacterController Cac;

    // Start is called before the first frame update
    void Start()
    {
        Cac = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Cac.isGrounded){
            mouvZ = new Vector3 (Input.GetAxis("Horizontal"), 0 ,Input.GetAxis("Vertical"));
            mouvZ = transform.TransformDirection(mouvZ);
            mouvZ *= speed;
        }
        mouvZ.y -= gravity * Time.deltaTime;
        transform.Rotate(Vector3.up * Input.GetAxis("rotation") * Time.deltaTime * speed * 100);
        Cac.Move(mouvZ*Time.deltaTime);
    }
}
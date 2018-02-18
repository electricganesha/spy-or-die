using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disc : MonoBehaviour {

    public Rigidbody rb;
    private player owner;
    public float force;

    private float initvel;
    private float currvel;
    private bool shotCooldown = false;
    private float elapsedTime = 0.5f;
    private float time = 0.0f;
    private bool hasShot = false;

    private Vector3 dir;
    private Vector3 hitNormal;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }
	
	// Update is called once per frame
	void Update () {

        if(hasShot)
        {
            rb.AddForce(dir * force);
            hasShot = false;
            shotCooldown = true;
        }
        else
        {
            //rb.transform.position = owner.dirV * 100;
        }

        if(shotCooldown)
        {
            time += Time.deltaTime;
            
            if(time > elapsedTime)
            {
                initvel = rb.GetPointVelocity(new Vector3(0, 0, 0)).magnitude;
                shotCooldown = false;
                time = 0.0f;
            }
        }

        currvel = rb.GetPointVelocity(new Vector3(0, 0, 0)).magnitude;

        if ((initvel * 0.2f) > currvel)
        {
            rb.useGravity = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {        
        Ray a = new Ray( transform.position, dir );
        RaycastHit hit;
         
        Deflect(a, out hit);
        
        if(rb.useGravity == false)
        {
            dir = Vector3.Reflect(dir,hitNormal ) * 0.65f;
            
        }
        else
        {
            dir = Vector3.Reflect(dir,hitNormal) * 0.25f;
        }
        rb.AddForce(dir * force);
    }
    
    public void Owned(GameObject player)
    {
        owner = player.GetComponent<player>();
    }
    
    public void Shoot(Vector3 direction)
    {
        dir = direction;
        hasShot = true;
        
        Ray a = new Ray( transform.position, dir );
        RaycastHit hit;

        Deflect(a, out hit);
    }
    
    private void Deflect( Ray ray, out RaycastHit hit ) {
         
        if( Physics.Raycast(ray, out hit) ) {
            hitNormal = hit.normal;
        }
    }
}

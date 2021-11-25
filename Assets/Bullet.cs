using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   private Rigidbody2D rb;
   public float speed;
   public int damage = 20;
   public GameObject impactEffect;

   
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }


    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        health _health = hitInfo.GetComponent<health>();
        if(_health!= null)
        {
            _health.TakeDamage(damage);
        }
       
        GameObject clone = (GameObject) Instantiate(impactEffect, transform.position, transform.rotation);
        GameObject.Destroy(clone,1);
        Destroy(gameObject);
        
    }
}

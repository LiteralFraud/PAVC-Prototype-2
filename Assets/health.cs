using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{

    public int Health = 100;
    public Animator anim;
    public Healthbar healthbar;

    public void Start()
    {
        healthbar.SetMaxHealth(Health);
    }
    public void TakeDamage(int damage)
    {
        Health -= damage;
        healthbar.SetHealth(Health);
        if(Health<=0)
        {
            Die();
        }    
    }

    void Die()
    {
        FindObjectOfType<AudioManager>().Play("EnemyDeath");
        anim.SetBool("isdead", true);
        Destroy(gameObject,1);
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wepon : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bulletPrefab;
    public Animator anim;
    private float nextTimetoFire = 0f;
    private float numofTimesBulletFired = 0;
    private float numofTimesBulletFired2 = 0;


    private int maxAmmo = 5;
    private int currentAmmo;
    private float reloadTime = 3f;
    private bool isReloading = false;
    private bool isOverheating = false;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }
    void Update()
    {
        if(isReloading == true)
        {
            return;
        }
        if (isOverheating == true)
        {
            return;
        }
        if (currentAmmo<=0)
        {
            StartCoroutine(Reload());
            return;
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {    
            return;
        }
        if(Input.GetKeyDown(KeyCode.Space) && Time.time >= nextTimetoFire)
        {
           
            FindObjectOfType<AudioManager>().Play("BlasterSound");
            StartCoroutine(Shoot());
            nextTimetoFire = Time.time + 1 / 2;
        }
      
 
    }

    IEnumerator Reload()
    {
        isReloading = true;

        FindObjectOfType<AudioManager>().Play("Reloading");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
      
        yield return new WaitForSeconds(0.5f);
        isReloading = false;
    }
    IEnumerator Shoot()
    {
        numofTimesBulletFired += 1;
        numofTimesBulletFired2 += 1;
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        currentAmmo--;
        if(numofTimesBulletFired ==3)
        {
            FindObjectOfType<AudioManager>().Play("Flipping");
            anim.SetBool("isFlipping",true);
            yield return new WaitForSeconds(0.5f);
            anim.SetBool("isFlipping", false);
            numofTimesBulletFired = 0;
        }
        else if(numofTimesBulletFired2 == 8)
        {
            isOverheating = true;
            FindObjectOfType<AudioManager>().Play("Overheating");
            yield return new WaitForSeconds(6f);
            numofTimesBulletFired2 = 0;
            isOverheating = false;
        }
    }

}

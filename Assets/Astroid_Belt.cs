using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid_Belt : MonoBehaviour
{
    public GameObject astroidPrefab;
    public float respawnTime = 3f;
    private Vector2 screenBounds;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(astroidWave());
    }

    private void spawnObject()
    {
        GameObject astroid = Instantiate(astroidPrefab) as GameObject;
        astroid.transform.position = new Vector2(screenBounds.x*2,Random.Range(-screenBounds.y,screenBounds.y));
    }

    IEnumerator astroidWave()
    {
        while(true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnObject();
        }
    }
}

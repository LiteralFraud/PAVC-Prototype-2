using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Scrolling : MonoBehaviour
{
    private float speed = 7f;
    public float clamposs;
    private Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
    }

  
    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * speed, clamposs);
        transform.position = startPos + Vector3.left * newPosition;
        
    }
}

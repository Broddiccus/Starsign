using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floatyboy : MonoBehaviour
{
    public float distance = 700;
    private void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + Mathf.Sin(Time.time) / distance);
    }
    
}

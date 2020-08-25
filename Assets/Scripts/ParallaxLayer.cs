using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParallaxLayer : MonoBehaviour
{
    public float parallaxFactor;
    public bool yFollow;
    public int yMinus;
    public void Move(float delta, float deltay)
    {
        Vector3 newPos = transform.localPosition;
        newPos.x -= delta * parallaxFactor;
        if (yFollow)
            newPos.y = deltay - yMinus;
        transform.localPosition = newPos;
    }
}
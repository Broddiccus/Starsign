using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class daynight : MonoBehaviour
{
    public SpriteRenderer[] Day;
    public ParticleSystem[] Night;
    public bool endit;
    private void Update()
    {
        if (endit)
        {
            foreach (SpriteRenderer x in Day)
            {
                x.color = new Color(1, 1, 1, x.color.a + 0.01f);
            }
            foreach (ParticleSystem y in Night)
            {
                y.Stop();
        }
        }
    }
    public void Forward()
    {
        foreach(SpriteRenderer x in Day)
        {
            x.color = new Color(1, 1, 1, x.color.a + 0.02f);
        }
        foreach(ParticleSystem y in Night)
        {
            y.emissionRate -= 20 / 7;
        }
    }
    public void Back()
    {
        foreach (SpriteRenderer x in Day)
        {
            x.color = new Color(1, 1, 1, x.color.a - 0.02f);
        }
        foreach (ParticleSystem y in Night)
        {
            y.emissionRate += 20 / 7;
        }
    }
    public void FullDay()
    {
        endit = true;
        
    }
}

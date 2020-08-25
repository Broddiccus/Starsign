using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] music;
    private int currTrack = -1;
    public void Player(int newTrack)
    {
        if(currTrack != -1)
        {

            music[newTrack].timeSamples = music[currTrack].timeSamples;
            music[currTrack].Stop();
            music[newTrack].Play();
            currTrack = newTrack;
            return;
        }
        music[0].Play();
        currTrack = 0;
    }
}

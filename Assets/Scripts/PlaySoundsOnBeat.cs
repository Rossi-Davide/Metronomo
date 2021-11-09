using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundsOnBeat : MonoBehaviour
{
    
    public AudioClip _tap, _tick;

    public AudioSource tap, tick;

    public bool Power{set; get;}

    public bool Quarter{set; get;}
    public bool  Eigth{set; get;}
    public bool Sixteenth{ set; get;}

    //specifies whether we are using the standard subdivision (1/4) or not
    public bool Standard { set; get; }
     
    // Start is called before the first frame update
    void Start()
    {
        Power = false;
        Quarter = true;
        BPM.beatCountFull = 1;
        BPM.beatCountSub = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Power)
        {
            if (Standard)
            {
                if (BPM.BeatFull && BPM.beatCountFull == 2)
                {


                    tap.Play();
                }
                else if (BPM.BeatFull)
                {


                    tick.Play();
                }
            }
            else
            {
                if (BPM.BeatSubMultiple)
                {
                    tap.Play();
                }
            }
            /*if (Quarter)
            {
                
                
                if (BPM.BeatFull && BPM.beatCountFull == 2)
                {

                   
                    tap.Play();
                }
                else if (BPM.BeatFull)
                {
                    
                   
                    tick.Play();
                }

                
            }
            else if (Eigth)
            {
                
                if (BPM.BeatD8 && (BPM.beatCountD8 == 2 || BPM.beatCountD8 == 4|| BPM.beatCountD8 == 6|| BPM.beatCountD8 == 8))
                {
                    tap.Play();
                }
               
            }
            else if (Sixteenth)
            {
                
                if (BPM.BeatD16 && (BPM.beatCountD16 == 2 || BPM.beatCountD16 == 6 || BPM.beatCountD16 == 10 || BPM.beatCountD16 == 14))
                {

                    tap.Play();
                }
               
            }*/




        }
        else
        {
            BPM.beatCountFull = 1;
            BPM.beatCountSub = 1;
        }
        
        
    }

   
}

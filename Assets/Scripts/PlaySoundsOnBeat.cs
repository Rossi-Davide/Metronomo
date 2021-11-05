using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundsOnBeat : MonoBehaviour
{
    
    public AudioClip _tap, _tick;

    public AudioSource tap, tick;

    public bool power = false;

    public bool quarter = true, eigth, sixteenth; 
    // Start is called before the first frame update
    void Start()
    {
        BPM._beatCountFull = 1;
        BPM._beatCountD8 = 1;
        BPM._beatCountD16 = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (power)
        {
            if (quarter)
            {
                
                if (BPM._beatFull && BPM._beatCountFull == 2)
                {

                   
                    tap.Play();
                }
                else if (BPM._beatFull)
                {

                   
                    tick.Play();
                }
            }
            else if (eigth)
            {
                
                if (BPM._beatD8 && (BPM._beatCountD8 == 2 || BPM._beatCountD8 == 4|| BPM._beatCountD8 == 6|| BPM._beatCountD8 == 8))
                {
                    tap.Play();
                }
               
            }
            else if (sixteenth)
            {
                
                if (BPM._beatD16 && (BPM._beatCountD16 == 2 || BPM._beatCountD16 == 6 || BPM._beatCountD16 == 10 || BPM._beatCountD16 == 14))
                {

                    tap.Play();
                }
               
            }




        }
        else
        {
            BPM._beatCountFull = 1;
            BPM._beatCountD8 = 1;
            BPM._beatCountD16 = 1;
        }
        
        
    }
}

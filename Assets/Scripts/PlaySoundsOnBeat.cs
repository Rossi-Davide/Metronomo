using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundsOnBeat : MonoBehaviour
{
    
    public AudioClip _tap, _tick;

    public AudioSource tap, tick;

    public bool Power{set; get;}

   

    //specifies whether we are using the standard subdivision (1/4) or not
    public bool Standard { set; get; }


    public bool MeasuresSmallerThan1 { set; get; }
    public int CountTickEvery { set; get; }
    public int counterTickMul { set; get; }

    //counting the beat subdivisions, used to determine when to play sounds
    public int counterTickSub;
     
    // Start is called before the first frame update
    void Start()
    {
        Power = false;
        Standard = true;
        MeasuresSmallerThan1 = false;
        counterTickSub = 0;
        counterTickMul = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Power)
        {
            if (Standard)
            {
                if (BPM.BeatFull && BPM.BeatLengthCount == 2)
                {


                    tap.Play();
                }
                else if (BPM.BeatFull)
                {


                    tick.Play();
                }
            }
            else if (MeasuresSmallerThan1)
            {
                if(BPM.BeatFull && counterTickMul == 0)
                {
                    tick.Play();
                }


                if (BPM.BeatFull)
                {
                    counterTickMul++;

                    if (counterTickMul >= CountTickEvery)
                    {
                        counterTickMul = 0;
                    }
                }
               
                
            }
            else
            {
                

                if (BPM.BeatSubMultiple && BPM.BeatSubLengthCount == 2)
                {
                    tap.Play();
                    
                }
                else if(BPM.BeatSubMultiple && counterTickSub%BPM.Divisor == 0)
                {
                    
                    tick.Play();
                }

                if (BPM.BeatSubMultiple)
                {
                    
                    counterTickSub++;

                    if (counterTickSub >= (BPM.Divisor*4))
                    {
                        counterTickSub = 0;
                    }
                }


            }
            




        }
        else
        {
            BPM.ResetCounters();
        }
        
        
    }

   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{

    private const float seconds = 60;

    private int timeDivisions = 4;

    public AudioSource up, down;

    private double nextToPlay;

    private bool toggle = false;
    

    public void PlayMetronome(float bpms)
    {

        if (bpms <= 0)
        {
            throw new System.Exception("Bpms can't be less than zero");
        }

        float delay = seconds / bpms;

        //Debug.Log(delay);

        if(timeDivisions == 8)
        {

           delay= delay / 2;

        }else if(timeDivisions == 16)
        {
            delay = delay / 4;
        }

        //Debug.Log("called metronome");

        StartCoroutine(MetronomePlayer(delay));
        
    }

    public void InterruptMetronome(){

        StopAllCoroutines();
    }

    IEnumerator MetronomePlayer(float time)
    {
        while (true)
        {
            up.Play();
            yield return new WaitForSeconds(time);

            /*for (int i = 0; i < timeDivisions-1; i++)
            {
                down.Play();
                yield return new WaitForSeconds(time);

            }*/
        }

       
    }


    private void Start()
    {
        nextToPlay = AudioSettings.dspTime + 2.0f;
    }

   /* private void Update()
    {
        double time = AudioSettings.dspTime;

        if (time + 1.0f > nextToPlay)
        {
            // We are now approx. 1 second before the time at which the sound should play,
            // so we will schedule it now in order for the system to have enough time
            // to prepare the playback at the specified time. This may involve opening
            // buffering a streamed file and should therefore take any worst-case delay into account.

            if (!toggle)
            {
                Debug.Log("Up");

                up.PlayScheduled(nextToPlay);
                up.SetScheduledEndTime(nextToPlay + 0.5);

            }
            else
            {
                Debug.Log("Down");

                down.PlayScheduled(nextToPlay);
                down.SetScheduledEndTime(nextToPlay + 0.5);
            }
            //Debug.Log("Scheduled source " + flip + " to start at time " + nextEventTime);

            // Place the next event 16 beats from here at a rate of 140 beats per minute
            nextToPlay += 60.0f / 150 ;

            toggle = !toggle;
        }
    }*/

}

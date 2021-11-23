using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Experimental.Rendering.Universal;

public class GameManager : MonoBehaviour
{

    float bpmMetronome= -1;

    //text input field for bpm
    public GameObject bpmLabel;

    public BPM _bpm;

    public PlaySoundsOnBeat soundPlayer;

    public Transform lightSwitches;

    public Transform[] positions;

    public GameObject quarter, eigth, sixteeth;

    public GameObject[] quarterLights;

    public GameObject[] eightLights;

    public GameObject[] sixteenLights;

    public  TextMeshProUGUI errorText;

    public Animator errorTextAnim;

    private int light4Counter, light8Counter, light16Counter;


    private void Start()
    {
        quarter.SetActive(true);
        eigth.SetActive(false);
        sixteeth.SetActive(false);
    }


    


    private void Update()
    {
        /*if (soundPlayer.Power)
        {
            if (soundPlayer.Quarter)
            {
                if (BPM.BeatFull)
                {
                    quarterLights[light4Counter].SetActive(true);

                    if(light4Counter == 0)
                    {
                        quarterLights[quarterLights.Length - 1].SetActive(false);
                    }
                    else
                    {
                        quarterLights[light4Counter - 1].SetActive(false);
                    }

                    light4Counter++;

                    if (light4Counter >= quarterLights.Length)
                    {
                        light4Counter = 0;
                    }

                }
            }else if (soundPlayer.Eigth)
            {
                if (BPM.BeatD8)
                {
                    eightLights[light8Counter].SetActive(true);

                    if (light8Counter == 0)
                    {
                        eightLights[eightLights.Length - 1].SetActive(false);
                    }
                    else
                    {
                        eightLights[light8Counter - 1].SetActive(false);
                    }

                    light8Counter++;

                    if (light8Counter >= eightLights.Length)
                    {
                        light8Counter = 0;
                    }
                }
            }
            else
            {
                if (BPM.BeatD16)
                {
                    sixteenLights[light16Counter].SetActive(true);

                    if (light16Counter == 0)
                    {
                        sixteenLights[sixteenLights.Length - 1].SetActive(false);
                    }
                    else
                    {
                        sixteenLights[light16Counter - 1].SetActive(false);
                    }

                    light16Counter++;

                    if (light16Counter >= sixteenLights.Length)
                    {
                        light16Counter = 0;
                    }
                }
            }
        }  */  
    }


    //method called by the bpms slider
    public void SliderSetBPM(float bpm)
    {

        bpmMetronome = bpm;
        bpmLabel.GetComponent<TMP_InputField>().text = bpmMetronome.ToString();

    }

    //method called by the bpms input text field
    public void InputTextFieldSetBPM(string bpm)
    {
       
        try{

            if(string.IsNullOrEmpty(bpm)){

                throw new Exception("The bpm field cannot be empty");
            }

            try
            {
                bpmMetronome = float.Parse(bpm);
            }catch(Exception ex)
            {
                throw new Exception("Can't use decimal values or strings as bpm");
            }


            if (bpmMetronome > 200)
            {
                bpmMetronome = 200;
                bpmLabel.GetComponent<TMP_InputField>().text = bpmMetronome.ToString();

                throw new Exception("Can't use values bigger than 200");

            }

            if (bpmMetronome< 0)
            {
                bpmMetronome = 0;
                bpmLabel.GetComponent<TMP_InputField>().text = bpmMetronome.ToString();

                throw new Exception("Can't use values lower than 0");

            }
        }catch(Exception ex){

            ThrowError(ex);

        }
        

    }

    #region Buttons 5
    public void UpToFive()
    {

        bpmMetronome += 5;
        bpmLabel.GetComponent<TMP_InputField>().text = bpmMetronome.ToString();
        StartMetronome();

    }

    public void DownToFive()
    {
        bpmMetronome -= 5;
        bpmLabel.GetComponent<TMP_InputField>().text = bpmMetronome.ToString();
        StartMetronome();

    }

    #endregion

    public void StartMetronome()
    {
        try
        {
            
            //No parameter ever specified
            if (bpmMetronome == -1)
            {
                throw new Exception("You haven't set a parameter in the bpm field");
            }

            _bpm.Bpm = bpmMetronome;

            soundPlayer.Power = true;

            
            
        }catch(Exception ex)
        {
            ThrowError(ex);
        }
    }

    public void InterruptMetronome()
    {
        try
        {
            soundPlayer.Power = false;

            PerformShutdown();

            ResetCountersLights();
        }
        catch(Exception ex)
        {
            ThrowError(ex);
        }
    }

    

    //general method used to display error messages on the screen
    private void ThrowError(Exception ex)
    {
        errorText.text = ex.Message;
        errorTextAnim.Play("ErrorAnimation");
    }


    #region NoteTransition switching methods
   /* public void OneToFour()
    {
        lightSwitches.position = positions[0].position;

        soundPlayer.Standard = true;



        quarter.SetActive(true);
        eigth.SetActive(false);
        sixteeth.SetActive(false);

        PerformShutdown();

        ResetCountersLights();

        BPM.ResetCounters();
        BPM.Divisor = 1;
    }

    public void OneToEight()
    {
        lightSwitches.position = positions[1].position;

        soundPlayer.Standard = false;


        quarter.SetActive(false);
        eigth.SetActive(true);
        sixteeth.SetActive(false);

        PerformShutdown();

        ResetCountersLights();

        BPM.ResetCounters();
        BPM.Divisor = 2;
    }

    public void OneToSixteen()
    {
        lightSwitches.position = positions[2].position;

        soundPlayer.Standard = false;


        quarter.SetActive(false);
        eigth.SetActive(false);
        sixteeth.SetActive(true);

        PerformShutdown();

        ResetCountersLights();

        BPM.ResetCounters();
        BPM.Divisor = 4;
    }*/

    public void NoteDuration(float division)
    {
        try
        {
            BPM.Divisor = division;
            if (division == 1)
            {
                soundPlayer.Standard = true;
            }
            else
            {
                soundPlayer.Standard = false;
            }

            BPM.ResetCounters();
            soundPlayer.counterTickSub = 0;
        }
        catch(Exception ex)
        {
            ThrowError(ex);
        }
        
    }


    //resets counters of all the blocks in the scene
    private void ResetCountersLights()
    {
        light4Counter = 0;
        light8Counter = 0;
        light16Counter = 0;
    }

    


    //shuts down every light in the scene
    private void PerformShutdown()
    {
        foreach(GameObject g in quarterLights)
        {
            g.SetActive(false);
        }

        foreach(GameObject g in eightLights)
        {
            g.SetActive(false);
        }

        foreach(GameObject g in sixteenLights)
        {
            g.SetActive(false);
        }
    }

    #endregion
}


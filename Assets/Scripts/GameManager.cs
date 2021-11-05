using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Experimental.Rendering.Universal;
public class GameManager : MonoBehaviour
{

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




    public void SetBPM(float bpm)
    {

        bpmLabel.GetComponent<TMP_InputField>().text = bpm.ToString();

    }

    public void UpToFive()
    {

        bpmLabel.GetComponent<TMP_InputField>().text = (_bpm.bpm+5).ToString();
        StartMetronome();

    }

    public void DownToFive()
    {

        bpmLabel.GetComponent<TMP_InputField>().text = (_bpm.bpm - 5).ToString();
        StartMetronome();

    }

    public void StartMetronome()
    {
        try
        {
            string bpmString = bpmLabel.GetComponent<TMP_InputField>().text;

            float bpms = 0;

            if (string.IsNullOrEmpty(bpmString))
            {
                throw new Exception("Empty pharameter in the bpm field");
            }


            try
            {
                bpms = float.Parse(bpmString);
            }catch(Exception ex)
            {
                throw new Exception("Can't use decimal values or strings as bpm");
            }


            if (bpms > 200)
            {
                bpmLabel.GetComponent<TMP_InputField>().text = "200";

                throw new Exception("Can't use values bigger than 200");

            }

            if (bpms < 0)
            {
                bpmLabel.GetComponent<TMP_InputField>().text = "0";

                throw new Exception("Can't use values lower than 0");

            }


            _bpm.bpm = bpms;

            soundPlayer.power = true;
            
        }catch(Exception ex)
        {
            ThrowError(ex);
        }
    }

    public void InterruptMetronome()
    {
        try
        {
            soundPlayer.power = false;

            PerformShutdown();

            ResetCountersLights();
        }
        catch(Exception ex)
        {
            ThrowError(ex);
        }
    }

    private void Start()
    {
        quarter.SetActive(true);
        eigth.SetActive(false);
        sixteeth.SetActive(false);
    }


    private void ThrowError(Exception ex)
    {
        errorText.text = ex.Message;
        errorTextAnim.Play("ErrorAnimation");
    }


    public void OneToFour()
    {
        lightSwitches.position = positions[0].position;

        soundPlayer.quarter = true;
        soundPlayer.eigth = false;
        soundPlayer.sixteenth = false;


        quarter.SetActive(true);
        eigth.SetActive(false);
        sixteeth.SetActive(false);

        PerformShutdown();

        ResetCountersLights();

        BPM._beatCountFull = 1;
        BPM._beatCountD8 = 1;
        BPM._beatCountD16 = 1;
    }

    public void OneToEight()
    {
        lightSwitches.position = positions[1].position;

        soundPlayer.quarter = false;
        soundPlayer.eigth = true;
        soundPlayer.sixteenth = false;


        quarter.SetActive(false);
        eigth.SetActive(true);
        sixteeth.SetActive(false);

        PerformShutdown();

        ResetCountersLights();

        BPM._beatCountFull = 1;
        BPM._beatCountD8 = 1;
        BPM._beatCountD16 = 1;
    }

    public void OneToSixteen()
    {
        lightSwitches.position = positions[2].position;

        soundPlayer.quarter = false;
        soundPlayer.eigth = false;
        soundPlayer.sixteenth = true;


        quarter.SetActive(false);
        eigth.SetActive(false);
        sixteeth.SetActive(true);

        PerformShutdown();

        ResetCountersLights();

        BPM._beatCountFull = 1;
        BPM._beatCountD8 = 1;
        BPM._beatCountD16 = 1;
    }


    private void ResetCountersLights()
    {
        light4Counter = 0;
        light8Counter = 0;
        light16Counter = 0;
    }

    private void Update()
    {
        if (soundPlayer.power)
        {
            if (soundPlayer.quarter)
            {
                if (BPM._beatFull)
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
            }else if (soundPlayer.eigth)
            {
                if (BPM._beatD8)
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
                if (BPM._beatD16)
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
        }

       
    }


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
}


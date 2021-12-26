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
    public GameObject bpmLabel,timeLabel;

    public BPM _bpm;

    public PlaySoundsOnBeat soundPlayer;

    public Transform lightSwitches;

    public Transform[] positions;

   

    public  TextMeshProUGUI errorText;

    public Animator errorTextAnim;

    public GameObjectSpawner spawner;


    public GameObject[] luciBlocchi;

    private int lightCounter;

    private void Start()
    {
      
        BPM.BeatLength = 4;
        BPM.Divisor = 1;
       
        lightCounter = 0;
        BPM.ResetCounters();
        RebuildBlockMap();

    }


    


    private void Update()
    {
        if (soundPlayer.Power)
        {
            if (soundPlayer.Standard)
            {
                if (BPM.BeatFull)
                {
                    luciBlocchi[lightCounter].SetActive(true);

                    if (lightCounter == 0)
                    {
                        luciBlocchi[luciBlocchi.Length - 1].SetActive(false);
                    }
                    else
                    {
                        luciBlocchi[lightCounter - 1].SetActive(false);
                    }

                    lightCounter++;

                    if (lightCounter == luciBlocchi.Length)
                    {
                        lightCounter = 0;
                    }
                }
            }
            else if (soundPlayer.MeasuresSmallerThan1)
            {
                if (BPM.BeatFull)
                {
                    if (soundPlayer.CountTickEvery == 2)
                    {
                        if (soundPlayer.counterTickMul % 2 == 0)
                        {
                            PerformShutdown();
                        }
                    }
                    else
                    {
                        if (soundPlayer.counterTickMul % 4 == 0)
                        {

                            PerformShutdown();
                        }
                    }

                    luciBlocchi[lightCounter].SetActive(true);

                    

                    lightCounter++;

                    if (lightCounter == luciBlocchi.Length)
                    {
                        lightCounter = 0;
                    }
                }
            }else
            {
                if (BPM.BeatSubMultiple)
                {
                    luciBlocchi[lightCounter].SetActive(true);

                    if (lightCounter == 0)
                    {
                        luciBlocchi[luciBlocchi.Length - 1].SetActive(false);
                    }
                    else
                    {
                        luciBlocchi[lightCounter - 1].SetActive(false);
                    }

                    lightCounter++;

                    if (lightCounter == luciBlocchi.Length)
                    {
                        lightCounter = 0;
                    }
                }
            }
        }  
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
            PerformShutdown();
            //No parameter ever specified
            if (bpmMetronome == -1)
            {
                throw new Exception("You haven't set a parameter in the bpm field");
            }

            lightCounter = 0;

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
            lightCounter = 0;

            PerformShutdown();

           
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
  


    public void InputTimeField(string time)
    {
        try
        {
            int timeInt = int.Parse(time);

            BPM.BeatLength = timeInt;

            RebuildBlockMap();
        }catch(Exception ex)
        {
            ThrowError(ex);
        }
    }

    public void NoteDuration(float division)
    {
        try
        {
            if (division < 1)
            {
                BPM.Divisor = 1;
                soundPlayer.MeasuresSmallerThan1 = true;
                soundPlayer.Standard = false;

                if (division == 0.5f)
                {
                    soundPlayer.CountTickEvery = 2;
                }
                else
                {
                    soundPlayer.CountTickEvery = 4;
                }
                
                
            }
            else
            {
                soundPlayer.MeasuresSmallerThan1 = false;
                soundPlayer.Standard = true;

                BPM.Divisor = division;

            }


            if (division == 1)
            {
                soundPlayer.Standard = true;
            }
            else
            {
                soundPlayer.Standard = false;
            }

            BPM.ResetCounters();
            RebuildBlockMap();
            PerformShutdown();
            lightCounter = 0;
            soundPlayer.counterTickSub = 0;
          
        }
        catch(Exception ex)
        {
            ThrowError(ex);
        }
        
    }


    public void TimeDivision(int beatLength)
    {
        try
        {
            //call from custom time
            if (beatLength == -1)
            {

                beatLength = int.Parse(timeLabel.GetComponent<TMP_InputField>().text);


            }

            BPM.BeatLength = beatLength;
            lightCounter = 0;
            BPM.ResetCounters();
            RebuildBlockMap();
            PerformShutdown();
        }
        catch (Exception ex)
        {
            ThrowError(ex);
        }
    } 

    


    //shuts down every light in the scene
    private void PerformShutdown()
    {
        foreach(GameObject g in luciBlocchi)
        {
            g.SetActive(false);
        }
    }

    #endregion



    private void RebuildBlockMap()
    {
        spawner.DestroyMap();
        spawner.BuildMap();
        luciBlocchi = new GameObject[GameObjectSpawner.instantiatedObjects.Count];

        for (int i = 0; i< luciBlocchi.Length;i++)
        {
            luciBlocchi[i] = GameObjectSpawner.instantiatedObjects[i].transform.Find("blockLight").gameObject;
            luciBlocchi[i].SetActive(false);
        }
        lightCounter = 0;
    }
}


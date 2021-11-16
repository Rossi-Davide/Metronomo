using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BPM : MonoBehaviour
{
    private static BPM bpmInstance;

    //current bpm used by the program
    private float _bpmSystem;

    private const int secondsInAMinute = 60;
    
    public float Bpm{

        set
        {
            if (value < 0)
            {
                throw new Exception("Bpm can't be less than 0");
            }

            if (value > 200)
            {
                throw new Exception("Bpm can't be greater than 200");
            }

            _beatInterval = secondsInAMinute / value;

            

            _bpmSystem = value;
        }

        get{ return _bpmSystem; }
    }

    //the duration of a tick in seconds
    private static float _beatInterval;

    //timer
    //updated as the time goes on
    private float _beatTimer;

    //submultiples
    private float _beatSubTimer;

    private static float _beatSubInterval;

    //boolean that triggers the sound to play
    public static bool BeatFull{set; get;}
    public static bool BeatSubMultiple { set; get; }

    //divisor
    //The divisor defines the beat subdivision
    private static int _divisor= 1;
    public static int Divisor
    {
        set
        {
            if (value < 1)
            {
                throw new Exception("Can't divide by numbers lower than 1");
            }
            _divisor = value;

            _beatSubInterval = _beatInterval / Divisor;
            _beatLengthCount = 1;
            _beatSubLengthCount = 1;
        }

        get
        {
            return _divisor;
        }
    }




    //counts the quarters of a beat and its submultiples
    private static int _beatLengthCount, _beatSubLengthCount;

    public static int BeatLengthCount
    {
        get
        {
            return _beatLengthCount;
        }
    }

    public static int BeatSubLengthCount
    {
        get
        {
            return _beatSubLengthCount;
        }
    }

   



    private int _beatLength = 4;


    public int BeatLength
    {
        set
        {
            if(value <= 0)
            {
                throw new Exception("The number of notes in a beat must be 1 or greater");
            }

            _beatLength = value;
        }

        get
        {
            return _beatLength;
        }

    }



    //singleton
    private void Awake()
    {
        if (bpmInstance != null && bpmInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            bpmInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }




    // Update is called once per frame
    void Update()
    {
       
            BeatDetection();
        
       
    }

    public static void ResetCounters()
    {
        _beatLengthCount = 1;
        _beatSubLengthCount = 1;
    }

    void BeatDetection()
    {

        
        BeatFull = false;
        
        _beatTimer += Time.deltaTime;

        //genereates a tick
        if(_beatTimer>= _beatInterval)
        {
            _beatTimer -= _beatInterval;
            BeatFull = true;

            

            _beatLengthCount++;
           
          
            if (_beatLengthCount > BeatLength)
            {
                _beatLengthCount = 1;
            }
        }



        BeatSubMultiple = false;
        
        _beatSubTimer += Time.deltaTime;

        if(_beatSubTimer>= _beatSubInterval)
        {
            _beatSubTimer -= _beatSubInterval;
            BeatSubMultiple = true;
            _beatSubLengthCount++;

            if (_beatSubLengthCount > Divisor*BeatLength)
            {
                _beatSubLengthCount = 1;
            }

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPM : MonoBehaviour
{
    private static BPM bpmInstance;

    private float _bpmSystem;
    
    public float Bpm{

        set
        { 
            _beatInterval = 60 / value;
        _beatIntervalD8 = _beatInterval / 2;
        _beatIntervalD16 = _beatInterval / 4;

        _bpmSystem = value;
        }

        get{ return _bpmSystem; }
    }

    //the duration of a tick in seconds
    private float _beatInterval;

    //timers and submultiples
    private float _beatTimer, _beatIntervalD8, _beatTimerD8, _beatIntervalD16,_beatTimerD16;

    public static bool BeatFull{set; get;}
    public static bool BeatD8{set; get;} 
    public static bool BeatD16{set; get;}

    public static int beatCountFull, beatCountD8,beatCountD16;

    
    //singleton
    private void Awake()
    {
        if(bpmInstance != null && bpmInstance != this)
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

    void BeatDetection()
    {

        
        BeatFull = false;
        
        _beatTimer += Time.deltaTime;

        //genereates a tick
        if(_beatTimer>= _beatInterval)
        {
            _beatTimer -= _beatInterval;
            BeatFull = true;

            

            beatCountFull++;
           
          
            if (beatCountFull > 4)
            {
                beatCountFull = 1;
            }
        }

        BeatD8 = false;
        
        _beatTimerD8 += Time.deltaTime;

        if(_beatTimerD8>= _beatIntervalD8)
        {
            _beatTimerD8 -= _beatIntervalD8;
            BeatD8 = true;
            beatCountD8++;

            if (beatCountD8 > 8)
            {
                beatCountD8 = 1;
            }

        }

        BeatD16 = false;
        
        _beatTimerD16 += Time.deltaTime;

        if (_beatTimerD16 >= _beatIntervalD16)
        {
            _beatTimerD16 -= _beatIntervalD16;
            BeatD16 = true;
            beatCountD16++;

            if (beatCountD16 > 16)
            {
                beatCountD16 = 1;
            }

        }
    }
}

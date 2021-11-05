using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPM : MonoBehaviour
{
    private static BPM bpmInstance;

    
    public float bpm;

    private float beatInterval, beatTimer, beatIntervalD8, beatTimerD8, beatIntervalD16,beatTimerD16;

    public static bool _beatFull, _beatD8,_beatD16;

    public static int _beatCountFull, _beatCountD8,_beatCountD16;

    

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
            BeatDetection();
        
       
    }

    void BeatDetection()
    {
        _beatFull = false;
        beatInterval = 60 / bpm;
        beatTimer += Time.deltaTime;

        if(beatTimer>= beatInterval)
        {
            beatTimer -= beatInterval;
            _beatFull = true;

            

            _beatCountFull++;
           
          
            if (_beatCountFull > 4)
            {
                _beatCountFull = 1;
            }
        }

        _beatD8 = false;
        beatIntervalD8 = beatInterval / 2;
        beatTimerD8 += Time.deltaTime;

        if(beatTimerD8>= beatIntervalD8)
        {
            beatTimerD8 -= beatIntervalD8;
            _beatD8 = true;
            _beatCountD8++;

            if (_beatCountD8 > 8)
            {
                _beatCountD8 = 1;
            }

        }

        _beatD16 = false;
        beatIntervalD16 = beatInterval / 4;
        beatTimerD16 += Time.deltaTime;

        if (beatTimerD16 >= beatIntervalD16)
        {
            beatTimerD16 -= beatIntervalD16;
            _beatD16 = true;
            _beatCountD16++;

            if (_beatCountD16 > 16)
            {
                _beatCountD16 = 1;
            }

        }
    }
}

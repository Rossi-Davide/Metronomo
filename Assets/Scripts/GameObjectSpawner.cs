using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSpawner : MonoBehaviour
{

    public Transform low;
    public Transform high;

    public GameObject spawner;
    public GameObject prefab;

    public void BuildMap()
    {
        int nCubes = BPM.BeatLength;

        int nSpaces = nCubes + 1;

        float totalX =high.position.x- low.position.x;
        float totalY = high.position.y - low.position.y;

        float spaceXCubes = prefab.GetComponent<Transform>().position.x * nCubes;

        float spaceAvailableX = totalX - spaceXCubes;

        float lengthSingleSpace = spaceAvailableX / nSpaces; 
    }
}

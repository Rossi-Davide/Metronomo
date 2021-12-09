using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameObjectSpawner : MonoBehaviour
{

    public RectTransform low;
    public RectTransform high;
    public Transform canvasParent;

    public GameObject spawner;
    public GameObject prefab;

    public static List<GameObject> instantiatedObjects;


    private void Start()
    {
        instantiatedObjects = new List<GameObject>();
    }

    public void BuildMap()
    {
        int nCubes = BPM.BeatLength;

        int nSpaces = nCubes + 1;

        float totalX =high.anchoredPosition.x- low.anchoredPosition.x;
        float totalY = high.anchoredPosition.y - low.anchoredPosition.y;

        float spaceXCubes =  nCubes;

        float spaceAvailableX = totalX - spaceXCubes;

        float lengthSingleSpace = spaceAvailableX / nSpaces;

        float divisionsPerCube = BPM.Divisor;

       

        spawner.GetComponent<RectTransform>().anchoredPosition = low.anchoredPosition;

        Vector2 vector;

        if (divisionsPerCube >= 1)
        {
            vector = new Vector2(0, (totalY / (divisionsPerCube*2)));
        }
        else
        {
            vector = new Vector2(0, (totalY / 2));

            divisionsPerCube = 1;
        }

       

        spawner.GetComponent<RectTransform>().anchoredPosition += vector;

        Vector2 singleSpace = new Vector2(lengthSingleSpace,0);

        Vector2 height = new Vector2(0,totalY/divisionsPerCube);

        Transform prefabTransform = prefab.GetComponent<Transform>();

        Transform blockChild = prefabTransform.Find("block");

        Vector3 blockChildNewScale = new Vector3(1/divisionsPerCube, blockChild.localScale.y, blockChild.localScale.z);

        blockChild.localScale = blockChildNewScale;

        for(int i= 0; i<nCubes; i++)
        {
            spawner.GetComponent<RectTransform>().anchoredPosition += singleSpace;

            if(divisionsPerCube != 1)
            {
                for (int j = 0; j < divisionsPerCube; j++)
                {
                    instantiatedObjects.Add(Instantiate(prefab, spawner.GetComponent<Transform>().position, prefab.GetComponent<Transform>().rotation, canvasParent));

                    spawner.GetComponent<RectTransform>().anchoredPosition += height;
                }



                spawner.GetComponent<RectTransform>().anchoredPosition -= (height * divisionsPerCube);
            }
            else
            {
                instantiatedObjects.Add(Instantiate(prefab, spawner.GetComponent<Transform>().position, prefab.GetComponent<Transform>().rotation, canvasParent));
            }
            


        }

        
    }

    public void DestroyMap()
    {
        foreach(GameObject g in instantiatedObjects)
        {
            Destroy(g);
            
        }

        instantiatedObjects.Clear();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Experimental.Rendering.Universal;


public class GameObjectSpawner : MonoBehaviour
{

    public RectTransform low;
    public RectTransform high;
    public Transform canvasParent;

    public GameObject spawner;
    public GameObject[] prefabs;

    public static List<GameObject> instantiatedObjects;


    private void Start()
    {
        instantiatedObjects = new List<GameObject>();
        
    }

    public void BuildMap()
    {
        
        //math for positions
        int nCubes = BPM.BeatLength;

        int nSpaces = nCubes + 1;

        float totalX =high.anchoredPosition.x- low.anchoredPosition.x;
        float totalY = high.anchoredPosition.y - low.anchoredPosition.y;

        float spaceXCubes =  nCubes;

        float spaceAvailableX = totalX - spaceXCubes;

        float lengthSingleSpace = spaceAvailableX / nSpaces;

        float divisionsPerCube = BPM.Divisor;

       
        //position spawner
        spawner.GetComponent<RectTransform>().anchoredPosition = low.anchoredPosition;

        //distance
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

       
        //position at height of first cube
        spawner.GetComponent<RectTransform>().anchoredPosition += vector;


        //measures necessary to move the spawner
        Vector2 singleSpace = new Vector2(lengthSingleSpace,0);

        Vector2 height = new Vector2(0,totalY/divisionsPerCube);

        //resetting block scale

        for(int i=0; i<prefabs.Length;i++)
        {
            Transform prefabTransform = prefabs[i].GetComponent<Transform>();

            Transform blockChild = prefabTransform.Find("block");

            Vector3 blockChildNewScale = new Vector3(1 / divisionsPerCube, blockChild.localScale.y, blockChild.localScale.z);

            blockChild.localScale = blockChildNewScale;

            if(divisionsPerCube >= 8)
            {
                Transform lightT = prefabTransform.Find("blockLight");

                Light2D light = lightT.GetComponent<Light2D>();

                light.pointLightOuterRadius = 2f;
            }
        }


        System.Random random = new System.Random();

        //spawning cycle
        for (int i= 0; i<nCubes; i++)
        {
            spawner.GetComponent<RectTransform>().anchoredPosition += singleSpace;

            

            


            if(divisionsPerCube != 1)
            {
                for (int j = 0; j < divisionsPerCube; j++)
                {
                    int blockType = random.Next(0, 14);

                    instantiatedObjects.Add(Instantiate(prefabs[blockType], spawner.GetComponent<Transform>().position, prefabs[blockType].GetComponent<Transform>().rotation, canvasParent));

                    
                    

                    if (j >= 1)
                    {
                        
                        if (instantiatedObjects[instantiatedObjects.Count - 1].name == instantiatedObjects[instantiatedObjects.Count - 2].name)
                        {
                            
                            Destroy(instantiatedObjects[instantiatedObjects.Count - 1]);
                            instantiatedObjects.Remove(instantiatedObjects[instantiatedObjects.Count - 1]);
                            spawner.GetComponent<RectTransform>().anchoredPosition -= height;
                            
                            j--;
                        }
                        
                    }

                    spawner.GetComponent<RectTransform>().anchoredPosition += height;


                }



                spawner.GetComponent<RectTransform>().anchoredPosition -= (height * divisionsPerCube);
            }
            else
            {
                

                int blockType = random.Next(0, 14);

                instantiatedObjects.Add(Instantiate(prefabs[blockType], spawner.GetComponent<Transform>().position, prefabs[blockType].GetComponent<Transform>().rotation, canvasParent));

                

                if (i >= 1)
                {
                    if (instantiatedObjects[instantiatedObjects.Count - 1].name == instantiatedObjects[instantiatedObjects.Count - 2].name)
                    {
                        
                        Destroy(instantiatedObjects[instantiatedObjects.Count - 1]);
                        instantiatedObjects.Remove(instantiatedObjects[instantiatedObjects.Count - 1]);
                        spawner.GetComponent<RectTransform>().anchoredPosition -= singleSpace;

                        i--;
                    }
                }

                

                

              
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

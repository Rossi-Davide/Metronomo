using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

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
        Transform prefabTransform = prefab.GetComponent<Transform>();

        Transform blockChild = prefabTransform.Find("block");

        Vector3 blockChildNewScale = new Vector3(1/divisionsPerCube, blockChild.localScale.y, blockChild.localScale.z);

        blockChild.localScale = blockChildNewScale;


        //spawning cycle
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
                //creates new sorting layer
                //solved at runtime, affects editor properties that cannot be stored after execution
                /*SerializedObject tagsAndLayersManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
                SerializedProperty sortingLayersProp = tagsAndLayersManager.FindProperty("m_SortingLayers");
                sortingLayersProp.InsertArrayElementAtIndex(sortingLayersProp.arraySize);
                var newlayer = sortingLayersProp.GetArrayElementAtIndex(sortingLayersProp.arraySize - 1);
                newlayer.FindPropertyRelative("uniqueID").intValue = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
                newlayer.FindPropertyRelative("name").stringValue = "my new layer";
                tagsAndLayersManager.ApplyModifiedProperties();*/

                instantiatedObjects.Add(Instantiate(prefab, spawner.GetComponent<Transform>().position, prefab.GetComponent<Transform>().rotation, canvasParent));

                GameObject blockGameobject =instantiatedObjects[instantiatedObjects.Count - 1].GetComponent<Transform>().Find("block").gameObject;

                

                //blockGameobject.GetComponent<SpriteRenderer>().sortingLayerName = newlayer.name.ToString();
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

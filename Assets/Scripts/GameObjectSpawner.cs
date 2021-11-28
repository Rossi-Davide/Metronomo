using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSpawner : MonoBehaviour
{

    public RectTransform low;
    public RectTransform high;
    public Transform canvasParent;

    public GameObject spawner;
    public GameObject prefab;

    private List<GameObject> instanciatedObjects;


    private void Start()
    {
        instanciatedObjects = new List<GameObject>();
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

        spawner.GetComponent<RectTransform>().anchoredPosition = low.anchoredPosition;

        Vector2 vector = new Vector2(0, (totalY / 2));

        spawner.GetComponent<RectTransform>().anchoredPosition += vector;

        Vector2 singleSpace = new Vector2(lengthSingleSpace,0);

        for(int i= 0; i<nCubes; i++)
        {

            spawner.GetComponent<RectTransform>().anchoredPosition += singleSpace;
            ;

            instanciatedObjects.Add(Instantiate(prefab, spawner.GetComponent<Transform>().position, prefab.GetComponent<Transform>().rotation, canvasParent));

        }

        
    }

    public void DestroyMap()
    {
        foreach(GameObject g in instanciatedObjects)
        {
            Destroy(g);
            instanciatedObjects.Remove(g);
        }
    }
}

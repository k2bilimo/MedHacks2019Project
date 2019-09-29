using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public bool forwardDirection;
    public List<int> BackTrackList = new List<int>();
    public ObjectScript[] ObjectList;
    public Renderer rend;    
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        //we set the object to be hidden.
        // Then we just set the subsequent item to be visible.
        if (forwardDirection)
        {
            if (gameObject.tag.Contains("1"))
            {
                ObjectList[0].GetComponent<Renderer>().enabled = false;
                ObjectList[1].GetComponent<Renderer>().enabled = true;

                
            }
            else if (gameObject.tag.Contains("2"))
            {
                ObjectList[1].GetComponent<Renderer>().enabled = false;
                ObjectList[2].GetComponent<Renderer>().enabled = true;
            }
            else if (gameObject.tag.Contains("3"))
            {
                ObjectList[2].GetComponent<Renderer>().enabled = false;
                ObjectList[3].GetComponent<Renderer>().enabled = true;
            }
            else
            {
                ObjectList[3].GetComponent<Renderer>().enabled = false;
                forwardDirection = false;
            }
        }
        else
        {
            // this is the backward direction, on each we track the list
            if (gameObject.tag.Contains("1"))
            {
                BackTrackList.Add(1);
            }
            else if (gameObject.tag.Contains("2"))
            {
                BackTrackList.Add(2);
            }
            else if (gameObject.tag.Contains("3"))
            {
                BackTrackList.Add(3);
            }
            else
            {
                if (gameObject.tag.Contains("4"))
                {

                    BackTrackList.Add(4);
                }
            }
        }
    }

    private void Start()
    {
        ObjectList = FindObjectsOfType(typeof(ObjectScript)) as ObjectScript[];
        forwardDirection = true;

    }
    private void Update()
    {
       

    }
}

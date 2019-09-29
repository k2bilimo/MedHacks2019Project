using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Scoring : MonoBehaviour
{
    public ObjectScript[] objlist;
    public static bool forwardDirection;
    public static List<int> BackTrackList = new List<int>();
    public static ObjectScript[] ObjectList;
    public static bool cylinder1Visited = false;
    public static bool cylinder2Visited = false;
    public static bool cylinder3Visited = false;
    public static bool cylinder4Visited = false;
    public int score = 0;
    // Start is called before the first frame update
    public static void AfterCylinderChanged(GameObject gameObject)
    {
        //we set the object to be hidden.
        // Then we just set the subsequent item to be visible.

        if (forwardDirection)
        {
            print(gameObject.tag.ToString());

            if (gameObject.tag.ToString().Equals("Cylinder 1") && !cylinder1Visited)
            {
                ObjectList[0].GetComponent<Renderer>().enabled = false;
                ObjectList[0].GetComponent<Renderer>().GetComponentInChildren<Renderer>().enabled = false;
                cylinder1Visited = true;
                ObjectList[1].GetComponent<Renderer>().enabled = true;
                print("Activated #2");


            }
            else if (gameObject.tag.ToString().Equals("Cylinder 2") && !cylinder2Visited)
            {
                print("Attempt to activate #3");
                ObjectList[1].GetComponent<Renderer>().enabled = false;
                ObjectList[1].GetComponent<Renderer>().GetComponentInChildren<Renderer>().enabled = false;
                cylinder2Visited = true;
                ObjectList[2].GetComponent<Renderer>().enabled = true;
                print("Activated #3");
            }
            else if (gameObject.tag.ToString().Equals("Cylinder 3") && !cylinder3Visited)
            {
                ObjectList[2].GetComponent<Renderer>().enabled = false;
                //ObjectList[2].GetComponent<Renderer>().GetComponentInChildren<Renderer>().enabled = false;
                cylinder3Visited = true;
                ObjectList[3].GetComponent<Renderer>().enabled = true;
                print("Activated #4");
            }
            else if (!cylinder4Visited && gameObject.tag.ToString().Equals("Cylinder 4"))
            {
                ObjectList[3].GetComponent<Renderer>().enabled = false;
                ObjectList[3].GetComponent<Renderer>().GetComponentInChildren<Renderer>().enabled = false;
                cylinder4Visited = true;
                forwardDirection = false;
                BackTrackList.Add(4);
                print("You must now go back to 4-3-2-1");
            }
        }
        else
        {
            // this is the backward direction, on each we track the list
            if (gameObject.tag.ToString().Equals("Cylinder 1") && BackTrackList[BackTrackList.Count - 1] != 1)
            {
                print("Hit 1 On the way back");
                BackTrackList.Add(1);
                foreach (int i in BackTrackList)
                {
                    print(i);
                }
            }
            else if (gameObject.tag.ToString().Equals("Cylinder 2") && BackTrackList[BackTrackList.Count - 1] != 2)
            {
                print("Hit 2 On the way back");
                BackTrackList.Add(2);
            }
            else if (gameObject.tag.ToString().Equals("Cylinder 3") && BackTrackList[BackTrackList.Count - 1] != 3)
            {
                print("Hit 3 On the way back");
                BackTrackList.Add(3);
            }
            else
            {
                if (gameObject.tag.ToString().Equals("Cylinder 4") && BackTrackList[BackTrackList.Count - 1] != 4)
                {
                    print("Hit 4 On the way back");
                    BackTrackList.Add(4);
                }
            }
        }
    }

    private void Start()
    {
        ObjectList = objlist;
        foreach (ObjectScript obj in objlist)
        {
            print(obj.tag.ToString());
        }
        ObjectList[1].GetComponent<Renderer>().enabled = false;
        ObjectList[2].GetComponent<Renderer>().enabled = false;
        ObjectList[3].GetComponent<Renderer>().enabled = false;
        forwardDirection = true;
        print("We've started");
        print(forwardDirection);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) print("Escape Clicked");
        if (Input.GetKeyDown(KeyCode.Escape) && !forwardDirection)
        // We've believed we're by area 1. 
        {
            bool completed = true;
            int correctGuesses = 0;
            // Two ways to calculate
            // 1. Accuracy of Array List.
            // 2. How far you are from the 1. Destination.
            if (BackTrackList.Count <= 4)
            {
                if (BackTrackList.Count != 4) completed = false;
                for (int i = 0; i < BackTrackList.Count; ++i)
                {
                    if (BackTrackList[i] != 4 - i)
                    {
                        completed = false;
                        break;
                    }
                    ++correctGuesses;
                }
                if (completed) score += 10;
                else score += (correctGuesses * 2); // most you can get is 6. 2 correct points for every correct one you hit. 
            }
            else
            { // BackTrackList.count > 4. We have a loop like top, if we stop at a certain correct guesses, the number of incorrect guesses
              //are backtracklist.count - correct guesses. Then do max((2 * correctGuesses) - (3 * incorrectGuesses), 0)
              
              for(int i = 0; i <4; ++i)
                {
                    if(BackTrackList[i] != 4 - i)
                    {
                        completed = false;
                        break;
                    }
                    ++correctGuesses;
                    if (completed) score += max(10 - ((BackTrackList.Count - correctGuesses) * 3), 0);
                    else score += max((correctGuesses * 2)-(BackTrackList.Count * 3), 0);
                }

            }
            print(score);
            print(BackTrackList.ToString());
            // EditorApplication.Exit(0);
            EditorApplication.ExitPlaymode();
            Application.Quit();
        }
    }
    public int max(int a, int b)
    {
        return a > b ? a : b;
    }
}
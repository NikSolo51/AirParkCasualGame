using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleManager : MonoBehaviour
{
    public List<GameObject> peoples = new List<GameObject>();
    public List<Transform> activePeoples = new List<Transform>();
    public int activepeople;
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            peoples.Add(transform.GetChild(i).gameObject);
        }
    }

    private void Update()
    {
        activepeople = 0;
        for (int i = 0; i < peoples.Count; i++)
        {
            if (peoples[i].activeSelf == true)
            {
                if (!activePeoples.Contains(peoples[i].transform))
                {
                    activePeoples.Add(peoples[i].transform);
                }
                
                activepeople++;
            }
            else
            {
                if (activePeoples.Contains(peoples[i].transform))
                {
                    activePeoples.RemoveAt(i);
                }
            }
                
        }
    }
}

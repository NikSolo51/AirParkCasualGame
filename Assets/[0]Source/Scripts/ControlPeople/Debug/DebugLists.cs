using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DebugLists : MonoBehaviour
{
    public List<Transform> debugList;
    public List<Vector3> ValueList = new List<Vector3>();
    public int Queue;
    private void Update()
    {
        debugList = ControlPeople.ControlPeopleBehaviour.LinkList;
        ValueList = ControlPeople.ControlPeopleBehaviour.ListOfDictionariesOfPointsAndPeople[Queue].Values.ToList();
        for (int i = 0; i < ControlPeople.ControlPeopleBehaviour.peopleDictionary.Count; i++)
        {
            ControlPeople.ControlPeopleBehaviour.localPeoplePosition[i] =
                ControlPeople.ControlPeopleBehaviour.peopleDictionary.ElementAt(i).Value.localPosition;
        }
        Queue = ControlPeople.ControlPeopleBehaviour.queue;
    }
}

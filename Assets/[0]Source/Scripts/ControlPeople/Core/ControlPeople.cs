using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[BurstCompile]
public class ControlPeople : MonoBehaviour
{
    public static readonly ControlPeopleBehaviour ControlPeopleBehaviour = new ControlPeopleBehaviour();
    
    public List<Transform> debugList;
    public List<Vector3> ValueList = new List<Vector3>();
    
    public int Queue;

    public float TimeToClear = 1;
    public Text ClearTime;
    
    public static ControlPeople Instance;


    private void Start()
    {
        debugList = ControlPeopleBehaviour.LinkList;
        ValueList = ControlPeopleBehaviour.ListOfDictionariesOfPointsAndPeople[Queue].Values.ToList();
    }

    private void Update()
    {
        float.TryParse(ClearTime.text, out TimeToClear);

        for (int i = 0; i < ControlPeople.ControlPeopleBehaviour.peopleDictionary.Count; i++)
        {
            ControlPeopleBehaviour.localPeoplePosition[i] =
               ControlPeopleBehaviour.peopleDictionary.ElementAt(i).Value.localPosition;
        }

        Queue = ControlPeopleBehaviour.queue;

        ControlPeopleBehaviour.MoveToPoint();

        if (Input.GetMouseButtonUp(0))
        {
            if (CreatePointsAtMousePosition.Instance.CanICreatePointInThisPlace())
            {
                Clearing.Instance.AddToClear(ControlPeopleBehaviour.ListOfDictionariesOfPointsAndPeople[Queue], TimeToClear);
                ControlPeople.ControlPeopleBehaviour.AddPointForMoveToPoint(
                    ControlPeople.ControlPeopleBehaviour.ListOfDictionariesOfPointsAndPeople[Queue]);
                ControlPeople.ControlPeopleBehaviour.IncrementQueue();
                ControlPeople.ControlPeopleBehaviour.ClearIfQueueEqualsZero();
            }
        }

        Clearing.Instance.Tick();
    }
    
}
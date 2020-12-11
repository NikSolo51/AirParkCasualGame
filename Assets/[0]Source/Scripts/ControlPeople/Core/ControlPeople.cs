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
    public static ControlPeople Instance;
    public static readonly ControlPeopleBehaviour ControlPeopleBehaviour = new ControlPeopleBehaviour();
    public static readonly MovePeopleToPointInDictionary MovePeopleToPointInDictionary = new MovePeopleToPointInDictionary();
    public Clearing Clearing = new Clearing();
    public float TimeToClear = 1;
    public List<Transform> debug = new List<Transform>();
    
    private void Update()
    {
        
        MovePeopleToPointInDictionary.MoveToPoint();
        debug = ControlPeopleBehaviour.peopleDictionary.Values.ToList();
        if (Input.GetMouseButtonUp(0))
        {
            if (CreatePointsAtMousePosition.Instance.CanICreatePointInThisPlace())
            {
                Clearing.Instance.AddToClear(ControlPeopleBehaviour.ListOfDictionariesOfPointsAndPeople[ControlPeopleBehaviour.queue], TimeToClear);
                
                MovePeopleToPointInDictionary.AddPointForMoveToPoint(
                    ControlPeople.ControlPeopleBehaviour.ListOfDictionariesOfPointsAndPeople[ControlPeopleBehaviour.queue]);
                
                ControlPeople.ControlPeopleBehaviour.IncrementQueue();
                ControlPeople.ControlPeopleBehaviour.ClearIfQueueEqualsZero();
            }
        }

        Clearing.Instance.Tick();
    }
    
}
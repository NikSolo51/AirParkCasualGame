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
    public readonly ControlPeopleBehaviour controlPeopleBehaviour = new ControlPeopleBehaviour();
    public readonly MovePeopleToPointInDictionary movePeopleToPointInDictionary = new MovePeopleToPointInDictionary();

    [SerializeField] private CreatePointsAtMousePosition _createPointsAtMousePosition;

    public float timeToClear = 0.1f;

    private Clearing clearing;

    private void Start()
    {
        clearing = new Clearing();
        clearing.controlPeople = this;
        clearing._createPointsAtMousePosition = _createPointsAtMousePosition;
        controlPeopleBehaviour._controlPeople = this;
        controlPeopleBehaviour._createPointsAtMousePosition = _createPointsAtMousePosition;
    }

    private void Update()
    {
        movePeopleToPointInDictionary.MoveToPoint();
        if (Input.GetMouseButtonUp(0))
        {
            if (_createPointsAtMousePosition.CanICreatePointInThisPlace())
            {
                clearing.AddToClear(
                    controlPeopleBehaviour.ListOfDictionariesOfPointsAndPeople[controlPeopleBehaviour.queue],
                    timeToClear);

                movePeopleToPointInDictionary.AddPointForMoveToPoint(
                    controlPeopleBehaviour.ListOfDictionariesOfPointsAndPeople[controlPeopleBehaviour.queue]);

                controlPeopleBehaviour.IncrementQueue();
                controlPeopleBehaviour.ClearIfQueueEqualsZero();
            }
        }

        clearing.Tick();
    }
}
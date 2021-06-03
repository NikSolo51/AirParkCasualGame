using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[System.Serializable]
public class TransformVectors
{
    public List<Transform> transformsList;
    public List<Vector3> vectorsList;
}

[BurstCompile]
public class ControlPeople : MonoBehaviour
{
    public float timeToClear = 0.1f;
    public readonly ControlPeopleBehaviour controlPeopleBehaviour = new ControlPeopleBehaviour();
    public readonly MovePeopleToPointInDictionary movePeopleToPointInDictionary = new MovePeopleToPointInDictionary();
    //************************
    public TransformVectors[] ListOfDictionariesPointsAndPeopleInspector = new TransformVectors[10];
    //************************
    [SerializeField] private CreatePointsAtMousePosition _createPointsAtMousePosition;
    [SerializeField] private Clearing clearing;
    
    private void Start()
    {
        controlPeopleBehaviour._controlPeople = this;
        controlPeopleBehaviour._createPointsAtMousePosition = _createPointsAtMousePosition;
        for (int i = 0; i < controlPeopleBehaviour.ListOfDictionariesPointsAndPeople.Count; i++)
        {
            movePeopleToPointInDictionary.PeopleAndPointsList.Add(new Dictionary<Transform, Vector3>());
        }
    }

    private void Update()
    {
        DebugDictories();
        controlPeopleBehaviour.CheckPeopleTransformList();
        
        
        movePeopleToPointInDictionary.MoveToPoint();
        
        if (Input.GetMouseButtonUp(0))
        {
            if (_createPointsAtMousePosition.CanICreatePointInThisPlace())
            {
                clearing.AddToClear(
                    controlPeopleBehaviour.ListOfDictionariesPointsAndPeople[controlPeopleBehaviour.queue],
                    timeToClear);

                movePeopleToPointInDictionary.AddPointForMoveToPoint(
                    controlPeopleBehaviour.ListOfDictionariesPointsAndPeople[controlPeopleBehaviour.queue]);

                controlPeopleBehaviour.queue++;
                
                if (controlPeopleBehaviour.queue == controlPeopleBehaviour.ListOfDictionariesPointsAndPeople.Count)
                    controlPeopleBehaviour.queue = 0;
            }
        }

        clearing.Tick();
    }

    public void DebugDictories()
    {
        for (int i = 0; i < controlPeopleBehaviour.ListOfDictionariesPointsAndPeople.Count; i++)
        {
            ListOfDictionariesPointsAndPeopleInspector[i].transformsList
                = controlPeopleBehaviour.ListOfDictionariesPointsAndPeople[i].Select(kvp => kvp.Key)
                    .ToList();
        
            ListOfDictionariesPointsAndPeopleInspector[i].vectorsList
                = controlPeopleBehaviour.ListOfDictionariesPointsAndPeople[i].Select(kvp => kvp.Value)
                    .ToList();
        }

    }
}
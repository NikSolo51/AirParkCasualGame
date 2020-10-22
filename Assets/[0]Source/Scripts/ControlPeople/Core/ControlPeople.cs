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
    public static ControlPeopleBehaviour controlPeopleBehaviour = new ControlPeopleBehaviour();
    public List<Transform> debugList;
    private static GameObject BufferGameObject;
    public GameObject bufferGameobj;
    public MouseInput MouseInput;
    public int Queue;
    private List<Dictionary<Transform, Vector3>> DictionaryList = new List<Dictionary<Transform, Vector3>>();
    private List<float> TimerList = new List<float>();
    public List<Vector3> ValueList = new List<Vector3>();
    public float TimeToClear = 1;
    public Text ClearTime;
    
   
  
    private void Start()
    {
        MouseInput = new MouseInput();
        controlPeopleBehaviour.GetMouseInput(MouseInput);
        BufferGameObject = bufferGameobj;
    }
    
    private void Update()
    {
        float.TryParse(ClearTime.text, out TimeToClear);
        for (int i = 0; i < ControlPeople.controlPeopleBehaviour.peopleDictionary.Count; i++)
        {
            ControlPeople.controlPeopleBehaviour.localPeoplePosition[i] =
                ControlPeople.controlPeopleBehaviour.peopleDictionary.ElementAt(i).Value.localPosition;
        }
        Queue = controlPeopleBehaviour.queue;
    
        MouseInput.Tick();
        //if(CreatePointsAtMousePosition.generate == false)
            controlPeopleBehaviour.MoveToPoint();
        debugList = controlPeopleBehaviour.LinkList;
        ValueList = controlPeopleBehaviour.ListOfDictionariesOfPointsAndPeople[Queue].Values.ToList();
        
        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log("MousePressed");
            if (CreatePointsAtMousePosition.Instance.CanICreatePointInThisPlace())
            {
              // Debug.Log("MousePressedInReal");
                AddToClearQueue(controlPeopleBehaviour.ListOfDictionariesOfPointsAndPeople[Queue], TimeToClear);
               
            }
        }

        Clear();

    }

    public void AddToClearQueue(Dictionary<Transform, Vector3> dic, float timeToDelete)
    {
        
        if(DictionaryList.Contains(dic))
            return;
        if(dic.Keys.Count == 0)
            return;
        
       // Debug.Log("Add");
        
        ControlPeople.controlPeopleBehaviour.AddPointForMoveToPoint(controlPeopleBehaviour.ListOfDictionariesOfPointsAndPeople[Queue]);
        ControlPeople.controlPeopleBehaviour.IncrementQueue();
        ControlPeople.controlPeopleBehaviour.ClearIfQueueEqualsZero();
        
        DictionaryList.Add(dic);
        TimerList.Add(timeToDelete);
        
    }

    public void Clear()
    {
        if(!controlPeopleBehaviour.HaveAllPeopleReachedThePoints())
            return;
        
        if(Input.GetMouseButton(0))
            return;
        
        if(DictionaryList == null || TimerList == null)
            return;
        

            for (int i = 0; i < DictionaryList.Count; i++)
        {
            if (DictionaryList[i].Keys.Count != 0)
            {
                if(TimerList[i] > -Mathf.Epsilon)
                TimerList[i] -= Time.deltaTime;
                
                if (TimerList[i] <= 0)
                {
                   
                    for (int j = 0; j < CreatePointsAtMousePosition.CoordinatesList.Count; j++)
                    {
                        //Debug.Log(CreatePointsAtMousePosition.CoordinatesList.Count);
                        //Debug.Log(DictionaryList.Count);
                        
                            foreach (var dic in DictionaryList.ElementAt(i))
                            {
                                if(CreatePointsAtMousePosition.CoordinatesList.Count <= 0)
                                    continue;
                               // Debug.Log("CoordList: " + CreatePointsAtMousePosition.CoordinatesList[j]);
                                if (dic.Value == CreatePointsAtMousePosition.CoordinatesList[j])
                                {
                                    CreatePointsAtMousePosition.CoordinatesList.RemoveAt(j);
                                }
                            }
                    }
                    
                    for (int j = 0; j < CreatePointsAtMousePosition.Instance.PointsAfterMouseUp.Count; j++)
                    {

                        
                            foreach (var dic in DictionaryList.ElementAt(i))
                            {
                                if(CreatePointsAtMousePosition.Instance.PointsAfterMouseUp.Count <= 0)
                                    continue;
                                if (dic.Value == CreatePointsAtMousePosition.Instance.PointsAfterMouseUp[j])
                                {
                                    CreatePointsAtMousePosition.Instance.PointsAfterMouseUp.RemoveAt(j);
                                }
                            }
                        
                    }
                    
                    
                    for (int j = 0; j < CreatePointsAtMousePosition.Instance.PointsAfterMouseUp.Count; j++)
                    {
                        
                            foreach (var dic in  DictionaryList.ElementAt(i))
                            {
                                if(CreatePointsAtMousePosition.CoordinatesList.Count <= 0)
                                    continue;
                               //Debug.Log("PointAfterMouseUp: " +  CreatePointsAtMousePosition.Instance.PointsAfterMouseUp[j]);
                                if(dic.Value.Equals(CreatePointsAtMousePosition.Instance.PointsAfterMouseUp[j]))
                                        CreatePointsAtMousePosition.Instance.PointsAfterMouseUp.RemoveAt(j);
                            }
                        
                    }
                    
                    for (int j = 0; j < CreatePointsAtMousePosition.debugObjects.Count; j++)
                    {
                        
                            foreach (var dic in  DictionaryList.ElementAt(i))
                            {
                                if(CreatePointsAtMousePosition.debugObjects.Count != 0 )
                               // Debug.Log("DebugObjects: " +CreatePointsAtMousePosition.debugObjects.Count);
                                if (dic.Value.Equals(CreatePointsAtMousePosition.debugObjects[j].transform.localPosition))
                                {
                                    Destroy(CreatePointsAtMousePosition.debugObjects[j]);
                                    CreatePointsAtMousePosition.debugObjects.RemoveAt(j);
                                }
                            }
                        
                        
                    }
                    
                    ControlPeople.controlPeopleBehaviour.DictionaryList.RemoveAt(i);
                    DictionaryList[i].Clear();
                    TimerList.RemoveAt(i);
                    DictionaryList.RemoveAt(i);
                    Debug.Log("Clear");
                }
                   
            }

            
        }
    }
   
    
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Clearing : MonoBehaviour
{
      public static Clearing Instance = new Clearing();
    private readonly List<Dictionary<Transform, Vector3>> DictionaryList = new List<Dictionary<Transform, Vector3>>();
    private readonly List<float> TimerList = new List<float>();

    public void Tick()
    {
        Clear();
    }

    public void AddToClear(Dictionary<Transform, Vector3> dic, float timeToDelete)
    {
        if (DictionaryList.Contains(dic))
            return;
        if (dic.Keys.Count == 0)
            return;
        
        DictionaryList.Add(dic);
        TimerList.Add(timeToDelete);
    }
    
     private void Clear()
    {
        if (!ControlPeople.MovePeopleToPointInDictionary.HaveAllPeopleReachedThePoints())
            return;

        if (Input.GetMouseButton(0))
            return;

        if (DictionaryList == null || TimerList == null)
            return;


        for (int i = 0; i < DictionaryList.Count; i++)
        {
            if (DictionaryList[i].Keys.Count != 0)
            {
                if (TimerList[i] > -Mathf.Epsilon)
                    TimerList[i] -= Time.deltaTime;

                if (TimerList[i] <= 0)
                {
                    RemovingPointsFromTheCoordinateSheetIfTheyAreFoundInTheDictionary(i);
                    
                    RemovingVisualizationSpheresIfTheyAreFoundInTheDictionary(i);

                    ControlPeople.MovePeopleToPointInDictionary.DictionaryList.RemoveAt(i);
                    DictionaryList[i].Clear();
                    TimerList.RemoveAt(i);
                    DictionaryList.RemoveAt(i);
                }
            }
        }
    }

     private void RemovingPointsFromTheCoordinateSheetIfTheyAreFoundInTheDictionary(int i)
     {
         for (int j = 0; j < CreatePointsAtMousePosition.Instance.coordinatesList.Count; j++)
         {
             foreach (var dic in DictionaryList.ElementAt(i))
             {
                 if (CreatePointsAtMousePosition.Instance.coordinatesList.Count != 0)

                     if (dic.Value == CreatePointsAtMousePosition.Instance.coordinatesList[j])
                     {
                         CreatePointsAtMousePosition.Instance.coordinatesList.RemoveAt(j);
                     }
             }
         }
     }

     private void RemovingVisualizationSpheresIfTheyAreFoundInTheDictionary(int i)
     {
         for (int j = 0; j < CreatePointsAtMousePosition.Instance.debugObjects.Count; j++)
         {
             foreach (var dic in DictionaryList.ElementAt(i))
             {
                 if (CreatePointsAtMousePosition.Instance.debugObjects.Count != 0)

                     if (dic.Value.Equals(CreatePointsAtMousePosition.Instance.debugObjects[j].transform
                         .localPosition))
                     {
                         Destroy(CreatePointsAtMousePosition.Instance.debugObjects[j]);
                         CreatePointsAtMousePosition.Instance.debugObjects.RemoveAt(j);
                     }
             }
         }
     }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Clearing : MonoBehaviour
{
    public ControlPeople controlPeople;
    public CreatePointsAtMousePosition _createPointsAtMousePosition;
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
        if (!controlPeople.movePeopleToPointInDictionary.HaveAllPeopleReachedThePoints())
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

                    controlPeople.movePeopleToPointInDictionary.DictionaryList.RemoveAt(i);
                    DictionaryList[i].Clear();
                    TimerList.RemoveAt(i);
                    DictionaryList.RemoveAt(i);
                }
            }
        }
    }

    private void RemovingPointsFromTheCoordinateSheetIfTheyAreFoundInTheDictionary(int i)
    {
        for (int j = 0; j < _createPointsAtMousePosition.coordinatesList.Count; j++)
        {
            foreach (var dic in DictionaryList.ElementAt(i))
            {
                if (_createPointsAtMousePosition.coordinatesList.Count != 0)

                    if (dic.Value == _createPointsAtMousePosition.coordinatesList[j])
                    {
                        _createPointsAtMousePosition.coordinatesList.RemoveAt(j);
                    }
            }
        }
    }

    private void RemovingVisualizationSpheresIfTheyAreFoundInTheDictionary(int i)
    {
        for (int j = 0; j < _createPointsAtMousePosition.debugObjects.Count; j++)
        {
            foreach (var dic in DictionaryList.ElementAt(i))
            {
                if (_createPointsAtMousePosition.debugObjects.Count != 0)

                    if (dic.Value.Equals(_createPointsAtMousePosition.debugObjects[j].transform
                        .localPosition))
                    {
                        Destroy(_createPointsAtMousePosition.debugObjects[j]);
                        _createPointsAtMousePosition.debugObjects.RemoveAt(j);
                    }
            }
        }
    }
}
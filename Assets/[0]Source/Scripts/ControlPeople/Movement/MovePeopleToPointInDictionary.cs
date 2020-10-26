using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePeopleToPointInDictionary 
{
    public  List<Dictionary<Transform, Vector3>> DictionaryList = new List<Dictionary<Transform, Vector3>>();
    
    public void AddPointForMoveToPoint(Dictionary<Transform,Vector3> dictionary)
    {
        DictionaryList.Add(dictionary);
    }

    public void MoveToPoint() 
    {
        foreach (var dic in DictionaryList)
        {
            
            foreach (var usedPeoples in dic)
            {
                if (usedPeoples.Key.localPosition != usedPeoples.Value)
                {
                    usedPeoples.Key.localPosition = Vector3.MoveTowards(usedPeoples.Key.localPosition,
                        usedPeoples.Value,
                        Time.deltaTime * 30f);
                }
            }
        }
    }

    public bool HaveAllPeopleReachedThePoints()
    {
        foreach (var dic in DictionaryList)
        {
            foreach (var usedPeoples in dic)
            {
                if (usedPeoples.Key.localPosition != usedPeoples.Value)
                {
                    return false;
                }
            }
        }
        return true;
    }
}

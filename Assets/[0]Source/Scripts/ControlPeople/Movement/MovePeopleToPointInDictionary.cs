using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePeopleToPointInDictionary
{
    public List<Dictionary<Transform, Vector3>> PeopleAndPointsList = new List<Dictionary<Transform, Vector3>>();
    private int queue;
    
    public void AddPointForMoveToPoint(Dictionary<Transform, Vector3> dictionary)
    {
        PeopleAndPointsList[queue] = dictionary;
        
        queue++;
        
        if (queue >= PeopleAndPointsList.Count - 1)
            queue = 0;
        
    }

    public void MoveToPoint()
    {
       
        
        foreach (var dic in PeopleAndPointsList)
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
        foreach (var dic in PeopleAndPointsList)
        {
            foreach (var usedPeoples in dic)
            {
                if(!usedPeoples.Key)
                    continue;
                if (usedPeoples.Key.localPosition != usedPeoples.Value)
                {
                    return false;
                }
            }
        }

        return true;
    }
}
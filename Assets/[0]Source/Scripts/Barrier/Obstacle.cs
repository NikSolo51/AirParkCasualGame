using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<AddPeoplesToLinkList>())
        {
            other.transform.SetParent(this.transform);

            for (int i = ControlPeople.ControlPeopleBehaviour.peopleDictionary.Keys.Count - 1; i >= 0; i--)
            {
                if(other.transform == ControlPeople.ControlPeopleBehaviour.peopleDictionary.ElementAt(i).Value)
                    ControlPeople.ControlPeopleBehaviour.peopleDictionary.Remove(i);
            }

            for (int i = ControlPeople.ControlPeopleBehaviour.ListOfDictionariesOfPointsAndPeople.Count - 1;
                i >= 0;
                i--)
            {
                for (int j = ControlPeople.ControlPeopleBehaviour.ListOfDictionariesOfPointsAndPeople[i].Keys.Count - 1;
                    j >= 0;
                    j--)
                {
                    if (ControlPeople.ControlPeopleBehaviour.ListOfDictionariesOfPointsAndPeople[i].ElementAt(j).Key ==
                        other.transform)
                        ControlPeople.ControlPeopleBehaviour.ListOfDictionariesOfPointsAndPeople[i].Remove(ControlPeople.ControlPeopleBehaviour.ListOfDictionariesOfPointsAndPeople[i].ElementAt(j).Key);
                }
                
            }
            
            foreach (var link in ControlPeople.ControlPeopleBehaviour.LinkList)
            {
                if (other.transform == link.transform)
                    ControlPeople.ControlPeopleBehaviour.LinkList.Remove(other.transform);
            }
        }
       
    }
}
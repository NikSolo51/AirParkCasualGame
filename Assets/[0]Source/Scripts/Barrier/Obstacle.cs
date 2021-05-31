using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private ControlPeople _controlPeople;
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<AddPeoplesToLinkList>())
        {
            other.transform.SetParent(this.transform);

            for (int i = _controlPeople.controlPeopleBehaviour.peopleDictionary.Keys.Count - 1; i >= 0; i--)
            {
                if(other.transform == _controlPeople.controlPeopleBehaviour.peopleDictionary.ElementAt(i).Value)
                    _controlPeople.controlPeopleBehaviour.peopleDictionary.Remove(i);
            }

            for (int i = _controlPeople.controlPeopleBehaviour.ListOfDictionariesOfPointsAndPeople.Count - 1;
                i >= 0;
                i--)
            {
                for (int j = _controlPeople.controlPeopleBehaviour.ListOfDictionariesOfPointsAndPeople[i].Keys.Count - 1;
                    j >= 0;
                    j--)
                {
                    if (_controlPeople.controlPeopleBehaviour.ListOfDictionariesOfPointsAndPeople[i].ElementAt(j).Key ==
                        other.transform)
                        _controlPeople.controlPeopleBehaviour.ListOfDictionariesOfPointsAndPeople[i].Remove(_controlPeople.controlPeopleBehaviour.ListOfDictionariesOfPointsAndPeople[i].ElementAt(j).Key);
                }
                
            }
            
            foreach (var link in _controlPeople.controlPeopleBehaviour.LinkList)
            {
                if (other.transform == link.transform)
                    _controlPeople.controlPeopleBehaviour.LinkList.Remove(other.transform);
            }
        }
       
    }
}
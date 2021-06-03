using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
  Description:
  Objects with the AddPeoplesTopeopleTransformsList component are attracted 
  to the coordinates recorded in the existingPointsList.
 */


public class ControlPeopleBehaviour : MonoBehaviour
{
    public ControlPeople _controlPeople;
    public CreatePointsAtMousePosition _createPointsAtMousePosition;
    public List<Transform> peopleTransformsList = new List<Transform>();
    public Dictionary<int, Transform> peopleDictionary = new Dictionary<int, Transform>();
    //List of dictionaries of Points and people adjacent to them
    public List<Dictionary<Transform, Vector3>> ListOfDictionariesPointsAndPeople =
        new List<Dictionary<Transform, Vector3>>();
    
    public int queue = 0;


    public void CheckPeopleTransformList()
    {
        for (int i = 0; i < peopleTransformsList.Count; i++)
        {
            if (!peopleTransformsList[i])
            {
                peopleTransformsList.RemoveAt(i);
            }
        }
    }
    
    public void InitializingTheUsedPeopleDictionarySheet()
    {
        ListOfDictionariesPointsAndPeople.Add(new Dictionary<Transform, Vector3>());
    }

    public void PeopleDictionaryAdd(int id, Transform people)
    {
        peopleDictionary.Add(id, people);
        
        // Сортируем людей по их Id, от меньшего к большему.
        peopleDictionary = SortDictionaryByKeyValue(peopleDictionary);
        
        peopleTransformsList = peopleDictionary.Select(kvp => kvp.Value).ToList();
    }
    
    
    private Dictionary<int, Transform> SortDictionaryByKeyValue(Dictionary<int, Transform> peopleDic)
    {
        var orderedKeyValuePairs = peopleDic.OrderBy(key => key.Key);

        return orderedKeyValuePairs.ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);
    }
    

    public int GetClosestPersonById(List<Transform> peopleList, Vector2 point)
    {
        float distanceToClosestPerson = Mathf.Infinity;
        int _closestPersonId = 0;

        foreach (Transform currentPerson in peopleList)
        {
            if(!currentPerson)
                return _closestPersonId;
            
            for (int i = 0; i < ListOfDictionariesPointsAndPeople.Count; i++)
            {
                if (ListOfDictionariesPointsAndPeople[i].ContainsKey(currentPerson))
                {
                    goto Continue;
                }
            }
            
            float distanceToPerson = (point - new Vector2(currentPerson.localPosition.x, currentPerson.localPosition.y))
                .sqrMagnitude;
            if (distanceToPerson < distanceToClosestPerson)
            {
                distanceToClosestPerson = distanceToPerson;
                _closestPersonId = currentPerson.GetSiblingIndex();
            }

            Continue: ;
        }

        return _closestPersonId;
    }


    public bool CheckContainsClosestPersonById(List<Dictionary<Transform, Vector3>> ListOfDictionary, int personId)
    {
        foreach (var dic in ListOfDictionary)
        {
            if (dic.ContainsKey(peopleTransformsList[personId]))
            {
                return true;
            }
        }

        return false;
    }

    public void ChooseTheNearestPerson()
    {
        int closestPersonId;
        
        for (int i = 0; i < _createPointsAtMousePosition.existingPointsList.Count; i++)
        {
            
            closestPersonId =  GetClosestPersonById(peopleTransformsList,
                _createPointsAtMousePosition.existingPointsList[i]);

            if (peopleTransformsList[i] != null)
            {
                
                foreach (var dic in ListOfDictionariesPointsAndPeople)
                {
                    if (dic.ContainsValue(_createPointsAtMousePosition.existingPointsList[i]))
                    {
                        goto Continue;
                    }
                }

                if (!CheckContainsClosestPersonById(ListOfDictionariesPointsAndPeople, closestPersonId))
                {
                    ListOfDictionariesPointsAndPeople[queue].Add(peopleTransformsList[closestPersonId],
                        _createPointsAtMousePosition.existingPointsList[i]);
                    _createPointsAtMousePosition.coord = _createPointsAtMousePosition.newPoint;
                }
                else
                {
                    _createPointsAtMousePosition.coord = new Vector3(Mathf.Infinity, Mathf.Infinity);
                }

                Continue : ;
            }
        }
    }
}
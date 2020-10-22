using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class AddPeoplesToLinkList : MonoBehaviour
{
    [SerializeField]private int id;
    private void Start()
    {
        id = transform.GetSiblingIndex();
        ControlPeople.controlPeopleBehaviour.PeopleDictionaryAdd(id,transform);
       ControlPeople.controlPeopleBehaviour.localPeoplePosition.Add(transform.localPosition);
           //yep x2 it is correct
        ControlPeople.controlPeopleBehaviour.InitializingTheUsedPeopleDictionarySheet();
        ControlPeople.controlPeopleBehaviour.InitializingTheUsedPeopleDictionarySheet();
        //Debug.Log(ControlPeople.controlPeopleBehaviour.ListOfDictionariesOfPointsAndPeople.Count);
        //ControlPeople.controlPeopleBehaviour.LinkList.Add(transform);

    }
}

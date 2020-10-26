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
        ControlPeople.ControlPeopleBehaviour.PeopleDictionaryAdd(id,transform);
       ControlPeople.ControlPeopleBehaviour.localPeoplePosition.Add(transform.localPosition);
           //yep x2 it is correct
        ControlPeople.ControlPeopleBehaviour.InitializingTheUsedPeopleDictionarySheet();
        ControlPeople.ControlPeopleBehaviour.InitializingTheUsedPeopleDictionarySheet();
        //Debug.Log(ControlPeople.controlPeopleBehaviour.ListOfDictionariesOfPointsAndPeople.Count);
        //ControlPeople.controlPeopleBehaviour.LinkList.Add(transform);

    }
}

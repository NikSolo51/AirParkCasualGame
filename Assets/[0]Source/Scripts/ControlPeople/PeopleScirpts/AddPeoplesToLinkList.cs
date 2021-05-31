using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class AddPeoplesToLinkList : MonoBehaviour
{
    [SerializeField] private ControlPeople _controlPeople;
    [SerializeField] private int id;

    private void Start()
    {
        id = transform.GetSiblingIndex();
        _controlPeople.controlPeopleBehaviour.PeopleDictionaryAdd(id, transform);
        //yep x2 it is correct
        _controlPeople.controlPeopleBehaviour.InitializingTheUsedPeopleDictionarySheet();
        _controlPeople.controlPeopleBehaviour.InitializingTheUsedPeopleDictionarySheet();
        //Debug.Log(ControlPeople.controlPeopleBehaviour.ListOfDictionariesOfPointsAndPeople.Count);
        //ControlPeople.controlPeopleBehaviour.LinkList.Add(transform);
    }
}
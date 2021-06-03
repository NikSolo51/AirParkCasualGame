using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[DisallowMultipleComponent]
public class AddPeoplesToLinkList : MonoBehaviour
{
    [SerializeField] private ControlPeople _controlPeople;
    [SerializeField] private bool initializeOnStart = true;
    [SerializeField] private int id;

    private void Awake()
    {
        if(initializeOnStart)
        {
            id = transform.GetSiblingIndex();
            _controlPeople.controlPeopleBehaviour.PeopleDictionaryAdd(id, transform);
            _controlPeople.controlPeopleBehaviour. InitializingTheUsedPeopleDictionarySheet();
        }
    }

    public void AddPeople()
    {
        _controlPeople.controlPeopleBehaviour.PeopleDictionaryAdd(id, transform);
        Debug.Log(id);
        _controlPeople.controlPeopleBehaviour. InitializingTheUsedPeopleDictionarySheet();
    }
    
}
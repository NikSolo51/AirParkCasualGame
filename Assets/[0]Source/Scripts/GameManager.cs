using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public List<GameObject> peoples;
    public bool update = true;
    public UnityEvent EndGame;
    public int activePeople;
    public List<GameObject> actvePeopleList;
    private void Start()
    {
        
            for (int i = 0; i < transform.childCount; i++)
            {
                peoples.Add(transform.GetChild(i).gameObject);
            }
        
    }

    private void Update()
    {
        if (update)
        {
            CheckEndGame();
            
            for (int i = 0; i < peoples.Count; i++)
            {
                if (!actvePeopleList.Contains(peoples[i]))
                {
                    if (peoples[i].activeSelf == true)
                    {
                        actvePeopleList.Add(peoples[i]);
                    }
                }
                else
                {
                    if (peoples[i].activeSelf == false)
                    {
                        actvePeopleList.RemoveAt(i);
                    }
                }
               
            }
            activePeople = actvePeopleList.Count;
            
        }
    }

    public void CheckEndGame()
    {
        for (int i = 0; i < peoples.Count; i++)
        {
            if(peoples[i].activeSelf == true)
                return;
        }
        EndGame.Invoke();
        update = false;
    }

    public void WorkUpdate(bool value)
    {
        update = value;
    }
}

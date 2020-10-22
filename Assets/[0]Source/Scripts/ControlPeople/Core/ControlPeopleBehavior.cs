using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using UnityEngine;

/*
  Description:
  Objects with the AddPeoplesToLinkList component are attracted 
  to the coordinates recorded in the CoordinatesList.
 */
[BurstCompile]
public class ControlPeopleBehaviour : MonoBehaviour
{
    public List<Transform> LinkList = new List<Transform>();
    public  Dictionary<int, Transform> peopleDictionary = new Dictionary<int, Transform>();
    public int nearestPoint;
    private MouseInput mouseInput;
    //public Dictionary<Transform,Vector3> PeoplesAndPoints = new Dictionary<Transform, Vector3>();
    //List of dictionaries of Points and people adjacent to them
    public  List<Dictionary<Transform,Vector3>> ListOfDictionariesOfPointsAndPeople  = new List<Dictionary<Transform, Vector3>>( );
    public  int queue = 0;
    public  List<Dictionary<Transform, Vector3>> DictionaryList = new List<Dictionary<Transform, Vector3>>();
    public List<Vector3> localPeoplePosition = new List<Vector3>();
    public void PeopleDictionaryAdd(int id, Transform people)
    {
        peopleDictionary.Add(id,people);
        peopleDictionary = SortDictionary(peopleDictionary);
        ConvertDictionaryToList();
    }

    public Dictionary<Transform,Vector3> returnCurrentDic()
    {
        return ListOfDictionariesOfPointsAndPeople[queue];
    }

    public bool PrevDictionaryNotEmpty(int Queue)
    {
        if (ListOfDictionariesOfPointsAndPeople[Queue - 1].Keys.Count != 0)
            return true;
        else
        {
            return false;
        }
    }

    public void InitializingTheUsedPeopleDictionarySheet()
    {
        ListOfDictionariesOfPointsAndPeople.Add(new Dictionary<Transform, Vector3>());
    }

    public void IncrementQueue()
    {
        queue++;
    }

    public void ClearIfQueueEqualsZero()
    {
        if(queue ==  ListOfDictionariesOfPointsAndPeople.Count)
            queue = 0;
    }

    public Dictionary<Transform,Vector3> GetNotEmptyDictionary()
    {
        foreach (var dic in ListOfDictionariesOfPointsAndPeople)
        {
            if (dic.Keys.Count != 0)
                return dic;
        }
        return null;
    }


    
    
    private Dictionary<int , Transform> SortDictionary(Dictionary<int , Transform> peopleDic)
    {
        var orderedKeyValuePairs = peopleDic.OrderBy(key => key.Key);
        
         return orderedKeyValuePairs.ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);
    }
    
    
    
    
    public void ConvertDictionaryToList()
    {
        LinkList = peopleDictionary.Select(kvp => kvp.Value).ToList();
    }
    
    
    //public void AddLink(Transform newLink) => this.LinkList.Add(newLink.transform);
    //public void RemoveLink(Transform newLink) => this.LinkList.Add(newLink.transform);
    //public int GetListCount() => LinkList.Count;
    
    public int GetPeopleDictionaryCount() => peopleDictionary.Count;
    
    //public bool containsCurrentLink(Transform transform) => LinkList.Contains(transform);
    
    

    public void NearestPoint(List<Transform> pointsList, Vector2 point)
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        int closestPoint = 0;


        foreach (Transform currentPoint in pointsList)
        {
            for (int i = 0; i < ListOfDictionariesOfPointsAndPeople.Count; i++)
            {
                if (ListOfDictionariesOfPointsAndPeople[i].ContainsKey(currentPoint))
                {
                    goto Continue;
                }
            }

            float distanceToEnemy = (point - new Vector2(currentPoint.localPosition.x, currentPoint.localPosition.y))
                .sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestPoint = currentPoint.GetSiblingIndex();
            }

            Continue: ;
        }
        nearestPoint = closestPoint; 
        
    }

    public void AddPointForMoveToPoint(Dictionary<Transform,Vector3> dictionary)
    {
        DictionaryList.Add(dictionary);
    }

    public void MoveToPoint()
    {
        //Debug.Log("DictionaryCount" + DictionaryList.Count);
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
                    // Debug.Log("NoHave");
                    return false;
                }
            }
        }
        //Debug.Log("YesHave");
        return true;

    }
   

    public void GetMouseInput(MouseInput mouseInput)
    {
        this.mouseInput = mouseInput;
    }

    public bool ContainsNearestPoint(List<Dictionary<Transform,Vector3>> ListOfDictionary, int NearestPoint )
    {
        foreach (var dic in ListOfDictionary)
        {
            if (dic.ContainsKey(LinkList[NearestPoint]))
            {
                return true;
            }
        }

        return false;
    }
    
    

    public void Tick()
    {
       
        for (int i = 0; i < CreatePointsAtMousePosition.CoordinatesList.Count; i++)
        {
            NearestPoint(ControlPeople.controlPeopleBehaviour.LinkList, CreatePointsAtMousePosition.CoordinatesList[i]);
            
            if (LinkList[i] != null)
            {
                
                foreach (var dic in ListOfDictionariesOfPointsAndPeople)
                {
                    if (dic.ContainsKey(LinkList[nearestPoint]))
                    {
                        NearestPoint(ControlPeople.controlPeopleBehaviour.LinkList,
                            CreatePointsAtMousePosition.CoordinatesList[i]);
                    }
                }


                foreach (var dic in ListOfDictionariesOfPointsAndPeople)
                {
                    if (dic.ContainsValue(CreatePointsAtMousePosition.CoordinatesList[i]))
                    {
                       // Debug.Log("containsValue");
                        goto Continue;
                    }
                }

              
                
                /*
                LinkList[nearestPoint].localPosition = Vector3.Lerp(LinkList[nearestPoint].localPosition,
                        CreatePointsAtMousePosition.CoordinatesList[i],
                        Time.fixedDeltaTime * 10f);
                        */
                
                if (!ContainsNearestPoint(ListOfDictionariesOfPointsAndPeople, nearestPoint))
                {
                    ListOfDictionariesOfPointsAndPeople[queue].Add(LinkList[nearestPoint], CreatePointsAtMousePosition.CoordinatesList[i]);
                    CreatePointsAtMousePosition.Instance.coord = CreatePointsAtMousePosition.Instance.localValueCoord;
                }
                else
                {
                    CreatePointsAtMousePosition.Instance.coord = new Vector3(Mathf.Infinity,Mathf.Infinity);
                }
                //Debug.Log("ContainsNearestPoint");
                Continue : ;
            }
            
            //LinkList[i].position = Vector3.Lerp(LinkList[i].position, LinkList[i - 1].position, Time.deltaTime * time  );
            //LinkList[i].rotation = Quaternion.Slerp(LinkList[i].rotation, LinkList[i - 1].rotation, Time.deltaTime * time );
           
        }
     

    }
}

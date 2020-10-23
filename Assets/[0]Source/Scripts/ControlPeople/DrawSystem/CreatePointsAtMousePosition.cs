using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[BurstCompile]
public class CreatePointsAtMousePosition : MonoBehaviour
{
    //Object relative to which the transition to the local coordinate system occurs
    [Header("[Origin Of Coordinate System]")] [SerializeField]
    private Transform OriginSystem;

    [Header("[Minimum distance between two points]")] [SerializeField]
    private float minOffset = 0.5f;

    public float differentFloat = 1f;
    public Text textForDifferentFloat;

    private List<Vector3> SheetOfAllPoints = new List<Vector3>();
    public static List<Vector3> CoordinatesList = new List<Vector3>();
    [SerializeField] private List<Vector3> CoordList;
    [SerializeField] private int countPointAfterZeroing;
    [SerializeField] public static bool generate;
    public static CreatePointsAtMousePosition Instance;
  
    [SerializeField] private GameObject debugPrefab;
    public static  List<GameObject> debugObjects = new List<GameObject>();
    public   List<GameObject> DebugObjects = new List<GameObject>();
    
    public Vector3 localValueCoord;

    public Vector3 coord;
    public static bool settingOn = true;
    private void Start()
    {
        Instance = this;
        CoordList = CoordinatesList;
        DebugObjects = debugObjects;
    }

    public void Update()
    {
        float.TryParse(textForDifferentFloat.text, out differentFloat);
        if (!settingOn)
        if (Input.GetMouseButton(0))
        {
           
            Generate();
            generate = true;
            
        }
        else
            generate = false;
        
      
    }

    public void OnSetting()
    {
        settingOn = true;
    }
    
    public void OffSetting()
    {
        settingOn = false;
    }

    
    
    private void Generate()
    {
        //Get Coordinates  mouse position in local space
        localValueCoord = CalculateLocalPointOnScreen();
      
       
        
        //Check does it contain an already existing point pointsList
        //and checks if there is a minimum distance between two points 
        if (SheetOfAllPoints.Count > 0)
        {
            if (IsTheCoordinateInTheList(SheetOfAllPoints, localValueCoord) ||
                IsThereADifferenceInDistanceBetweenTwoPoints(SheetOfAllPoints, localValueCoord))
                return;
        }
        
        if(!IsThereADifferenceBetweenTheTwoPoints(CoordinatesList,localValueCoord))
            return;
        
        if(!IsThereADifferenceBetweenTheTwoPoints(ControlPeople.controlPeopleBehaviour.localPeoplePosition,localValueCoord))
            return;

        
        SheetOfAllPoints.Add(localValueCoord);
        

        if (CoordinatesList.Count < ControlPeople.controlPeopleBehaviour.GetPeopleDictionaryCount())
        {
            CoordinatesList.Add(localValueCoord);
            ControlPeople.controlPeopleBehaviour.Tick();
        }

        if (countPointAfterZeroing >= ControlPeople.controlPeopleBehaviour.GetPeopleDictionaryCount())
            countPointAfterZeroing = 0;
        
        
        if (CoordinatesList.Count == ControlPeople.controlPeopleBehaviour.GetPeopleDictionaryCount())
        {
           // CoordinatesList[countPointAfterZeroing] = coordinates;
            //SynchronizationWithADictionary();
            ControlPeople.controlPeopleBehaviour.Tick();
            //countPointAfterZeroing++;
            
        }
        
        DebugPrimitive(localValueCoord);
       
        
    }

    
    public bool CanICreatePointInThisPlace()
    {
        if (CoordinatesList.Count != 0)
        {
            if (SheetOfAllPoints.Count > 0)
            {
                if (IsThereADifferenceBetweenTheTwoPoints(CoordinatesList.GetRange(0, CoordinatesList.Count - 1),
                    coord))
                {
                    
                        return true;
                }
                    
            }
               
        }
        return false;
        
    }
   
    
    public bool IsThereADifferenceBetweenTheTwoPoints(List<Vector3> pointsList, Vector2 point)
    {
        float distanceToClosestPoint = Mathf.Infinity;
        Vector3 closestPoint = Vector3.zero;


        foreach (Vector3 currentPoint in pointsList)
        {
            {
               
                float distanceToPoint = (point - new Vector2(currentPoint.x, currentPoint.y))
                    .sqrMagnitude;
                if (distanceToPoint < distanceToClosestPoint)
                {
                    distanceToClosestPoint = distanceToPoint;
                    closestPoint = currentPoint;
                }
            }
        }
        
        Vector2 different = point - new Vector2(closestPoint.x,closestPoint.y);
        
        if (point == new Vector2(closestPoint.x, closestPoint.y))
        {
            if(IsThereADifferenceBetweenTheTwoPoints(new List<Vector3>(pointsList.Where(x => x != closestPoint )), point));
            return true; 
        }
        
        if (Mathf.Abs(different.x) >= differentFloat || Mathf.Abs(different.y) >= differentFloat)
        {
            
            return true;
        }
        return false;
    }
  

    private void DebugPrimitive(Vector3 coord)
    {
        if(debugObjects.Count >= CreatePointsAtMousePosition.CoordinatesList.Count )
            return;
        
        GameObject pref =  Instantiate(debugPrefab,OriginSystem);
       pref.transform.localPosition = coord;
       pref.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
       debugObjects.Add(pref);
    }
    public Vector3 CalculateLocalPointOnScreen()
    {
        Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 coordinates = mRay.origin + (mRay.direction * 12f);
        coordinates = OriginSystem.transform.InverseTransformPoint(coordinates);
        coordinates.z = 2 ;
        
        return coordinates;
    }

    private bool IsThereADifferenceInDistanceBetweenTwoPoints(List<Vector3> listCoordinates,Vector3 coordinate)
    {
        
        //check if there is a minimum distance between two points 
        if (Vector3.Distance(coordinate, listCoordinates[listCoordinates.Count - 1]) <= minOffset)
        {
            return true; //skip the point
        }

        return false;
    }

    public bool IsTheCoordinateInTheList(List<Vector3> listCoordinates,Vector3 coordinate)
    {
        //if the coordinate is in the list of all points, then we skip it
        if (listCoordinates.Contains(coordinate))
        {
            return true;
        }

        return false;

    }
        

    
}
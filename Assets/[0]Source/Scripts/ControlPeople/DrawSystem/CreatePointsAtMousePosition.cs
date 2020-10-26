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
    public float different = 1f;
    
    public Text textForDifferentFloat;

    private readonly List<Vector3> listOfAllPoints = new List<Vector3>();
    public  List<Vector3> coordinatesList = new List<Vector3>();
    
    public static CreatePointsAtMousePosition Instance;

    [SerializeField] private GameObject debugPrefab;
    public List<GameObject> debugObjects = new List<GameObject>();

    public Vector3 localValueCoord;

    public Vector3 coord;
    
    private void Start()
    {
        Instance = this;
    }

    public void Update()
    {
        float.TryParse(textForDifferentFloat.text, out different);

        if (Input.GetMouseButton(0))
        {
            Generate();
        }
    }
    
    
    private void Generate()
    {
        //Get Coordinates  mouse position in local space
        localValueCoord = CalculateLocalPointOnScreen(OriginSystem,2);
      
       
        
        //Check does it contain an already existing point pointsList
        //and checks if there is a minimum distance between two points 
        if (listOfAllPoints.Count > 0)
        {
            if (IsTheCoordinateInTheList(listOfAllPoints, localValueCoord) ||
                IsThereADifferenceInDistanceBetweenTwoPoints(listOfAllPoints, localValueCoord))
                return;
        }

        if (!IsThereADifferenceBetweenTheTwoPoints(coordinatesList, localValueCoord, different) ||
            !IsThereADifferenceBetweenTheTwoPoints(ControlPeople.ControlPeopleBehaviour.localPeoplePosition,
                localValueCoord, different))
            return;
        
        
        listOfAllPoints.Add(localValueCoord);
        

        if (coordinatesList.Count < ControlPeople.ControlPeopleBehaviour.GetPeopleDictionaryCount())
        {
            coordinatesList.Add(localValueCoord);
            ControlPeople.ControlPeopleBehaviour.ChooseTheNearestPerson();
        }
        else
        {
            ControlPeople.ControlPeopleBehaviour.ChooseTheNearestPerson();
        }
        
        if (debugObjects.Count >= coordinatesList.Count)
            return;
        GameObject point = PointVisualization(debugPrefab, OriginSystem, localValueCoord, Vector3.one / 2);
        debugObjects.Add(point);
        
    }

    
    public bool CanICreatePointInThisPlace()
    {
        if (coordinatesList.Count != 0)
        {
            if (listOfAllPoints.Count > 0)
            {
                if (IsThereADifferenceBetweenTheTwoPoints(coordinatesList.GetRange(0, coordinatesList.Count - 1),
                    coord, different))
                {
                    return true;
                }
            }
               
        }
        return false;
        
    }
   
    
    public bool IsThereADifferenceBetweenTheTwoPoints(List<Vector3> pointsList, Vector2 point , float allowedDifference)
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

        Vector2 different = point - new Vector2(closestPoint.x, closestPoint.y);

        if (point == new Vector2(closestPoint.x, closestPoint.y))
        {
            if (IsThereADifferenceBetweenTheTwoPoints(new List<Vector3>(pointsList.Where(x => x != closestPoint)),
                point, allowedDifference)) ;
            return true;
        }

        if (Mathf.Abs(different.x) >= allowedDifference || Mathf.Abs(different.y) >= allowedDifference)
        {
            return true;
        }

        return false;
    }


    
    
    private GameObject PointVisualization(GameObject Prefab,Transform Origin,Vector3 coordinate , Vector3 scale)
    {
        GameObject point = Instantiate(debugPrefab, Origin);
        point.transform.localPosition = coordinate;
        point.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        return point;
    }

    public Vector3 CalculateLocalPointOnScreen(Transform Origin, float beamLength)
    {
        Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 coordinates = mRay.origin + (mRay.direction * 12f);
        coordinates = Origin.transform.InverseTransformPoint(coordinates);
        coordinates.z = beamLength;
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
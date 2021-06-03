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
    public List<Vector3> existingPointsList = new List<Vector3>();
    [HideInInspector] public List<GameObject> debugObjects = new List<GameObject>();
    [HideInInspector] public Vector3 newPoint;
    [HideInInspector] public Vector3 coord;


    [SerializeField] private ControlPeople _controlPeople;

    [SerializeField] private GameObject debugPrefab;

    //Object relative to which the transition to the local coordinate system occurs
    [Header("[Origin Of Coordinate System]")] [SerializeField]
    private Transform OriginSystem;

    [Header("[Minimum distance between two points]")] [SerializeField]
    private float minOffset = 1f;

    private readonly List<Vector3> listOfAllPoints = new List<Vector3>();

    public bool CanICreatePointInThisPlace()
    {
        if (existingPointsList.Count != 0)
        {
            if (listOfAllPoints.Count > 0)
            {
                if (IsThereADifferenceBetweenTheTwoPoints(existingPointsList.GetRange(0, existingPointsList.Count - 1),
                    coord, minOffset))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Generate();
        }
    }

    private void Generate()
    {
        //Get Coordinates  mouse position in local space
        newPoint = GetPointOnScreen(OriginSystem, 2);

        //Check if list of all points contains new point
        //and checks if there is a minimum distance between new point and closest point in CoordinateList
        if (listOfAllPoints.Count > 0)
        {
            if (listOfAllPoints.Contains(newPoint) ||
                IsThereADifferenceInDistanceBetweenTwoPoints(listOfAllPoints, newPoint, minOffset))
                return;
        }


        // check if there a minimum distance between new point and closest existingPoint in existingPointsList
        if (!IsThereADifferenceBetweenTheTwoPoints(existingPointsList, newPoint, minOffset))
            return;

        listOfAllPoints.Add(newPoint);


        if (existingPointsList.Count < _controlPeople.controlPeopleBehaviour.peopleTransformsList.Count)
        {
            existingPointsList.Add(newPoint);
        }

        //choose closest person for new point
        _controlPeople.controlPeopleBehaviour.ChooseTheNearestPerson();

        //Draw debug objects
        if (debugObjects.Count < existingPointsList.Count)
        {
            GameObject point = PointVisualization(debugPrefab, OriginSystem, newPoint, Vector3.one / 2);
            debugObjects.Add(point);
        }
    }

    public Vector3 GetPointOnScreen(Transform Origin, float beamLength)
    {
        Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 coordinates = mRay.origin + (mRay.direction * 12f);

        coordinates = Origin.transform.InverseTransformPoint(coordinates);
        coordinates.z = beamLength;
        return coordinates;
    }

    private bool IsThereADifferenceInDistanceBetweenTwoPoints(List<Vector3> listCoordinates, Vector3 coordinate,
        float minOffset)
    {
        //check if there is a minimum distance between two points 
        if (Vector3.Distance(coordinate, listCoordinates[listCoordinates.Count - 1]) <= minOffset)
        {
            return true; //skip the point
        }

        return false;
    }

    public bool IsThereADifferenceBetweenTheTwoPoints(List<Vector3> pointsList, Vector2 point, float minOffset)
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

        Vector2 difference = point - new Vector2(closestPoint.x, closestPoint.y);


        if (Mathf.Abs(difference.x) >= minOffset || Mathf.Abs(difference.y) >= minOffset)
        {
            return true;
        }

        return false;
    }


    private GameObject PointVisualization(GameObject Prefab, Transform Origin, Vector3 coordinate, Vector3 scale)
    {
        GameObject point = Instantiate(debugPrefab, Origin);
        point.transform.localPosition = coordinate;
        point.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        return point;
    }
}
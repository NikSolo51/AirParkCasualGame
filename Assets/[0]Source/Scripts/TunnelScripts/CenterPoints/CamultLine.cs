using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using UnityEngine;

[BurstCompile]
public class CamultLine : MonoBehaviour
{
    public List<GameObject> linePoints = new List<GameObject>();
    public Vector3[] curveCoordinates;
    
    [SerializeField] CalculatePoint _calculatePoint;
    
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject centerPointPrefab;
    [SerializeField] private int curveResolution = 20;
    
    [SerializeField] private bool closedLoop = false;
    [SerializeField] private bool renderCube = false;
    [SerializeField] private bool renderCubeStatic;
    [SerializeField] private bool renderLine = false;
    
    
    private GameObject[] points = new GameObject[4];
    private Vector3[] Tangents;
    private bool genPointExecute = false;


    public enum Uniformity
    {
        Uniform,
        Centripetal,
        Chordal
    }

    private void Start()
    {
        StartCoroutine("Calculate");
    }

    IEnumerator Calculate()
    {
        yield return new WaitForSeconds(0.1f);
        renderCubeStatic = renderCube;
        points = _calculatePoint.points.ToArray();

        CalculateCamultLine();
    }


    public static Vector3 Interpolate(Vector3 start, Vector3 end, Vector3 tanPoint1, Vector3 tanPoint2, float t)
    {
        // Catmull-Rom splines are Hermite curves with special tangent values.
        // Hermite curve formula:
        // (2t^3 - 3t^2 + 1) * p0 + (t^3 - 2t^2 + t) * m0 + (-2t^3 + 3t^2) * p1 + (t^3 - t^2) * m1
        // For points p0 and p1 passing through points m0 and m1 interpolated over t = [0, 1]
        // Tangent M[k] = (P[k+1] - P[k-1]) / 2
        // With [] indicating subscript
        Vector3 position = (2.0f * t * t * t - 3.0f * t * t + 1.0f) * start
                           + (t * t * t - 2.0f * t * t + t) * tanPoint1
                           + (-2.0f * t * t * t + 3.0f * t * t) * end
                           + (t * t * t - t * t) * tanPoint2;

        return position;
    }

    public static Vector3 Interpolate(Vector3 start, Vector3 end, Vector3 tanPoint1, Vector3 tanPoint2, float t,
        out Vector3 tangent)
    {
        // Calculate tangents
        // p'(t) = (6t² - 6t)p0 + (3t² - 4t + 1)m0 + (-6t² + 6t)p1 + (3t² - 2t)m1
        tangent = (6 * t * t - 6 * t) * start
                  + (3 * t * t - 4 * t + 1) * tanPoint1
                  + (-6 * t * t + 6 * t) * end
                  + (3 * t * t - 2 * t) * tanPoint2;
        return Interpolate(start, end, tanPoint1, tanPoint2, t);
    }

    public static Vector3 Interpolate(Vector3 start, Vector3 end, Vector3 tanPoint1, Vector3 tanPoint2, float t,
        out Vector3 tangent, out Vector3 curvature)
    {
        // Calculate second derivative (curvature)
        // p''(t) = (12t - 6)p0 + (6t - 4)m0 + (-12t + 6)p1 + (6t - 2)m1
        curvature = (12 * t - 6) * start
                    + (6 * t - 4) * tanPoint1
                    + (-12 * t + 6) * end
                    + (6 * t - 2) * tanPoint2;
        return Interpolate(start, end, tanPoint1, tanPoint2, t, out tangent);
    }

    public static float[] GetNonuniformT(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float alpha)
    {
        // See here: http://stackoverflow.com/a/23980479/837825
        // C'(t1) = (P1 - P0) / (t1 - t0) - (P2 - P0) / (t2 - t0) + (P2 - P1) / (t2 - t1)
        // C'(t2) = (P2 - P1) / (t2 - t1) - (P3 - P1) / (t3 - t1) + (P3 - P2) / (t3 - t2)
        float[] values = new float[4];
        for (int i = 0; i < 4; i++)
        {
            //values[i] = Mathf.Pow(Vector3.SqrMagnitude())
            break;
        }

        return values;
    }


    void CalculateCamultLine()
    {
        Vector3 p0;
        Vector3 p1;
        Vector3 m0;
        Vector3 m1;
        int pointsToMake;


        if (closedLoop == true)
        {
            pointsToMake = (curveResolution) * (points.Length);
        }
        else
        {
            pointsToMake = (curveResolution) * (points.Length - 1);
        }

        curveCoordinates = new Vector3[pointsToMake];
        
        Tangents = new Vector3[pointsToMake];

        int closedAdjustment = closedLoop ? 0 : 1;

        // First for loop goes through each individual control point and connects it to the next, so 0-1, 1-2, 2-3 and so on
        for (int i = 0; i < points.Length - closedAdjustment; i++)
        {
            //if (points[i] == null || points[i + 1] == null || (i > 0 && points[i - 1] == null) || (i < points.Length - 2 && points[i + 2] == null))
            //{
            //    return;
            //}

            p0 = points[i].transform.position;
            p1 = (closedLoop == true && i == points.Length - 1)
                ? points[0].transform.position
                : points[i + 1].transform.position;

            // Tangent calculation for each control point
            // Tangent M[k] = (P[k+1] - P[k-1]) / 2
            // With [] indicating subscript

            // m0
            if (i == 0)
            {
                m0 = closedLoop ? 0.5f * (p1 - points[points.Length - 1].transform.position) : p1 - p0;
            }
            else
            {
                m0 = 0.5f * (p1 - points[i - 1].transform.position);
            }

            // m1
            if (closedLoop)
            {
                if (i == points.Length - 1)
                {
                    m1 = 0.5f * (points[(i + 2) % points.Length].transform.position - p0);
                }
                else if (i == 0)
                {
                    m1 = 0.5f * (points[i + 2].transform.position - p0);
                }
                else
                {
                    m1 = 0.5f * (points[(i + 2) % points.Length].transform.position - p0);
                }
            }
            else
            {
                if (i < points.Length - 2)
                {
                    m1 = 0.5f * (points[(i + 2) % points.Length].transform.position - p0);
                }
                else
                {
                    m1 = p1 - p0;
                }
            }

            Vector3 position;
            float t;
            float pointstep = 1.0f / curveResolution;

            if ((i == points.Length - 2 && closedLoop == false) || (i == points.Length - 1 && closedLoop))
            {
                pointstep = 1.0f / (curveResolution - 1);
                // last point of last segment should reach p1
            }

            // Second for loop actually creates the spline for this particular segment
            for (int j = 0; j < curveResolution; j++)
            {
                t = j * pointstep;
                Vector3 tangent;
                position = Interpolate(p0, p1, m0, m1, t, out tangent);
                curveCoordinates[i * curveResolution + j] = position;
                Tangents[i * curveResolution + j] = tangent;
                //Debug.DrawRay(position, tangent.normalized * 2, Color.red);
                if (renderLine == true)
                    Debug.DrawLine(position + Vector3.Cross(tangent, Vector3.up).normalized,
                        position - Vector3.Cross(tangent, Vector3.up).normalized, Color.red);
            }
        }

        for (int i = 0; i < curveCoordinates.Length - 1; ++i)
        {
            if (renderLine == true)
                Debug.DrawLine(curveCoordinates[i], curveCoordinates[i + 1]);
        }

        if (genPointExecute == false)
            GeneratePoints();
    }

    public void GeneratePoints()
    {
        for (int i = 0; i < curveCoordinates.Length; i++)
        {
            GameObject centerPoint = Instantiate(centerPointPrefab, curveCoordinates[i], Quaternion.identity,
                parent.transform);
            centerPoint.name = " center point " + i;
            centerPoint.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            centerPoint.GetComponent<MeshRenderer>().enabled = renderCubeStatic;

            linePoints.Add(centerPoint);
        }

        genPointExecute = true;
    }

    void OnDrawGizmos()
    {
        if (curveCoordinates == null)
            return;
        if (renderLine == true)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < curveCoordinates.Length; i++)
            {
                Gizmos.DrawWireCube(curveCoordinates[i], new Vector3(.1f, .1f, .1f));
            }
        }
    }
}
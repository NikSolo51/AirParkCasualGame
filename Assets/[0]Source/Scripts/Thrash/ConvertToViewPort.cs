using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertToViewPort : MonoBehaviour
{
    [SerializeField] private Transform parent = null;
    [SerializeField] private GameObject prefab = null ;
    [SerializeField] private Vector2 position;
    private GameObject a;
    private GameObject cube;
    public Vector2 viewPos;

    private void Start()
    {
        viewPos = Camera.main.WorldToScreenPoint(transform.position);
        a= Instantiate(prefab, parent);
        a.GetComponent<RectTransform>().position = viewPos;
    }

    private void Update()
    {
        //a.GetComponent<RectTransform>().localPosition = position;
    }
}

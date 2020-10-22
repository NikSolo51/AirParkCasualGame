using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Movement/Settings", fileName = "MovementData")]
public class MovementSetting : ScriptableObject
{
    [SerializeField] private float moveSpeed = 25f;
    [SerializeField] private float rotationSpeed = 100f;
    

    public float MoveSpeed{ get { return moveSpeed; } }
    public float RotationSpeed{ get { return rotationSpeed; } }
    
}


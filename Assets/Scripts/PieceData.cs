using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Piece Data")]
public class PieceData : ScriptableObject
{
    public Vector2[] data;
    public float weight = 1;
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TileType : ScriptableObject
{
    public TileBase[] tiles;

    public void OnStep() { } 
    public void OnEnd() { }
}
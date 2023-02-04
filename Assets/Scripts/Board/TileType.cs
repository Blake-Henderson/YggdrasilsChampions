using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TileType : ScriptableObject
{
    public TileBase[] tiles;

    public virtual void OnStep() { } 
    public virtual void OnEnd() { }
}

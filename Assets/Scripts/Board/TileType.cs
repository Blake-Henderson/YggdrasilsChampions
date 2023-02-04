using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TileType : ScriptableObject
{
    public TileBase[] tiles;

    public virtual bool OnStep() { return false; } 
    public virtual void OnEnd() { }
}

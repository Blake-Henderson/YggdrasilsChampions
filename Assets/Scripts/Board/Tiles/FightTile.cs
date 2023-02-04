using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FightTile : TileType
{
    public override bool OnEnd()
    {
        Debug.Log("Tile ended on!");
        //load fight
        return false;
    }
}

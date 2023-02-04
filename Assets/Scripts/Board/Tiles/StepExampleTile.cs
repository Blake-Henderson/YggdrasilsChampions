using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StepExampleTile : TileType
{
    public override bool OnStep()
    {
        //Debug.Log("Tile stepped on!");
        return false;
    }
}

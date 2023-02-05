using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HealTile : TileType
{
    public override bool OnEnd()
    {
        Debug.Log("HEAL TILE ONEND");
        TurnManager.instance.characters[TurnManager.instance.gm.turnCount].stats.heal(100);
        //TurnManager.instance.EndTurn();
        return false;
    }

    public override bool OnStep()
    {
        Debug.Log("HEAL TILE OnStep");
        TurnManager.instance.characters[TurnManager.instance.gm.turnCount].stats.heal(TurnManager.instance.characters[TurnManager.instance.gm.turnCount].stats.hp / 2 + TurnManager.instance.characters[TurnManager.instance.gm.turnCount].stats.hp % 2);
        return false;
    }
}

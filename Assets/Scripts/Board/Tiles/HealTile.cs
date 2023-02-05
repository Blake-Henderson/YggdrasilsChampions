using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTile : TileType
{
    public override bool OnEnd()
    {
        TurnManager.instance.characters[TurnManager.instance.gm.turnCount].stats.heal(100);
        return true;
    }

    public override bool OnStep()
    {
        TurnManager.instance.characters[TurnManager.instance.gm.turnCount].stats.heal(TurnManager.instance.characters[TurnManager.instance.gm.turnCount].stats.hp / 2);
        return true;
    }
}

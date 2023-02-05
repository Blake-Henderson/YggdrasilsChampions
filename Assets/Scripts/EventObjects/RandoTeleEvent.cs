using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RandoTeleEvent : EventObject
{
    public override bool Effect()
    {
        Character ch = TurnManager.instance.characters[TurnManager.instance.gm.turnCount];
        ch.lastTile = ch.currentTile;
        ch.currentTile = TurnManager.instance.board.GetRandomTile();
        ch.transform.position = TurnManager.instance.board.map.CellToWorld(ch.currentTile) + TurnManager.instance.board.map.tileAnchor;
        return false;
    }

    public override void End()
    {
        throw new System.NotImplementedException();
    }
}

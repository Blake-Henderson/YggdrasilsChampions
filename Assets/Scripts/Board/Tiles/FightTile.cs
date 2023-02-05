using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FightTile : TileType
{

    public override bool OnEnd()
    {
        PlayerInfoUI.instance.gameObject.SetActive(false);
        Battle.instance.BattleStart(TurnManager.instance.characters[TurnManager.instance.gm.turnCount], GetNPCForFight.instance.GetCharacter());
        Battle.instance.finishEvent.AddListener(FightComplete);
        return true;
    }

    public void FightComplete()
    {
        PlayerInfoUI.instance.gameObject.SetActive(true);
        Battle.instance.finishEvent.RemoveListener(FightComplete);
        TurnManager.instance.EndTurn();
    }
}

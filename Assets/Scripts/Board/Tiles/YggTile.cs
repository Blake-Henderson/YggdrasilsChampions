using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class YggTile : TileType
{
    public Character Yggdrasil;

    public override bool OnStep()
    {
        PlayerInfoUI.instance.gameObject.SetActive(false);
        if (Yggdrasil == null) Yggdrasil = GetYggdrasil.instance.character;
        Battle.instance.BattleStart(TurnManager.instance.characters[TurnManager.instance.gm.turnCount], Yggdrasil);
        Battle.instance.finishEvent.AddListener(FightComplete);
        return true;
    }
    public void FightComplete()
    {

        Battle.instance.finishEvent.RemoveListener(FightComplete);
        if (Yggdrasil.stats.health <= 0) WinText.instance.Win();
        else
        {
            PlayerInfoUI.instance.gameObject.SetActive(true);
            TurnManager.instance.EndTurn();
        }
    }
}

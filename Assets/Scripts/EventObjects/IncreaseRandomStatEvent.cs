using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IncreaseRandomStatEvent : EventObject
{
    public GiveItemPopup prefab;
    public GiveItemPopup tmp;

    public override bool Effect()
    {
        Character ch = TurnManager.instance.characters[TurnManager.instance.gm.turnCount];
        int statChange = Random.Range(0, 5);
        string statName = "";
        int statAmount = 0;
        switch (statChange)
        {
            case 0:
                statName = "Health";
                statAmount = Random.Range(1, 4);
                ch.stats.hp += statAmount;
                break;
            case 1:
                statName = "Attack";
                statAmount = Random.Range(1, 3);
                ch.stats.attack += statAmount;
                break;
            case 2:
                statName = "Defense";
                statAmount = Random.Range(1, 3);
                ch.stats.defense += statAmount;
                break;
            case 3:
                statName = "Evasion";
                statAmount = Random.Range(1, 3);
                ch.stats.evade += statAmount;
                break;
            case 4:
                statName = "Speed";
                statAmount = Random.Range(1, 3);
                ch.stats.speed += statAmount;
                break;
        }
        tmp = Instantiate(prefab);
        tmp.text.text = TurnManager.instance.characters[TurnManager.instance.gm.turnCount].name + " boosted " +statName+ " by " +statAmount;
        tmp.button.onClick.AddListener(End);
        return true;
    }

    public override void End()
    {
        Destroy(tmp.gameObject);
        TurnManager.instance.EndTurn();
    }
}

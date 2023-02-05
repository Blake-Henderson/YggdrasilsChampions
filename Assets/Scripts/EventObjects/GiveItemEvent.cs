using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GiveItemEvent : EventObject
{
    public Item item;
    public GiveItemPopup prefab;
    public GiveItemPopup tmp;

    public override bool Effect()
    {
        bool worked = false;
        for (int i = 0; i < 3; ++i)
        {
            if (TurnManager.instance.characters[TurnManager.instance.gm.turnCount].inventory[i] == TurnManager.instance.characters[TurnManager.instance.gm.turnCount].empty)
            {
                worked = true;
                TurnManager.instance.characters[TurnManager.instance.gm.turnCount].inventory[i] = item;
                break;
            }
        }
        tmp = Instantiate(prefab);
        if (worked) {
            tmp.text.text = TurnManager.instance.characters[TurnManager.instance.gm.turnCount].name + " got a " + item.name;
        } 
        else
        {
            tmp.text.text = TurnManager.instance.characters[TurnManager.instance.gm.turnCount].name + " got a " + item.name + ", but did not have inventory space to keep it!";
        }
        tmp.button.onClick.AddListener(End);
        return true;
    }

    public override void End()
    {
        Destroy(tmp.gameObject);
        TurnManager.instance.EndTurn();
    }
}

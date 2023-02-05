using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GiveItemEvent : EventObject
{
    public Item item;

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
        if (worked) { } //display ui showing it worked
        else { } //display ui showing inventory full
        return false;
    }

    public override void End()
    {
        throw new System.NotImplementedException();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScriptBattleCall : MonoBehaviour
{
    public Battle battle;

    public Character c1;
    public Character c2;
    // Start is called before the first frame update
    void Start()
    {
        battle.BattleStart(c1, c2);
    }

}

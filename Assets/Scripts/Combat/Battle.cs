using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{

    //Enum used to control the battle state
    public enum BattleState
    {
        OFF,
        START,
        INPUT,
        RESOLUTION,
        END
    }
    //The varaible for said enum
    public BattleState bs = BattleState.OFF;

    //This will start the battle and play the animations for the characters in question
    void BattleStart(Character c1, Character c2)
    {
        bs = BattleState.START;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

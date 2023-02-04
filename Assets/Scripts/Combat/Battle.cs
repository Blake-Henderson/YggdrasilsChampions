using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle : MonoBehaviour
{
    public GameObject BattlePanel;

    public Image C1Bust;
    public Image C2Bust;

    //This is the image that we will apply the Die facing on
    public Image DiceD6;
    //This should contain 6 elements with 6 die facings
    public List<Sprite> DiceRolls;

    //This decides who goes first, false is first character and true is second character 
    public bool firstTurn = false;

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
    public void BattleStart(Character c1, Character c2)
    {
        bs = BattleState.START;
        C1Bust.sprite = c1.stats.sprite;
        C2Bust.sprite = c2.stats.sprite;
        BattlePanel.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        //Changes the battle based upon battle state
        switch (bs)
        {
            case BattleState.INPUT:

                break;

            case BattleState.RESOLUTION:
                resolution();
                break;

            case BattleState.END:

                break;

            //Start and Off do nothing as both are covered in the battlestart function
            case BattleState.START:
                break;

            case BattleState.OFF:
                BattlePanel.SetActive(false);
                break;
            //If we are somehow outside of entirety of the enum then just turn the state to off
            default:
                bs = BattleState.OFF;
                break;
        }
    }

    //This resolves for turns
    private void resolution()
    {

    }
}


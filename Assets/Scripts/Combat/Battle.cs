using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Battle : MonoBehaviour
{
    //Strings for exactly what they are named after
    public string turnOrderPrompt;
    public string rollPromt;
    private string nothing = "";

    //This tells us if it is the first turn of a cycle
    public bool firstTurn = true;
    //This decides if it is time to roll for initiative
    public bool decidingTurn = true;

    //this determines who's turn it is in code
    private bool c1turn = true;
    private bool c2turn = false;

    //this determines who rolled last True = character1 rolled last
    private bool lastRoll = false;
    
    //This determines turn order
    private int c1Initiative = 0;
    private int c2Initiative = 0;

    //These are combat stats
    private int c1atk = 0;
    private int c1def = 0;
    private int c1evd = 0;
    private int c2atk = 0;
    private int c2def = 0;
    private int c2evd = 0;

    //This is a timer, this will be used in place of animations for things which need to wait
    private bool timer = true;

    //This is the current value of the die
    public int diceRoll = 1;

    //Important for UI calls
    private BattleUI BUI;

    //Copied from battlestart
    private Character c1temp;
    private Character c2temp;

    //Defend is when this is true, evade is when this is false
    public bool defEvd = false;
    private bool optionPicked = false;

    //Enum used to control the battle state
    public enum BattleState
    {
        OFF,
        START,
        ACTION,
        RESOLUTION,
        END
    }
    //The varaible for said enum
    public BattleState bs = BattleState.OFF;

    //This will start the battle and play the animations for the characters in question
    public void BattleStart(Character c1, Character c2)
    {
        c1temp = c1;
        c2temp = c2;
        BUI = GetComponent<BattleUI>();
        bs = BattleState.START;
        BUI.setSprites(c1, c2);
        BUI.setStats(c1, c2);
        BUI.updateHealth(c1, c2);
        BUI.cutinPan(true);

    }

    // Update is called once per frame
    void Update()
    {

        //Changes the battle based upon battle state
        switch (bs)
        {
            case BattleState.ACTION:
                action();
                break;

            case BattleState.RESOLUTION:
                BUI.cutinPan(false);
                BUI.battlePan(true);
                resolution();
                break;

            case BattleState.END:

                break;

            //Start and Off do nothing as both are covered in the battlestart function
            case BattleState.START:
                break;

            case BattleState.OFF:
                BUI.battlePan(false);
                BUI.cutinPan(false);
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
        //This is specifically to see if we are deciding turn order
        if (decidingTurn)
        {
            c1turn = true;
            c2turn = false;
            BUI.setBattleText(turnOrderPrompt);
            //Sets UI elements based on who rolled last (which tells us who is rolling now)
            if (!lastRoll)
            {
                BUI.setC2Roll(false);
                //BUI.setC2Text(nothing);
                BUI.setC1Text(rollPromt);
                BUI.setC1Roll(true);
            }
            else
            {
                BUI.setC1Roll(false);
                //BUI.setC1Text(nothing);
                BUI.setC2Text(rollPromt);
                BUI.setC2Roll(true);

            }
        }
        //This exist until an animation can trigger the below code
        else if(!timer)
        {
            //Finds who had higher initiative
            if (c1Initiative > c2Initiative) 
            {
                c1turn = true;
                c2turn = false;
                BUI.setBattleText("C1 will go first");
                bs = BattleState.ACTION;
            }
            else if(c1Initiative < c2Initiative)
            {
                c1turn = false;
                c2turn = true;
                BUI.setBattleText("C2 will go first");
                bs = BattleState.ACTION;
            }
            else
            {
                decidingTurn = true;
                timer = false;
                BUI.setC1Text(nothing);
                BUI.setC2Text(nothing);
            }
        }
    }

    //This is the action phase of combat where players will roll to attack and defend/evade
    private void action()
    {
        //checks if it is the first turn of the cycle
        if (firstTurn)
        {
            actionRolls();
        }
        else
        {
            actionRolls();
            timer = true;
            decidingTurn = true;
            BUI.setBattleText("INITIATIVE SHAKEUP!");
            StartCoroutine(timed());
            if (!timer)
            {
                bs = BattleState.RESOLUTION;
            }
        }
    }

    private void actionRolls()
    {
        if (c1turn)
        {
            lastRoll = false;
            BUI.setC1Text("Roll to Attack! ");
            BUI.setC1Roll(true);

            if (lastRoll)
            {
                BUI.setC1Text("Attack: " + c1atk);
                BUI.setC1Roll(false);
                BUI.setC2But(true);
                BUI.setC2Text("Pick an option");
                if (optionPicked)
                {
                    optionPicked = false;
                    BUI.setC2But(false);
                    BUI.setC2Roll(true);
                    BUI.setC2Text("ROLL!");
                }
            }
        }
        else if (c2turn)
        {
            lastRoll = true;

        }
    }

    //This will be called by the "roll" buttons
    public void DiceRoll()
    {
        diceRoll = Random.Range(1, 7);

        //Deciding turn is true only when deciding the turn order
        if (decidingTurn)
        {
            //Checks who's roll it is
            if (!lastRoll)
            {
                c1Initiative = diceRoll + c1temp.stats.speed;
                BUI.setC1Text("Initiative: " + c1Initiative);
            }
            else
            {
                c2Initiative = diceRoll + c2temp.stats.speed;
                BUI.setC2Text("Initiative: " + c2Initiative);
                decidingTurn = false;
                StartCoroutine(timed());
            }
        }
        //this is for when turn is not being decided
        else
        {
            if (!lastRoll)
            {
                c1atk = diceRoll + c1temp.stats.attack;
                c1def = diceRoll + c1temp.stats.defense;
                c1evd = diceRoll + c1temp.stats.evade;
            }
            else
            {
                c2atk = diceRoll + c2temp.stats.attack;
                c2def = diceRoll + c2temp.stats.defense;
                c2evd = diceRoll + c2temp.stats.evade;
            }
        }
        lastRoll = !lastRoll;

        //sets the image on the dice
        BUI.SetDieImg(true, diceRoll);

        //removes buttons so the player can't spam roll
        BUI.setC1Roll(false);
        BUI.setC2Roll(false);
    }

    //This will play when the roll animation is finished
    //public void RollCalc()
    //{
    //    if (decidingTurn)
    //    {
    //        if (!lastRoll)
    //        {
    //            c1Initiative = diceRoll + c1temp.stats.speed;
    //            BUI.setC1Text("Initiative: " + c1Initiative);
    //        }
    //        else
    //        {
    //            c2Initiative = diceRoll + c2temp.stats.speed;
    //            BUI.setC2Text("Initiative: " + c2Initiative);
    //            decidingTurn = false;
    //        }
    //    }

    //    lastRoll = !lastRoll;
    //}

    public void setDefenseive(bool b)
    {
        defEvd = b;
        optionPicked = true;
    }

    private IEnumerator timed()
    {
        yield return new WaitForSeconds(1f);
        timer = false;
    }
}


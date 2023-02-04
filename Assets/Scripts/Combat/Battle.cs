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

    //This determines who has rolled for each individual player turn
    private bool c1rolled = false;
    private bool c2rolled = false;

    //this determines who rolled last True = character1 rolled last
    private bool lastRoll = false;

    //This tells us if damage has been resolved
    private bool dmgResolve = false;

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
                ending();
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

    //Turns everything off and resets boolean values
    private void ending()
    {
        BUI.setC1But(false);
        BUI.setC2But(false);
        BUI.setC2Roll(false);
        BUI.setC1Roll(false);

        timer = true;
        firstTurn = true;
        decidingTurn = true;
        c1turn = true;
        c2turn = false;
        c1rolled = false;
        c2rolled = false;
        lastRoll = false;
        dmgResolve = false;

        BUI.battlePan(false);
        BUI.cutinPan(false);

        bs = BattleState.OFF;
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
            //ensuring important bools are set
            c1rolled = false;
            c2rolled = false;
            firstTurn = true;
            //Finds who had higher initiative
            if (c1Initiative > c2Initiative) 
            {
                c1turn = true;
                c2turn = false;
                BUI.setBattleText(c1temp.stats.name + " will go first");
                lastRoll = false;
                bs = BattleState.ACTION;
            }
            else if(c1Initiative < c2Initiative)
            {
                c1turn = false;
                c2turn = true;
                BUI.setBattleText(c2temp.stats.name + " will go first");
                lastRoll = true;
                bs = BattleState.ACTION;
            }
            else
            {
                decidingTurn = true;
                timer = true;
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
            //ensures turn has ended before initiative shakeup
            if (dmgResolve)
            {
                timer = true;
                decidingTurn = true;
                dmgResolve = false;
                firstTurn = true;
                lastRoll = false;
                bs = BattleState.RESOLUTION;
            }
        }
    }

    private void actionRolls()
    {
        if (c1turn)
        {
            BUI.setC1Text("Roll to Attack! ");
            BUI.setC1Roll(true);

            if (lastRoll)
            {
                timer = true;
                //displays the attack total for character 1, prevents character 1 from rolling
                BUI.setC1Roll(false);
                BUI.setC1Text("Attack: " + c1atk);
                //This is where the if statement will go after picking
                if (optionPicked)
                {
                    BUI.setC2But(false);
                    BUI.setC2Roll(true);
                    BUI.setC2Text("ROLL!");
                    BUI.setBattleText(nothing);
                }
                //This is where the if statement will go prior to picking defend or evade
                else
                {
                    BUI.setC2But(true);
                    BUI.setC2Text(nothing);
                    BUI.setBattleText("Choose an option");
                }

            }
            //both will be true when both of rolled
            if(c1rolled && c2rolled)
            {
                //displays the attack total for character 1, prevents character 1 from rolling, prevents 2 from rolling
                BUI.setC1Roll(false);
                BUI.setC1Text("Attack: " + c1atk);
                BUI.setC2Roll(false);
                //if true defending calculation is used, if false we are evading
                if (defEvd)
                {
                    int damage = c1atk - c2def;
                    //ensures while defending you always take one damage at minimum
                    if(damage < 1)
                    {
                        damage = 1;
                    }

                    BUI.setC2Text("Defense: " + c2def);
                    BUI.setBattleText(c2temp.stats.name + " Takes " + damage + " Damage!");

                    if (!timer)
                    {

                        c2temp.stats.takeDamage(damage);

                        c1rolled = false;
                        c2rolled = false;

                        //Sets healthbar
                        BUI.updateHealth(c1temp,c2temp);

                        Debug.Log("C2 health: " + c2temp.stats.health);
                        if(c2temp.stats.health < 1)
                        {
                            Debug.Log("In WIN");
                            BUI.setBattleText(c1temp.stats.name + " WINS");
                            bs = BattleState.END;
                        }
                        else
                        {
                            c1turn = false;
                            c2turn = true;
                            dmgResolve = true;
                        }
                        timer = true;
                        

                    }
                    else
                    {
                        StartCoroutine(timed());
                    }
                }
                else
                {
                    int damage = c1atk;

                    BUI.setC2Text("Evade: " + c2evd);
                    if (c2evd > damage)
                    {
                        BUI.setBattleText("Dodged!");
                    }
                    else
                    {
                        BUI.setBattleText(c2temp.stats.name + " Takes " + damage + " Damage!");
                    }

                    if (!timer)
                    {
                        if (c2evd <= damage)
                        {
                            c2temp.stats.takeDamage(damage);
                        }
                        c1rolled = false;
                        c2rolled = false;

                        //Sets healthbar
                        BUI.updateHealth(c1temp, c2temp);

                        Debug.Log("C2 health: " + c2temp.stats.health);

                        if (c2temp.stats.health < 1)
                        {
                            Debug.Log("In WIN");
                            BUI.setBattleText(c1temp.stats.name + " WINS");
                            bs = BattleState.END;
                        }
                        else
                        {
                            c1turn = false;
                            c2turn = true;
                            dmgResolve = true;
                        }
                        timer = true;

                    }
                    else
                    {
                        StartCoroutine(timed());
                    }
                }
            }

            //after dmage is resolved makes it no longer first turn, also ensures the last roll is correct
            if (dmgResolve && firstTurn)
            {
                firstTurn = false;
                dmgResolve = false;
                lastRoll = true;
            }
        }
        //WHITESPACE
        //
        //This is the same battle stuff but for the second player
        //
        //
        else if (c2turn)
        {
            BUI.setC2Text("Roll to Attack! ");
            BUI.setC2Roll(true);

            if (!lastRoll)
            {
                timer = true;
                //displays the attack total for character 2, prevents character 2 from rolling
                BUI.setC2Roll(false);
                BUI.setC2Text("Attack: " + c2atk);
                //This is where the if statement will go after picking
                if (optionPicked)
                {
                    BUI.setC1But(false);
                    BUI.setC1Roll(true);
                    BUI.setC1Text("ROLL!");
                    BUI.setBattleText(nothing);
                }
                //This is where the if statement will go prior to picking defend or evade
                else
                {
                    BUI.setC1But(true);
                    BUI.setC1Text(nothing);
                    BUI.setBattleText("Choose an option");
                }

            }
            //both will be true when both of rolled
            if (c1rolled && c2rolled)
            {
                //displays the attack total for character 2, prevents character 2 from rolling, prevents 1 from rolling
                BUI.setC2Roll(false);
                BUI.setC2Text("Attack: " + c2atk);
                BUI.setC1Roll(false);
                //if true defending calculation is used, if false we are evading
                if (defEvd)
                {
                    int damage = c2atk - c1def;
                    //ensures while defending you always take one damage at minimum
                    if (damage < 1)
                    {
                        damage = 1;
                    }

                    BUI.setC1Text("Defense: " + c1def);
                    BUI.setBattleText(c1temp.stats.name + " Takes " + damage + " Damage!");

                    if (!timer)
                    {
                        c1temp.stats.takeDamage(damage);
                        c1rolled = false;
                        c2rolled = false;

                        //Sets healthbar
                        BUI.updateHealth(c1temp, c2temp);

                        Debug.Log("C1 health: " + c1temp.stats.health);

                        if (c1temp.stats.health < 1)
                        {
                            Debug.Log("In WIN");
                            BUI.setBattleText(c2temp.stats.name + " WINS");
                            bs = BattleState.END;
                        }
                        else
                        {
                            c1turn = true;
                            c2turn = false;
                            dmgResolve = true;
                        }
                        timer = true;

                    }
                    else
                    {
                        StartCoroutine(timed());
                    }
                }
                else
                {
                    int damage = c2atk;

                    BUI.setC1Text("Evade: " + c1evd);
                    if (c1evd > damage)
                    {
                        BUI.setBattleText("Dodged!");
                    }
                    else
                    {
                        BUI.setBattleText(c1temp.stats.name + " Takes " + damage + " Damage!");
                    }

                    if (!timer)
                    {
                        if (c1evd <= damage)
                        {
                            c1temp.stats.takeDamage(damage);
                        }
                        c1rolled = false;
                        c2rolled = false;

                        //Sets healthbar
                        BUI.updateHealth(c1temp, c2temp);

                        Debug.Log("C1 health: " + c1temp.stats.health);

                        if (c1temp.stats.health < 1)
                        {
                            Debug.Log("In WIN");
                            BUI.setBattleText(c2temp.stats.name + " WINS");
                            bs = BattleState.END;
                        }
                        else
                        {
                            c1turn = true;
                            c2turn = false;
                            dmgResolve = true;
                        }
                        timer = true;

                    }
                    else
                    {
                        StartCoroutine(timed());
                    }
                }
            }

            //after dmage is resolved makes it no longer first turn
            if (dmgResolve && firstTurn)
            {
                firstTurn = false;
                dmgResolve = false;
                lastRoll = false;
            }
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
                timer = true;
            }
            else
            {
                c2Initiative = diceRoll + c2temp.stats.speed;
                BUI.setC2Text("Initiative: " + c2Initiative);
                decidingTurn = false;
                timer = true;
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
                c1rolled = true;
                optionPicked = false;
            }
            else
            {
                c2atk = diceRoll + c2temp.stats.attack;
                c2def = diceRoll + c2temp.stats.defense;
                c2evd = diceRoll + c2temp.stats.evade;
                c2rolled = true;
                optionPicked = false;
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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    GameManager gm;
    float AIWaitTime = 1;
    List<Character> characters;
    private int moveDirection;

    private void Start()
    {
        //Display Roll UI
    }


    IEnumerator AIStall()
    {
        yield return new WaitForSeconds(AIWaitTime);
    }


    public void Roll()
    {
        int movement = Random.Range(1, 6);
        if (gm.AIturn)
        {
            //display roll
            AIStall();
            Move(movement);
        }
        else
        {
            //display roll with confirm button
        }
    }

    public void Move(int spaces)
    {
        if(spaces >= 1)
        {
            if (gm.AIturn)
            {
                //go random direction if needed
                //avoid last tile
                //move characters[gm.turnCount].GameObject movement number of spaces
            }
            else
            {
                //pick direction if nessicary
                //move characters[gm.turnCount].GameObject movement number of spaces
            }
            //update last tile
            //update current tile
            Move(spaces - 1);
        }
        else
        {
            //check tile type
            //resolve tile effect if any;
            gm.incrementTurn();
            if (!gm.AIturn)
            {
                //display turn start UI
            }
            else
            {
                AIStall();
                Roll();
            }
        }        
    }

    /// <summary>
    /// 0 for up, 1 for right, 2 for downn, 3 for left
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public void PickDirection(int direction)
    {
        moveDirection = direction;
    }
}

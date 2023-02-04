using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int playercount;
    public int turnCount = 1;
    GameManager instance;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    /// <summary>
    /// Returns true for a players turn and false for an AI's turn
    /// </summary>
    /// <returns></returns>
    public bool incrementTurn()
    {
        if(turnCount < 4)
        {
            turnCount++;
        }
        else
        {
            turnCount = 1;
        }
        if(turnCount > playercount && turnCount < 4)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}

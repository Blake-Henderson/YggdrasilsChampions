using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int playercount;
    public int turnCount = 0;
    public bool AIturn;
    public static GameManager instance;

    private void Start()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Returns true for a players turn and false for an AI's turn
    /// </summary>
    /// <returns></returns>
    public void incrementTurn()
    {
        if(turnCount < 3)
        {
            turnCount++;
        }
        else
        {
            turnCount = 0;
        }
        if(turnCount > playercount)
        {
            AIturn = true;
        }
        else
        {
            AIturn = false;
        }
    }
}

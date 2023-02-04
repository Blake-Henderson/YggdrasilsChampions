using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public Board board;
    public GameManager gm;
    float AIWaitTime = 1;
    [SerializeField]
    public List<Character> characters;
    private int moveDirection;
    
    public GameObject[] dirButtons = new GameObject[4];
    public GameObject rollButton;
    public GameObject results, resultsConfirm;
    public TextMeshProUGUI resultsText;

    int moves;

    private void Start()
    {
        foreach (Character c in characters) //place initial character
        {
            c.lastTile = c.currentTile;
            c.currentTile = board.map.WorldToCell(c.transform.position);
            c.transform.position = board.map.CellToWorld(c.currentTile) + board.map.tileAnchor;

        }
        PromptRoll();
    }


    IEnumerator AIStall()
    {
        yield return new WaitForSeconds(AIWaitTime);
    }

    IEnumerator AIDelayToShowRoll()
    {
        yield return new WaitForSeconds(AIWaitTime);
        results.SetActive(false);
        Move(moves);
    }

    void PromptRoll()
    {
        rollButton.SetActive(true);
    }

    void DisplayRoll(int result, bool confirmButton = false)
    {
        results.SetActive(true);
        resultsText.text = result.ToString();
        moves = result;
        resultsConfirm.SetActive(confirmButton);
    }

    public void Roll()
    {
        int movement = Random.Range(1, 6);
        if (gm.AIturn)
        {
            DisplayRoll(movement);
            StartCoroutine(AIDelayToShowRoll());
        }
        else
        {
            DisplayRoll(movement, true);
        }
    }

    public void Move()
    {
        Move(moves);
    }

    public void MoveCharacter(int index)
    {
        foreach (GameObject o in dirButtons) o.SetActive(false);
        Vector3Int[] n = board.GetNeighbors(characters[gm.turnCount].currentTile);
        MoveCharacter(n[index]);
    }

    void MoveCharacter(Vector3Int cell, bool initialMove = false)
    {
        characters[gm.turnCount].lastTile = characters[gm.turnCount].currentTile;
        characters[gm.turnCount].currentTile = cell;
        characters[gm.turnCount].transform.position = board.map.CellToWorld(characters[gm.turnCount].currentTile) + board.map.tileAnchor;
        board.StepOnTile(cell);
    }

    public void Move(int spaces)
    {
        Debug.Log(spaces);
        if(spaces >= 1)
        {
            if (gm.AIturn)
            {
                //go random direction if needed
                //avoid last tile
                Vector3Int[] n = board.GetNeighbors(characters[gm.turnCount].currentTile);
                List<int> valid = new List<int>();
                for (int i = 0; i < 4; ++i)
                {
                    if (!(n[i] == new Vector3Int(-99999, -99999) || n[i] == characters[gm.turnCount].lastTile))
                    {
                        valid.Add(i);
                    }
                }
                if (valid.Count == 0)
                {
                    MoveCharacter(characters[gm.turnCount].lastTile);
                } else
                {
                    MoveCharacter(n[valid[Random.Range(0, valid.Count)]]);
                }
                Move(spaces - 1);
                //move characters[gm.turnCount].GameObject movement number of spaces
            }
            else
            {
                foreach (GameObject o in dirButtons) o.SetActive(false);
                //pick direction if nessicary
                Vector3Int[] n = board.GetNeighbors(characters[gm.turnCount].currentTile);
                int options = 0;
                Vector3Int only = Vector3Int.zero; //only valid option, if changed more than once, it wont end up being used
                for (int i = 0; i < 4; ++i)
                {
                    if (!(n[i] == new Vector3Int(-99999, -99999) || n[i] == characters[gm.turnCount].lastTile))
                    {
                        dirButtons[i].SetActive(true);
                        only = n[i];
                        options++;
                    }
                }
                if (options == 0)
                {
                    MoveCharacter(characters[gm.turnCount].lastTile);
                    Move(spaces - 1);
                }
                else if (options == 1)
                {
                    MoveCharacter(only);
                    Move(spaces - 1);
                } else
                {
                    moves = spaces - 1;
                }
                //move characters[gm.turnCount].GameObject movement number of spaces
            }
        }
        else
        {
            foreach (GameObject o in dirButtons) o.SetActive(false);
            board.EndOnTile(characters[gm.turnCount].currentTile);
            //check tile type
            //resolve tile effect if any;
            gm.incrementTurn();
            if (!gm.AIturn)
            {
                PromptRoll();
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

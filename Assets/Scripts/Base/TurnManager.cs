using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;

    public Board board;
    public GameManager gm;
    float AIWaitTime = 1;
    public List<Character> characters;
    private int moveDirection;
    
    public GameObject[] dirButtons = new GameObject[4];
    public GameObject dirButtonContainer;
    public GameObject rollButton;
    public GameObject results, resultsConfirm;
    public TextMeshProUGUI resultsText;

    public OccupiedTileSelect occupiedCanvas;

    int moves;
    bool needOptions;

    List<Character> fightable = new List<Character>();

    private void Start()
    {
        instance = this;
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
        Move();
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
        int movement = Random.Range(1, 7);
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
    }

    public void FinishInterrupt(bool occupiedTileInterrupt)
    {
        bool interrupt = false;
        if (occupiedTileInterrupt) interrupt = board.StepOnTile(characters[gm.turnCount].currentTile);
        if (!interrupt) Move();
    }

    public List<Character> OthersOccupyingTile(Character entering)
    {
        List<Character> ret = new List<Character>();
        foreach (Character c in characters)
        {
            if (c == entering) continue;
            if (c.currentTile == entering.currentTile && c.stats.health > 0) ret.Add(c);
        }
        return ret;
    }

    public void MoveSelectionMade() //Called by the move buttons, if there's nothing to interrupt the move in tile interactions, keep moving
    {
        bool interrupted = HandleTileInteractions();
        if (!interrupted) Move();
    }

    public void AIDoneFighting()
    {
        PlayerInfoUI.instance.gameObject.SetActive(true);
        Battle.instance.finishEvent.RemoveListener(AIDoneFighting);
        if (characters[gm.turnCount].stats.health <= 0)
        {
            EndTurn();
            return;
        }
        if (fightable.Count == 0) FinishInterrupt(true);
        else AIChooseWhichToFight();
    }

    public void AIChooseWhichToFight()
    {
        int i = Random.Range(0, fightable.Count);
        int fight = Random.Range(0, 2);
        Character toFight = fightable[i];
        fightable.RemoveAt(i);
        if (fight == 1)
        {
            PlayerInfoUI.instance.gameObject.SetActive(false);
            Battle.instance.BattleStart(characters[gm.turnCount], toFight);
            Battle.instance.finishEvent.AddListener(AIDoneFighting);
        } else
        {
            AIDoneFighting();
        }
    }

    public void Move()
    {
        if (characters[gm.turnCount].stats.health <= 0)
        {
            EndTurn();
            return;
        }
        foreach (GameObject o in dirButtons) o.SetActive(false);
        dirButtonContainer.SetActive(true);
        while (moves > 0)
        {
            moves--;
            if (gm.AIturn)
            {
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
                }
                else
                {
                    MoveCharacter(n[valid[Random.Range(0, valid.Count)]]);
                }
                List<Character> occupying = OthersOccupyingTile(characters[gm.turnCount]);
                if (occupying.Count > 0)
                {
                    fightable = occupying;
                    AIChooseWhichToFight();
                    return;
                }
                bool interrupt = board.StepOnTile(characters[gm.turnCount].currentTile);
                if (interrupt) return;
            } else
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
                Vector3Int target = new Vector3Int(-99999, -99999);
                if (options == 0)
                {
                    target = characters[gm.turnCount].lastTile;
                }
                else if (options == 1)
                {
                    target = only;
                }
                if (target != new Vector3Int(-99999, -99999))
                {
                    MoveCharacter(target);
                    bool interrupted = HandleTileInteractions();
                    if (interrupted) return;
                }
                else //player needs to make a choice
                {
                    return; //wait for player input if not at end of movement
                }
            }
        }
        foreach (GameObject o in dirButtons) o.SetActive(false);
        bool endInterrupt = board.EndOnTile(characters[gm.turnCount].currentTile);
        if (!endInterrupt) EndTurn();
        //check tile type
        //resolve tile effect if any;
    }

    public void EndTurn()
    {

        gm.incrementTurn();
        if (characters[gm.turnCount].stats.health <= 0) //player is KO, skip turn for heal
        {
            characters[gm.turnCount].stats.heal(9999);
            EndTurn();
        } else
        {
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

    public bool HandleTileInteractions()
    {
        List<Character> occupying = OthersOccupyingTile(characters[gm.turnCount]);
        if (occupying.Count > 0)
        {
            dirButtonContainer.SetActive(false);
            occupiedCanvas.DisplayOptions(occupying);
            return true;
        }
        bool interrupt = board.StepOnTile(characters[gm.turnCount].currentTile);
        return interrupt;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
    public Board board;
    public Tilemap tilemap;
    public GameObject[] buttons = new GameObject[4];
    public Character ch;
    public float timeToNextMove = 0f;

    private void Start()
    {
        MoveCharacter(tilemap.WorldToCell(ch.transform.position));
    }

    void MoveCharacter(Vector3Int cell)
    {
        foreach (GameObject o in buttons) o.SetActive(false);
        ch.lastTile = ch.currentTile;
        ch.currentTile = cell;
        ch.transform.position = tilemap.CellToWorld(ch.currentTile) + tilemap.tileAnchor;
        timeToNextMove = 0.5f;
    }

    public void MoveCharacter(int index)
    {
        foreach (GameObject o in buttons) o.SetActive(false);
        Vector3Int[] n = board.GetNeighbors(ch.currentTile);
        MoveCharacter(n[index]);
    }

    void HandleMove()
    {
        Vector3Int[] n = board.GetNeighbors(ch.currentTile);
        foreach (GameObject o in buttons) o.SetActive(false);
        int options = 0;
        Vector3Int only = Vector3Int.zero;
        for (int i = 0; i < 4; ++i)
        {
            if (!(n[i] == new Vector3Int(-99999, -99999) || n[i] == ch.lastTile))
            {
                only = n[i];
                options++;
                buttons[i].gameObject.SetActive(true);
            }
        }
        if (options == 1)
        {
            MoveCharacter(only);
        } else if (options == 0)
        {
            MoveCharacter(ch.lastTile);
        }
    }

    void Update()
    {
        if (timeToNextMove <= 0f)
        {
            HandleMove();
        } else
        {
            timeToNextMove -= Time.deltaTime;
        }
    }
}

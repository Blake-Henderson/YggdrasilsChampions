using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Stats stats;
    public Vector3Int currentTile = new Vector3Int(-99999, -99999);
    public Vector3Int lastTile;
    public Item empty;
    public Item[] inventory = new Item[3];
    private void Start()
    {
        for(int i = 0; i<3; i++)
        {
            inventory[i] = empty;
        }
    }
}

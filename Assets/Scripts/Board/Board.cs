using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Tilemap map;
    [SerializeField]
    private List<TileType> tileTypes = new List<TileType>();

    private Dictionary<TileBase, TileType> baseTypeDict = new Dictionary<TileBase, TileType>();

    private Plane plane;
    
    void Start()
    {
        plane = new Plane(Vector3.forward, Vector3.zero);
        foreach (TileType ty in tileTypes)
        {
            foreach (TileBase ba in ty.tiles)
            {
                if (!baseTypeDict.ContainsKey(ba)) baseTypeDict.Add(ba, ty);
            }
        }
    }

    public bool StepOnTile(Vector3Int pos)
    {
        TileBase tile = map.GetTile(pos);
        return baseTypeDict[tile].OnStep();
    }

    public bool EndOnTile(Vector3Int pos)
    {
        TileBase tile = map.GetTile(pos);
        return baseTypeDict[tile].OnEnd();
    }

    static Vector3Int[] directions = { new Vector3Int(0, 1), new Vector3Int(1, 0), new Vector3Int(0, -1), new Vector3Int(-1, 0) };

    public Vector3Int[] GetNeighbors(Vector3Int pos)
    {
        Vector3Int[] ret = new Vector3Int[4];
        for (int i = 0; i < 4; ++i)
        {
            TileBase tile = map.GetTile(pos + directions[i]);
            ret[i] = tile == null ? new Vector3Int(-99999, -99999) : pos + directions[i];
        }
        return ret;
    }
}

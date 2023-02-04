using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    [SerializeField]
    private Tilemap map;
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
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out float enter))
            {
                Vector3 mouse = ray.GetPoint(enter);
                Vector3Int gridPos = map.WorldToCell(mouse);
                Debug.Log(Input.mousePosition + " " + mouse + " " + gridPos);
                TileBase tile = map.GetTile(gridPos);

                Debug.Log("Tile at " + gridPos + " is of type " + baseTypeDict[tile]); //assuming its in the dict rn
                string str = "Tile at " + gridPos + " walkable neighbors: \n";
                Vector3Int[] neighbors = GetNeighbors(gridPos);
                foreach (Vector3Int v in neighbors) str += v.ToString() + '\n';
                Debug.Log(str);
            }
        }
    }
}

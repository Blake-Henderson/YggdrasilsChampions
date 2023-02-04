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
            }
        }
    }
}

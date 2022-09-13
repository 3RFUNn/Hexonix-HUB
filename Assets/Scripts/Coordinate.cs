using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Coordinate : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] public Tilemap Tilemap;
    [SerializeField] private Tile cells;
    [SerializeField] private GridBoard bgBoardOBJ;
    [SerializeField] public GridBoard[,] grid;
    [SerializeField] private GameObject Parent;
    [SerializeField] private GameObject g;
    
    private void Start()
    {
        
        grid = new GridBoard[gridSize.x, gridSize.y];
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                if (Tilemap.GetTile(new Vector3Int(x,y)) == cells)
                {
                    var b = Instantiate(bgBoardOBJ, Parent.transform, true);
                    b.Setup(true,x,y,b.transform);
                    b.transform.position = Tilemap.GetCellCenterWorld((new Vector3Int(x, y)));
                    Tilemap.SetTile(new Vector3Int(x, y), null);
                }
                
            }
        }
    }

    public Vector3 GetGridPos(Vector3 pos)
    {
        return Tilemap.GetCellCenterWorld(Tilemap.WorldToCell(pos));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

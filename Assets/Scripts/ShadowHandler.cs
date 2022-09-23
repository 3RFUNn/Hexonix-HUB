using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShadowHandler : MonoBehaviour
{
    [SerializeField] private int shadowsCount;


    private SpriteRenderer[] cells;

    private int activeCells;
    private bool isDragging;
    private PieceHolder piece;
    private Tilemap Tilemap;


    private bool temp;
    void Start()
    {
        Tilemap = GameManager.Instance.tileMap;
        cells = new SpriteRenderer[shadowsCount];
        var a = new GameObject("Shadow", typeof(SpriteRenderer)).GetComponent<SpriteRenderer>();

        for (int i = 0; i < shadowsCount; i++)
        {
            cells[i] = Instantiate(a);
            cells[i].gameObject.SetActive(false);
            cells[i].transform.SetParent(transform);
            var c = cells[i].color;
            c.a = 0.5f;
            cells[i].color = c;
        }
        Destroy(a.gameObject);
        PlaceHolder.OnClicked += OnPlaceHolderClicked;
        PlaceHolder.OnReleaseFull += OnPlaceHolderReleased;
    }

    void OnPlaceHolderClicked(PlaceHolder holder)
    {
        activeCells = holder.piece.data.data.Length;
        isDragging = true;
        piece = holder.piece;
        for (int i = 0; i < activeCells; i++)
        {
            cells[i].sprite = piece.shapes[i].sprite;
        }
    }
    void OnPlaceHolderReleased(PlaceHolder holder)
    {
        foreach (var VARIABLE in cells)
        {
            VARIABLE.gameObject.SetActive((false));
        }

        isDragging = false;
    }

    void Update()
    {
        if (!isDragging)
            return;

        for (int i = 0; i < activeCells; i++)
        {
            var gridID = Tilemap.WorldToCell(piece.shapes[i].transform.position);
            var grid = GameManager.Instance.grid.GetGridBoard(gridID.x, gridID.y);
            if(grid==null)
            {
                temp = false;
                break;
            }
            
            if (grid.Placable)
            {
                temp = true;
                cells[i].transform.position = Tilemap.CellToWorld(new Vector3Int(grid.X, grid.Y));
            }
            else
            {
                temp = false;
                break;
            }
        }
        
        foreach (var UPPER in cells)
        {
            UPPER.gameObject.SetActive(temp);
        }
        

    }
}

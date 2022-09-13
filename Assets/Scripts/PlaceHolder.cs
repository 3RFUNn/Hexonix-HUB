using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceHolder : MonoBehaviour
{
    public static Action<PlaceHolder>OnClicked=delegate(PlaceHolder holder) {  };
    public static Action<PlaceHolder>OnClickRelease=delegate(PlaceHolder holder) {  };
    private Vector3 _dragOffset;
    private Camera _cam;
    private Tilemap tm;
    private PieceHolder piece;
    private bool isDragging;
    void Awake() {
        _cam = Camera.main;
    }

    private void Start()
    {
        piece = GameManager.Instance.GetPiece();
        Transform transform1;
        (transform1 = piece.transform).SetParent(transform);
        transform1.localPosition=Vector3.zero;
        tm = GameManager.Instance.tileMap;
    }

    private void Update()
    {
        if (!isDragging)
            return;
        Transform transform1;
        (transform1 = piece.transform).position = GetMousePos() + _dragOffset;
            //Vector3.MoveTowards(transform.position, GetMousePos() + _dragOffset, _speed * Time.deltaTime);
       Debug.Log(tm.WorldToCell(piece.transform.position)); 
    }

    void OnMouseDown()
    {
        isDragging = true;
        _dragOffset = transform.position - GetMousePos();
        OnClicked(this);
    }

    private void OnMouseUp()
    {
        if (!isDragging)
        {
            return;
        }

        isDragging = false;
        piece.transform.localPosition= Vector3.zero;
        OnClickRelease(this);
    }

    Vector3 GetMousePos() {
        var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
    public bool CheckPiecePlacement(PieceHolder p,GridBoard g)
    {
        Vector2[] x = p.data.data;
        for(int i = 0; i < x.Length; i++)
        {
            GridBoard gg;
            if (x[i].Equals(new Vector2(0,0)))
            {
                if (!g.IsFull)
                    return false;
            }
            if (g.Y % 2 == 0)
            {
                if (x[i].Equals(new Vector2(0, 1)))
                {
                    gg = WebSpider.FindTheTopRight(g);
                    if (!gg.IsFull)
                    {
                        return false;
                    }
                    g = gg;
                }
                if (x[i].Equals(new Vector2(1, 0)))
                {
                    gg = WebSpider.FindTheRight(g);
                    if (!gg.IsFull)
                    {
                        return false;
                    }
                    g = gg;
                }
                if (x[i].Equals(new Vector2(0, -1)))
                {
                    gg = WebSpider.FindTheRightDown(g);
                    if (!gg.IsFull)
                    {
                        return false;
                    }
                    g = gg;
                }
                if (x[i].Equals(new Vector2(-1, 1)))
                {
                    gg = WebSpider.FindTheLeftTop(g);
                    if (!gg.IsFull)
                    {
                        return false;
                    }
                    g = gg;
                }
                if (x[i].Equals(new Vector2(-1, 0)))
                {
                    gg = WebSpider.FindTheLeft(g);
                    if (!gg.IsFull)
                    {
                        return false;
                    }
                    g = gg;
                }
                if (x[i].Equals(new Vector2(-1, -1)))
                {
                    gg = WebSpider.FindTheLeftDown(g);
                    if (!gg.IsFull)
                    {
                        return false;
                    }
                    g = gg;
                }
            }
            else
            {
                if (x[i].Equals(new Vector2(1, 1)))
                {
                    gg = WebSpider.FindTheTopRight(g);
                    if (!gg.IsFull)
                    {
                        return false;
                    }
                    g = gg;
                }
                if (x[i].Equals(new Vector2(1, 0)))
                {
                    gg = WebSpider.FindTheRight(g);
                    if (!gg.IsFull)
                    {
                        return false;
                    }
                    g = gg;
                }
                if (x[i].Equals(new Vector2(1, -1)))
                {
                    gg = WebSpider.FindTheLeftDown(g);
                    if (!gg.IsFull)
                    {
                        return false;
                    }
                    g = gg;
                }
                if (x[i].Equals(new Vector2(0, 1)))
                {
                    gg = WebSpider.FindTheLeftTop(g);
                    if (!gg.IsFull)
                    {
                        return false;
                    }
                    g = gg;
                }
                if (x[i].Equals(new Vector2(-1, 0)))
                {
                    gg = WebSpider.FindTheLeft(g);
                    if (!gg.IsFull)
                    {
                        return false;
                    }
                    g = gg;
                }
                if (x[i].Equals(new Vector2(0, -1)))
                {
                    gg = WebSpider.FindTheLeftDown(g);
                    if (!gg.IsFull)
                    {
                        return false;
                    }
                    g = gg;
                }
            }
        }
        return true;
    }
}

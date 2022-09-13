using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceHolder : MonoBehaviour
{
    public static Action<PlaceHolder>OnClicked=delegate(PlaceHolder holder) {  };
    public static Action<PlaceHolder>OnClickRelease=delegate(PlaceHolder holder) {  };
    public PieceHolder piece;
    private Vector3 _dragOffset;
    private Camera _cam;
    private Tilemap tm;
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
        var gg = CheckPiecePlacement(piece,
            GameManager.Instance.grid.WorldPosToGridboard(piece.shapes[0].transform.position));
        if (gg)
        {
            foreach (var VARIABLE in piece.shapes)
            {
                var g = GameManager.Instance.grid.WorldPosToGridboard(VARIABLE.transform.position);
                g.Child = VARIABLE.transform.parent;
            }
            Destroy(piece.gameObject);
            piece = GameManager.Instance.GetPiece();
        }
        else
        {
            piece.transform.localPosition= Vector3.zero;
            OnClickRelease(this);
        }

        Debug.Log(gg);

    }

    Vector3 GetMousePos() {
        var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
    public bool CheckPiecePlacement(PieceHolder p,GridBoard g)
    {
        Debug.Log(g.X + " " + g.Y);
        Vector2[] x = p.data.data;
        for(int i = 0; i < x.Length; i++)
        {
            Debug.Log(g.X + " " + g.Y);
            GridBoard gg=null;
            bool condition = !g.IsFull && g.Placable;
            if (x[i].Equals(new Vector2(0,0)))
            {
                if (condition)
                    return false;
            }
            if (g.Y % 2 == 0)
            {
                condition = !gg.IsFull && gg.Placable;
                if (x[i].Equals(new Vector2(0, 1)))
                {
                    gg = WebSpider.FindTheTopRight(g);
                    if (condition)
                    {
                        return false;
                    }
                    g = gg;
                }                

                if (x[i].Equals(new Vector2(1, 0)))
                {
                    gg = WebSpider.FindTheRight(g);
                    if (condition)
                    {
                        return false;
                    }
                    g = gg;
                }
                if (x[i].Equals(new Vector2(0, -1)))
                {
                    gg = WebSpider.FindTheRightDown(g);
                    if (condition)
                    {
                        return false;
                    }
                    g = gg;
                }
                if (x[i].Equals(new Vector2(-1, 1)))
                {
                    gg = WebSpider.FindTheLeftTop(g);
                    if (condition)
                    {
                        return false;
                    }
                    g = gg;
                }
                if (x[i].Equals(new Vector2(-1, 0)))
                {
                    gg = WebSpider.FindTheLeft(g);
                    if (condition)
                    {
                        return false;
                    }
                    g = gg;
                }
                if (x[i].Equals(new Vector2(-1, -1)))
                {
                    gg = WebSpider.FindTheLeftDown(g);
                    if (condition)
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
                    if (condition)
                    {
                        return false;
                    }
                    g = gg;
                }
                if (x[i].Equals(new Vector2(1, 0)))
                {
                    gg = WebSpider.FindTheRight(g);
                    if (condition)
                    {
                        return false;
                    }
                    g = gg;
                }
                if (x[i].Equals(new Vector2(1, -1)))
                {
                    gg = WebSpider.FindTheLeftDown(g);
                    if (condition)
                    {
                        return false;
                    }
                    g = gg;
                }
                if (x[i].Equals(new Vector2(0, 1)))
                {
                    gg = WebSpider.FindTheLeftTop(g);
                    if (condition)
                    {
                        return false;
                    }
                    g = gg;
                }
                if (x[i].Equals(new Vector2(-1, 0)))
                {
                    gg = WebSpider.FindTheLeft(g);
                    if (condition)
                    {
                        return false;
                    }
                    g = gg;
                }
                if (x[i].Equals(new Vector2(0, -1)))
                {
                    gg = WebSpider.FindTheLeftDown(g);
                    if (condition)
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

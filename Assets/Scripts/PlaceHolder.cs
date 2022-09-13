using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHolder : MonoBehaviour
{
    private Vector3 _dragOffset;
    private Camera _cam;

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
    }

    private void Update()
    {
        if (!isDragging)
            return;
        piece.transform.position = GetMousePos() + _dragOffset;
            //Vector3.MoveTowards(transform.position, GetMousePos() + _dragOffset, _speed * Time.deltaTime);
            
    }

    void OnMouseDown()
    {
        isDragging = true;
        _dragOffset = transform.position - GetMousePos();
    }

    private void OnMouseUp()
    {
        if (!isDragging)
        {
            return;
        }

        isDragging = false;
        piece.transform.localPosition= Vector3.zero;
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
            if (x[i]==new Vector2(0,0))
            {
                if (!g.IsFull)
                    return false;
            }
            if (g.Y % 2 == 0)
            {
                if (x[i]==new Vector2(0, 1))
                {
                    if (!WebSpider.FindTheTopRight(g).IsFull)
                    {
                        return false;
                    }
                }
                if (x[i] == new Vector2(1, 0))
                {
                    if (!WebSpider.FindTheRight(g).IsFull)
                    {
                        return false;
                    }
                }
                if (x[i] == new Vector2(0, -1))
                {
                    if (!WebSpider.FindTheRightDown(g).IsFull)
                    {
                        return false;
                    }
                }
                if (x[i] == new Vector2(-1, 1))
                {
                    if (!WebSpider.FindTheLeftTop(g).IsFull)
                    {
                        return false;
                    }
                }
                if (x[i] == new Vector2(-1, 0))
                {
                    if (!WebSpider.FindTheLeft(g).IsFull)
                    {
                        return false;
                    }
                }
                if (x[i] == new Vector2(-1, -1))
                {
                    if (!WebSpider.FindTheLeftDown(g).IsFull)
                    {
                        return false;
                    }
                }
            }
            else
            {
                if (x[i] == new Vector2(1, 1))
                {
                    if (!WebSpider.FindTheTopRight(g).IsFull)
                    {
                        return false;
                    }
                }
                if (x[i] == new Vector2(1, 0))
                {
                    if (!WebSpider.FindTheRight(g).IsFull)
                    {
                        return false;
                    }
                }
                if (x[i] == new Vector2(1, -1))
                {
                    if (!WebSpider.FindTheLeftDown(g).IsFull)
                    {
                        return false;
                    }
                }
                if (x[i] == new Vector2(0, 1))
                {
                    if (!WebSpider.FindTheLeftTop(g).IsFull)
                    {
                        return false;
                    }
                }
                if (x[i] == new Vector2(-1, 0))
                {
                    if (!WebSpider.FindTheLeft(g).IsFull)
                    {
                        return false;
                    }
                }
                if (x[i] == new Vector2(0, -1))
                {
                    if (!WebSpider.FindTheLeftDown(g).IsFull)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
}

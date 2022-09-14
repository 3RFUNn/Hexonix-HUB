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
        var tmp = GameManager.Instance.grid.WorldPosToGridboard(piece.shapes[0].transform.position);
        bool gg=false;
        if (tmp != null)
        {
            if (tmp.Y % 2 == 1)
            {
                gg=CheckPiecePlacement(piece.data.fard, tmp);
            }
            else
            {
                gg=CheckPiecePlacement(piece.data, tmp);
            }
        }
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
    public bool CheckPiecePlacement(PieceData p,GridBoard g)
    {
        Vector2[] x = p.data;
            for (int i = 0; i < x.Length; i++)
            {
                GridBoard tmp = GameManager.Instance.grid.grid[
                    g.X + (int)x[i].x, g.Y + (int)x[i].y
                    ];
                if (tmp.IsFull || !tmp.Placable)
                {
                    return false;
                }
            }
        return true;
    }
}

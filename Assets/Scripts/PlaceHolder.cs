using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Unity.Burst.Intrinsics.X86.Avx;

public class PlaceHolder : MonoBehaviour
{
    public static Action<PlaceHolder>OnClicked=delegate(PlaceHolder holder) {  };
    public static Action<PlaceHolder>OnReleaseFull=delegate(PlaceHolder holder) {  };
    public static Action<PlaceHolder>OnReleaseEmpty = delegate(PlaceHolder holder) {  };
    public static Action<PlaceHolder>OnRelease = delegate(PlaceHolder holder) {  };
    public PieceHolder piece;
    private Vector3 _dragOffset;
    private Camera _cam;
    private Tilemap tm;
    private bool isDragging;
    private bool isPlacable;
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
        OnRelease += OnPlaceHolderRelease;
       isPlacable= PlacableShape();
       RotationSetup();
       
    }

    private void Update()
    {
        if (!isDragging)
            return;
        Transform transform1;
        (transform1 = piece.transform).position = GetMousePos() + _dragOffset;
            //Vector3.MoveTowards(transform.position, GetMousePos() + _dragOffset, _speed * Time.deltaTime);
    }

    void OnPlaceHolderRelease(PlaceHolder holder)
    {
        isPlacable= PlacableShape();
    }
    void OnMouseDown()
    {
        if (isPlacable)
        {
            isDragging = true;
            _dragOffset = transform.position - GetMousePos();
            OnClicked(this);
        }
    }

    public void SetPiece(PieceHolder piece)
    {
        this.piece = piece;
        Transform PieceTransform;
        (PieceTransform = piece.transform).SetParent(transform);
        PieceTransform.localPosition=Vector3.zero;
    }

    private void OnMouseUp()
    {
        if (!isDragging)
        {
            return;
        }

        isDragging = false;
        var tmp = GameManager.Instance.grid.WorldPosToGridboard(piece.shapes[0].transform.position);
        //Debug.Log(RotatePieceClockwise(piece.data).data);
        bool gg = CheckPiecePlacement(piece.data, tmp);
        if (gg)
        {
            foreach (var VARIABLE in piece.shapes)
            {
                var g = GameManager.Instance.grid.WorldPosToGridboard(VARIABLE.transform.position);
                g.Child = VARIABLE.transform.parent;
            }
            Destroy(piece.gameObject);
            OnReleaseEmpty(this);
        }
        else
        {
            piece.transform.localPosition= Vector3.zero;
            OnReleaseFull(this);
        }

        OnRelease(this);
    }

    Vector3 GetMousePos() {
        var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

    public bool PlacableShape()
    {
        var gridsize = GameManager.Instance.grid.gridSize;
        var grid = GameManager.Instance.grid.grid;
        
        for (int i = 0; i <gridsize.x ; i++)
        {
            for (int j = 0; j <gridsize.y; j++)
            {
                if (CheckPiecePlacement(piece.data, grid[i, j]))
                {
                    Debug.Log("true");
                    piece.ChangeOpticy(1);
                    return true;
                }

            }
            
        }
        piece.ChangeOpticy(0.5f);
        Debug.Log("false");
        return false;
    }
    
    
    public bool CheckPiecePlacement(PieceData p,GridBoard g)
    {
        if (g != null)
        {
            if (g.Y % 2 == 1)
            {
                p = p.fard;
            }
        }
        else
        {
            return false;
        }
        Vector2[] x = p.data;
        for (int i = 0; i < x.Length; i++)
        {
            //Debug.Log(g.X +" "+ (int)x[i].x + " " + g.Y +" "+ (int)x[i].y);
            if (g.X + (int) x[i].x < 0 || g.Y + (int) x[i].y < 0)
            {
                return false;
            }

            GridBoard tmp;
            try
            {
                 tmp= GameManager.Instance.grid.grid[
                    g.X + (int) x[i].x, g.Y + (int) x[i].y
                ];

            }
            catch
            {
//                Debug.Log("shode");
                return false;
            }

        if (tmp == null)
            {
                return false;
            }
            if (tmp.IsFull || !tmp.Placable)
            {
                return false;
            }
        }
        return true;
    }
    public void Rotate()
    {
        
        if (!(piece.data == GameManager.Instance.database.datas[1] ||
              piece.data == GameManager.Instance.database.datas[3]))
        {
            piece.data.Temp = RotatePieceClockwise(piece.data.Temp);
            piece.data.fard.Temp = RotatePieceClockwise(piece.data.fard.Temp);
        }
    }

    public void RotationSetup()
    {
        var a = GameManager.Instance.database.datas;
        for (int i = 0; i < a.Length; i++)
        {
            a[i].Temp.data = a[i].data;
            a[i].Temp.fard.data = a[i].fard.data;
        }
    }
    public PieceData RotatePieceClockwise(PieceData p)
    {
        //piece jadid :
        Vector2[] results = new Vector2[p.data.Length];
        Dictionary<Vector2,Vector2> map = new Dictionary<Vector2, Vector2>();
        int cnt = 0;
        Vector2 current;
        Queue<Vector2> queue = new Queue<Vector2>();
        queue.Enqueue(p.data[0]);
        results[cnt] = p.data[0];
        cnt++;
        map.Add(p.data[0], p.data[0]);
        while(queue.Count>0)
        {
            current=queue.Dequeue();
            //Debug.LogWarning("current is :" + current);
            foreach(Vector2 vec in p.data)
            {
                if (vec.Equals(current))
                {
                    continue;
                }
                if(vec.Equals(FindTheTopRight(current)))
                {
                    if (!map.ContainsKey(vec))
                    {
                        queue.Enqueue(vec);
                        results[cnt] = FindTheLeftTop(map[current]);
                        cnt++;
                        map.Add(vec, results[cnt - 1]);
                        //Debug.Log(results[cnt - 1] + "  " + (cnt - 1) + "  " + vec);
                    }
                }
                if (vec.Equals(FindTheLeftTop(current)))
                {
                    if (!map.ContainsKey(vec))
                    {
                        queue.Enqueue(vec);
                        results[cnt] = FindTheLeft(map[current]);
                        cnt++;
                        map.Add(vec, results[cnt - 1]);
                       // Debug.Log(results[cnt - 1] + "  " + (cnt - 1) + "  " + vec);
                    }
                }
                else
                {
                    //Debug.Log("ummmm " + (current + FindTheLeftTop(current)) + " " + FindTheLeftTop(current));
                }
                if (vec.Equals(FindTheLeft(current)))
                {
                    if (!map.ContainsKey(vec))
                    {
                        queue.Enqueue(vec);
                        results[cnt] = FindTheLeftDown(map[current]);
                        cnt++;
                        map.Add(vec, results[cnt - 1]);
                        //Debug.Log(results[cnt - 1] + "  " + (cnt - 1) + "  " + vec);
                    }
                }
                if (vec.Equals(FindTheLeftDown(current)))
                {
                    if (!map.ContainsKey(vec))
                    {
                        queue.Enqueue(vec);
                        results[cnt] = FindTheRightDown(map[current]);
                        cnt++;
                        map.Add(vec, results[cnt - 1]);
                        //Debug.Log(results[cnt - 1] + "  " + (cnt - 1) + "  " + vec);
                    }
                }
                if (vec.Equals(FindTheRightDown(current)))
                {
                    if (!map.ContainsKey(vec))
                    {
                        queue.Enqueue(vec);
                        results[cnt] = FindTheRight(map[current]);
                        cnt++;
                        map.Add(vec, results[cnt - 1]);
                        //Debug.Log(results[cnt - 1] + "  " + (cnt - 1) + "  " + vec);
                    }
                }
                if (vec.Equals(FindTheRight(current)))
                {
                    if (!map.ContainsKey(vec))
                    {
                        queue.Enqueue(vec);
                        results[cnt] = FindTheTopRight(map[current]);
                        cnt++;
                        map.Add(vec, results[cnt - 1]);
                        //Debug.Log(results[cnt - 1] + "  " + (cnt - 1) + "  " + vec);
                    }
                }
                //Debug.Log("all that while vec is " + vec);
            }
        }
        p.data = results;
        return p;
    }
    public static Vector2 FindTheTopRight(Vector2 x)
    {
        if (x.y % 2 == 0)
        {
            return new Vector2(x.x, x.y + 1);
        }
        else
        {
            return new Vector2(x.x+1, x.y + 1);
        }
    }
    public static Vector2 FindTheRight(Vector2 x)
    {
        return new Vector2(x.x+1, x.y);
    }
    public static Vector2 FindTheRightDown(Vector2 x)
    {
        if (x.y % 2 == 0)
        {
            return new Vector2(x.x, x.y - 1);
        }
        else
        {
            return new Vector2(x.x+1, x.y - 1);
        }
    }
    public static Vector2 FindTheLeftTop(Vector2 x)
    {
        if (x.y % 2 == 0)
        {
            return new Vector2(x.x-1, x.y + 1);
        }
        else
        {
            return new Vector2(x.x, x.y + 1);
        }
    }
    public static Vector2 FindTheLeft(Vector2 x)
    {
        return new Vector2(x.x-1, x.y);
    }
    public static Vector2 FindTheLeftDown(Vector2 x)
    {
        if (x.y % 2 == 0)
        {
            return new Vector2(x.x -1, x.y - 1);
        }
        else
        {
            return new Vector2(x.x, x.y - 1);
        }
    }
}

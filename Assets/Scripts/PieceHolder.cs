using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class PieceHolder : MonoBehaviour
{
    public SpriteRenderer[] shapes;
    private Transform shape;
    private Tilemap tm;
    public PieceData data;

    public void Setup(PieceData data,Tilemap tilemap,GameObject piecePrefab,Sprite sprite)
    {
        this.data = data;
        this.data.fard.Temp = data.fard.data;
        this.data.Temp = this.data.data;
        tm = tilemap;
        shapes = new SpriteRenderer[data.data.Length];
        for (int i = 0; i < data.data.Length; i++)
        {
            var ti = tm.GetCellCenterWorld(new Vector3Int((int)this.data.data[i].x,(int)this.data.data[i].y));
            shape= Instantiate(piecePrefab,transform).transform;
            shape.name = string.Format("{0}:{1}", data.data[i].x, data.data[i].y);
            shape.transform.position = ti;
            shapes[i] = shape.GetChild(0).GetComponent<SpriteRenderer>();
            shapes[i].sprite = sprite;
        }

    }

    public void ChangeOpticy(float amount)
    {
        foreach (var VARIABLE in shapes)
        {
            var c = VARIABLE.color;
            c.a = amount;
            VARIABLE.color = c;
        }
    }

}

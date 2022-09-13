using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class PieceHolder : MonoBehaviour
{

    [SerializeField] private GameObject piecePrefab;

    private SpriteRenderer[] shapes;
    private Transform shape;
    private Tilemap tm;
    public PieceData data;
    // Start is called before the first frame update
    public void Setup(PieceData data,Tilemap tilemap)
    {
        this.data = data;
        tm = tilemap;
        shapes = new SpriteRenderer[data.data.Length];
        for (int i = 0; i < data.data.Length; i++)
        {
            var ti = tm.GetCellCenterWorld(new Vector3Int((int)this.data.data[i].x,(int)this.data.data[i].y));
            shape= Instantiate(piecePrefab,transform).transform;
            shape.name = string.Format("{0}:{1}", data.data[i].x, data.data[i].y);
            shape.transform.position = ti;
            shapes[i] = shape.GetChild(0).GetComponent<SpriteRenderer>();
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
}

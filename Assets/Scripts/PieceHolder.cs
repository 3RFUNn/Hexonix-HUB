using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class PieceHolder : MonoBehaviour
{
    [SerializeField] private PieceData data;

    [SerializeField] private Tilemap tm;
    [SerializeField] private GameObject piecePrefab;
    [SerializeField] private GameObject parent;
    private Transform shape;
    // Start is called before the first frame update
    public void Setup(PieceData data)
    {
        parent.transform.position = Vector2.zero;
        for (int i = 0; i < data.data.Length; i++)
        {
            var t = tm.WorldToCell(data.data[i]);
            var ti = tm.GetCellCenterWorld(t);
            shape= Instantiate(piecePrefab,parent.transform).transform;
            shape.name = string.Format("{0}:{1}", data.data[i].x, data.data[i].y);
            shape.transform.position = ti;

        }

    }

    // Update is called once per frame
    void Update()
    {
    }
}

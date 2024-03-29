using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private PieceHolder Piece;
    [field:SerializeField] public PieceDatabase database { get; private set; }
    [SerializeField] public Coordinate grid;
    [SerializeField] public GameObject cellPrefab;
    [SerializeField] public GridBoard GridBoard;
    public Tilemap tileMap;

    private void Awake()
    {
        Instance = this;
    }

    public PieceHolder GetPiece()
    {
        var g = Instantiate(Piece);
        g.transform.position=Vector3.zero;
        var data = database.GetData();
        g.Setup(data, grid.Tilemap,cellPrefab,database.GetSprite());
        return g;
    }

    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
}

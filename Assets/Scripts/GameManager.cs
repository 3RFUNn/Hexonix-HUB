using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private PieceHolder Piece;
    [SerializeField] private PieceDatabase database;
    [SerializeField] public Coordinate grid;
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
        g.Setup(data, grid.Tilemap);

        return g;
    }

    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private PieceHolder Piece;
    [SerializeField] private PieceData Data;
    [SerializeField] private PieceDatabase database;
    [SerializeField] private Coordinate grid;

    private void Awake()
    {
        Instance = this;
    }

    public PieceHolder GetPiece()
    {
        var g = Instantiate(Piece);
        var data = database.GetData();
        g.Setup(data,grid.Tilemap);

        return g;
    }
   
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private PieceHolder ph;
    [SerializeField] private PieceData Data;
    
    

    private void Awake()
    {
        Instance = this;
        ph.Setup(Data);
    }

    // public PieceHolder GetPiece()
    // {
    //     
    // }
   
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
}

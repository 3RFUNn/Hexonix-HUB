using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenPlaceHolder : MonoBehaviour
{
    private PieceHolder piece;
    void Start()
    {
        PlaceHolder.OnEmpty += OnPlaceHoldeEmpty;
        MakePiece();

    }

    void MakePiece()
    {
        piece = GameManager.Instance.GetPiece();
        piece.gameObject.SetActive(false);
    }

    void OnPlaceHoldeEmpty(PlaceHolder holder)
    {
        piece.gameObject.SetActive(true);
        holder.SetPiece(piece);
        MakePiece();
    }
    void Update()
    {
        
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenPlaceHolder : MonoBehaviour
{
    private PieceHolder piece;
    void Start()
    {
        PlaceHolder.OnReleaseEmpty += OnPlaceHoldEmpty;
        PlaceHolder.OnClicked += OnPlaceHolderClick;
        PlaceHolder.OnReleaseFull += OnPlaceHolderReleased;
        MakePiece();

    }

    void MakePiece()
    {
        piece = GameManager.Instance.GetPiece();
        piece.gameObject.SetActive(false);
    }

    void OnPlaceHoldEmpty(PlaceHolder holder)
    {
        piece.ChangeOpticy(1f);
        piece.gameObject.SetActive(true);
        holder.SetPiece(piece);
        MakePiece();
        
    }
    
    void OnPlaceHolderClick(PlaceHolder holder)
    {
        piece.gameObject.SetActive(true);
        piece.ChangeOpticy(0.5f);
        piece.transform.position = holder.transform.position;
    }
    void OnPlaceHolderReleased(PlaceHolder holder)
    {
        piece.gameObject.SetActive(false);
    }

    // void ShowHiddenPiece(PlaceHolder placeHolder)
    // {
    //     placeHolder.SetPiece(piece);
    //     piece.gameObject.SetActive(true);
    // }
    //
    // void HideHiddenPiece(PlaceHolder hidePlaceHolder)
    // {
    //     piece.gameObject.SetActive(false);
    // }
    
    
}

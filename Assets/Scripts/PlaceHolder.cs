using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHolder : MonoBehaviour
{
    private Vector3 _dragOffset;
    private Camera _cam;

    [SerializeField] private float _speed = 100;
    private PieceHolder piece;
    private bool isDragging;
    void Awake() {
        _cam = Camera.main;
    }

    private void Start()
    {
        piece = GameManager.Instance.GetPiece();
        piece.transform.SetParent(transform);
        piece.transform.localPosition=Vector3.zero;
    }

    private void Update()
    {
        if (!isDragging)
            return;
        piece.transform.position =
            Vector3.MoveTowards(transform.position, GetMousePos() + _dragOffset, _speed * Time.deltaTime);
    }

    void OnMouseDown()
    {
        isDragging = true;
        _dragOffset = transform.position - GetMousePos();
    }

    private void OnMouseUp()
    {
        if (isDragging)
            isDragging = false;
    }

    Vector3 GetMousePos() {
        var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}

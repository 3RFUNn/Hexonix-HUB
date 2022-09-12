using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHolder : MonoBehaviour
{
    private Vector3 _dragOffset;
    private Camera _cam;

    [SerializeField] private float _speed = 100;

    void Awake() {
        _cam = Camera.main;
    }

    void OnMouseDown() {
        _dragOffset = transform.position - GetMousePos();
    }

    void OnMouseDrag() {
        transform.position = 
            Vector3.MoveTowards(transform.position, GetMousePos() + _dragOffset, _speed * Time.deltaTime) ;
    }

    Vector3 GetMousePos() {
        var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}

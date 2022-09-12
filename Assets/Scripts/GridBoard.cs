using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class GridBoard : MonoBehaviour
{
    [field:SerializeField] public bool Placable { get;private set; }
    [field:SerializeField] public int X { get;private set; }
    [field:SerializeField] public int Y { get;private set; }
    [field:SerializeField] public Transform Transform { get;private set; }
    [field: SerializeField] public bool IsFull=> Child!=null;
    public Transform Child { get
        {
            return _child;
        }
        set
        {
            _child = value;
            if (value == null)
                return;
            value.SetParent(Transform);
            value.localPosition = Vector3.zero;
        }
    }
    Transform _child;
    public void Setup(bool isPlaceable,int x,int y,Transform transform)
    {
        Placable = isPlaceable;
        X = x;
        Y = y;
        Transform = transform;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

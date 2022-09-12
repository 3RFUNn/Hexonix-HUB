using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBoard : MonoBehaviour
{
    [field:SerializeField] public bool Placable { get;private set; }
    [field:SerializeField] public int X { get;private set; }
    [field:SerializeField] public int Y { get;private set; }
    [field: SerializeField] public bool IsFull { get; private set; }
    public void Setup(bool isPlaceable,int x,int y)
    {
        Placable = isPlaceable;
        X = x;
        Y = y;
        
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

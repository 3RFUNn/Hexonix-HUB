
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Piece Database")]
public class PieceDatabase : ScriptableObject
{
    public PieceData[] datas;
    [SerializeField] private float totalweight;

    void Setup()
    {
        
        for (int i = 0; i < datas.Length; i++)
        {
            totalweight += datas[i].weight;
        }

        datas = datas.OrderBy(a => a.weight).ToArray();
    }

    public PieceData GetData()
    {
        var a = Random.Range(0, 12);
        return datas[a];


    }
}
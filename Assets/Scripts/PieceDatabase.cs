
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Piece Database")]
public class PieceDatabase : ScriptableObject
{
    public PieceData[] datas;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float totalweight;

    private int lastSpriteIndex = 0;
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

    public Sprite GetSprite()
    {
        int index;
        do
        {
            index = Random.Range(0, sprites.Length);
        } while (index==lastSpriteIndex);

        lastSpriteIndex = index;

        return sprites[index];
    }
}
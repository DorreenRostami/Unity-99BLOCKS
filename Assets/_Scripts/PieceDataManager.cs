using UnityEngine;

[CreateAssetMenu(menuName ="Piece Data Manager")]
public class PieceDataManager:ScriptableObject
{
    [SerializeField] PieceData[] Datas;

    public PieceData GetRandomData()
    {
        return Datas[Random.Range(0, Datas.Length)];
    }
}
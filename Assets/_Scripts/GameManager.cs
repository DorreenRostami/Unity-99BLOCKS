using Assets._Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PieceDataManager DataManager;
    public GridController gridController;
    public GameObject[] cellsPrefab;
    public Vector2 shapeCellSize;

    public GameObject playButton;
    public GameObject pauseButton;

    private void Awake()
    {
        Instance = this;
        //size = gridController.cellSize;
    }
    
    public PieceController GenerateShape()
    {
        var cells = new List<Transform>();

        var piece = new GameObject("Piece",typeof(PieceController)).GetComponent<PieceController>();

        //piece.transform.SetParent(transform);
        //piece.transform.localPosition = Vector3.zero;

        var data = DataManager.GetRandomData();
        GameObject cell = cellsPrefab[UnityEngine.Random.Range(0, cellsPrefab.Length)];
        foreach (Vector2 cord in data.Coordinations)
        {
            GameObject cellObj = Instantiate(cell);
            Transform shape = cellObj.transform.GetChild(0);
            shape.transform.localScale = new Vector2(shapeCellSize.x, shapeCellSize.y);
            cellObj.transform.SetParent(piece.transform);
            cells.Add(cellObj.transform);
        }
        piece.Setup(data, cells.ToArray());
        return piece;
    }
    public static Vector3 PosGen(Rotation rot, Vector3 vec)
    {
        switch (rot)
        {
            case Rotation.p90:
                return new Vector3(vec.y, -vec.x, vec.z);
            case Rotation.p180:
                return new Vector3(-vec.x, -vec.y, vec.z);
            case Rotation.p270:
                return new Vector3(-vec.y, vec.x, vec.z);
            case Rotation.zero:
            default:
                return vec;
        }
    }

    public void PlayClicked()
    {
        playButton.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void PauseClicked()
    {
        playButton.SetActive(true);
        pauseButton.SetActive(false);
    }
}


public enum Rotation
{
    zero, p90, p180, p270
}

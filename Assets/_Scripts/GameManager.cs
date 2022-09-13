using Assets._Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PieceDataManager DataManager;
    public GridController gridController;
    public GameObject[] cellsPrefab;

    public GameObject playButton;
    public GameObject pauseButton;
    public GameObject score;
    public TextMeshProUGUI scoreTxt;
    public int scoreInt;

    public GameSetting gameSetting;
    public float timeInSeconds = 300.0f;

    private void Awake()
    {
        Instance = this;
        scoreTxt = score.GetComponent<TextMeshProUGUI>();
        scoreTxt.text = "0";
        scoreInt = 0;
    }
    
    public PieceController GenerateShape()
    {
        var cells = new List<Transform>();

        var piece = new GameObject("Piece",typeof(PieceController)).GetComponent<PieceController>();

        var data = DataManager.GetRandomData();
        GameObject cell = cellsPrefab[UnityEngine.Random.Range(0, cellsPrefab.Length)];
        foreach (Vector2 cord in data.Coordinations)
        {
            GameObject cellObj = Instantiate(cell);
            Transform shape = cellObj.transform.GetChild(0);
            shape.transform.localScale = new Vector2(gameSetting.startedScale.x, gameSetting.startedScale.y);
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

    void Update()
    {
        timeInSeconds -= Time.deltaTime;
        
        if (timeInSeconds <= 0.0f)
        {
            TimerEnded();
        }
    }

    private void TimerEnded()
    {
        Finish();
    }

    private void Finish()
    {

    }
}


public enum Rotation
{
    zero, p90, p180, p270
}

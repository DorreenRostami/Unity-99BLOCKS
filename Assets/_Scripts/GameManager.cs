using Assets._Scripts;
using RTLTMPro;
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

    public GameSetting gameSettings;

    public GameObject playButton;
    public GameObject pauseButton;

    //public RTLTextMeshPro scoreTxt;
    public GameObject score;
    private TextMeshProUGUI scoreText;
    public int scoreInt;
    
    //public RTLTextMeshPro timerTxt;
    public GameObject timer;
    private TextMeshProUGUI timerText;
    private float timeInSeconds;

    private void Awake()
    {
        Instance = this;
        scoreInt = 0;
        timeInSeconds = gameSettings.timeInSeconds;
        timerText = timer.GetComponent<TextMeshProUGUI>();
        scoreText = score.GetComponent<TextMeshProUGUI>();
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
            shape.transform.localScale = new Vector2(gameSettings.startedScale.x, gameSettings.startedScale.y);
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
        scoreText.text = scoreInt.ToString();

        timeInSeconds -= Time.deltaTime;
        string tmp = string.Format("{0}:{1}", (int)timeInSeconds/60, (int)timeInSeconds%60);
        //timerTxt.SetText(tmp);
        timerText.text = tmp;
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

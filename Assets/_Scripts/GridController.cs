using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] int blockWidth, blockHeight;
    private GridBlock[,] grid;
    [SerializeField] Vector2 borderSize;
    [SerializeField] Vector2 blockSize;
    public GameObject border;
    public GameObject block;


    public GameObject cell;
    [SerializeField] int cellWidth, cellHeight;
    [SerializeField] Vector2 cellSize;
    //SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        InitializeGrid();
        //sr.size
    }

    private void InitializeGrid()
    {
        GameObject borderObj = Instantiate(border);
        borderObj.GetComponent<SpriteRenderer>().size = new Vector2(borderSize.x, borderSize.y);
        borderObj.transform.position = new Vector2(0, 6);
        //borderObj.GetComponent<SpriteRenderer>().size = new Vector2(Screen.width / 2, Screen.height / 2);
        
        grid = new GridBlock[blockWidth, blockHeight];
        for (int x = 0; x < blockWidth; x++)
        {
            for (int y = 0; y < blockHeight; y++)
            {
                GameObject blockObj = Instantiate(block);
                blockObj.GetComponent<SpriteRenderer>().size = new Vector2(blockSize.x, blockSize.y);
                blockObj.transform.SetParent(borderObj.transform);
                blockObj.transform.localPosition = new Vector2(blockSize.x * (x-1), blockSize.y * (y-1));
                grid[x, y] = new GridBlock(x-1, y-1, blockObj.transform);
                grid[x, y].InitializeBlock(cellWidth, cellHeight, cellSize, cell);
            }
        }

    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class GridBlock 
{
    public GridCell[,] cellGrid { get; set; }

    public int X { get; private set; }
    public int Y { get; private set; }
    public Transform Transform { get; private set; }

    public GridBlock(int x, int y,Transform transform)
    {
        X = x;
        Y = y;
        Transform = transform;
    }

    public void InitializeBlock(int cellWidth, int cellHeight, Vector2 cellSize, GameObject cell)
    {
        cellGrid = new GridCell[cellWidth, cellHeight];
        for (int x = 0; x < cellWidth; x++)
        {
            for (int y = 0; y < cellHeight; y++)
            {
                GameObject cellObj = MonoBehaviour.Instantiate(cell);
                cellObj.GetComponent<SpriteRenderer>().size = new Vector2(cellSize.x, cellSize.y);
                cellObj.transform.SetParent(Transform);
                cellObj.transform.localPosition = new Vector2(cellSize.x * (x-1), cellSize.y * (y-1));
                cellGrid[x, y] = new GridCell(x-1, y-1);
            }
        }
    }
}
public class GridCell
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public GridCell(int x, int y)
    {
        X = x;
        Y = y;
    }
}

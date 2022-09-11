using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] int blockWidth, blockHeight;
    private GridBlock[,] gridBlock;
    private GridCell[,] grid;
    [SerializeField] Vector2 borderSize;
    [SerializeField] Vector2 blockSize;
    public GameObject border;
    public GameObject block;

    private Vector2 minPos;
    private Vector2 maxPos;


    public GameObject cell;
    [SerializeField] int cellWidth, cellHeight;
    public Vector2 cellSize;

    void Start()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        GameObject borderObj = Instantiate(border);
        borderObj.GetComponent<SpriteRenderer>().size = new Vector2(borderSize.x, borderSize.y);
        borderObj.transform.position = new Vector2(0, 6);
        //borderObj.GetComponent<SpriteRenderer>().size = new Vector2(Screen.width / 2, Screen.height / 2);



        
        ///////////
        grid = new GridCell[cellWidth * blockWidth, cellHeight * blockHeight];
        /*for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                GameObject cellObj = MonoBehaviour.Instantiate(cell);
                cellObj.GetComponent<SpriteRenderer>().size = new Vector2(cellSize.x, cellSize.y);
                grid[x, y] = new GridCell(x , y , cellObj.transform);
            }
        }*/

        gridBlock = new GridBlock[blockWidth, blockHeight];
        for (int x = 0; x < blockWidth; x++)
        {
            for (int y = 0; y < blockHeight; y++)
            {
                GameObject blockObj = Instantiate(block);
                blockObj.GetComponent<SpriteRenderer>().size = new Vector2(blockSize.x, blockSize.y);
                blockObj.transform.SetParent(borderObj.transform);
                blockObj.transform.localPosition = new Vector2(blockSize.x * (x - 1), blockSize.y * (y - 1));
                gridBlock[x, y] = new GridBlock(x, y, blockObj.transform);
                //gridBlock[x, y].InitializeBlock(cellWidth, cellHeight, cellSize, cell);

                GridCell[,] gridList = new GridCell[cellWidth, cellHeight];

                for (int q = 0; q < cellWidth  ; q++)
                {
                    for (int z = 0; z < cellHeight; z++)
                    {
                        GameObject cellObj = MonoBehaviour.Instantiate(cell);
                        cellObj.GetComponent<SpriteRenderer>().size = new Vector2(cellSize.x, cellSize.y);
                        int xi = q + (x * cellWidth);
                        int yi = z + (y * cellHeight);
                        grid[xi, yi] = new GridCell(xi, yi, cellObj.transform);
                        gridList[q,z] = grid[xi, yi];
                    }
                }

                gridBlock[x, y].InitializeBlock(cellWidth, cellHeight, cellSize, gridList);
            }
        }
        minPos = (Vector2) grid[0,0].Transform.position - cellSize / 2;
        maxPos = (Vector2) grid[blockWidth * cellWidth - 1, blockHeight * cellHeight - 1].Transform.position + cellSize/2;


    }

    public bool IsValid(Vector3 pos)
    {
        return pos.x >= minPos.x &&
                pos.x <= maxPos.x &&
                pos.y >= minPos.y &&
                pos.y <= maxPos.y;
    }

    public Vector2 GetCellPositionFromWorldPosition(Vector2 pos)
    {
        var t = pos;
        t.x = MathF.Floor(pos.x);
        t.y = Mathf.Floor(pos.y);
        if (t.x > 1)
            t.x += 0.5f;
        else if (t.x < -1)
            t.x -= 0.5f;
        if (t.y > 7)
            t.y += 0.5f;
        else if (t.y < 5)
            t.y -= 0.5f;

        return t;
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

    public void InitializeBlock(int cellWidth, int cellHeight, Vector2 cellSize, GridCell[,] cell)
    {
        //cellGrid = new GridCell[cellWidth, cellHeight];
        for (int x = 0; x < cellWidth; x++)
        {
            for (int y = 0; y < cellHeight; y++)
            {
                //GameObject cellObj = MonoBehaviour.Instantiate(cell);
                cell[x, y].Transform.localScale = cellSize;
                cell[x, y].Transform.SetParent(Transform);
                cell[x, y].Transform.localPosition = new Vector2(cellSize.x * (x-1), cellSize.y * (y-1));
            }
        }
    }
}
public class GridCell
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public Transform Transform { get; private set; }
    public GridCell(int x, int y, Transform transform)
    {
        X = x;
        Y = y;
        Transform = transform;
    }
}

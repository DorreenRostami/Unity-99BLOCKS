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

    public GameSetting gameSetting;

    void Start()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        GameObject borderObj = Instantiate(border);
        borderObj.GetComponent<SpriteRenderer>().size = new Vector2(borderSize.x, borderSize.y);
        borderObj.transform.position = new Vector2(0, 6);

        grid = new GridCell[cellWidth * blockWidth, cellHeight * blockHeight];

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
                gridBlock[x, y].cellGrid = new GridCell[cellWidth, cellHeight];

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
                        gridBlock[x, y].cellGrid[q, z] = grid[xi, yi];
                        gridList[q,z] = grid[xi, yi];
                    }
                }

                gridBlock[x, y].InitializeBlock(cellWidth, cellHeight, cellSize, gridList);
            }
        }
        minPos = (Vector2) grid[0,0].Transform.position - cellSize / 2;
        maxPos = (Vector2) grid[blockWidth * cellWidth - 1, blockHeight * cellHeight - 1].Transform.position + cellSize/2;


    }

    public Vector2Int CellXY(Vector3 pos)
    {
        var t = pos;
        t.x = MathF.Floor(pos.x);
        t.y = MathF.Floor(pos.y) - 6;

        if (t.x <= -3)
            t.x += 5;
        else
            t.x += 4;

        if (t.y <= -3)
            t.y += 5;
        else
            t.y += 4;

        return new Vector2Int((int)t.x, (int)t.y);
    }

    public bool IsInsideGrid(Vector3 pos)
    {
        return pos.x >= minPos.x &&
                pos.x <= maxPos.x &&
                pos.y >= minPos.y &&
                pos.y <= maxPos.y;
    }

    public bool IsValidToFill(Vector3 pos)
    {
        var t = CellXY(pos);


        if (grid[(int)t.x, (int)t.y].Full)
            return false;
        return true;
    }

    public void CheckBlockForScore()
    {
        List<GridBlock> popBlocks = new List<GridBlock>();

        for (int i = 0; i < blockWidth; i++)
        {
            for (int j = 0; j < blockHeight; j++)
            {
                if (gridBlock[i,j].IsFull)
                {
                    //pop animation
                    popBlocks.Add(gridBlock[i,j]);
                    
                    Debug.Log("time to pop");
                }
            }
        }

        //sending blocks to pop 
        GameManager.Instance.gameSettings.popBlocks = popBlocks;

        //counting score
        for (int i = 0; i < popBlocks.Count; i++)
        {
            int score = 0;
            for (int m = 0; m < cellWidth; m++)
            {
                for (int n = 0; n < cellHeight; n++)
                {
                    popBlocks[i].cellGrid[m, n].ChildObject = null;
                    score += popBlocks[i].cellGrid[m, n].Multiply;
                }
            }
            score *= (popBlocks.Count * 20);
            GameManager.Instance.scoreInt += score;
        }

        

    }
    

    public void FillGrid(Vector3 pos, Transform trans, int multiply)
    {
        var t = CellXY(pos);


        grid[t.x, t.y].ChildObject = trans;
        grid[t.x, t.y].Multiply = multiply;

    }

    public Vector2 GetCellPositionFromWorldPosition(Vector2 pos)
    {
        var t = pos + new Vector2(0.5f, 0.5f);
        t.x = MathF.Floor(t.x);
        t.y = MathF.Floor(t.y);

        /*if (pos.x >= 1.5)
            t.x += GameManager.Instance.gameSettings.borderDistance;
        else if (pos.x <= -1.5)
            t.x -= GameManager.Instance.gameSettings.borderDistance;
        if (pos.y >= 7.5)
            t.y += GameManager.Instance.gameSettings.borderDistance;
        else if (pos.y <= 4.5)
            t.y -= GameManager.Instance.gameSettings.borderDistance;*/

        if (t.x >= 2)
            t.x += GameManager.Instance.gameSettings.borderDistance;
        else if (t.x <= -2)
            t.x -= GameManager.Instance.gameSettings.borderDistance;
        if (t.y >= 8)
            t.y += GameManager.Instance.gameSettings.borderDistance;
        else if (t.y <= 4)
            t.y -= GameManager.Instance.gameSettings.borderDistance;

        return t;
    }

}

public class GridBlock 
{
    public GridCell[,] cellGrid { get; set; }

    public int X { get; private set; }
    public int Y { get; private set; }
    public Transform Transform { get; private set; }
    public bool IsFull { get
        {

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!cellGrid[i, j].Full)
                        return false;
                }
                
            }
            return true;
        } 
    }

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

        cellGrid = cell;
    }
}
public class GridCell
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Multiply { get; set; }
    public bool Full => ChildObject != null;
    public Transform Transform;

    public Transform ChildObject { get
        {
            return _childObject;
        }
        set
        {
            _childObject = value;
            if (value == null)
                return;
            value.transform.SetParent(Transform);
            value.transform.localPosition = Vector3.zero;
        }
    }

    Transform _childObject;
    public GridCell(int x, int y, Transform transform)
    {
        X = x;
        Y = y;
        Transform = transform;
    }
    
    
}

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
        t.y = Mathf.Floor(pos.y) - 6;

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

    public bool IsValid(Vector3 pos)
    {
        return pos.x >= minPos.x &&
                pos.x <= maxPos.x &&
                pos.y >= minPos.y &&
                pos.y <= maxPos.y;
    }

    public bool IsValidToFill(Vector3 pos)
    {
        var t = CellXY(pos);

        Debug.Log("x " + t.x + " y " + t.y);
        Debug.Log("-----------------------");

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

        //check these blocks are more than 4
        if(popBlocks.Count > 0)
        {
            GameManager.Instance.scoreInt += (popBlocks.Count ^ 2) * 9;
            GameManager.Instance.scoreTxt.text = GameManager.Instance.scoreInt.ToString();
        }

        for (int i = 0; i < popBlocks.Count; i++)
        {
            for (int m = 0; m < cellWidth; m++)
            {
                for (int n = 0; n < cellHeight; n++)
                {
                    GameObject cellObj = MonoBehaviour.Instantiate(cell);
                    popBlocks[i].cellGrid[m, n].ChildObject = null;
                }
            }
        }

        

    }
    

    public void FillGrid(Vector3 pos, Transform trans)
    {
        var t = CellXY(pos);

        grid[t.x, t.y].ChildObject = trans;

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
    }
}
public class GridCell
{
    public int X { get; private set; }
    public int Y { get; private set; }
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

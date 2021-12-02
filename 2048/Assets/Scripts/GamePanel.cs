using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    public enum MoveType
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3,
    } 


    public Transform gridParent;

    public GameObject gridPrefab;

    public EmptyGrid[,] grids = null;

    public List<EmptyGrid> emptyGrids = new List<EmptyGrid>();

    public GameObject numberGridPrefab;

    private Vector3 pointerDownPosition, pointerUpPosition;

    void Start()
    {
        InitGrids();
        CreateNumberGrid();
        CreateNumberGrid();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(MoveType.Up);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(MoveType.Down);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(MoveType.Left);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(MoveType.Right);
        }
    }

    // 初始化数字格子
    public void InitGrids()
    {
        // 只允许4X4
        int gridNum = 4;
        grids = new EmptyGrid[gridNum, gridNum];


        GridLayoutGroup g = gridParent.GetComponent<GridLayoutGroup>();
        g.constraintCount = gridNum;// 数量赋值，尺寸固定

        for (int i = 0; i < gridNum; i++)
        {
            for (int j = 0; j < gridNum; j++)
            {
                grids[i, j] = CreateGrid();
            }
        }

    }

    // 创建格子
    public EmptyGrid CreateGrid()
    {
        GameObject gameObject = Instantiate(gridPrefab, gridParent);

        return gameObject.GetComponent<EmptyGrid>();
    }

    // 创建数字
    public void CreateNumberGrid()
    {
        // random锁定一个格子
        emptyGrids.Clear();
        foreach (var item in grids)
        {
            if (!item.IsOccupied())
                emptyGrids.Add(item);
        }

        if (emptyGrids.Count == 0)
        {
            return;
        }

        int index = Random.Range(0, emptyGrids.Count);
        // 创建数字格子
        GameObject obj = GameObject.Instantiate(numberGridPrefab, emptyGrids[index].transform);
        obj.GetComponent<NumberGrid>().Init(emptyGrids[index]);



    }

    public void Move(MoveType moveType)
    {
        switch (moveType)
        {
            case MoveType.Left:
                break;
            case MoveType.Right:
                break;
            case MoveType.Up:
                break;
            case MoveType.Down:
                break;

        }
    }
}

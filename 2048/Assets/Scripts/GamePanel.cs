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
    // 只允许4X4
    private static int GRIDNUM = 4;

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
        // 游戏结束的操作
        if (!IsPlayable())
        {
            
        };

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

    private bool IsPlayable()
    {

        foreach(var item in grids)
        {
            if(item.GetNumberGrid() == null)
            {
                return true;
            }
        }

        for(int i = 0;i < GRIDNUM-1; i++)
        {
            for(int j = 0;j < GRIDNUM-1; j++)
            {
                if(grids[i,j].GetNumberGrid().GetNum() == grids[i, j + 1].GetNumberGrid().GetNum())
                {
                    return true;
                }
                if(grids[i, j].GetNumberGrid().GetNum() == grids[i+1, j].GetNumberGrid().GetNum())
                {
                    return true;
                }
            }
        }

        return false;
    }

    // 初始化数字格子
    public void InitGrids()
    {

        grids = new EmptyGrid[GRIDNUM, GRIDNUM];


        GridLayoutGroup g = gridParent.GetComponent<GridLayoutGroup>();
        g.constraintCount = GRIDNUM;// 数量赋值，尺寸固定

        for (int i = 0; i < GRIDNUM; i++)
        {
            for (int j = 0; j < GRIDNUM; j++)
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
                for (int i = 0; i < GRIDNUM; i++)
                {
                    //头结点无需比对
                    for (int j = 1; j < GRIDNUM; j++)
                    {
                        // 有数字
                        if (grids[i, j].IsOccupied())
                        {

                            NumberGrid ng = grids[i, j].GetNumberGrid();

                            // 向左侧比对
                            for (int k = 0; k < j; k++)
                            {
                                // 合并
                                if (grids[i,k].IsOccupied())
                                {
                                    // 中间还存在有数字的格子，不能合并
                                    bool flag = false;
                                    for(int l = k + 1; l < j-1; l++)
                                    {
                                        if (grids[i,l].IsOccupied())
                                        {
                                            flag = true;
                                            break;
                                        }
                                    }
                                    if(flag == true)
                                    {
                                        continue;
                                    }

                                    // 已合并过的不能合并
                                    if(grids[i, k].GetNumberGrid().GetMerged() == true)
                                    {
                                        continue;
                                    }

                                    // 数字相同才需要有动作
                                    if(grids[i, k].GetNumberGrid().GetNum() == ng.GetNum())
                                    {
                                        grids[i, k].GetNumberGrid().MergeGrid();
                                        ng.GetGrid().SetNumberGrid(null);
                                        GameObject.Destroy(ng.gameObject);
                                        break;
                                    }
                                }
                                // 数字不同，移动
                                else
                                {
                                    ng.MoveToGrid(grids[i, k]);
                                    break;
                                }

                            }

                        }


                    }
                }
                break;
            case MoveType.Right:
                for (int i = 0; i < GRIDNUM; i++)
                {
                    //向左遍历
                    for (int j = GRIDNUM - 2; j >= 0; j--)
                    {
                        // 有数字
                        if (grids[i, j].IsOccupied())
                        {

                            NumberGrid ng = grids[i, j].GetNumberGrid();

                            // 向右侧比对
                            for (int k = GRIDNUM - 1; k > j; k--)
                            {
                                // 合并
                                if (grids[i, k].IsOccupied())
                                {
                                    // 中间还存在有数字的格子，不能合并
                                    bool flag = false;
                                    for (int l = k - 1; l > j + 1; l--)
                                    {
                                        if (grids[i, l].IsOccupied())
                                        {
                                            flag = true;
                                            break;
                                        }
                                    }
                                    if (flag == true)
                                    {
                                        continue;
                                    }

                                    // 已合并过的不能合并
                                    if (grids[i, k].GetNumberGrid().GetMerged() == true)
                                    {
                                        continue;
                                    }

                                    //数字相同才需要有动作
                                    if (grids[i, k].GetNumberGrid().GetNum() == ng.GetNum())
                                    {
                                        grids[i, k].GetNumberGrid().MergeGrid();
                                        ng.GetGrid().SetNumberGrid(null);
                                        GameObject.Destroy(ng.gameObject);
                                        break;
                                    }
                                }
                                // 数字不同，移动
                                else
                                {
                                    ng.MoveToGrid(grids[i, k]);
                                    break;
                                }

                            }

                        }


                    }
                }
                break;
            case MoveType.Up:
                for (int i = 0; i < GRIDNUM; i++)
                {
                    //向左遍历
                    for (int j = 1; j < GRIDNUM; j++)
                    {
                        // 有数字
                        if (grids[j, i].IsOccupied())
                        {

                            NumberGrid ng = grids[j, i].GetNumberGrid();

                            // 向右侧比对
                            for (int k = 0; k < j; k++)
                            {
                                // 合并
                                if (grids[k, i].IsOccupied())
                                {
                                    // 中间还存在有数字的格子，不能合并
                                    bool flag = false;
                                    for (int l = k + 1; l < j - 1; l++)
                                    {
                                        if (grids[l, i].IsOccupied())
                                        {
                                            flag = true;
                                            break;
                                        }
                                    }
                                    if (flag == true)
                                    {
                                        continue;
                                    }

                                    // 已合并过的不能合并
                                    if (grids[k, i].GetNumberGrid().GetMerged() == true)
                                    {
                                        continue;
                                    }

                                    //数字相同才需要有动作
                                    if (grids[k, i].GetNumberGrid().GetNum() == ng.GetNum())
                                    {
                                        grids[k, i].GetNumberGrid().MergeGrid();
                                        ng.GetGrid().SetNumberGrid(null);
                                        GameObject.Destroy(ng.gameObject);
                                        break;
                                    }
                                }
                                // 数字不同，移动
                                else
                                {
                                    ng.MoveToGrid(grids[k, i]);
                                    break;
                                }

                            }

                        }


                    }
                }
                break;
            case MoveType.Down:
                for (int i = 0; i < GRIDNUM; i++)
                {
                    //遍历
                    for (int j = GRIDNUM - 2; j >= 0; j--)
                    {
                        // 有数字
                        if (grids[j, i].IsOccupied())
                        {

                            NumberGrid ng = grids[j, i].GetNumberGrid();

                            // 向上比对
                            for (int k = GRIDNUM - 1; k > j; k--)
                            {
                                // 合并
                                if (grids[k, i].IsOccupied())
                                {
                                    // 中间还存在有数字的格子，不能合并
                                    bool flag = false;
                                    for (int l = k - 1; l > j + 1; l--)
                                    {
                                        if (grids[l, i].IsOccupied())
                                        {
                                            flag = true;
                                            break;
                                        }
                                    }
                                    if (flag == true)
                                    {
                                        continue;
                                    }

                                    // 已合并过的不能合并
                                    if (grids[k, i].GetNumberGrid().GetMerged() == true)
                                    {
                                        continue;
                                    }

                                    //数字相同才需要有动作
                                    if (grids[k, i].GetNumberGrid().GetNum() == ng.GetNum())
                                    {
                                        grids[k, i].GetNumberGrid().MergeGrid();
                                        ng.GetGrid().SetNumberGrid(null);
                                        GameObject.Destroy(ng.gameObject);
                                        break;
                                    }
                                }
                                // 数字不同，移动
                                else
                                {
                                    ng.MoveToGrid(grids[k, i]);
                                    break;
                                }

                            }

                        }


                    }
                }
                break;

        }

        CreateNumberGrid();
        CreateNumberGrid();

        foreach(var item in grids)
        {
            if(item.GetNumberGrid() != null)
            {
                item.GetNumberGrid().SetMerged(false);
            }
        }
    }


}

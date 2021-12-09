using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberGrid : MonoBehaviour
{
    private Image bg;
    private Text num;
    private EmptyGrid grid;

    private float scaletTime = 0;

    public void Awake()
    {
        bg = GetComponent<Image>();
        num = transform.Find("Txt_Number").GetComponent<Text>();
    }

    public void Update()
    {
        scaletTime += Time.deltaTime * 4;
        transform.localScale = Vector3.Lerp(Vector3.zero,Vector3.one, scaletTime);
    }

    public void Init(EmptyGrid grid)
    {
        grid.SetNumberGrid(this);
        this.SetGrid(grid);

        this.SetNum(2);

        transform.localScale = Vector3.zero;
    }

    private void SetGrid(EmptyGrid grid)
    {
        this.grid = grid;
    }

    private void SetNum(int num)
    {
        this.num.text = num.ToString();
    }

    public int GetNum()
    {
        return int.Parse(num.text);
    }

    public EmptyGrid GetGrid()
    {
        if (this.grid == null) throw new NullReferenceException();
        return this.grid;
    }

    public void MoveToGrid(EmptyGrid grid)
    {
        transform.SetParent(grid.transform);
        transform.localPosition = Vector3.zero;
        this.GetGrid().SetNumberGrid(null);
        grid.SetNumberGrid(this);
        this.SetGrid(grid);
    }

    public void MergeGrid()
    {
        this.SetNum(this.GetNum() * 2);
    }
}

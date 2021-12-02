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

    public void Awake()
    {
        bg = GetComponent<Image>();
        num = transform.Find("Txt_Number").GetComponent<Text>();
    }

    public void Init(EmptyGrid grid)
    {
        grid.SetNumberGrid(this);
        this.SetGrid(grid);

        this.SetNum(2);
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
}

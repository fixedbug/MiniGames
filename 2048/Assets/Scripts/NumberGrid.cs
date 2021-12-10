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

    private float spawnScaletTime = 0;
    private float mergeScaletTime = 0;
    private float mergeReverseScaletTime = 0;
    private float moveScaletTime = 0;
    private bool merged = false;
    private bool spawning = false;
    private bool merging = false;
    private bool moving = false;
    private Vector3 startPos = Vector3.zero, endPos = Vector3.zero;

    public Color[] colors;

    public void Awake()
    {
        bg = GetComponent<Image>();
        num = transform.Find("Txt_Number").GetComponent<Text>();
    }

    public void Update()
    {
        // 诞生动画
        if (spawning && spawnScaletTime <= 1)
        {
            spawnScaletTime += Time.deltaTime * 4;
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, spawnScaletTime);
        }
        else
        {
            this.spawning = false;
        }

        // 合并动画
        if (merging && mergeScaletTime <= 1 && mergeReverseScaletTime == 0)
        {
            mergeScaletTime += Time.deltaTime * 6;
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 1.3f, mergeScaletTime);
        }
        else if (mergeScaletTime >= 1 && mergeReverseScaletTime <= 1)
        {
            mergeReverseScaletTime += Time.deltaTime * 6;
            transform.localScale = Vector3.Lerp(Vector3.one * 1.3f, Vector3.one, mergeReverseScaletTime);
        }
        else
        {
            mergeReverseScaletTime = 0;
            mergeScaletTime = 0;
            merging = false;
        }

        // 移动动画
        if (moving && moveScaletTime <= 1)
        {
            moveScaletTime += Time.deltaTime * 6;
            transform.position = Vector3.Lerp(startPos, endPos, moveScaletTime);
        }
        else
        {
            moving = false;
            moveScaletTime = 0;
        }




    }

    public void Init(EmptyGrid grid)
    {
        this.spawning = true;
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
        SetColor();
    }

    public int GetNum()
    {
        return int.Parse(num.text);
    }

    public EmptyGrid GetGrid()
    {
        if (this.grid == null)
            throw new NullReferenceException();
        return this.grid;
    }

    public void MoveToGrid(EmptyGrid grid)
    {
        if (moving)
        {
            transform.position = endPos;
            startPos = Vector3.zero;
            endPos = Vector3.zero;
            moving = false;
        }
        transform.SetParent(grid.transform);
        // transform.localPosition = Vector3.zero;
        startPos = this.transform.position;
        endPos = grid.transform.position;
        this.GetGrid().SetNumberGrid(null);
        grid.SetNumberGrid(this);
        this.SetGrid(grid);
        moving = true;
    }

    public void MergeGrid()
    {
        this.SetNum(this.GetNum() * 2);
        this.merged = true;
        this.merging = true;
        this.mergeScaletTime = 0;
        this.mergeReverseScaletTime = 0;
        transform.localScale = Vector3.one;
    }

    public void SetMerged(bool flag)
    {
        this.merged = flag;
    }

    public bool GetMerged()
    {
        return this.merged;
    }

    private void SetColor()
    {
        if(this.GetNum() == 2)
        {
            this.num.color = colors[0];
        }

        if (this.GetNum() == 4)
        {
            this.num.color = colors[1];
        }

        if (this.GetNum() == 8)
        {
            this.num.color = colors[2];
        }

        if (this.GetNum() == 16)
        {
            this.num.color = colors[3];
        }

        if (this.GetNum() == 32)
        {
            this.num.color = colors[4];
        }

        if (this.GetNum() == 64)
        {
            this.num.color = colors[5];
        }

        if (this.GetNum() == 128)
        {
            this.num.color = colors[6];
        }

        if (this.GetNum() == 256)
        {
            this.num.color = colors[7];
        }

        if (this.GetNum() == 512)
        {
            this.num.color = colors[8];
        }

        if (this.GetNum() == 1024)
        {
            this.num.color = colors[9];
        }

        if (this.GetNum() == 2048)
        {
            this.num.color = colors[10];
        }

        if (this.GetNum() == 4096)
        {
            this.num.color = colors[11];
        }

        if (this.GetNum() == 8192)
        {
            this.num.color = colors[12];
        }
    }
}

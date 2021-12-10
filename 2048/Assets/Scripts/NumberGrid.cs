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
    private bool merged = false;
    private bool spawning = false;
    private bool merging = false;


    public void Awake()
    {
        bg = GetComponent<Image>();
        num = transform.Find("Txt_Number").GetComponent<Text>();
    }

    public void Update()
    {
        // 诞生动画
        if (spawning == true && spawnScaletTime <= 1)
        {
            spawnScaletTime += Time.deltaTime * 4;
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, spawnScaletTime);
        }
        else
        {
            this.spawning = false;
        }

        // 合并动画
        if (merging == true && mergeScaletTime <= 1 && mergeReverseScaletTime == 0)
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
        transform.SetParent(grid.transform);
        transform.localPosition = Vector3.zero;
        this.GetGrid().SetNumberGrid(null);
        grid.SetNumberGrid(this);
        this.SetGrid(grid);
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
}

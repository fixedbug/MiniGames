using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyGrid : MonoBehaviour
{
    public NumberGrid numberGrid;

    public bool IsOccupied()
    {
        return numberGrid != null;
    }

    public NumberGrid GetNumberGrid()
    {
        return numberGrid;
    }

    public void SetNumberGrid(NumberGrid numberGrid)
    {
        this.numberGrid = numberGrid;
    }
}

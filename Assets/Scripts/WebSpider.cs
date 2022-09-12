using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class WebSpider : MonoBehaviour
{
    // [SerializeField] private Tilemap start;
    List<GridBoard> finalRes = new List<GridBoard>();
    List<GridBoard> tmp2 = new List<GridBoard>();
    public GridBoard[] FindMatches()
    {
        finalRes=new List<GridBoard> ();
        GridBoard start = GameManager.Instance.coordinate.grid[2, 0];
        List<GridBoard> results = new List<GridBoard>();
        results = GetVerticals(start);
        tmp2=new List<GridBoard>();
        for(int i = 0; i < results.Count; i++)
        {
            FindResultsOfFunc(FindTheTopRight, results[i]);
        }
        for (int i = 0; i < results.Count; i++)
        {
            FindResultsOfFunc(FindTheLeftTop, results[i]);
        }
        results = GetHorizontalsLeft(start, out GridBoard end);
        for (int i = 0; i < results.Count; i++)
        {

            FindResultsOfFunc(FindTheRight, results[i]);
        }
        for (int i = 0; i < results.Count; i++)
        {

            FindResultsOfFunc(FindTheTopRight, results[i]);
        }
        results = GetHorizontalsRight(end);
        for (int i = 0; i < results.Count; i++)
        {
            FindResultsOfFunc(FindTheRight, results[i]);
        }
        for (int i = 0; i < results.Count; i++)
        {
            FindResultsOfFunc(FindTheRightDown, results[i]);
        }
        Dictionary<GridBoard, bool> dic = new Dictionary<GridBoard, bool>();
        List<GridBoard> theRealFinalResults = new List<GridBoard>();
        for (int i = 0; i < finalRes.Count; i++)
        {
            if (!dic.ContainsKey(finalRes[i])){
                dic.Add(finalRes[i],true);
                theRealFinalResults.Add(finalRes[i]);
            }
        }
        return theRealFinalResults.ToArray();
    }
    public void FindResultsOfFunc(Func<GridBoard, GridBoard> myMethodName, GridBoard currentGrid)
    {
        bool found = true;
        while (currentGrid.Placable)
        {
            if (!currentGrid.IsFull)
            {
                found = false;
                break;
            }
            tmp2.Add(currentGrid);
            currentGrid = myMethodName(currentGrid);
        }
        if (found)
        {
            finalRes.AddRange(tmp2);
        }
        tmp2.Clear();
    }
    public List<GridBoard> GetHorizontalsLeft(GridBoard start , out GridBoard end)
    {
        List<GridBoard> results=new List<GridBoard>();
        var currentPos = start;
        var prev = currentPos;
        while(currentPos.Placable)
        {
            if(currentPos.IsFull)
                results.Add(currentPos);
            prev = currentPos;
            currentPos = FindTheLeftTop(currentPos);
        }
        end = prev;
        return results;
    }

    public List<GridBoard> GetVerticals(GridBoard start)
    {
        List<GridBoard> results = new List<GridBoard>();
        var currentPos = start;
        while (currentPos.Placable)
        {
            if (currentPos.IsFull)
                results.Add(currentPos);
            currentPos = FindTheRight(currentPos);
        }
        return results;
    }

    public List<GridBoard> GetHorizontalsRight(GridBoard start)
    {
        List<GridBoard> results = new List<GridBoard>();
        var currentPos = start;
        while (currentPos.Placable)
        {
            if (currentPos.IsFull)
                results.Add(currentPos);
            currentPos=FindTheTopRight(currentPos);
        }
        return results;
    }
    public static GridBoard FindTheTopRight(GridBoard x)
    {
        if (x.Y % 2 == 0)
        {
            return GameManager.Instance.coordinate.grid[x.X, x.Y+1];
        }
        else
        {
            return GameManager.Instance.coordinate.grid[x.X + 1, x.Y + 1];
        }
    }
    public static GridBoard FindTheRight(GridBoard x)
    {
        return GameManager.Instance.coordinate.grid[x.X + 1, x.Y];
    }
    public static GridBoard FindTheRightDown(GridBoard x)
    {
        if (x.Y % 2 == 0)
        {
            return GameManager.Instance.coordinate.grid[x.X, x.Y - 1];
        }
        else
        {
            return GameManager.Instance.coordinate.grid[x.X+1, x.Y - 1];
        }
    }
    public static GridBoard FindTheLeftTop(GridBoard x)
    {
        if (x.Y % 2 == 0)
        {
            return GameManager.Instance.coordinate.grid[x.X - 1, x.Y + 1];
        }
        else
        {
            return GameManager.Instance.coordinate.grid[x.X, x.Y + 1];
        }
    }
    public static GridBoard FindTheLeft(GridBoard x)
    {
        return GameManager.Instance.coordinate.grid[x.X - 1, x.Y];
    }
    public static GridBoard FindTheLeftDown(GridBoard x)
    {
        if (x.Y % 2 == 0)
        {
            return GameManager.Instance.coordinate.grid[x.X - 1, x.Y - 1];
        }
        else
        {
            return GameManager.Instance.coordinate.grid[x.X, x.Y - 1];
        }
    }
}

using UnityEngine;
using System.Collections;

public class Griddy
{
    public static int w = 10;
    public static int h = 13; 
    public static Element[,] elements = new Element[w, h];

    public static void uncoverMines()
    {
        foreach (Element elem in elements)
            if (elem.isMine)
                elem.loadTexture(0);
    }

    public static bool mineAt(int x, int y)
    {
        // Coordinates in range? Then check for mine.
        if (x >= 0 && y >= 0 && x < w && y < h)
            return elements[x, y].isMine;
        return false;
    }

    public static int adjacentMines(int x, int y)
    {
        //from top all the way around to top-left determine if mine
        int count = 0;

        if (mineAt(x, y + 1)) ++count;
        if (mineAt(x + 1, y + 1)) ++count;
        if (mineAt(x + 1, y)) ++count;
        if (mineAt(x + 1, y - 1)) ++count;
        if (mineAt(x, y - 1)) ++count;
        if (mineAt(x - 1, y - 1)) ++count;
        if (mineAt(x - 1, y)) ++count;
        if (mineAt(x - 1, y + 1)) ++count;

        return count;
    }

    public static void FFuncover(int x, int y, bool[,] visited)
    {
        if (x >= 0 && y >= 0 && x < w && y < h)
        {
            if (visited[x, y])
                return;
            elements[x, y].loadTexture(adjacentMines(x, y));
            if (adjacentMines(x, y) > 0)
                return;
            visited[x, y] = true;
            FFuncover(x - 1, y, visited);
            FFuncover(x + 1, y, visited);
            FFuncover(x, y - 1, visited);
            FFuncover(x, y + 1, visited);
        }
    }

    public static bool isFinished()
    {
        foreach (Element elem in elements)
        {
            if (elem.isCovered() && !elem.isMine)
            {
                return false; //continue
            }   
        }
        return true; // win!
    }

}

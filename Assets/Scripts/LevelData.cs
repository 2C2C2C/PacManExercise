using UnityEngine;

[SerializeField]
public class LevelData
{
    public string name;
    public int[,] grids;
    public LevelData()
    {
        name = "01";
        grids = new int[,]
        {
            { 0,0}
        };
    }
}
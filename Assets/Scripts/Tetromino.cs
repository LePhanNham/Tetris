using UnityEngine;
using UnityEngine.Tilemaps;



public enum Tetromino
{
    I, J, L, O, S, T, Z
}
[System.Serializable]
public class TetrominoData
{
    public Tetromino tetromino;
    public Tile tile;
    public Vector2Int[] cells; 
    public Vector2Int[,] wallKicks { get;private set; }
    public void Initialize()
    {
        this.cells = Data.Cells[this.tetromino];
        this.wallKicks = Data.WallKicks[this.tetromino];
    }

    public void GetCells()
    {
        foreach (var cell in this.cells)
        {
            // Debug.Log(cell);    
        }
    }
}
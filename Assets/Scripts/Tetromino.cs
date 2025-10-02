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

    public void Initialize()
    {
        this.cells = Data.Cells[this.tetromino];
    }

    public void GetCells()
    {
        foreach (var cell in this.cells)
        {
            Debug.Log(cell);    
        }
    }
}
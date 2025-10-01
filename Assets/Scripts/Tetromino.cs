using UnityEngine;
using UnityEngine.Tilemaps;



public enum Tetromino
{
    I, O, T, S, Z, J, L
}
[System.Serializable]
public struct TetrominoData
{
    public Tetromino tetromino;
    public Tile tile;
    public Vector2Int[] cells; 
}
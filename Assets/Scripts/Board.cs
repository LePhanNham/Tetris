using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Tilemap tilemap
    {
        get; private set;
    }

    public Piece activePiece
    {
        get; private set;
    }
    public List<TetrominoData> tetrominoes;
    public Vector3Int spawnPosition;
    public Vector2Int boardSize = new Vector2Int(10,20);

    public RectInt Bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-this.boardSize.x/2, -this.boardSize.y/2);
            return new RectInt(position, this.boardSize);
        }
    }
    private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<Piece>();
        for (int i=0;i<this.tetrominoes.Count;i++)
        {
            tetrominoes[i].Initialize();
        }
    }

    private void Start()
    {
        SpawnPiece();
    }
    public void SpawnPiece()
    {
        int random = Random.Range(0, tetrominoes.Count);
        TetrominoData data = tetrominoes[random];
        this.activePiece.Initialize(this,this.spawnPosition,data);
        if (isValidPosition(this.activePiece, this.spawnPosition))
        {
            Set(this.activePiece);
        }
        else
        {
            GameOver();
        }
        Set(this.activePiece);
        
        
    }

    private void GameOver()
    {
        this.tilemap.ClearAllTiles();
    }


    public void Set(Piece piece) 
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.position + piece.cells[i];
            this.tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }
    public void Clear(Piece piece) 
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.position + piece.cells[i];
            this.tilemap.SetTile(tilePosition, null);
        }
    }
    
    public bool isValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = this.Bounds;
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + position;
            if (!bounds.Contains((Vector2Int)tilePosition))
            {
                return false;
            }
            if (this.tilemap.HasTile(tilePosition))
            {
                return false;
            }
        }

        return true;
    }


    public void ClearLine()
    {
        RectInt bounds = this.Bounds;
        int row = Bounds.yMin;

        while (row < bounds.yMax)
        {
            if (IsLineFull(row))
            {
                LineClear(row);
            }

            else
            {
                row++;
            }
        }
    }

    private bool IsLineFull(int row)
    {
        RectInt bounds = this.Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int pos = new Vector3Int(col, row, 0);
            if (!tilemap.HasTile(pos)) return false;
        }

        return true;
    }

    private void LineClear(int row)
    {
        RectInt bounds = this.Bounds;
        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int pos = new Vector3Int(col, row, 0);
            this.tilemap.SetTile(pos, null);
        }

        while (row < bounds.yMax)
        {
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int pos = new Vector3Int(col, row + 1, 0);
                TileBase above = this.tilemap.GetTile(pos);
                pos = new Vector3Int(col, row , 0);
                this.tilemap.SetTile(pos, above);
            }
        }
    }
}

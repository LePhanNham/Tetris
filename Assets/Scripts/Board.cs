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
        Debug.Log(random);
        TetrominoData data = tetrominoes[random];
        data.GetCells();
        this.activePiece.Initialize(this,this.spawnPosition,data);
        Set(this.activePiece);
    }


    public void Set(Piece piece) 
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.position + piece.cells[i];
            this.tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }
}

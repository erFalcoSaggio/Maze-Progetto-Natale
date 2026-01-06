using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private MazeCell _mazeCellPrefab;

    [Header("Dimensioni iniziali")]
    [SerializeField] private int _mazeWidth = 8;
    [SerializeField] private int _mazeDepth = 8;

    private MazeCell[,] _mazeGrid;

    [Header("Start / Exit")]
    [SerializeField] private Transform _startMarker;
    [SerializeField] private Transform _exitMarker;

    public int CurrentLevel { get; private set; } = 1;

    public void GenerateLevel(int level)
    {
        CurrentLevel = level;

        // crescenti
        _mazeWidth = 8 + (level - 1) * 2;
        _mazeDepth = 8 + (level - 1) * 2;

        // canc vecchio labirinto
        if (_mazeGrid != null)
        {
            foreach (var cell in _mazeGrid)
                Destroy(cell.gameObject);
        }

        // griglia
        _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];

        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int z = 0; z < _mazeDepth; z++)
            {
                _mazeGrid[x, z] = Instantiate(_mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);
            }
        }

        // gen
        GenerateMaze(null, _mazeGrid[0, 0]);

        // muro di ingresso
        _mazeGrid[0, 0].ClearLeftWall();

        // posizione corretta dello start
        Vector3 cellPos = _mazeGrid[0, 0].transform.position;

        // il muro sinistro è a x - 0.5
        float wallX = cellPos.x - 0.5f;

        //furoi dal muro
        _startMarker.position = new Vector3(
            wallX - 0.6f,
            0.5f,
            cellPos.z
        );


        //exit
        MazeCell exitCell = _mazeGrid[_mazeWidth - 1, _mazeDepth - 1];
        _exitMarker.position = exitCell.transform.position + Vector3.up * 0.5f;
    }

    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);

        MazeCell nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);
    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);
        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = (int)currentCell.transform.position.x;
        int z = (int)currentCell.transform.position.z;

        if (x + 1 < _mazeWidth && !_mazeGrid[x + 1, z].IsVisited)
            yield return _mazeGrid[x + 1, z];

        if (x - 1 >= 0 && !_mazeGrid[x - 1, z].IsVisited)
            yield return _mazeGrid[x - 1, z];

        if (z + 1 < _mazeDepth && !_mazeGrid[x, z + 1].IsVisited)
            yield return _mazeGrid[x, z + 1];

        if (z - 1 >= 0 && !_mazeGrid[x, z - 1].IsVisited)
            yield return _mazeGrid[x, z - 1];
    }

    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null)
            return;

        if (previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }

        if (previousCell.transform.position.x > currentCell.transform.position.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }

        if (previousCell.transform.position.z < currentCell.transform.position.z)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }

        if (previousCell.transform.position.z > currentCell.transform.position.z)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }

    public Vector3 GetStartPosition()
    {
        return _startMarker.position;
    }

}

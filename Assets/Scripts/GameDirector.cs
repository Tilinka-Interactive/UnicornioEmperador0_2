using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameDirector : MonoBehaviour
{
    public Tilemap tiles;
    public Tile road;
    public Tile wall;
    public Tile sideRoad;
    public Tile water;
    public Tile point;
    public Vector3 myVector;
    public int x, y;
    //public Vector3Int location;
    //maze.UpdateGrid();
    private void Start()
    {
        MazeGenerator maze;
        maze = new MazeGenerator(x, y);
        maze.draw();
        //maze.Solve(0,0);
        //Debug.Log(maze.GetCell(0,1).wall);
        for (int i = 0; i < maze.gridDimensionX; i++)
        {
            for (int j = 0; j < maze.gridDimensionY; j++) {
               
                    myVector = new Vector3(i + 0.0f, j + 0.0f, 0.0f);
                switch (maze.grid[i,j])
                {
                    case 'X':
                        tiles.SetTile(Vector3Int.FloorToInt(myVector), wall);
                        break;

                    case ' ':
                        tiles.SetTile(Vector3Int.FloorToInt(myVector), sideRoad);
                        break;

                    case '_':
                        tiles.SetTile(Vector3Int.FloorToInt(myVector), road);
                        break;

                    case '*':
                        tiles.SetTile(Vector3Int.FloorToInt(myVector), point);
                        break;

                    default:
                        tiles.SetTile(Vector3Int.FloorToInt(myVector), water);
                        break;
                }
            }
        }
    }
}

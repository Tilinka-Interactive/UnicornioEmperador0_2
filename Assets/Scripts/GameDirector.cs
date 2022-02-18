using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameDirector : MonoBehaviour
{
    public Tilemap bases;
    public Tilemap walls;
    //public Tilemap collision;
    public Tile road;
    public Tile wall;
    public Tile sideRoad;
    public Tile point;
    public Tile colliderTile;
    public Vector3 myVector;
    public int x, y;
    private void Start()
    {
        MazeGenerator maze;
        maze = new MazeGenerator(x, y);
        maze.Solve(0, 0);
        maze.draw();
        for (int i = 0; i < maze.gridDimensionX; i++)
        {
            for (int j = 0; j < maze.gridDimensionY; j++) {
               
                    myVector = new Vector3(i + 0.0f, j + 0.0f, 0.0f);
                switch (maze.grid[i,j])
                {
                    case 'X':
                        //collision.SetTile(Vector3Int.FloorToInt(myVector), colliderTile);
                        //walls.SetTile(Vector3Int.FloorToInt(myVector), wall);
                        break;

                    case ' ':
                        //collision.SetTile(Vector3Int.FloorToInt(myVector), colliderTile);
                        //bases.SetTile(Vector3Int.FloorToInt(myVector), sideRoad);
                        break;

                    case '_':
                        bases.SetTile(Vector3Int.FloorToInt(myVector), road);
                        break;

                    case '*':
                        bases.SetTile(Vector3Int.FloorToInt(myVector), point);
                        break;

                    default:
                        bases.SetTile(Vector3Int.FloorToInt(myVector), road);
                        break;
                }
            }
        }
    }
}

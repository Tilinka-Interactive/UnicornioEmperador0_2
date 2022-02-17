using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameDirector : MonoBehaviour
{
    public Tilemap basement;
    public Tilemap walls;
    public Tile road;
    public Tile wall;
    public Tile sideRoad;
    public Tile water;
    public Tile point;
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
                //Basement
                switch (maze.grid[i,j])
                {
                    case 'X':
                        //walls.SetTile(Vector3Int.FloorToInt(myVector), road);
                        //basement.SetTile(Vector3Int.FloorToInt(myVector), wall);
                        break;

                    case ' ':
                        basement.SetTile(Vector3Int.FloorToInt(myVector), road);
                        break;

                    case '_':
                        basement.SetTile(Vector3Int.FloorToInt(myVector), road);
                        break;

                    case '*':
                        basement.SetTile(Vector3Int.FloorToInt(myVector), road);
                        //tiles.SetTile(Vector3Int.FloorToInt(myVector), point);
                        break;

                    default:
                        //tiles.SetTile(Vector3Int.FloorToInt(myVector), water);
                        break;
                }
                switch (maze.grid[i, j])
                {
                    case 'X':
                        walls.SetTile(Vector3Int.FloorToInt(myVector), wall);
                        //basement.SetTile(Vector3Int.FloorToInt(myVector), wall);
                        break;


                    default:
                        //tiles.SetTile(Vector3Int.FloorToInt(myVector), water);
                        break;
                }
            }
        }
    }
}

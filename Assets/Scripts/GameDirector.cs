using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameDirector : MonoBehaviour
{
    public Tilemap bases;
    public Tilemap walls;
    public Tile road;
    public Tile wall;
    public Tile sideRoad;
    public Tile point;
    public Vector3 myVector;
    public int x, y;
    public PlayerMovement jugador;
    
    private void Awake()
    {
        MazeGenerator maze;
        maze = new MazeGenerator(x, y);
        //maze.Solve(0, 0);
        maze.draw();
        jugador.endPos = new Vector3((maze.gridDimensionX-3) - ((maze.gridDimensionY - 2)), (maze.gridDimensionX - 3) * 0.5f + ((maze.gridDimensionY - 2) * 0.5f), 0f);
        Debug.Log("x: " +maze.gridDimensionX+" y: " +maze.gridDimensionY);
        for (int i = 0; i < maze.gridDimensionX; i++)
        {
            for (int j = 0; j < maze.gridDimensionY; j++) {
               
                    myVector = new Vector3(i + 0.0f, j + 0.0f, 0.0f);
                switch (maze.grid[i,j])
                {
                    case 'X':
                        walls.SetTile(Vector3Int.FloorToInt(myVector), wall);
                        bases.SetTile(Vector3Int.FloorToInt(myVector), wall);
                        break;

                    case ' ':
                        walls.SetTile(Vector3Int.FloorToInt(myVector), wall);
                        break;

                    case '_':
                        bases.SetTile(Vector3Int.FloorToInt(myVector), road);
                        break;

                    case '*':
                        bases.SetTile(Vector3Int.FloorToInt(myVector), point);
                        break;

                    case 'º':
                        bases.SetTile(Vector3Int.FloorToInt(myVector), wall);
                        break;

                    default:
                        bases.SetTile(Vector3Int.FloorToInt(myVector), road);
                        break;
                }
            }
        }
    }
}

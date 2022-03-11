using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameDirector : MonoBehaviour
{
    public Tilemap bases;
    public Tilemap walls;
    public Tilemap hint;
    public Tile[] colorTile;
    public Tile[] bwTile;
    public Tile road;
    public Tile wall;
    public Tile sideRoad;
    public Tile point;
    public Vector3 myVector;
    private string xPrefsName = "x";
    private string yPrefsName = "y";
    public int x, y;
    public PlayerMovement jugador;
    private MazeGenerator maze;
    private void Awake()
    {
        x = PlayerPrefs.GetInt(xPrefsName, 0);
        y = PlayerPrefs.GetInt(yPrefsName, 0);
        maze = new MazeGenerator(x, x);
        //maze.Solve();
        maze.draw();
        jugador.endPos = new Vector3((maze.gridDimensionX-3) - ((maze.gridDimensionY - 2)), (maze.gridDimensionX - 3) * 0.5f + ((maze.gridDimensionY - 2) * 0.5f), 0f);
        jugador.crono.StartCrono();
        for (int i = 0; i < maze.gridDimensionX; i++)
        {
            for (int j = 0; j < maze.gridDimensionY; j++) {
                    myVector = new Vector3(i + 0.0f, j + 0.0f, 0.0f);
                switch (maze.grid[i,j])
                {
                    case 'X':
                        walls.SetTile(Vector3Int.FloorToInt(myVector), wall);
                        //bases.SetTile(Vector3Int.FloorToInt(myVector), wall);
                        break;

                    case ' ':
                        walls.SetTile(Vector3Int.FloorToInt(myVector), wall);
                        break;

                    case '_':
                        bases.SetTile(Vector3Int.FloorToInt(myVector), getTile(bwTile));
                        break;

                    case '*':
                        //bases.SetTile(Vector3Int.FloorToInt(myVector), point);
                        break;

                    case '�':
                        bases.SetTile(Vector3Int.FloorToInt(myVector), getTile(colorTile));
                        break;

                    default:
                        bases.SetTile(Vector3Int.FloorToInt(myVector), road);
                        break;
                }
            }
        }
    }

    public void GaveHint()
    {
        maze.Solve();
        maze.draw();
        for (int i = 0; i < maze.gridDimensionX; i++)
        {
            for (int j = 0; j < maze.gridDimensionY; j++)
            {
                myVector = new Vector3(i + 0.0f, j + 0.0f, 0.0f);
                if (maze.grid[i, j] == '*')
                {
                    hint.SetTile(Vector3Int.FloorToInt(myVector), point);
                }
            }
        }
    }

    private Tile getTile(Tile[] list) {
        return list[Random.Range(0, list.Length)];
    }
}

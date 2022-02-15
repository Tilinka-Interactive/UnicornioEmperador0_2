using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MazeGenerator : Object
{
    private int dimensionX, dimensionY; 
    public int gridDimensionX, gridDimensionY;
    public char[,] grid;
    public CellClass[][] cells;
    public CellClass aux;
    private CellClass endCell;
    private CellClass current;
    private CellClass backtracking;

    public MazeGenerator(int xDimension, int yDimension)
    {
        dimensionX = xDimension;
        dimensionY = yDimension;
        gridDimensionX = xDimension * 4 + 1;
        gridDimensionY = yDimension * 2 + 1;
        grid = new char[gridDimensionX,gridDimensionY];
        Init();
        GenerateMaze(0,0);
    }

    private void Init()
    {
        cells = new CellClass[dimensionX][];
        for (int j = 0; j < dimensionX; j++)
        {
            cells[j] = new CellClass[dimensionY];
        }

        for (int x = 0; x < dimensionX; x++)
        {
            for (int y = 0; y < dimensionY; y++)
            {
                cells[x][y] = new CellClass(x, y, false);
            }
        }
    }


    private void GenerateMaze(int x, int y)
    {
        //Debug.Log(GetCell(x, y));
        GenerateMaze(GetCell(x, y)); 
    }

    private void GenerateMaze(CellClass startAt)
    {
        //Debug.Log(startAt is null);
        if (startAt is null) return;
        startAt.open = false;
        List<CellClass> cells = new List<CellClass>();
        cells.Add(startAt);
        //Debug.Log("First While");
        while (!(cells.Count == 0))
        {
            int aux;
            CellClass cell;
            if (UnityEngine.Random.Range(0, 11) == 0)
            {
                aux = UnityEngine.Random.Range(0, cells.Count);
                cell = cells[aux];
                //Debug.Log(cells[aux].x);
                cells.RemoveAt(aux);
                //if (cells[aux]) Debug.Log("Exists");
            }
            else
            {
                cell = cells[cells.Count - 1];
                cells.RemoveAt(cells.Count - 1);
            }
            List<CellClass> neighbors = new List<CellClass>();
            CellClass[] potentialNeighbors = new CellClass[]{
              GetCell(cell.x + 1, cell.y),
              GetCell(cell.x, cell.y + 1),
              GetCell(cell.x - 1, cell.y),
              GetCell(cell.x, cell.y - 1)
          };
            foreach (CellClass other in potentialNeighbors)
            {
                if (other is null || other.wall || !other.open) continue;
                neighbors.Add(other);
            }

            if (neighbors.Count == 0) continue;
            CellClass selected = neighbors[UnityEngine.Random.Range(0, neighbors.Count)];
            selected.open = false; 
            cell.AddNeighbor(selected);
            cells.Add(cell);
            cells.Add(selected);
        }
    }
    
    public CellClass GetCell(int x, int y)
    {
        if (x < dimensionX && y < dimensionY && x >= 0 && y >=0) return cells[x][y];
        else return null;
    }


    public void Solve (int startX, int startY)
    {
        Solve(startX, startY, dimensionX - 1, dimensionY - 1);
    }
    public void Solve (int startX, int startY, int endX, int endY)
    {
        foreach (CellClass[] cellrow in cells)
        {
            foreach (CellClass cell in cellrow)
            {
                cell.parent = null;
                cell.visited = false;
                cell.inPath = false;
                cell.travelled = 0;
                cell.projectedDist = -1;
            }
        }

        List<CellClass> openCells = new List<CellClass>();
        endCell = GetCell(endX, endY);

        if (endCell is null) return; 
        else
            { 
                CellClass start = GetCell(startX, startY);
                if (start is null) return; 
                start.projectedDist = GetProjectedDistance(start, 0, endCell);
                start.visited = true;
                openCells.Add(start);
            }

        bool solving = true;

        while (solving)
        {
            if (openCells.Count == 0) return;
            openCells.Sort();
            current = openCells[0];
            openCells.RemoveAt(0);


            if (current == endCell) break; 
            foreach (CellClass neighbor in current.neighbors)
            {
            double projDist = GetProjectedDistance(neighbor, current.travelled + 1, endCell);
            if (!neighbor.visited || projDist < neighbor.projectedDist)
            { 
                neighbor.parent = current;
                neighbor.visited = true;
                neighbor.projectedDist = projDist;
                neighbor.travelled = current.travelled + 1;
                if (!openCells.Contains(neighbor)) openCells.Add(neighbor);
            }
        }
    }
        backtracking = endCell;
        backtracking.inPath = true;
        while (backtracking.parent != null)
        {
            backtracking = backtracking.parent;
            backtracking.inPath = true;
        }
    }

    public double GetProjectedDistance(CellClass current, double travelled, CellClass end)
    {
        return travelled + Mathf.Abs(current.x - end.x) + Mathf.Abs(current.y - current.x);
    }

    public void UpdateGrid()
    {
        char backChar = ' ', wallChar = 'X', cellChar = '_', pathChar = '*';
        
        for (int x = 0; x < gridDimensionX; x++)
        {
            for (int y = 0; y < gridDimensionY; y++)
            {
                grid[x,y] = backChar;
            }
        }

        for (int x = 0; x < gridDimensionX; x++)
        {
            for (int y = 0; y < gridDimensionY; y++)
            {
                if (x % 4 == 0 || y % 2 == 0)
                    grid[x,y] = wallChar;
            }
        }

        for (int x = 0; x < dimensionX; x++)
        {
            for (int y = 0; y < dimensionY; y++)
            {
                CellClass current = GetCell(x, y);
                //Debug.Log(GetCell(x, y).x);
                int gridX = x * 4 + 2, gridY = y * 2 + 1;
                if (current.inPath)
                {
                    grid[gridX,gridY] = pathChar;
                    if (current.IsCellBelowNeighbor())
                        if (GetCell(x, y + 1).inPath)
                        {
                            grid[gridX,gridY + 1] = pathChar;
                            grid[gridX + 1,gridY + 1] = backChar;
                            grid[gridX - 1,gridY + 1] = backChar;
                        }
                        else
                        {
                            grid[gridX,gridY + 1] = cellChar;
                            grid[gridX + 1,gridY + 1] = backChar;
                            grid[gridX - 1,gridY + 1] = backChar;
                        }
                    if (current.IsCellRightNeighbor())
                        if (GetCell(x + 1, y).inPath)
                        {
                            grid[gridX + 2,gridY] = pathChar;
                            grid[gridX + 1,gridY] = pathChar;
                            grid[gridX + 3,gridY] = pathChar;
                        }
                        else
                        {
                            grid[gridX + 2,gridY] = cellChar;
                            grid[gridX + 1,gridY] = cellChar;
                            grid[gridX + 3,gridY] = cellChar;
                        }
                }
                else
                {
                    grid[gridX,gridY] = cellChar;
                    if (current.IsCellBelowNeighbor())
                    {
                        grid[gridX,gridY + 1] = cellChar;
                        grid[gridX + 1,gridY + 1] = backChar;
                        grid[gridX - 1,gridY + 1] = backChar;
                    }
                    if (current.IsCellRightNeighbor())
                    {
                        grid[gridX + 2,gridY] = cellChar;
                        grid[gridX + 1,gridY] = cellChar;
                        grid[gridX + 3,gridY] = cellChar;
                    }
                }
            }
        }
    }
    public void draw()
    {
        UpdateGrid(); 
        //Debug.Log(this);
    }

    public override string ToString()
    {
        //UpdateGrid();
        string output = "";
        for (int y = 0; y < gridDimensionY; y++)
        {
            for (int x = 0; x < gridDimensionX; x++)
            {
                output += grid[x,y];
            }
            output += "\n";
        }
        return output;
    }
}

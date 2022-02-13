using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    private int dimensionX, dimensionY; 
    private int gridDimensionX, gridDimensionY;
    private char[,] grid;
    private char[,] grid2;
    private CellClass[,] cells;
    //private CellClass auxCellClass;


    public MazeGenerator(int xDimension, int yDimension)
    {
        dimensionX = xDimension;
        dimensionY = yDimension;
        gridDimensionX = xDimension * 4 + 1;
        gridDimensionY = yDimension * 2 + 1;
        grid = new char[gridDimensionX,gridDimensionY];
        grid2 = new char[xDimension,yDimension];
        Init();
        GenerateMaze();
    }

    private void Init()
    {
        cells = new CellClass[dimensionX,dimensionY];
        for (int x = 0; x < dimensionX; x++)
        {
            for (int y = 0; y < dimensionY; y++)
            {
                cells[x,y] = new CellClass(x, y, false);
            }
        }
    }

    private void GenerateMaze()
    {
        GenerateMaze(0, 0);
    }

    private void GenerateMaze(int x, int y)
    {
        GenerateMaze(GetCell(x, y)); 
    }


    private void GenerateMaze(CellClass startAt)
    {
        if (startAt == null) return;
        startAt.open = false;
        List<CellClass> cells = new List<CellClass>();
        cells.Add(startAt);

        while (!(cells.Count == 0))
        {
            int aux;
            CellClass cell;
            if (UnityEngine.Random.Range(0, 11) == 0)
            {
                aux = UnityEngine.Random.Range(0, cells.Count);
                cell = cells[aux];
                cells.RemoveAt(aux);
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
                if (other == null || other.wall || !other.open) continue;
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
        if (cells.Rank > 1)
        {
            if (x < cells.GetUpperBound(1 - 1) + 1 && y < cells.GetUpperBound(2 - 1) + 1) return cells[x, y];
            else return null;
        }
        else
        { 
            if (cells.Rank == 1)
            {
                if (x == 0 && y < cells.Length) return cells[x, y];
                else return null;
            }
            if (cells.Rank == 0) return null;
            else {
                return null;
            }
        }
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
        // cells still being considered
        ArrayList<Cell> openCells = new ArrayList<>();
        // cell being considered
        Cell endCell = getCell(endX, endY);
        if (endCell == null) return; // quit if end out of bounds
        { // anonymous block to delete start, because not used later
            Cell start = getCell(startX, startY);
            if (start == null) return; // quit if start out of bounds
            start.projectedDist = getProjectedDistance(start, 0, endCell);
            start.visited = true;
            openCells.add(start);
        }
        boolean solving = true;
        while (solving)
        {
            if (openCells.isEmpty()) return; // quit, no path
                                             // sort openCells according to least projected distance
            Collections.sort(openCells, new Comparator<Cell>(){
              @Override
              public int compare(Cell cell1, Cell cell2)
            {
                double diff = cell1.projectedDist - cell2.projectedDist;
                if (diff > 0) return 1;
                else if (diff < 0) return -1;
                else return 0;
            }
        });
        Cell current = openCells.remove(0); // pop cell least projectedDist
        if (current == endCell) break; // at end
        for (Cell neighbor : current.neighbors)
        {
            double projDist = getProjectedDistance(neighbor,
                    current.travelled + 1, endCell);
            if (!neighbor.visited || // not visited yet
                    projDist < neighbor.projectedDist)
            { // better path
                neighbor.parent = current;
                neighbor.visited = true;
                neighbor.projectedDist = projDist;
                neighbor.travelled = current.travelled + 1;
                if (!openCells.contains(neighbor))
                    openCells.add(neighbor);
            }
        }
    }
    // create path from end to beginning
    Cell backtracking = endCell;
    backtracking.inPath = true;
      while (backtracking.parent != null) {
          backtracking = backtracking.parent;
          backtracking.inPath = true;
      }
  }
}

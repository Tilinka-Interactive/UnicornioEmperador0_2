using System.Collections.Generic;
using UnityEngine;

public class CellClass : Object
{
    public int id;
    public int x;
    public int y;
    public List<CellClass> neighbors = new List<CellClass>();
    public bool visited = false;
    public CellClass parent = null;
    public bool inPath = false;
    public double travelled;
    public double projectedDist;
    public bool wall = true;
    public bool open = true;
    
    public CellClass (int x, int y, bool isWall)
    {
        this.x = x;
        this.y = y;
        this.wall = isWall;
    }

    public void AddNeighbor(CellClass other) 
    {
        if (!this.neighbors.Contains(other)) this.neighbors.Add(other);
        if (!other.neighbors.Contains(this)) other.neighbors.Add(this);
    }

    public bool IsCellBelowNeighbor() 
    {
        //var aux = new CellClass(x, y + 1);
        //return neighbors.Contains(aux);
        return neighbors.Contains(new CellClass(this.x, this.y + 1,true));
    }

    public bool IsCellRightNeighbor()
    {
        return this.neighbors.Contains(new CellClass(this.x + 1, this.y, true));
    }

    public override string ToString()
    {
        return string.Format("Cell(%s, %s)", x, y);
    }

    public override bool Equals (object other)
    {
        if (! typeof(CellClass).IsInstanceOfType(other)) return false;
        //if (!(other typeOf CellClass)) return false;
        CellClass otherCell = (CellClass)other;
        return (this.x == otherCell.x && this.y == otherCell.y);
    }

    public int HashCode()
    {
        return  x + y * 256; 
    }

    public override int GetHashCode()
    {
        return x + y * 256;
    }

    /*
    public int CompareTo(CellClass cell)
    {
        if (cell == null)
            return 0;

        else
            return this.id.CompareTo(cell.id);
    }
    */

    public bool Equals(CellClass other)
    {
        if (other == null) return false;
        return (this.id.Equals(other.id));
    }
}

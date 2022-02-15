using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellClass : Object
{
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

    
    public CellClass(int x, int y) 
    {   /*
        CellClass aux = gameObject.AddComponent<CellClass>();
        aux.x = x;
        aux.y = y;
        aux.wall = true;
        */
        //CellClass(x, y, true);
    }
    
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
    public int Compare(CellClass cell1, CellClass cell2)
    {
        double diff = cell1.projectedDist - cell2.projectedDist;
        if (diff > 0) return 1;
        else if (diff < 0) return -1;
        else return 0;
    }

}

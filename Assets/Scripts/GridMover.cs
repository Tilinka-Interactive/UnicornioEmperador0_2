using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMover : MonoBehaviour
{
    public Grid grid;
    public float moveSpeed = 5f;

    private Vector2Int _targetCell;
    private Vector2 _targetPosition;

    private void Start()
    {
        // get initial grid location
        _targetCell = (Vector2Int)grid.WorldToCell(transform.position);

        // snap to the current cell
        transform.position = grid.CellToWorld((Vector3Int)_targetCell);
    }

    private void Update()
    {
        Vector2Int gridMovement = new Vector2Int();

        if (Input.GetKeyDown(KeyCode.W))
        {
            gridMovement.x += 1;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            gridMovement.y -= 1;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            gridMovement.x -= 1;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            gridMovement.y += 1;
        }

        if (gridMovement != Vector2Int.zero)
        {
            _targetCell += gridMovement;
            _targetPosition = grid.CellToWorld((Vector3Int)_targetCell);
        }

        MoveToward(_targetPosition);
    }

    private void MoveToward(Vector2 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }
}
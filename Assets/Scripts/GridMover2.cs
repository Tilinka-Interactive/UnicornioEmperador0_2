using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMover2 : MonoBehaviour
{
    // Start is called before the first frame update
    public Grid grid;
    public GameObject player;
    private bool isMoving;
    private Vector3 origPos, targetPos;
    private float timeToMove = 0.5f;
    private Vector3Int _targetCell;
    private Vector3 _targetPosition;

    private void Start()
    {
        // Get initial position of the player on the world grid
        Vector3 initialPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

        _targetCell = grid.WorldToCell(initialPosition);

        // Snap the player to the center of the initial cell
        _targetPosition = grid.CellToWorld(_targetCell);
    }

    void Update()
    {
        Vector3Int gridMovement = new Vector3Int();
        if (Input.GetKey(KeyCode.W) && !isMoving)
        {
            gridMovement.x += 1;
            if (gridMovement != Vector3Int.zero)
            {
                _targetCell += gridMovement;
                _targetPosition = grid.CellToWorld(_targetCell);
                StartCoroutine(MovePlayer(_targetPosition));
            }
            //StartCoroutine(MovePlayer(Vector3.up));
        }
        if (Input.GetKey(KeyCode.A) && !isMoving)
        {
            gridMovement.y += 1;
            if (gridMovement != Vector3Int.zero)
            {
                _targetCell += gridMovement;
                _targetPosition = grid.CellToWorld(_targetCell);
                StartCoroutine(MovePlayer(_targetPosition));
            }
        }
        //StartCoroutine(MovePlayer(Vector3.left));
        if (Input.GetKey(KeyCode.S) && !isMoving)
        {
            gridMovement.x -= 1;
            if (gridMovement != Vector3Int.zero)
            {
                _targetCell += gridMovement;
                _targetPosition = grid.CellToWorld(_targetCell);
                StartCoroutine(MovePlayer(_targetPosition));
            }
        }
        //StartCoroutine(MovePlayer(Vector3.right));
        if (Input.GetKey(KeyCode.D) && !isMoving)
        {
            gridMovement.y -= 1;
            if (gridMovement != Vector3Int.zero)
            {
                _targetCell += gridMovement;
                _targetPosition = grid.CellToWorld(_targetCell);
                StartCoroutine(MovePlayer(_targetPosition));
            }
            //StartCoroutine(MovePlayer(Vector3.down));
        }


    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;
        float elapsedTime = 0;
        origPos = transform.position;
        targetPos = origPos + direction;

        while (elapsedTime < timeToMove)
        {
            //transform.position = Vector3.Lerp(origPos, targetPos, 10f * Time.deltaTime);
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        isMoving = false;
    }
}


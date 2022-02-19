using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    private bool isMoving;
    private Vector3 origPos, targetPos;
    private float timeToMove = 0.2f;
    public Joystick joystick;
    public Tilemap tiles;
    public Tile tile;
    public Vector3Int location;

    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            location = tiles.WorldToCell(mp);
            tiles.SetTile(location, tile);
        }

        if (Input.GetMouseButton(1))
        {
            //Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            getTNormal();
        }
        


        Vector3 aux;
        if ((joystick.Horizontal != 0) && (joystick.Vertical != 0))
        {
            if ((joystick.Horizontal > 0f) && (joystick.Vertical > 0f) && !isMoving)
            {
                aux = new Vector3(1.0f, 0.5f, 0f);
                if (getT(aux)) {
                    Debug.Log("Wall!");
                }
                else
                    StartCoroutine(MovePlayer(aux));
            }
            if ((joystick.Horizontal < 0f) && (joystick.Vertical < 0f) && !isMoving)
            {
                aux = new Vector3(-1.0f, -0.5f, 0f);
                StartCoroutine(MovePlayer(aux));
            }
            if ((joystick.Horizontal < 0f) && (joystick.Vertical > 0f) && !isMoving)
            {
                aux = new Vector3(-1.0f, 0.5f, 0f);
                StartCoroutine(MovePlayer(aux));
            }
            if ((joystick.Horizontal > 0f) && (joystick.Vertical < 0f) && !isMoving)
            {
                aux = new Vector3(1.0f, -0.5f, 0f);
                StartCoroutine(MovePlayer(aux));
            }
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
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        isMoving = false;
    }

    private bool getT(Vector3 posSiguiente)
    {
        
        location = tiles.WorldToCell(posSiguiente);
        if (tiles.GetTile(location))
        {
            return true;
        }
        else
            return false;
    }

    private void getTNormal()
    {
        Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        location = tiles.WorldToCell(mp);
        if (tiles.GetTile(location))
        {
            Debug.Log("Tile");
        }
        else
            Debug.Log("Wall");
    }
}

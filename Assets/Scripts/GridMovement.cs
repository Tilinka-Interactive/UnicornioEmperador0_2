using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridMovement : MonoBehaviour
{
    private bool isMoving;
    private Vector3 origPos, targetPos;
    private float timeToMove = 0.2f;
    public Joystick joystick;
    [SerializeField]
    private Tilemap map;
    [SerializeField]
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    private Vector3Int gridPosition;
    private TileBase nextTile;

    void Update()
    {
        Vector3 aux;
        if ((joystick.Horizontal != 0) && (joystick.Vertical != 0))
        {
            if ((joystick.Horizontal > 0f) && (joystick.Vertical > 0f) && !isMoving)
            {
                aux = new Vector3(1.0f, 0.5f, 0f);
                targetPos = origPos + aux;
                Debug.Log("targetPos:"+targetPos);
                gridPosition = map.WorldToCell(targetPos);
                Debug.Log("gridPostion."+gridPosition);
                nextTile = map.GetTile(gridPosition);
                Debug.Log("nextTile:"+nextTile);
                if (!dataFromTiles[nextTile].wall)
                {
                    StartCoroutine(MovePlayer(aux));
                }
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
            
    public void dictionary()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();
        foreach (var tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles)
            {
                Debug.Log(tileData.wall);
                dataFromTiles.Add(tile, tileData);
            }
        }
    }

}

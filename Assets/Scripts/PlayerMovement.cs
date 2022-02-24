using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    private bool isMoving;
    private Vector3 origPos, targetPos;
    public Vector3 endPos;
    private float timeToMove = 0.2f;
    private bool timesOut;
    public Tile inPlaceTile;
    public Tile coloredTile;
    public Joystick joystick;
    public Tilemap wallTiles;
    public Tilemap baseTiles;
    public Vector3Int location;
    public GameObject menuWin;
    public GameObject JoyStick;
    public Timer crono;

    void Update()
    {
        if (transform.position == (endPos)) 
        {
            crono.StopCrono();
            JoyStick.SetActive(false);
            menuWin.SetActive(true);
        }
        else 
        {
            if (!timesOut)
            {
                Vector3 aux;
                if ((joystick.Horizontal != 0) && (joystick.Vertical != 0))
                {
                    //setTileBase
                    if ((joystick.Horizontal > 0f) && (joystick.Vertical > 0f) && !isMoving)
                    {
                        aux = new Vector3(1.0f, 0.5f, 0f);
                        origPos = transform.position;
                        if (!getT(origPos + aux)) StartCoroutine(MovePlayer(aux, origPos));
                    }
                    if ((joystick.Horizontal < 0f) && (joystick.Vertical < 0f) && !isMoving)
                    {
                        aux = new Vector3(-1.0f, -0.5f, 0f);
                        origPos = transform.position;
                        if (!getT(origPos + aux)) StartCoroutine(MovePlayer(aux, origPos));
                    }
                    if ((joystick.Horizontal < 0f) && (joystick.Vertical > 0f) && !isMoving)
                    {
                        aux = new Vector3(-1.0f, 0.5f, 0f);
                        origPos = transform.position;
                        if (!getT(origPos + aux)) StartCoroutine(MovePlayer(aux, origPos));
                    }
                    if ((joystick.Horizontal > 0f) && (joystick.Vertical < 0f) && !isMoving)
                    {
                        aux = new Vector3(1.0f, -0.5f, 0f);
                        origPos = transform.position;
                        if (!getT(origPos + aux)) StartCoroutine(MovePlayer(aux, origPos));
                    }
                }

            }
            
        }
        
    }

    private IEnumerator MovePlayer(Vector3 direction, Vector3 origPos)
    {
        isMoving = true;
        float elapsedTime = 0;
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
        location = wallTiles.WorldToCell(posSiguiente);
        return wallTiles.GetTile(location);
    }

    public void TimesOut() {
        timesOut = true;
    }

    private void setTileBase(Vector3 posTile, Tile changeTile) 
    {
        location = wallTiles.WorldToCell(posTile);
        baseTiles.SetTile(location, changeTile);
    }
}

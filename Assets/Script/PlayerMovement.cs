using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private bool isMoving;
    private bool timesOut;
    private bool isJetPackOn;
    private Vector3 origPos, targetPos;
    private float timeToMove = 0.2f;
    public Vector3 endPos;
    public Tile inPlaceTile;
    public Tile coloredTile;
    public Joystick joystick;
    public Tilemap wallTiles;
    public Tilemap baseTiles;
    public Vector3Int location;
    public GameObject menuWin;
    public GameObject JoyStick;
    public Timer crono;
    public Text powerSpeedIndicator;
    
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
            Vector3 aux;
            if ((joystick.Horizontal != 0) && (joystick.Vertical != 0) && !isMoving && !timesOut)
            {
                if ((joystick.Horizontal > 0f) && (joystick.Vertical > 0f))
                {
                    if (!isJetPackOn)
                    {
                        aux = new Vector3(1.0f, 0.5f, 0f);
                        origPos = transform.position;
                        if (GetT(origPos + aux))
                        {
                            SetTileBase(origPos, coloredTile);
                            StartCoroutine(MovePlayer(aux, origPos));
                        }
                    }
                    else
                    {
                        Debug.Log("Rocket activated");
                        aux = new (10f, 5f, 0f);
                        Vector3 resta = new (1.0f, 0.5f, 0f);
                        origPos = transform.position;
                        SetTileBase(GetTJet(aux + origPos, resta), coloredTile);
                        StartCoroutine(MovePlayerPropeled(GetTJet(aux + origPos, resta), origPos));
                        isJetPackOn = false;
                    }
                    
                }
                if ((joystick.Horizontal < 0f) && (joystick.Vertical < 0f))
                {
                    aux = new Vector3(-1.0f, -0.5f, 0f);
                    origPos = transform.position;
                    if (GetT(origPos + aux))
                    {
                        SetTileBase(origPos, coloredTile);
                        StartCoroutine(MovePlayer(aux, origPos));
                    }
                }
                if ((joystick.Horizontal < 0f) && (joystick.Vertical > 0f))
                {
                    aux = new Vector3(-1.0f, 0.5f, 0f);
                    origPos = transform.position;
                    if (GetT(origPos + aux))
                    {
                        SetTileBase(origPos, coloredTile);
                        StartCoroutine(MovePlayer(aux, origPos));
                    }
                }
                if ((joystick.Horizontal > 0f) && (joystick.Vertical < 0f))
                {
                    aux = new Vector3(1.0f, -0.5f, 0f);
                    origPos = transform.position;
                    if (GetT(origPos + aux))
                    {
                        SetTileBase(origPos, coloredTile);
                        StartCoroutine(MovePlayer(aux, origPos));
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
            SetTileBase(targetPos, inPlaceTile);
        }
        transform.position = targetPos;
        isMoving = false;
    }

    private IEnumerator MovePlayerPropeled(Vector3 direction, Vector3 origPos)
    {
        isMoving = true;
        float elapsedTime = 0;
        while (elapsedTime < 0.1f)
        {
            transform.position = Vector3.Lerp(origPos, direction, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = direction;
        isMoving = false;
    }


    private bool GetT(Vector3 posSiguiente)
    {
        location = baseTiles.WorldToCell(posSiguiente);
        return baseTiles.GetTile(location);
    }

    private Vector3 GetTJet(Vector3 final, Vector3 vectorResta)
    {
        if (final == vectorResta) {
            return transform.position;
        }
        if (GetT(final))
        {
            return final;
        }
        else {
            final -= vectorResta;
            return GetTJet(final, vectorResta);
        }
    }

    public void TimesOut() {
        timesOut = true;
    }

    private void SetTileBase(Vector3 posTile, Tile changeTile) 
    {
        location = wallTiles.WorldToCell(posTile);
        baseTiles.SetTile(location, changeTile);
    }

    public void ActivatePowerSpeed() 
    {
        timeToMove = 0.1f;
        StartCoroutine(PlayCounter(10.0f));
    }

    public void ActivatePowerJetPack()
    {
        isJetPackOn = true;
    }

    public void StopPowerSpeed()
    {
        timeToMove = 0.2f;
    }

    public IEnumerator PlayCounter(float powerTime)
    {
        float elapsedTime = 0;
        while (elapsedTime < powerTime)
        {
            elapsedTime += Time.deltaTime;
            powerSpeedIndicator.text = Mathf.RoundToInt(powerTime - elapsedTime).ToString();
            yield return null;
        }
        StopPowerSpeed();
    }
}

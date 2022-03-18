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
    private bool isSpeedOn;
    private bool isJumpOn;
    private Vector3 origPos, targetPos;
    private float timeToMove = 0.2f;
    private int dirIdle;
    public Animator Animator;
    public Vector3 endPos;
    public Tile inPlaceTile;
    public Tile baseTile;
    public Joystick joystick;
    public Tilemap wallTiles;
    public Tilemap baseTiles;
    public Vector3Int location;
    public GameObject menuWin;
    public GameObject JoyStick;
    public Timer crono;
    public Text powerSpeedIndicator;
    public Tile[] colorTile;
    
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
            if((joystick.Horizontal == 0) && (joystick.Vertical == 0) && !isMoving && !timesOut) Animator.SetInteger("DirInt", dirIdle *10);
            Vector3 aux;
            if ((joystick.Horizontal != 0) && (joystick.Vertical != 0) && !isMoving && !timesOut)
            {
                
                if ((joystick.Horizontal > 0f) && (joystick.Vertical > 0f))
                {
                    if (!isJetPackOn)
                    {
                        if (!isJumpOn)
                        {
                            aux = new Vector3(1f, 0.5f, 0f);
                            origPos = transform.position;
                            dirIdle = 1;
                            Animator.SetInteger("DirInt", 1);
                            if (GetT(origPos + aux))
                            {
                                SetTileBase(origPos, null);
                                StartCoroutine(MovePlayer(aux, origPos));
                            }
                        }
                        else
                        {
                            DeactivatePowerJump();
                            aux = new Vector3(2f, 1f, 0f);
                            origPos = transform.position;
                            if (GetT(origPos + aux))
                            {
                                SetTileBase(origPos, null);
                                StartCoroutine(MovePlayer(aux, origPos));
                                isJumpOn = false;
                            }
                        }
                    }
                    else
                    {
                        //Debug.Log("Rocket activated");
                        aux = new (10f, 5f, 0f);
                        Vector3 resta = new (1.0f, 0.5f, 0f);
                        origPos = transform.position;
                        SetTileBase(GetTJet(aux + origPos, resta), null);
                        StartCoroutine(MovePlayerPropeled(GetTJet(aux + origPos, resta), origPos));
                        isJetPackOn = false;
                    }
                }
                if ((joystick.Horizontal < 0f) && (joystick.Vertical < 0f))
                {
                    if (!isJetPackOn)
                    {
                        if (!isJumpOn)
                        {
                            aux = new Vector3(-1.0f, -0.5f, 0f);
                            origPos = transform.position;
                            dirIdle = 3;
                            Animator.SetInteger("DirInt", 3);
                            if (GetT(origPos + aux))
                            {
                                SetTileBase(origPos, null);
                                StartCoroutine(MovePlayer(aux, origPos));
                            }
                        }
                        else 
                        {
                            DeactivatePowerJump();
                            aux = new Vector3(-2f, -1f, 0f);
                            origPos = transform.position;
                            if (GetT(origPos + aux))
                            {
                                SetTileBase(origPos, null);
                                StartCoroutine(MovePlayer(aux, origPos));
                                isJumpOn = false;
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Rocket activated");
                        aux = new(-10f, -5f, 0f);
                        Vector3 resta = new(-1.0f, -0.5f, 0f);
                        origPos = transform.position;
                        SetTileBase(GetTJet(aux + origPos, resta), null);
                        StartCoroutine(MovePlayerPropeled(GetTJet(aux + origPos, resta), origPos));
                        isJetPackOn = false;
                    }
                }
                if ((joystick.Horizontal < 0f) && (joystick.Vertical > 0f))
                {
                    if (!isJetPackOn)
                    {
                        if (!isJumpOn) 
                        {
                            aux = new Vector3(-1.0f, 0.5f, 0f);
                            origPos = transform.position;
                            dirIdle = 2;
                            Animator.SetInteger("DirInt", 2);
                            if (GetT(origPos + aux))
                            {
                                SetTileBase(origPos, null);
                                StartCoroutine(MovePlayer(aux, origPos));
                            }
                        }
                        else
                        {
                            DeactivatePowerJump();
                            aux = new Vector3(-2f, 1f, 0f);
                            origPos = transform.position;
                            if (GetT(origPos + aux))
                            {
                                SetTileBase(origPos, null);
                                StartCoroutine(MovePlayer(aux, origPos));
                                isJumpOn = false;
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Rocket activated");
                        aux = new(-10f, 5f, 0f);
                        Vector3 resta = new(-1.0f, 0.5f, 0f);
                        origPos = transform.position;
                        SetTileBase(GetTJet(aux + origPos, resta), null);
                        StartCoroutine(MovePlayerPropeled(GetTJet(aux + origPos, resta), origPos));
                        isJetPackOn = false;
                    }
                }
                if ((joystick.Horizontal > 0f) && (joystick.Vertical < 0f))
                {
                    if (!isJetPackOn)
                    {
                        if (!isJumpOn)
                        {
                            aux = new Vector3(1.0f, -0.5f, 0f);
                            origPos = transform.position;
                            dirIdle = 4;
                            Animator.SetInteger("DirInt", 4);
                            if (GetT(origPos + aux))
                            {
                                SetTileBase(origPos, null);
                                StartCoroutine(MovePlayer(aux, origPos));
                            }
                        }
                        else 
                        {
                            DeactivatePowerJump();
                            aux = new Vector3(2f, -1f, 0f);
                            origPos = transform.position;
                            if (GetT(origPos + aux))
                            {
                                SetTileBase(origPos, null);
                                StartCoroutine(MovePlayer(aux, origPos));
                                isJumpOn = false;
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Rocket activated");
                        aux = new(10f, -5f, 0f);
                        Vector3 resta = new(1.0f, -0.5f, 0f);
                        origPos = transform.position;
                        SetTileBase(GetTJet(aux + origPos, resta), null);
                        StartCoroutine(MovePlayerPropeled(GetTJet(aux + origPos, resta), origPos));
                        isJetPackOn = false;
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

    private void SetTileBase(Vector3 posTile, Tile tile) 
    {
        location = wallTiles.WorldToCell(posTile);
        if (tile == null) baseTiles.SetTile(location, colorTile[Random.Range(0, colorTile.Length)]);
        else baseTiles.SetTile(location, tile);

    }


    //add function to stack powerup time
    public void ActivatePowerSpeed() 
    {
        if (!isSpeedOn)
        {
            timeToMove = 0.1f;
            StartCoroutine(PlayCounter(10.0f));
        }
        
    }

    public void StopPowerSpeed()
    {
        timeToMove = 0.2f;
    }

    public void ActivatePowerJetPack()
    {
        isJetPackOn = true;
    }

    //this has to be done in a layer above the Tilebase layer
    public void ActivatePowerJump()
    {
        Vector3 origPos = transform.position;
        isJumpOn = true;
        Vector3 aux = new(2f, 1f, 0f);
        if(GetT(origPos + aux)) SetTileBase(origPos + aux, inPlaceTile);
        aux = new(-2f, -1f, 0f);
        if (GetT(origPos + aux)) SetTileBase(origPos + aux, inPlaceTile);
        aux = new(-2f, 1f, 0f);
        if (GetT(origPos + aux)) SetTileBase(origPos + aux, inPlaceTile);
        aux = new(2f, -1f, 0f);
        if (GetT(origPos + aux)) SetTileBase(origPos + aux, inPlaceTile);
    }

    //this has to be done in a layer above the Tilebase layer
    public void DeactivatePowerJump()
    {
        Vector3 origPos = transform.position;
        Vector3 aux = new(2f, 1f, 0f);
        if (GetT(origPos + aux)) SetTileBase(origPos + aux, baseTile);
        aux = new(-2f, -1f, 0f);
        if (GetT(origPos + aux)) SetTileBase(origPos + aux, baseTile);
        aux = new(-2f, 1f, 0f);
        if (GetT(origPos + aux)) SetTileBase(origPos + aux, baseTile);
        aux = new(2f, -1f, 0f);
        if (GetT(origPos + aux)) SetTileBase(origPos + aux, baseTile);
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

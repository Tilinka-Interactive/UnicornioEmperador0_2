using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    private bool isMoving;
    private Vector3 origPos, targetPos;
    private float timeToMove = 0.2f;
    public Joystick joystick;

    void Update()
    {
        Vector3 aux;
        if ((joystick.Horizontal != 0) && (joystick.Vertical != 0))
        {
            if ((joystick.Horizontal > 0f) && (joystick.Vertical > 0f) && !isMoving)
            {
                aux = new Vector3(1.0f, 0.5f, 0f);
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerScript : MonoBehaviour
{
    public GameObject Emperor;
    void Update()
    {
        if (Emperor != null)
        {
            Vector3 position = transform.position;
            position.x = Emperor.transform.position.x+0.5f;
            position.y = Emperor.transform.position.y+1;
            transform.position = position;
        }


    }
}

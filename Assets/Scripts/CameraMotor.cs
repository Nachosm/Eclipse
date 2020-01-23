using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraMotor : MonoBehaviour
{
    private Transform lookAt;
    public float boundX = 0.15f;
    public float boundY = 0.05f;
    private void Start()
    {
        lookAt = GameObject.Find("Player").transform;
    }


    private void LateUpdate()
    {
        try
        {
            Vector3 delta = Vector3.zero;

            //THis is to check if we're inside the bounds on the X axis
            float deltaX = lookAt.position.x - transform.position.x;
            if (deltaX > boundX || deltaX < -boundX)
            {
                if (transform.position.x < lookAt.position.x)
                {
                    delta.x = deltaX - boundX;
                }
                else
                {
                    delta.x = deltaX + boundX;
                }
            }

            float deltaY = lookAt.position.y - transform.position.y;
            if (deltaY > boundY || deltaY < -boundY)
            {
                if (transform.position.y < lookAt.position.y)
                {
                    delta.y = deltaY - boundY;
                }
                else
                {
                    delta.y = deltaY + boundY;
                }
            }

            transform.position += new Vector3(delta.x, delta.y, 0);
        } catch (MissingReferenceException)
        {
            Debug.Log("Add the Player gameObject to the Scene.");
        }
        
    } 
}

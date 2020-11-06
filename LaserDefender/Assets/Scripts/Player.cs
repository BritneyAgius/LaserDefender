using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.XR.WSA.Input;

public class Player : MonoBehaviour
{
    // Makes the variable editable from Unity editor
    [SerializeField] float movementSpeed = 10f;

    float xMin, xMax, yMin, yMax;

    // Start is called before the first frame update
    void Start()
    {  
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    // Sets up the boundaries according to the camera
    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        //xMin = 0 according to the camera view
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)).y;

    }

    // Moves the Player ship
    private void Move()
    {
        //var changes its variable type depending on what I save in it
        //deltaX will have the difference in the x-axis which the Player moves
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;

        //newXPos = current x-position + difference in x
        var newXPos = transform.position.x + deltaX;

        //clamp the ship between xMin and xMax
        newXPos = Mathf.Clamp(newXPos, xMin, xMax);

        //the above in the y-axis
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;
        var newYPos = transform.position.y + deltaY;

        //clamp the ship between yMin and yMax
        newYPos = Mathf.Clamp(newYPos, yMin, yMax);

        //move the Player ship to the newXPos
        this.transform.position = new Vector2(newXPos, newYPos);
    }
}
 
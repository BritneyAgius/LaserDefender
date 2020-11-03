using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.XR.WSA.Input;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {  
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    // Moves the Player ship
    private void Move()
    {
        //var changes its variable type depending on what I save in it
        //deltaX will have the difference in the x-axis which the Player moves
        var deltaX = Input.GetAxis("Horizontal");

        //newXPos = current x-position + difference in x
        var newXPos = transform.position.x + deltaX;

        //the above in the y-axis
        var deltaY = Input.GetAxis("Vertical");
        var newYPos = transform.position.y + deltaY;

        //move the Player ship to the newXPos
        this.transform.position = new Vector2(newXPos, newYPos);
    }
}
 
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
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 15f;
    [SerializeField] float laserFiringTime = 0.2f;

    Coroutine firingCoroutine;


    float xMin, xMax, yMin, yMax;

    float padding = 0.5f;

    bool coroutineStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        //StartCoroutine(PrintAndWait());
    }

    //update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private IEnumerator FireContinously()
    {
        while(true) //while coroutine is running
        {
            //create an instance of laserPrefab at the position of the Player ship
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;

            //add a velocity to the laser in the y-axis
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);

            //wait x seconds before repeating
            yield return new WaitForSeconds(laserFiringTime);
        }
    }

    /*coroutine example
    private IEnumerator PrintAndWait()
    {
        print("Message 1");
        //wait 10 seconds
        yield return new WaitForSeconds(10);
        print("Message 2 after 10 seconds");
    } */

    //sets up the boundaries according to the camera
    private void SetUpMoveBoundaries()
    {
        //get camera from unity
        Camera gameCamera = Camera.main;

        //xMin = 0 according to the camera view
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

    }

    private void Fire()
    {
        if (!coroutineStarted) //if coroutineStarted == false
        {
            //if fire button is pressed, Start coroutine to fire
            if (Input.GetButtonDown("Fire1"))
            {
                firingCoroutine = StartCoroutine(FireContinously());
                coroutineStarted = true;
            }
        }

        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
            coroutineStarted = false;
        }
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
 
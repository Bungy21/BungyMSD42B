using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float moveSpeed = 10f; // SerialiazeField makes the variable editable from Unity Editor

    float xMin, xMax, yMin, yMax;

    float padding = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void SetUpMoveBoundaries()
    {   //setup the boundaries of movement according to the camera
        Camera gameCamera = Camera.main;

        //xMin = 0;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        //xMax = 1;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        //yMin = 0;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        //yMax = 1;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

    }

    private void Move()
    {
        //var is a generic variable which changes its type according to value
        //deltaX is the difference the Player Ship moves in the x-axis
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;

        //newXPosition = current pos in x + difference moved in x-axis
        var newXPos = transform.position.x + deltaX;
        
        //clamp the value of newXPos between
        newXPos = Mathf.Clamp(newXPos, xMin, xMax);
 
        //the same as above on the y-axis
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = transform.position.y + deltaY;

        newYPos = Mathf.Clamp(newYPos, yMin, yMax);

        //move the Player Ship on the x-axis only (newXPos)
        transform.position = new Vector2(newXPos, newYPos);
    }
}

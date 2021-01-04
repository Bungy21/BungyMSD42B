using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // SerialiazeField makes the variable editable from Unity Editor
    [SerializeField] int health = 100;
    [SerializeField] float moveSpeed = 10f; 
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 15f;
    [SerializeField] float laserFiringTime = 0.2f;


    [SerializeField] AudioClip playerDeathSound;
    [SerializeField] [Range(0, 1)] float playerDeathSoundVolume = 0.3f;


    [SerializeField] AudioClip playerShootSound;
    [SerializeField] [Range(0, 1)] float playerShootSoundVolume = 0.1f;

    float xMin, xMax, yMin, yMax;

    float padding = 0.5f;

    bool coroutineStarted = false;

    Coroutine printCoroutine;

    public Coroutine FireCoroutine { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();

        //printCoroutine = StartCoroutine(PrintAndWait());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    //coroutine example
    //private IEnumerator PrintAndWait()
    //{
    //    print("Message 1");
    //    yield return new WaitForSeconds(10);
    //    print("Message 2 after 10 seconds");
    //}
    
    //fires lasers continuously every firitingTime seconds
    private IEnumerator FireContinuously()
    {
        while (true) //while coroutine is running
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            //give the laser a velocity in the y-axis
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            AudioSource.PlayClipAtPoint(playerShootSound, Camera.main.transform.position, playerShootSoundVolume);
            yield return new WaitForSeconds(laserFiringTime);
        }
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

    private void Fire()
    {
        //if fire ispressed spawn a laser at the Player ship position
        if(Input.GetButtonDown("Fire1"))
        {  
            //if coroutine has not started
            // to avoid starting more than 1 same coroutine
            if (!coroutineStarted) //if coroutineStarted == false
            {
                //start coroutine
                FireCoroutine = StartCoroutine(FireContinuously());
                //set coroutineStarted = true
                coroutineStarted = true;
            }

        }
        //when button is released
        if(Input.GetButtonUp("Fire1"))
        { 
            StopCoroutine(FireCoroutine);
            coroutineStarted = false;
        }

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

    // Reduce enemy health whenever enemy collides with a gameObject that has a DamageDealer component
    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        // Access DamageDealer from otherObject that hit the enemy and reduce health
        DamageDealer dmg = otherObject.gameObject.GetComponent<DamageDealer>();

        ProcessHit(dmg);
    }
    private void ProcessHit(DamageDealer dmg)
    {
        health -= dmg.GetDamage();
        //Destroy enemy laser when collided with player
        dmg.Hit();

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(playerDeathSound, Camera.main.transform.position, playerDeathSoundVolume);
    }
}

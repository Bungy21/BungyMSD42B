using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100;

    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;

    [SerializeField] GameObject enemyLaserPrefab;
    [SerializeField] float enemyLaserSpeed = 5f;

    [SerializeField] AudioClip enemyDeathSound;
    [SerializeField] [Range(0, 1)] float enemyDeathSoundVolume = 0.3f;


    [SerializeField] AudioClip enemyShootSound;
    [SerializeField] [Range(0, 1)] float enemyShootSoundVolume = 0.3f;

    [SerializeField] GameObject deathEffects;
    [SerializeField] float explotionDuration = 1f;

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
        //Destroy player laser when collided with enemy
        dmg.Hit();

        if (health <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(enemyDeathSound, Camera.main.transform.position, enemyDeathSoundVolume);
        //create an explosion particle
        GameObject explosion = Instantiate(deathEffects, transform.position, Quaternion.identity);
        Destroy(explosion, explotionDuration);
    }

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        // Reduce time every frame
        shotCounter -= Time.deltaTime;

        if (shotCounter <= 0f)
        {
            // Enemy shoots
            EnemyFire();
            // Reset shotCounter timer
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void EnemyFire()
    {
        GameObject laser = Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity) as GameObject;
        //give the laser a velocity in the y-axis
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyLaserSpeed);
        AudioSource.PlayClipAtPoint(enemyShootSound, Camera.main.transform.position, enemyShootSoundVolume);
    }
}

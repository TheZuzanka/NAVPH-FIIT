using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{

    public Transform setLeftBoundary;
    private float leftBoundaryX;
    public Transform setRightBoundary;
    private float rightBoundaryX;

    public GameObject player;
    public float hostileDistance = 10.0f;
    public float enemySpeed = 2.0f;

    public GameObject FX;
    public float throwForce = 20.0f;

    public float spawnInterval = 1.0f;
    private float timePassed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        leftBoundaryX = setLeftBoundary.position.x;
        rightBoundaryX = setRightBoundary.position.x;
    }

    private void Move()
    {
        float moveUnit = enemySpeed * Time.deltaTime;
        float moveX = Vector2.MoveTowards(transform.position,
                    player.transform.position, enemySpeed * Time.deltaTime).x;
        float moveY = transform.position.y;

        if (((moveX + moveUnit) > leftBoundaryX) && ((moveX + moveUnit) < rightBoundaryX))
        {
            transform.position = new Vector2(moveX, moveY);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);

        // Create new FX object if player is in hostileDistance and time set in spawnInterval
        // has passed since previous FX object spawn
        if (distanceFromPlayer <= hostileDistance)
        {
            if ((transform.position.x > leftBoundaryX) && (transform.position.x < rightBoundaryX))
            {
                Move();
            }

            if (timePassed >= spawnInterval)
            {
                // Set vector that will point FX object towards player
                Vector3 fxPosition = transform.position;
                Vector3 fromEnemyToPlayer = player.transform.position - fxPosition;
                fromEnemyToPlayer.Normalize();

                GameObject newFX = Instantiate(FX, fxPosition, Quaternion.identity);

                // fire FX object towards player
                newFX.GetComponent<Rigidbody2D>().velocity = fromEnemyToPlayer * throwForce;
            }
        }

        if (timePassed >= spawnInterval)
        {
            timePassed = 0.0f;
        }

        timePassed += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject player;
    public float hostileDistance = 10.0f;
    
    public GameObject FX;
    public float throwForce = 20.0f;

    public float spawnInterval = 1.0f;
    private float timePassed = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);
        // Create new FX object if player is in hostileDistance and time set in spawnInterval
        // has passed since previous FX object spawn
        if (distanceFromPlayer <= hostileDistance)
        {

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
}

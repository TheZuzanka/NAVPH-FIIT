using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX : MonoBehaviour
{
    private float timePassed = 0.0f;
    public float maxExistTime = 2.0f;

    public GameObject enemy;

   
    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;

        if (timePassed >= maxExistTime)
        {
            Destroy(gameObject);
        }
    }

    // If player is hit, decrease player's health and delete FX object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player playerScript = collision.gameObject.GetComponent<Player>();
            playerScript.RemoveHeart();

            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FXMark : MonoBehaviour
{
    private float timePassed = 0.0f;
    [SerializeField] float maxExistTime = 2.0f;

    // Update is called once per frame
    void Update()
    {
        // Destroy FX object if time set in maxExistTime has passed
        timePassed += Time.deltaTime;

        if (timePassed >= maxExistTime)
        {
            Destroy(gameObject);
        }
    }

    // If player is hit, decrease player's health and delete FX object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player playerScript = collision.gameObject.GetComponent<Player>();
            playerScript.RemoveHeart();
        
            Destroy(gameObject);
        }
    }
}

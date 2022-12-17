using System;
using UnityEngine;

public class FXMark : MonoBehaviour
{
    private float _timePassed;
    private float _maxExistTime;

    private void Start()
    {
        _maxExistTime = GameLogicValues.FxExistingTime;
    }

    void Update()
    {
        // Destroy FX object if time set in maxExistTime has passed
        _timePassed += Time.deltaTime;

        if (_timePassed >= _maxExistTime)
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

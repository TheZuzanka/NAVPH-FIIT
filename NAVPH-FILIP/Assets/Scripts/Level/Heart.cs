using System;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = (Player)FindObjectOfType(typeof(Player));
            player.AddHeart();
            Destroy(gameObject);
        }
    }
}
using System;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public Player player;

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            player.AddHeart();
            Destroy(gameObject);
        }
    }

    public void SetPlayer(Player spawnedPlayer)
    {
        player = spawnedPlayer;
    }
}

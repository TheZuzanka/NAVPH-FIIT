using UnityEngine;

public class Coffee : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.coffeeActive = true;
            player.coffeeDelegate(player.coffeeActive);
            Destroy(gameObject);
        }
    }
}

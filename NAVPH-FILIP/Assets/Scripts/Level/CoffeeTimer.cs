using UnityEngine;

public class CoffeeTimer : MonoBehaviour
{
    [SerializeField] private int timer;
    void OnEnable()
    {
        timer = (int) (500 * Settings.CoffeeTimeMultiplier);
    }
    
    void Update()
    {
        if (timer > 0)
        {
            timer--;
        }
        else
        {
            Player player = GetComponent<Player>();
            player.coffeeActive = false;
            player.coffeeDelegate(player.coffeeActive);
            enabled = false;
        }
    }
}

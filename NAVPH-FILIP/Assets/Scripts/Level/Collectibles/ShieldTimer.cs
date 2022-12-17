using UnityEngine;

public class ShieldTimer : MonoBehaviour
{
    [SerializeField] private int timer;
    void OnEnable()
    {
        timer = GameLogicValues.ShieldTimerValue;
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
            player.shieldActive = false;
            player.shieldDelegate(player.shieldActive);
            enabled = false;
        }
    }
}

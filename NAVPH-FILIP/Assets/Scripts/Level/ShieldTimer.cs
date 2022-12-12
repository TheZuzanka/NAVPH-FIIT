using UnityEngine;

public class ShieldTimer : MonoBehaviour
{
    [SerializeField] private int timer;
    void OnEnable()
    {
        timer = 500;
    }
    
    void Update()
    {
        if (timer > 0)
        {
            timer--;
        }
        else
        {
            enabled = false;
        }
    }
}

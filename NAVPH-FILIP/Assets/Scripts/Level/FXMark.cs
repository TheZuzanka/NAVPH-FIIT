using UnityEngine;

public class FXMark : MonoBehaviour
{
    private float _timePassed;
    [SerializeField] float maxExistTime = 2.0f;
    
    void Update()
    {
        // Destroy FX object if time set in maxExistTime has passed
        _timePassed += Time.deltaTime;

        if (_timePassed >= maxExistTime)
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

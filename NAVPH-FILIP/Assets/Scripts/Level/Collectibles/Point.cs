using UnityEngine;

public class Point : MonoBehaviour
{
    private PointCounter _pointCounter;
    [SerializeField] int pointsToAdd = 1;

    void Start()
    {
        _pointCounter = GameObject.FindWithTag("PointCounter").GetComponent<PointCounter>();
    }

    // Increase player's score if he/she collects a points object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _pointCounter.AddPoints(pointsToAdd);
            Destroy(gameObject);
        }
    }
}
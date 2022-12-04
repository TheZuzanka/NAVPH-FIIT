using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    private PointCounter _pointCounter;
    [SerializeField] int pointsToAdd = 1;

    void Start()
    {
        _pointCounter = GameObject.FindWithTag("PointCounter").GetComponent<PointCounter>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _pointCounter.AddPoints(pointsToAdd);
            Destroy(gameObject);
        }
    }
}
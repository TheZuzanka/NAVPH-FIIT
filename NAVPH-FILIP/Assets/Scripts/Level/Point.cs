using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public GameObject pointCounter;
    public int pointsToAdd = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pointCounter.GetComponent<PointCounter>().AddPoints(pointsToAdd);
            Destroy(gameObject);
        }
    }
}

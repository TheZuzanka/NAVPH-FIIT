using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NonFXMark : MonoBehaviour
{
    private Boss boss;

    private float timePassed = 0.0f;
    [SerializeField] float maxExistTime = 5.0f;

    private string mark;

    void Start()
    {
        boss = GameObject.FindWithTag("Boss").GetComponent<Boss>();
    }

    void Update()
    {
        // If time set in maxExistTime has passed, destroy the GameObject
        timePassed += Time.deltaTime;

        if (timePassed >= maxExistTime)
        {
            Destroy(gameObject);
        }
    }

    public void SetMark(string mark)
    {
        this.mark = mark;
    }

    // If player catches the mark
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            boss.AskAboutMark();
            Destroy(gameObject);
        }
    }
}

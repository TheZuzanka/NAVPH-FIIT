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
        boss = FindObjectOfType<Boss>();
    }


    // Update is called once per frame
    void Update()
    {
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

    // If player is hit, decrease player's health and delete FX object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(mark.Equals("A"))
            {
                Destroy(boss.gameObject);
            }

            else
            {
                boss.MoveToNextMark();
            }
            

            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mark : MonoBehaviour
{
    private float timePassed = 0.0f;
    public float maxExistTime = 2.0f;

    public TextMeshProUGUI markText;
    private string mark;

    void Start()
    {
        
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
        markText.SetText(mark);
    }

    // If player is hit, decrease player's health and delete FX object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (mark)
            { 
                case "FX":
                    {
                        Player playerScript = collision.gameObject.GetComponent<Player>();
                        playerScript.RemoveHeart();

                        Destroy(gameObject);
                        break;
                    }
            }
        }
    }
}

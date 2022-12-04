using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boss : MonoBehaviour
{
    private Player _player;
    [SerializeField] float hostileDistance = 10.0f;

    private GameObject _markDialog;

    [SerializeField]  List<GameObject> MarksList;

    [SerializeField] float fxThrowForce = 10.0f;
    [SerializeField] float otherMarkThrowForce = 5.0f;

    [SerializeField] float fxSpawnInterval = 2.0f;
    [SerializeField] float otherMarkSpawnInterval = 5.0f;
    private float fxTimePassed = 0.0f;
    private float otherMarkTimePassed = 0.0f;


    private string[] marks = new string[] { "FX", "E", "D", "C", "B", "A" };
    private int currentNonFXMarkIndex = 1;

    void Start()
    {
        _markDialog = GameObject.FindWithTag("MarkDialog");
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    GameObject GetMarkObject(string markStr)
    {
        return MarksList[Array.IndexOf(marks, markStr)];
    }

    private void ThrowMark(string markStr, float throwForce)
    {
        // Set vector that will point FX object towards player
        Vector3 fxPosition = transform.position;
        Vector3 fromEnemyToPlayer = _player.transform.position - fxPosition;
        fromEnemyToPlayer.Normalize();

        // Create Mark object
        GameObject newMark = Instantiate(GetMarkObject(markStr), fxPosition, Quaternion.identity);

        if (!markStr.Equals("FX"))
            newMark.GetComponent<NonFXMark>().SetMark(markStr);

        // fire Mark object towards player
        newMark.GetComponent<Rigidbody2D>().velocity = fromEnemyToPlayer * throwForce;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector3.Distance(_player.transform.position, transform.position);
        // Create new FX object if player is in hostileDistance and time set in spawnInterval
        // has passed since previous FX object spawn
        if (distanceFromPlayer <= hostileDistance)
        {
            if (fxTimePassed >= fxSpawnInterval)
            {
                ThrowMark("FX",fxThrowForce);
                fxTimePassed = 0.0f;
            }

            if (otherMarkTimePassed >= otherMarkSpawnInterval)
            {
                ThrowMark(marks[currentNonFXMarkIndex], otherMarkThrowForce);
                otherMarkTimePassed = 0.0f;
            }

            fxTimePassed += Time.deltaTime;
            otherMarkTimePassed += Time.deltaTime;
        }
    }


    private void DisplayDialog(string mark)
    {
        _markDialog.GetComponent<Canvas>().enabled = true;
        _markDialog.GetComponent<MarkDialog>().DisplayMark(mark);
        Time.timeScale = 0.0f;
    }

    public void AskAboutMark()
    {
        if (currentNonFXMarkIndex < marks.Length - 1)
        {
            DisplayDialog(marks[currentNonFXMarkIndex]);
            currentNonFXMarkIndex++;
        }

        else 
        {
            Destroy(gameObject);
        }
    }

}

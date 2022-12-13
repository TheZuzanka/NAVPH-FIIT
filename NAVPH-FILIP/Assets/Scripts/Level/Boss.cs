using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boss : MonoBehaviour
{
    private Player _player;
    // If player crosses this boundary, Boss starts throwing the marks
    [SerializeField] Transform throwingBoundary;

    [SerializeField] MarkDialog markDialog;

    [SerializeField]  List<GameObject> MarksList;

    [SerializeField] float fxThrowForce = 10.0f;
    [SerializeField] float otherMarkThrowForce = 5.0f;

    [SerializeField] float fxSpawnInterval = 2.0f;
    [SerializeField] float otherMarkSpawnInterval = 5.0f;
    private float fxTimePassed = 0.0f;
    private float otherMarkTimePassed = 0.0f;


    private string[] marks = new string[] { "FX", "E", "D", "C", "B", "A" };
    private int currentNonFXMarkIndex = 1;
    private string finalMark = "FX";

    void Start()
    {

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
        // Set vector that will point mark towards player
        Vector3 markPosition = transform.position;
        Vector3 fromEnemyToPlayer = _player.transform.position - markPosition;
        fromEnemyToPlayer.Normalize();

        // Create mark
        GameObject newMark = Instantiate(GetMarkObject(markStr), markPosition, Quaternion.identity);

        // Poznamka: newMark je preto GameObject a nie rovno NonFXMark (script), lebo newMark moze mat aj FXMark script komponent 
        if (!markStr.Equals("FX"))
            newMark.GetComponent<NonFXMark>().SetMark(markStr);

        // Fire mark towards player
        newMark.GetComponent<Rigidbody2D>().velocity = fromEnemyToPlayer * throwForce;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector3.Distance(_player.transform.position, transform.position);
        // Create new FX/Non FX mark if player's x coor. is >= than throwingBoundary's x coor and time set in fxSpawnInterval/otherMarkSpawnInterval
        // has passed since previous FX/Non FX mark spawn
        if (_player.transform.position.x >= throwingBoundary.position.x)
        {
            // Throw FX
            if (fxTimePassed >= fxSpawnInterval)
            {
                ThrowMark("FX",fxThrowForce);
                fxTimePassed = 0.0f;
            }

            //Throw Non FX mark
            if (otherMarkTimePassed >= otherMarkSpawnInterval)
            {
                ThrowMark(marks[currentNonFXMarkIndex], otherMarkThrowForce);
                otherMarkTimePassed = 0.0f;
            }

            fxTimePassed += Time.deltaTime;
            otherMarkTimePassed += Time.deltaTime;
        }
    }

    //Open Dialog asking whether player is satisfied with obtianed mark
    // or wants to continue playing for better mark
    private void DisplayDialog(string mark)
    {
        //_markDialog.GetComponent<Canvas>().enabled = true;
        MarkDialog markDialogInstance = Instantiate(markDialog);
        markDialogInstance.DisplayMark(mark);
        //_markDialog.GetComponent<MarkDialog>().DisplayMark(mark);
        Time.timeScale = 0.0f;
    }

    public void AskAboutMark()
    {
        // Open dialog
        if (currentNonFXMarkIndex < marks.Length - 1)
        {
            DisplayDialog(marks[currentNonFXMarkIndex]);
            currentNonFXMarkIndex++;
        }

        // Destroy Boss object if player catches A
        else 
        {
            currentNonFXMarkIndex++;
            SetFinalMark();
            
            Destroy(gameObject);
        }
    }

    // Set final mark which will be displayed on panel with final score
    public void SetFinalMark()
    {
        finalMark = marks[currentNonFXMarkIndex - 1];
    }

    // Get final mark which will be displayed on panel with final score
    public string GetFinalMark()
    {
        return finalMark;
    }
}

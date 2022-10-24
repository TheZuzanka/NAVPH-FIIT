using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Player player;
    public GameObject levelContainer;

    void Start()
    {
        Debug.Log("x = " + levelContainer.transform.position.x);
        player = Instantiate(player, new Vector2(0, 0), 
            Quaternion.identity, levelContainer.transform);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 speed;
    private Rigidbody2D _rigidbody2D;
    private int _hearts;
    public LevelManager levelManager;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _hearts = 1;
    }

    private void FixedUpdate()
    {
        //nevola sa rovnako ako update
        if (Input.GetKey(KeyCode.D))
        {
            _rigidbody2D.velocity = new Vector2(speed.x, _rigidbody2D.velocity.y);
        }
        
        else if (Input.GetKey(KeyCode.A))
        {
            _rigidbody2D.velocity = new Vector2(-speed.x, _rigidbody2D.velocity.y);
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, speed.y);
        }
        
        else if (Input.GetKey(KeyCode.S))
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, -speed.y);
        }
    }

    public int GetHearts()
    {
        return _hearts;
    }

    public void AddHeart()
    {
        levelManager.DrawHeart();
        _hearts += 1;
    }
}
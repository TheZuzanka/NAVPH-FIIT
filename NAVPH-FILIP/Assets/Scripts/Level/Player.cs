using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private Vector2 speed;
    [SerializeField] private int currentHearts;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private float touchTheGroundThreshold = 0.35f;
    [SerializeField] private List<GameObject> heartsObjects;
    [SerializeField] private int coffeeTimer;

    public int score;

    private void SetPlayerAsReference()
    {
        levelManager.boss.SetPlayer(this);

        foreach (var enemy in levelManager.enemies)
        {
            enemy.SetPlayer(this);
        }
    }

    public void SetPlayersAttributesFromScene(LevelManager levelManager,
        List<GameObject> heartsObjects)
    {
        // public = levelManager uses this method when player is spawned to set references

        this.levelManager = levelManager;
        this.heartsObjects = heartsObjects;
    }


    private void Start()
    {
        // max hearts depends on whether the player has the sweetheart trait selected
        currentHearts = Settings.Settings.MaxHearts;
        DisplayHearts();

        score = 0;
        coffeeTimer = 0;
        _rigidbody2D = GetComponent<Rigidbody2D>();

        SetSpeed(3, 5);
        SetPlayerAsReference();
    }

    private void CheckIfNotFallen()
    {
        if (transform.position.y < levelManager.toKillY)
        {
            // remove all hearts when player falls from platform
            for (int i = 0; i < currentHearts; i++)
            {
                RemoveHeart();
            }
        }
    }

    private void Move()
    {
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
            // player can jump only when standing on the platform

            RaycastHit2D[] allHits = Physics2D.RaycastAll(transform.position,
                Vector2.down, touchTheGroundThreshold);

            if (allHits.Length > 1)
            {
                // we hit something else than player's own collider
                RaycastHit2D firstHitNotPlayer = allHits[1];

                // if hit is the ground, player can jump
                if (firstHitNotPlayer.collider.CompareTag("ground"))
                {
                    _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, speed.y);
                }
            }
        }
    }

    public void SetSpeed(float x, float y)
    {
        // speed depends on whether the player has the fitness trait selected
        
        speed = new(x * Settings.Settings.SpeedMultiplier,
            y * Settings.Settings.SpeedMultiplier);
    }

    private void FixedUpdate()
    {
        CheckIfNotFallen();
        
        CheckIfCoffeeActive();

        Move();
    }
    
    private void CheckIfCoffeeActive()
    {
        if (coffeeTimer == 0)
        {
            // time for coffee effect is over, default speed is restored
            SetSpeed(3, 5);
        }
        if (coffeeTimer > 0)
        {
            coffeeTimer--;
        }
    }

    private void DisplayHearts()
    {
        for (int i = 0; i < heartsObjects.Count; i++)
        {
            if (i < currentHearts)
            {
                heartsObjects[i].SetActive(true);
            }
            else
            {
                heartsObjects[i].SetActive(false);
            }
        }
    }

    public void AddHeart()
    {
        // public = when player collects heart this method is called

        if (currentHearts < 2)
        {
            currentHearts += 1;
            DisplayHearts();
        }
    }

    public void RemoveHeart()
    {
        // public = when player collects FX this method is called

        currentHearts -= 1;
        Debug.Log($"Heart Removed, remaining = {currentHearts}");
        DisplayHearts();

        if (currentHearts == 0)
        {
            levelManager.ReturnToMainMenu();
        }
    }

    public void SetCoffeeTimer()
    {
        coffeeTimer = (int) (500 * Settings.Settings.CoffeeTimeMultiplier);
    }
}
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private Vector2 speed;
    [SerializeField] private int currentHearts;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private float touchTheGroundThreshold = 0.35f;
    [SerializeField] private int coffeeTimer;
    [SerializeField] private Sprite[] images;
    [SerializeField] private SpriteRenderer currentImage;

    public int score;

    // this is a publisher for health system
    public delegate void HeartDelegate(int heartCount);

    public HeartDelegate heartDelegate;
    
    public float shieldTimer;
    public bool shieldActive;
    public delegate void ShieldDelegate(bool shieldActive);
    public ShieldDelegate shieldDelegate;

    private void SetPlayerAsReference()
    {
        levelManager.boss.SetPlayer(this);

        foreach (var enemy in levelManager.enemies)
        {
            enemy.SetPlayer(this);
        }
    }

    public void SetLevelManager(LevelManager levelManager)
    {
        // public = levelManager uses this method when player is spawned to set references

        this.levelManager = levelManager;
    }


    private void Start()
    {
        currentImage.sprite = images[Settings.SelectedPerson];
        
        // max hearts depends on whether the player has the sweetheart trait selected
        currentHearts = Settings.MaxHearts;
        heartDelegate(currentHearts);

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
            currentHearts = 0;
            heartDelegate(currentHearts);
        }
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.D))
        {
            _rigidbody2D.velocity = new Vector2(speed.x, _rigidbody2D.velocity.y);
            currentImage.flipX = false;
        }

        else if (Input.GetKey(KeyCode.A))
        {
            _rigidbody2D.velocity = new Vector2(-speed.x, _rigidbody2D.velocity.y);
            currentImage.flipX = true;
        }

        if (Input.GetKey(KeyCode.W))
        {
            // player can jump only when standing on the platform
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position,
                Vector2.down, touchTheGroundThreshold);

            // if hit is the ground, player can jump

            if (hit.collider != null && hit.collider.CompareTag("ground"))
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, speed.y);
            }
        }
    }

    public void SetSpeed(float x, float y)
    {
        // speed depends on whether the player has the fitness trait selected
        
        speed = new(3 * Settings.SpeedMultiplier,
            5 * Settings.SpeedMultiplier);
    }

    private void CheckIfShieldActive()
    {
        if (shieldTimer > 0)
        {
            shieldTimer--;
        }
        else
        {
            shieldActive = false;
            shieldDelegate(shieldActive);
        }
    }

    private void FixedUpdate()
    {
        CheckIfNotFallen();
        
        CheckIfCoffeeActive();

        CheckIfShieldActive();

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

    public void AddHeart()
    {
        // public = when player collects heart this method is called

        if (currentHearts < Settings.MaxHearts)
        {
            currentHearts += 1;
            heartDelegate(currentHearts);
        }
    }

    public void RemoveHeart()
    {
        // public = when player collects FX this method is called

        if (!shieldActive)
        {
            currentHearts -= 1;
            Debug.Log($"Heart Removed, remaining = {currentHearts}");
            heartDelegate(currentHearts);
        }
    }

    public void SetCoffeeTimer()
    {
        coffeeTimer = (int) (500 * Settings.CoffeeTimeMultiplier);
    }

    public void UpdateState()
    {
        // force check for number of hearts displayed
        // this method is used when when player spawned in scene, as all heart icons are enabled by
        // default and player can have 2 or 3 hearts based on selected traits
        
        heartDelegate(currentHearts);
    }
}
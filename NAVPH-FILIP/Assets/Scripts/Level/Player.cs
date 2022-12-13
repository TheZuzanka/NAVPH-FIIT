using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private Vector2 speed;
    [SerializeField] private int currentHearts;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private float touchTheGroundThreshold = 0.35f;
    [SerializeField] private Sprite[] images;
    [SerializeField] private SpriteRenderer currentImage;

    public int score;
    public bool shieldActive;
    public bool coffeeActive;
    public SpriteRenderer shieldImage;

    // this is a publisher for health system
    public delegate void HeartDelegate(int heartCount);
    public HeartDelegate heartDelegate;
    public delegate void ShieldDelegate(bool shieldActive);
    public ShieldDelegate shieldDelegate;
    public delegate void CoffeeDelegate(bool coffeeActive);
    public CoffeeDelegate coffeeDelegate;

    private void SetPlayerWidth()
    {
        // if Filip is selected, he needs wider collider because of Dante
        
        if (Settings.SelectedPerson == 1)
        {
            BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
            boxCollider.size = new Vector2(2,3);

            shieldImage.transform.localScale = new Vector3(5f, 1.6f, 1);
        }
    }

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
        SetPlayerWidth();
        
        // max hearts depends on whether the player has the sweetheart trait selected
        currentHearts = Settings.MaxHearts;
        heartDelegate(currentHearts);

        score = 0;
        _rigidbody2D = GetComponent<Rigidbody2D>();

        SetSpeed(3.5f, 6f);
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
        
        speed = new(x * Settings.SpeedMultiplier,
            y * Settings.SpeedMultiplier);
    }

    public Vector2 GetSpeed()
    {
        return speed;
    }

    private void FixedUpdate()
    {
        CheckIfNotFallen();

        Move();
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

    public void UpdateState()
    {
        // force check for number of hearts displayed
        // this method is used when when player spawned in scene, as all heart icons are enabled by
        // default and player can have 2 or 3 hearts based on selected traits
        
        heartDelegate(currentHearts);
    }
}
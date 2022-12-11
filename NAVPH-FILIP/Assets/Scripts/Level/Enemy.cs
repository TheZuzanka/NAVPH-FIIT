using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Boundaries beyond which enemy cannot move
    [SerializeField] Transform leftBoundary;
    private float leftBoundaryX;
    [SerializeField] Transform rightBoundary;
    private float rightBoundaryX;

    private Player player;
    [SerializeField] float hostileDistance = 10.0f;
    [SerializeField] float enemySpeed = 2.0f;

    [SerializeField] GameObject FX;
    [SerializeField] float throwForce = 20.0f;

    [SerializeField] float spawnInterval;
    private float timePassed = 0.0f;

    [SerializeField] private float isBLockedTimer;
    public bool isBlocked;
    public delegate void ShieldDelegate(bool _isBlocked);
    public ShieldDelegate shieldDelegate;
    
    void Start()
    {
        leftBoundaryX = leftBoundary.position.x;
        rightBoundaryX = rightBoundary.position.x;
        spawnInterval = 3.0f * Settings.FxTimeIntervalMultiplier;

        isBlocked = false;
        shieldDelegate(isBlocked);
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    // Move towards player
    private void Move()
    {
        float currentPosistionX = transform.position.x;



        float moveX = Vector2.MoveTowards(transform.position,
                player.transform.position, enemySpeed*Time.deltaTime).x;

        // Inside boundaries
        if (moveX > leftBoundaryX && moveX < rightBoundaryX)
        {
            transform.position = new Vector2(moveX, transform.position.y);
        }

        // Move right from left boudary
        else if (currentPosistionX <= leftBoundaryX && (moveX > currentPosistionX))
        {
            transform.position = new Vector2(moveX, transform.position.y);
        }

        // Move left from right boudary
        else if (currentPosistionX >= rightBoundaryX && (moveX < currentPosistionX))
        {
            transform.position = new Vector2(moveX, transform.position.y);
        }
    }

    private void ThrowFX()
    {
        // Set vector that will point FX object towards player
        Vector3 fxPosition = transform.position;
        Vector3 fromEnemyToPlayer = player.transform.position - fxPosition;
        fromEnemyToPlayer.Normalize();

        GameObject newFX = Instantiate(FX, fxPosition, Quaternion.identity);

        // fire FX object towards player
        newFX.GetComponent<Rigidbody2D>().velocity = fromEnemyToPlayer * throwForce;
    }

    void Update()
    {
        float distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);

        // Create new FX object if player is in hostileDistance and time set in spawnInterval
        // has passed since previous FX object spawn
        if (distanceFromPlayer <= hostileDistance)
        {
            Move();
  
            if (timePassed >= spawnInterval && !isBlocked)
            {
                ThrowFX();
                timePassed = 0.0f;
            }

            timePassed += Time.deltaTime;

            if (isBLockedTimer > 0)
            {
                isBLockedTimer--;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    public void SetBlockedTimer(float time)
    {
        isBLockedTimer = time;
    }
}

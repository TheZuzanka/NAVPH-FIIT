using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _leftBoundaryX;
    private float _rightBoundaryX;
    private Player _player;
    private float _timePassed;
    
    // Boundaries beyond which enemy cannot move
    [SerializeField] Transform leftBoundary;
    [SerializeField] Transform rightBoundary;
    [SerializeField] float hostileDistance = 10.0f;
    [SerializeField] float speed = 2.0f;
    [SerializeField] GameObject FX;
    [SerializeField] float throwForce = 20.0f;
    [SerializeField] float spawnInterval;
    

    void Start()
    {
        _leftBoundaryX = leftBoundary.position.x;
        _rightBoundaryX = rightBoundary.position.x;
        spawnInterval *= Settings.FxTimeIntervalMultiplier;
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }
    
    private void MoveTowardsPlayer()
    {
        float currentPositionX = transform.position.x;
        float moveX = Vector2.MoveTowards(transform.position,
                _player.transform.position, speed*Time.deltaTime).x;

        // Inside boundaries
        if (moveX > _leftBoundaryX && moveX < _rightBoundaryX)
        {
            transform.position = new Vector2(moveX, transform.position.y);
        }

        // Move right from left boundary
        else if (currentPositionX <= _leftBoundaryX && moveX > currentPositionX)
        {
            transform.position = new Vector2(moveX, transform.position.y);
        }

        // Move left from right boundary
        else if (currentPositionX >= _rightBoundaryX && moveX < currentPositionX)
        {
            transform.position = new Vector2(moveX, transform.position.y);
        }
    }

    private void ThrowFX()
    {
        // Set vector that will point FX object towards player
        Vector3 fxPosition = transform.position;
        Vector3 fromEnemyToPlayer = _player.transform.position - fxPosition;
        fromEnemyToPlayer.Normalize();

        GameObject newFX = Instantiate(FX, fxPosition, Quaternion.identity);

        // fire FX object towards player
        newFX.GetComponent<Rigidbody2D>().velocity = fromEnemyToPlayer * throwForce;
    }

    void Update()
    {
        float distanceFromPlayer = Vector3.Distance(_player.transform.position, transform.position);

        // Create new FX object if player is in hostileDistance and time set in spawnInterval
        // has passed since previous FX object spawn
        if (distanceFromPlayer <= hostileDistance)
        {
            MoveTowardsPlayer();
  
            if (_timePassed >= spawnInterval)
            {
                ThrowFX();
                _timePassed = 0.0f;
            }

            _timePassed += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}

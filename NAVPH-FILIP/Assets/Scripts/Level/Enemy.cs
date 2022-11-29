using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] Transform setLeftBoundary;
    private float leftBoundaryX;
    [SerializeField] Transform setRightBoundary;
    private float rightBoundaryX;

    private Player player;
    [SerializeField] float hostileDistance = 10.0f;
    [SerializeField] float enemySpeed = 2.0f;

    [SerializeField] GameObject FX;
    [SerializeField] float throwForce = 20.0f;

    [SerializeField] float spawnInterval;
    private float timePassed = 0.0f;
    
    void Start()
    {
        leftBoundaryX = setLeftBoundary.position.x;
        rightBoundaryX = setRightBoundary.position.x;
        spawnInterval = 3.0f * Settings.Settings.FxTimeIntervalMultiplier;
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

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
        newFX.GetComponent<Mark>().SetMark("FX");

        // fire FX object towards player
        newFX.GetComponent<Rigidbody2D>().velocity = fromEnemyToPlayer * throwForce;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);

        // Create new FX object if player is in hostileDistance and time set in spawnInterval
        // has passed since previous FX object spawn
        if (distanceFromPlayer <= hostileDistance)
        {
            Move();
  
            if (timePassed >= spawnInterval)
            {
                ThrowFX();
                timePassed = 0.0f;
            }

            timePassed += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}

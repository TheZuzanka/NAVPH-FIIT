using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Vector2 speed;
    
    private Rigidbody2D _rigidbody2D;
    
    [SerializeField]
    private int hearts;
    
    [SerializeField]
    private LevelManager levelManager;

    public int score;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        hearts = 2;
        score = 0;
    }

    private void CheckIfNotFallen()
    {
        if (transform.position.y < levelManager.toKillY)
        {
            //levelManager.ReturnToMainMenu();

            for (int i = 0; i < hearts; i++)
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
            RaycastHit2D hit = Physics2D.Raycast(
                new Vector2(transform.position.x, (float) (transform.position.y - 0.5)),
                Vector2.down, (float) 0.1);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("ground")){
                    _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, speed.y);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        //nevola sa rovnako ako update

        CheckIfNotFallen();

        Move();
    }

    public int GetHearts()
    {
        return hearts;
    }

    public void AddHeart()
    {
        if (hearts < 2)
        {
            levelManager.DrawHeart();
            hearts += 1;
        }
    }

    public void RemoveHeart()
    {
        Debug.Log($"Heart Removed, remaining = {hearts - 1}");
        hearts -= 1;
        levelManager.DestroyHeart();
    }
}
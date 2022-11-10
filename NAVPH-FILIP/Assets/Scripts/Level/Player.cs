using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 speed;
    private Rigidbody2D _rigidbody2D;
    private int _hearts;
    public LevelManager levelManager;
    public int score;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _hearts = 2;
        score = 0;
    }

    private void FixedUpdate()
    {
        if (transform.position.y < levelManager.toKillY)
        {
            //levelManager.ReturnToMainMenu();

            for (int i = 0; i < _hearts; i++) {
                RemoveHeart();
            }
        }

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
        if (_hearts < 2)
        {
            levelManager.DrawHeart();
            _hearts += 1;
        }
    }

    public void RemoveHeart()
    {
        Debug.Log($"Heart Removed, remaining = {_hearts - 1}");
        _hearts -= 1;
        levelManager.DestroyHeart();
    }

  
}
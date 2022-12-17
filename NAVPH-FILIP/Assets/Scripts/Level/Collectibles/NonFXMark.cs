using UnityEngine;

public class NonFXMark : MonoBehaviour
{
    private Boss _boss;
    private float _timePassed;
    private string _mark;
    private float _maxExistTime;

    void Start()
    {
        _boss = GameObject.FindWithTag("Boss").GetComponent<Boss>();
        _maxExistTime = GameLogicValues.MarkExistingTime;
    }

    void Update()
    {
        // If time set in maxExistTime has passed, destroy the GameObject
        _timePassed += Time.deltaTime;

        if (_timePassed >= _maxExistTime)
        {
            Destroy(gameObject);
        }
    }

    public void SetMark(string mark)
    {
        _mark = mark;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _boss.AskAboutMark();
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private bool _isFinished;
    public delegate void IsFinishedDelegate(bool isFinished);
    public IsFinishedDelegate isFinishedDelegate;
    
    [SerializeField] private AchievementsManager achievementsManager;

    private void Start()
    {
        _isFinished = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isFinished = true;
            isFinishedDelegate(_isFinished);

            achievementsManager.achievementsDelegate(true);
            
            Destroy(gameObject);
        }
    }
}
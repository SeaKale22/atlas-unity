using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
    public float targetMoveSpeed = 0.05f;// Speed in units per second
    
    private Vector3 _startPosition;
    private Vector3 _moveTo;
    private bool _movingToTarget = true;

    void Awake()
    {
        _startPosition = transform.position;
        _moveTo = new Vector3(Random.Range(-2f, 2f), 0f, Random.Range(-2f, 2f));
    }

    void Update()
    {
        Vector3 targetPosition = _movingToTarget ? _moveTo : _startPosition;
        float step = targetMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        // Check if the target position has been reached
        if (transform.position == targetPosition)
        {
            _movingToTarget = !_movingToTarget; // Switch direction
        }
    }
}

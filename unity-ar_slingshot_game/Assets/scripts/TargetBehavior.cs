using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetBehavior : MonoBehaviour
{
    public float targetMoveSpeed = 1f;// Speed in units per second
    public GameManager gameManager;
    
    private Vector3 _startPosition;
    private Vector3 _moveTo;
    private bool _movingToTarget = true;

    void Awake()
    {
        _startPosition = transform.position;
        _moveTo = new Vector3(Random.Range(-2f, 2f), _startPosition.y, Random.Range(-2f, 2f));
    }

    void LateUpdate()
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ammo"))
        {
            gameManager.Score();
            Destroy(this.gameObject);
        }
    }
}

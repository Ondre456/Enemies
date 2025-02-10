using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Repainter))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 15f;

    private Rigidbody _rigidbody;
    private Goal _goal;

    public event Action<Enemy> Deactivated;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_goal != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _goal.transform.position, _moveSpeed * Time.deltaTime);
            transform.LookAt(_goal.transform);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Goal goal))
        {
            if (goal == _goal)
                Deactivate();
        }
    }

    public void AcceptGoal(Goal goal)
    {
        _goal = goal;
    }

    private void Deactivate()
    {
        Deactivated?.Invoke(this);
    }
}

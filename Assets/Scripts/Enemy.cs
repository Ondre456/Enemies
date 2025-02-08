using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Repainter))]
[RequireComponent(typeof(DestructionTimer))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;

    private Vector3 _moveDirection;
    private Rigidbody _body;
    private DestructionTimer _destructionTimer;

    public event Action<Enemy> Deactivated;

    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
        _destructionTimer = GetComponent<DestructionTimer>();
        _destructionTimer.ActivateDestruction();
    }

    private void OnEnable()
    {
        _destructionTimer.TimeUntilDestructionExpired += Deactivate;
    }

    private void OnDisable()
    {
        _destructionTimer.TimeUntilDestructionExpired -= Deactivate;
    }

    private void FixedUpdate()
    {
        if (_moveDirection != Vector3.zero)
        {
            _body.MovePosition(_body.position + _moveDirection * _moveSpeed * Time.fixedDeltaTime);
        }
    }

    public void AcceptDirection(Vector3 direction)
    {
        _moveDirection = direction.normalized;
    }


    private void Deactivate()
    {
        Deactivated?.Invoke(this);
    }
}

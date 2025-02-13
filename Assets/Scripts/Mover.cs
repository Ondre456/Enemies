using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathBuilder))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private int _numberOfWaypoints = 10;

    private PathBuilder _pathBuilder;
    private List<Vector3> _pathPoints;
    private Vector3 _nextPosition;
    private int _currentPointIndex = 0;

    private void Awake()
    {
        _pathBuilder = GetComponent<PathBuilder>();
        _pathPoints = _pathBuilder.BuildPath(_numberOfWaypoints);
        _nextPosition = _pathPoints[0];
    }

    private void FixedUpdate()
    {
        const float WaypointAchievementCriterion = 0.2f;

        if (Mathf.Abs(transform.position.x - _nextPosition.x) < WaypointAchievementCriterion && Mathf.Abs(transform.position.z - _nextPosition.z) < WaypointAchievementCriterion)
            _nextPosition = _pathPoints[++_currentPointIndex % _pathPoints.Count];
        
        transform.position = Vector3.MoveTowards(transform.position, _nextPosition, _moveSpeed * Time.deltaTime);
    }
}

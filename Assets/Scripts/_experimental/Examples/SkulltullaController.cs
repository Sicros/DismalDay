using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkulltullaController : MonoBehaviour
{
    [SerializeField] private float m_speed = 4f;

    [SerializeField] private float m_distanceThreshold;
    private List<Transform> m_waypoints;

    private int m_currentWaypointIndex;

    public void ReceiveWaypoints(Transform[] p_waypoints)
    {
        m_waypoints = p_waypoints.ToList();
    }

    public void AddWaypoint(Transform m_newWaypoint)
    {
        if (!m_waypoints.Contains(m_newWaypoint))
            m_waypoints.Add(m_newWaypoint);
    }

    public void TryRemoveWaypoint(Transform m_waypointToRemove)
    {
        if (m_waypoints.Contains(m_waypointToRemove))
            m_waypoints.Remove(m_waypointToRemove);
    }

    public void Init()
    {
        m_currentWaypointIndex = Random.Range(0, m_waypoints.Count);
    }


    private void Update()
    {
        Patrol();
    }

    private void Move(Vector3 p_direction)
    {
        transform.position += p_direction * (m_speed * Time.deltaTime);
    }

    private void Patrol()
    {
        var l_currentWaypoint = m_waypoints[m_currentWaypointIndex];
        var l_currDifference = (l_currentWaypoint.position - transform.position);
        var l_direction = l_currDifference.normalized;
        Move(l_direction);
        var l_currDistance = l_currDifference.magnitude;

        if (l_currDistance <= m_distanceThreshold)
        {
            NextWaypoint();
        }
    }

    private void NextWaypoint()
    {
        m_currentWaypointIndex++;
        if (m_currentWaypointIndex > m_waypoints.Count - 1)
        {
            m_currentWaypointIndex = 0;
        }
    }
}
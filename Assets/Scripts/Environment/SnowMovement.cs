using UnityEngine;

public class SnowMovement : MonoBehaviour
{
    [SerializeField] private Transform followObjectPosition;

    [SerializeField] float addYAxis;

    private Vector3 _addPosition;

    private void Awake()
    {
        _addPosition = Vector3.zero;
        _addPosition.y += addYAxis;
    }

    private void Update()
    {
        FollowPosition();
    }

    private void FollowPosition()
    {
        transform.position = followObjectPosition.position + _addPosition;
    }
}

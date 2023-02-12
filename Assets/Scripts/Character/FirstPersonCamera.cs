using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform character;
    [SerializeField] private Transform pointOfView;
    [SerializeField] private float minimumVertical;
    [SerializeField] private float maximumVertical;
    [SerializeField] private float sensitivityVertical;
    [SerializeField] private float sensitivityHorizontal;
    private float _rotationX;
    private Quaternion _characterInitialRotation;

    private void OnEnable()
    {
        _rotationX = 0f;
        _characterInitialRotation = character.transform.rotation;
    }

    private void Update()
    {
        transform.position = pointOfView.transform.position;
        _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVertical;
        _rotationX = Mathf.Clamp(_rotationX, minimumVertical, maximumVertical);
        float delta = Input.GetAxis("Mouse X") * sensitivityHorizontal;
        float rotationY = transform.eulerAngles.y + delta;
        transform.eulerAngles = new Vector3(_rotationX, rotationY, 0);
        character.transform.eulerAngles = new Vector3(_rotationX, rotationY, 0);
    }

    private void OnDisable()
    {
        character.transform.rotation = Quaternion.Euler(
            _characterInitialRotation.eulerAngles.x,
            character.transform.rotation.eulerAngles.y,
            character.transform.rotation.eulerAngles.z
        );
    }
}

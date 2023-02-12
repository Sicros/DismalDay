using UnityEngine;

public class BulletAttributes : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * bulletSpeed * Time.deltaTime;
    }
}

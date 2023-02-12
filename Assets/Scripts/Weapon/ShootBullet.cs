using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform pointOfShoot;
    [SerializeField] private Vector3 positionBullet;
    [SerializeField] private Quaternion rotationBullet;

    // Update is called once per frame
    void Update()
    {
        positionBullet = pointOfShoot.position;
        rotationBullet = pointOfShoot.rotation;
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, positionBullet, rotationBullet);
        }
    }
}

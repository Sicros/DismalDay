using UnityEngine;

public class ShootSoundRadius1 : MonoBehaviour
{
    public CharacterInputs inputs;
    private Vector3 ShootPosition;
    private ZombieAttributes _zombieAttributes;
    private Transform _zombieTransform;
    private Animator _zombieAnimator;

    // private void OnTriggerEnter(Collider[] others)
    // {
    //     foreach (var other in others)
    //     {
    //         Debug.Log(other.transform.name);
    //     }
        // if (other.gameObject.tag == "Zombie")
        // {
            // _zombieTransform = other.transform;
            // other.transform.TryGetComponent<ZombieAttributes>(out _zombieAttributes);
            // other.transform.TryGetComponent<Animator>(out _zombieAnimator);
            // _zombieAttributes.reachedShootSound = false;
        // }
    // }

    // private void OnTriggerStay(Collider other)
    // {
    //     if (other.gameObject.tag == "Zombie")
    //     {
    //         if (!_zombieAnimator.GetBool("isAttacking") && !_zombieAnimator.GetBool("isChasing") && !_zombieAttributes.reachedShootSound)
    //         {
    //             ShootPosition = transform.position;
    //             _zombieAnimator.SetBool("justWalking", true);
    //             WatchingShoot();
    //             ChasingShoot();
    //         }
    //         else
    //         {
    //             _zombieAnimator.SetBool("justWalking", false);
    //             _zombieAttributes.reachedShootSound = true;
    //         }
    //     }
    // }    

    // private void ChasingShoot()
    // {
    //     var vectorToPlayer = ShootPosition - _zombieTransform.position;
    //     if (vectorToPlayer.magnitude > _zombieAttributes.keepDistanceShoot)
    //     {
    //         _zombieTransform.position += new Vector3 (vectorToPlayer.normalized.x, 0, vectorToPlayer.normalized.z) * _zombieAttributes.chasingSpeed * Time.deltaTime;
    //     }
    //     else
    //     {
    //         _zombieAnimator.SetBool("justWalking", false);
    //         _zombieAttributes.reachedShootSound = true;
    //     }
    // }

    // private void WatchingShoot()
    // {
    //     var vectorToPlayer = ShootPosition - _zombieTransform.position;
    //     Quaternion newRotation = Quaternion.LookRotation(vectorToPlayer);
    //     _zombieTransform.rotation = Quaternion.Lerp(_zombieTransform.rotation, newRotation, Time.deltaTime * _zombieAttributes.rotationSpeed);
    // }
}

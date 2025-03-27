using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimAndShoot : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform BulletSpawn;
    
    private GameObject bulletInst;
    private Vector2 worldPosition;
    private Vector2 direction;
    private void Update()
    {
        HandleGunRotation();
        HandleGunShooting();
    }

    private void HandleGunRotation()
    {
        worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        direction = (worldPosition - (Vector2)transform.position).normalized;
        transform.up = direction;
    }

    private void HandleGunShooting()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            bulletInst = Instantiate(Bullet, BulletSpawn.position, transform.rotation);
        }
    }
}

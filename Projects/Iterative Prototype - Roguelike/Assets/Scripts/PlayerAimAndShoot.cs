using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimAndShoot : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform BulletSpawn;
    public static float fireRate = 0.3f;
    private float lastShotTime = 0f;
    
    private GameObject bulletInst;
    private Vector2 worldPosition;
    private Vector2 direction;
    private void FixedUpdate()
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
        if (Mouse.current.leftButton.isPressed && Time.time - lastShotTime >= fireRate)
        {
            bulletInst = Instantiate(Bullet, BulletSpawn.position, transform.rotation);
            lastShotTime = Time.time;
        }
    }
}

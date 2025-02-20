using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class TK_PlayerAimAndShoot : MonoBehaviour
{
    public GameObject TK_Bullet;
    public Transform BulletSpawn;
    
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
            bulletInst = Instantiate(TK_Bullet, BulletSpawn.position, transform.rotation);
        }
    }
}

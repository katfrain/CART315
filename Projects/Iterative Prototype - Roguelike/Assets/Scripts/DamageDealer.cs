using System;
using UnityEngine;

// Eventually turn into interface for Enemies
public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float _damageAmount = 10;
    private Player _player;

    void Start()
    {
        _player = Player.Instance;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Damage();
        }
    }

    void Damage()
    {
        if (_player == null)
        {
            Debug.Log("Player is null");
            return;
        }
        _player.Damage(_damageAmount);
    }
}

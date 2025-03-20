using UnityEngine;

public class Healer : MonoBehaviour
{
    [SerializeField] private float _healAmount = 10;
    private Player _player;

    void Start()
    {
        _player = Player.Instance;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Heal();
        }
    }

    void Heal()
    {
        if (_player == null)
        {
            Debug.Log("Player is null");
            return;
        }
        _player.Heal(_healAmount);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelZone : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private LevelLoader _levelLoader;

    private bool _isExitUnlock;
    private BoxCollider2D _colider;

    private void Start()
    {
        _spriteRenderer.color = Color.red;
        _colider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            if (_isExitUnlock)
            {
                _levelLoader.LoadNextLevel();
            }
        }
    }

    public void UnlockExit()
    {
        _colider.isTrigger = true;
        _isExitUnlock = true;
        _spriteRenderer.color = Color.green;
    }
}

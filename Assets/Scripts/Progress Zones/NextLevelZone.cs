using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class NextLevelZone : MonoBehaviour
{
    [Inject] private LevelLoader _levelLoader;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _passOpenColor;

    private bool _isExitUnlock;
    private BoxCollider2D _colider;

    public event UnityAction ExitUnlocked;

    private void Start()
    {
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
        ExitUnlocked?.Invoke();
        _spriteRenderer.color = _passOpenColor;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Hook : MonoBehaviour
{
    [SerializeField] private List<HookType> _hookTypes;
    [SerializeField] private FocusingLaser _focusingLaser;
    [SerializeField] private PlatformTracker _platformTracker;
    [SerializeField] private PlatformMatcher _platformMatcher;
    [SerializeField] private Color _hookColor;

    private HookType _currentHookType;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = _hookColor; //Зачем???

        foreach (var type in _hookTypes)
        {
            type.gameObject.SetActive(false);
        }

        _hookTypes[0].gameObject.SetActive(true);
        _currentHookType = _hookTypes[0];

        _platformTracker.PlatformFocusChanged += OnPlatformFocusChanged;
    }

    private void OnDisable()
    {
        _platformTracker.PlatformFocusChanged -= OnPlatformFocusChanged;
    }

    private void OnPlatformFocusChanged(Platform platform)
    {
        print("jjjj");
        _currentHookType.gameObject.SetActive(false);
        _currentHookType = _platformMatcher.Match(platform, _hookTypes);
        _currentHookType.gameObject.SetActive(true);
    }
}

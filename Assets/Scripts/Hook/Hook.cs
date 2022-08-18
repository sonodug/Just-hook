using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Hook : MonoBehaviour
{
    public enum Hook_Type
    {
        AttractingHookType,
        BounceHookType,
        PhysicsHookType,
        TransporterHookType
    }

    [Header("Dictionary Alternative | Match according to the order:")]
    [SerializeField] private List<Hook_Type> _hook_Types;
    [SerializeField] private List<HookController> _hookTypes;

    [SerializeField] private FocusingLaser _focusingLaser;
    [SerializeField] private PlatformTracker _platformTracker;
    [SerializeField] private Color _hookColor;

    private Dictionary<Hook_Type, HookController> _hookTypesDic;
    private readonly PlatformToHookMatcherVisitor _platformVisitor = new PlatformToHookMatcherVisitor();
    private HookController _currentHookType;

    private void Start()
    {
        _platformTracker.PlatformFocusChanged += OnPlatformFocusChanged;

        FillDictionary();

        foreach (var type in _hookTypes)
        {
            type.gameObject.SetActive(false);
        }

        _platformVisitor.Init(this);

        _hookTypes[0].gameObject.SetActive(true);
        _currentHookType = _hookTypes[0];

    }

    private void OnDisable()
    {
        _platformTracker.PlatformFocusChanged -= OnPlatformFocusChanged;
    }

    public void FillDictionary()
    {
        _hookTypesDic = new Dictionary<Hook_Type, HookController>();

        for (int i = 0; i < _hookTypes.Count; i++)
        {
            _hookTypesDic.Add(_hook_Types[i], _hookTypes[i]);
        }
    }

    private void OnPlatformFocusChanged(Platform platform)
    {
        _currentHookType.gameObject.SetActive(false);

        platform.Accept(_platformVisitor);

        _currentHookType.gameObject.SetActive(true);
    }

    public void SetCurrentHook(Hook_Type hook_Type)
    {
        if (_hookTypesDic.TryGetValue(hook_Type, out HookController hookType))
        {
            _currentHookType = hookType;
        }
    }
}

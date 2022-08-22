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
    [SerializeField] private List<HookEngine> _hookTypes;

    [SerializeField] private FocusingLaser _focusingLaser;
    [SerializeField] private PlatformTracker _platformTracker;
    [SerializeField] private Color _hookColor;

    private Dictionary<Hook_Type, HookEngine> _hookTypesDic;
    private readonly PlatformToHookMatcherVisitor _platformVisitor = new PlatformToHookMatcherVisitor();
    private HookEngine _currentHookType;

    private void Start()
    {
        _platformTracker.PlatformFocusChangedWithChangable += OnPlatformFocusChanged;

        FillDictionary();

        _platformVisitor.Init(this);

        foreach (var hookType in _hookTypes)
        {
            hookType.gameObject.SetActive(true);
        }

        _currentHookType = _hookTypes[0];

    }

    private void Update()
    {
        ListenInputs();
    }

    private void OnDisable()
    {
        _platformTracker.PlatformFocusChangedWithChangable -= OnPlatformFocusChanged;
    }

    public void FillDictionary()
    {
        _hookTypesDic = new Dictionary<Hook_Type, HookEngine>();

        for (int i = 0; i < _hookTypes.Count; i++)
        {
            _hookTypesDic.Add(_hook_Types[i], _hookTypes[i]);
        }
    }

    private void OnPlatformFocusChanged(Platform platform)
    {
        platform.Accept(_platformVisitor);
    }

    public void SetCurrentHook(Hook_Type hook_Type)
    {
        _currentHookType.Disable();

        if (_hookTypesDic.TryGetValue(hook_Type, out HookEngine hookType))
        {
            _currentHookType = hookType;
        }
    }

    private void ListenInputs()
    {
        if (Input.GetMouseButton(1))
        {
            _focusingLaser.DrawStraightTrajectory(transform.position, false);

            if (Input.GetMouseButtonDown(0))
            {
                _focusingLaser.DrawStraightTrajectory(transform.position, true);
                _currentHookType.Enable();
            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            _focusingLaser.Disable();
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            _currentHookType.Disable();
        }
    }
}

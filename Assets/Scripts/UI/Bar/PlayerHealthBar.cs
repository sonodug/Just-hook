using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerHealthBar : Bar
{
    [Inject] private Player _player;

    private void OnEnable()
    {
        _player.HealthChanged += OnValueChanged;
        _player.Died += OnPlayerDied;
        Slider.value = 1;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnValueChanged;
        _player.Died -= OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        Slider.value = 1;
    }
}

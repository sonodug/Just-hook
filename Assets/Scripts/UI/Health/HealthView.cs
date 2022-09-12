using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Player;
using UnityEngine;
using Zenject;

public class HealthView : MonoBehaviour
{
    [Inject] private Player _player;

    [SerializeField] private List<HealthModel> _healthModels;
    [SerializeField] private float _transitionDelay;

    private int _index;

    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChanged;
        _player.Died += OnPlayerDied;
        _index = 0;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
        _player.Died -= OnPlayerDied;
    }

    private void OnHealthChanged()
    {
        if (_index <= _healthModels.Count)
        {
            _healthModels[_index].gameObject.SetActive(false);
            _index++;
        }
    }

    private async void OnPlayerDied()
    {
        _index = 0;
        foreach (var healthModel in _healthModels)
        {
            healthModel.gameObject.SetActive(false);
        }

        await Delay();

        gameObject.SetActive(true);
        foreach (var healthModel in _healthModels)
        {
            healthModel.gameObject.SetActive(true);
        }
    }


    private async Task Delay()
    {
        for (float a = 0f; a <= _transitionDelay; a += 0.02f)
        {
            await Task.Yield();
        }
    }
}

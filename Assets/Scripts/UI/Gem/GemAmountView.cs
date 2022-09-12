using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Player;
using Progress_Zones;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GemAmountView : MonoBehaviour
{
    [Inject] private Player _player;
    [Inject] private NextLevelZone _nextLevel;

    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _gemsCollectedLabel;
    [SerializeField] private Image _openLabel;
    [SerializeField] private LevelConfigurator _levelConfigurator;

    private int _collected;
    private int _amount;

    private void OnEnable()
    {
        _collected = 0;
        _amount = _levelConfigurator.GemsCollectToFinish;

        _score.text = $"{_collected} / {_amount}";

        _player.LevelScoreChanged += OnLevelScoreChanged;
        _player.LevelScoreChanged += OnLevelScoreChangedAsync;

        _nextLevel.ExitUnlocked += OnExitUnlocked;

        _openLabel.enabled = false;
        _gemsCollectedLabel.enabled = false;
    }

    private void OnDisable()
    {
        _player.LevelScoreChanged -= OnLevelScoreChanged;
        _player.LevelScoreChanged -= OnLevelScoreChangedAsync;
        _nextLevel.ExitUnlocked -= OnExitUnlocked;
    }

    private void OnLevelScoreChanged()
    {
        _collected++;
        _score.text = $"{_collected} / {_amount}";
    }

     private async void OnLevelScoreChangedAsync()
    {
        _gemsCollectedLabel.gameObject.SetActive(true);
        _gemsCollectedLabel.enabled = true;
        await Fade();
    }

    private void OnExitUnlocked()
    {
        _openLabel.gameObject.SetActive(true);
        _openLabel.enabled = true;
    }

    public async Task Fade()
    {
        Color color = _gemsCollectedLabel.color;

        for (float alpha = 0f; alpha <= 1; alpha += 0.008f)
        {
            color.a = alpha;
            _gemsCollectedLabel.color = color;

            await Task.Yield();
        }

        for (float alpha = 1f; alpha >= 0f; alpha -= 0.008f)
        {
            color.a = alpha;
            _gemsCollectedLabel.color = color;

            await Task.Yield();
        }
    }
}

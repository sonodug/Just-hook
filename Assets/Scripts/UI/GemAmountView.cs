using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GemAmountView : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _openLabel;
    [SerializeField] private Player _player;
    [SerializeField] private NextLevelZone _nextLevel;
    [SerializeField] private LevelConfigurator _levelConfigurator;

    private int _collected;
    private int _amount;

    private void OnEnable()
    {
        _collected = 0;
        _amount = _levelConfigurator.GemsCollectToFinish;

        _score.text = $"{_collected} / {_amount}";

        _player.LevelScoreChanged += OnLevelScoreChanged;
        _nextLevel.ExitUnlocked += OnExitUnlocked;

        _openLabel.enabled = false;
    }

    private void OnDisable()
    {
        _player.LevelScoreChanged -= OnLevelScoreChanged;
    }

    private void OnLevelScoreChanged()
    {
        _collected++;
        _score.text = $"{_collected} / {_amount}";
    }

    private void OnExitUnlocked()
    {
        _openLabel.enabled = true;
    }
}

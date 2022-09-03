using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GemAmountView : MonoBehaviour
{
    [Inject] private Player _player;

    [SerializeField] private TMP_Text _score;
    [SerializeField] private Image _openLabel;
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
        _openLabel.gameObject.SetActive(true);
        _openLabel.enabled = true;
    }
}

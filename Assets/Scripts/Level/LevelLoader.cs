using System.Collections;
using System.Collections.Generic;
using IJunior.TypedScenes;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LevelLoader : MonoBehaviour
{
    [Inject] private Player _player;

    [SerializeField] private LevelConfigurator _levelConfig;
    [SerializeField] private NextLevelZone _nextLevel;

    //levelInfo
    private int _gemsCollectedAmount;

    private void OnEnable()
    {
        _player.LevelScoreChanged += OnLevelScoreChanged;
    }

    private void OnDisable()
    {
        _player.LevelScoreChanged -= OnLevelScoreChanged;
    }

    private void OnLevelScoreChanged()
    {
        _gemsCollectedAmount++;

        if (_gemsCollectedAmount == _levelConfig.GemsCollectToFinish)
        {
            _nextLevel.UnlockExit();
        }
    }

    public void LoadNextLevel()
    {
        //GameManager => get next level, => load
        //Test_Level2.Load(_levelConfig);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadLevel()
    {
        //Test_Level1.Load();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

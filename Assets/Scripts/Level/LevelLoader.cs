using System.Collections;
using System.Collections.Generic;
using IJunior.TypedScenes;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Zenject.SpaceFighter;

public class LevelLoader : MonoBehaviour
{
    [Inject] private Player _player;
    [Inject] private DiContainer _container;

    [SerializeField] private LevelConfigurator _levelConfig;
    [SerializeField] private NextLevelZone _nextLevel;
    [SerializeField] private List<Transform> _controlPoints;
    [SerializeField] private float _playerDieTransitionSpeed;

    //levelInfo
    private int _gemsCollectedAmount;
    private Transform _currentControlPoint;
    private int _currentControlPointIndex;

    private void Start()
    {
        _currentControlPoint = _controlPoints[0];
        _currentControlPointIndex = 0;
    }

    private void OnEnable()
    {
        _player.LevelScoreChanged += OnLevelScoreChanged;
        _player.ControlPointChanged += OnControlPointChanged;
        _player.Died += OnPlayerDied;
    }

    private void OnDisable()
    {
        _player.LevelScoreChanged -= OnLevelScoreChanged;
        _player.ControlPointChanged -= OnControlPointChanged;
        _player.Died -= OnPlayerDied;
    }

    private void OnLevelScoreChanged()
    {
        _gemsCollectedAmount++;

        if (_gemsCollectedAmount == _levelConfig.GemsCollectToFinish)
        {
            _nextLevel.UnlockExit();
        }
    }

    private void OnControlPointChanged(Transform controlPoint)
    {
        _currentControlPoint = controlPoint;
    }

    private void OnPlayerDied()
    {
        ReloadPlayerWithControlPoint();
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
        //control point
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReloadPlayerWithControlPoint()
    {
        _player.transform.position = Vector2.MoveTowards(_currentControlPoint.position, transform.position, 0);
    }
}

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{

    public static float _constantSpeed = 5;
    public static float _gameSpeed = 5;
    public static float _defaultGameSpeed = 5;

    public static int _playerScore;
    public static int _enemyScore;
    public static float _roundStartDelay = 0f;
    public static float _gameOverDelay = 3f;
    public static float _roundOverDelay = 0f;
    public static int _ballSpawnFromPlayer = -10;
    public static int _ballSpawnFromEnemy = 10;
    public static int _scoreToWin = 10;
    public static int _startingScore;
    private static bool _playerScoredLast = true;
    public static bool _roundHadWinner = false;


    private static Enums.GameStep _gameStep;
    private static Vector3[] _playerStartingVectors = new Vector3[4];
    private static Vector3[] _enemyStartingVectors = new Vector3[4];
    private static List<string> _scenes;
    private static List<string> _scenePlayList;
    private static System.Random random = new System.Random();
    private static List<string> _disabledScenesList;

    // Use this for initialization
    public void Start()
    {
        _scenes = new List<string>();
        LoadStartingVectors();
        LoadSceneList();
        _gameStep = Enums.GameStep.RoundStarting;
        DontDestroyOnLoad(this.gameObject);
        if(_disabledScenesList == null) { 
            _disabledScenesList = new List<string>();
        }
    }


    public static int GetPlayerScore()
    {
        return _playerScore;
    }

    public static void SetPlayerScore(int playerScore)
    {
        _playerScore = playerScore;
    }

    public static int GetEnemyScore()
    {
        return _enemyScore;
    }

    public static void SetEnemyScore(int enemyScore)
    {
        _enemyScore = enemyScore;
    }

    public static string GetNextScene(int sceneNumber)
    {
        return _scenePlayList[sceneNumber];
    }

    public static List<string> GetScenes()
    {
        return _scenes;
    }

    public static Enums.GameStep GetGameStep()
    {
        return _gameStep;
    }

    public static void SetGameStep(Enums.GameStep gameStep)
    {
        _gameStep = gameStep;
    }

    public static void DisableScene(string sceneName)
    {
        _disabledScenesList.Add(sceneName);
    }

    public static void EnableScene(string sceneName)
    {
        _disabledScenesList.Remove(sceneName);
    }

    private void LoadStartingVectors()
    {
        _playerStartingVectors[0] = new Vector3(3, 2, 0);
        _playerStartingVectors[1] = new Vector3(3, -2, 0);
        _playerStartingVectors[2] = new Vector3(4, 3, 0);
        _playerStartingVectors[3] = new Vector3(4, -3, 0);
        _enemyStartingVectors[0] = new Vector3(-3, 2, 0);
        _enemyStartingVectors[1] = new Vector3(-3, -2, 0);
        _enemyStartingVectors[2] = new Vector3(-4, 3, 0);
        _enemyStartingVectors[3] = new Vector3(-4, -3, 0);
    }

    public static Vector3[] GetPlayerStartingVectors()
    {
        return _playerStartingVectors;
    }

    public static Vector3[] GetEnemyStartingVectors()
    {
        return _enemyStartingVectors;
    }

    public static List<string> GetScenePlayList()
    {
        return _scenePlayList;
    }

    public static void CreateRandomPlayList()
    {
        List<string> activeScenes = new List<string>();
        _scenePlayList = new List<string>();
        foreach (string scene in _scenes)
        {
            if (!_disabledScenesList.Contains(scene))
            {
                activeScenes.Add(scene);
            }
        }
        //If Pong isn't disabled, always start with Pong
        if (_disabledScenesList.Contains(Constants.PONG))
        {
            for (int i = 0; i < 20; i++)
            {
                _scenePlayList.Add(activeScenes[Random.Range(0, activeScenes.Count)]);
            }
        }
        else { 
            _scenePlayList.Add(Constants.PONG);
            for (int i = 0; i < 19; i++)
            {
                _scenePlayList.Add(_scenes[Random.Range(0, _scenes.Count)]);
            }
        }
    }

    public static void CreatePlayListFromList(List<string> playList)
    {
        _scenePlayList = playList;
    }

    public static string GetSceneByIndex(int sceneIndex)
    {
        return _scenePlayList[sceneIndex];
    }

    private void LoadSceneList()
    {
        _scenes.Add(Constants.PONG);
        _scenes.Add(Constants.MULTIBALL);
        _scenes.Add(Constants.INVISIBALL);
        _scenes.Add(Constants.MISSILE);
        _scenes.Add(Constants.WINDMILL);
        _scenes.Add(Constants.PORTAL);
        _scenes.Add(Constants.BREAKOUTBALL);
    }

    public static void ResetScenes()
    {
        _scenePlayList = new List<string>();
    }

    public static float GetGameSpeed()
    {
        return _gameSpeed;
    }

    public static void SetGameSpeed(int gameSpeed)
    {
        _gameSpeed = gameSpeed;
    }

    public static bool GetPlayerScoredLast()
    {
        return _playerScoredLast;
    }

    public static void SetPlayerScoredLast(bool playerScoredLast)
    {
        _playerScoredLast = playerScoredLast;
    }

    public static bool GetRoundHadWinner()
    {
        return _roundHadWinner;
    }

    public static void SetRoundHadWinner(bool roundHadWinner)
    {
        _roundHadWinner = roundHadWinner;
    }

    public static int GetScoreToWin()
    {
        return _scoreToWin;
    }

    public static bool GameHasWinner()
    {
        if (_playerScore == _scoreToWin || _enemyScore == _scoreToWin)
        {
            return true;
        }
        return false;
    }
}

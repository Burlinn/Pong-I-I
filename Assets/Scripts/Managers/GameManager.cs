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
    private static List<SceneHelper> _scenes;
    private static System.Random random = new System.Random();

    // Use this for initialization
    public void Start()
    {
        _scenes = new List<SceneHelper>();
        LoadStartingVectors();
        LoadSceneList();
        _gameStep = Enums.GameStep.RoundStarting;
        DontDestroyOnLoad(this.gameObject);
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

    public static string GetNextScene()
    {
        List<SceneHelper> unplayedScenes = _scenes.Where(x => x.ScenePlayed == false).ToList();
        int index = random.Next(unplayedScenes.Count);
        string nextScene = unplayedScenes[index].SceneName;
        _scenes.Where(x => x.SceneName.Equals(nextScene)).First().ScenePlayed = true;
        return unplayedScenes[index].SceneName;
    }

    public static List<SceneHelper> GetScenes()
    {
        return _scenes;
    }

    public static void ResetScenes()
    {
        foreach(SceneHelper scene in _scenes)
        {
            scene.ScenePlayed = false;
        }
    }

    public static Enums.GameStep GetGameStep()
    {
        return _gameStep;
    }

    public static void SetGameStep(Enums.GameStep gameStep)
    {
        _gameStep = gameStep;
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

    private void LoadSceneList()
    {
        _scenes.Add(new SceneHelper("Pong", true));
        _scenes.Add(new SceneHelper("Multiball", false));
        _scenes.Add(new SceneHelper("Invisiball", false));
        _scenes.Add(new SceneHelper("Missile", false));
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

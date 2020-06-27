using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public float _constantSpeed = 5;
    public float _gameSpeed = 5;

    //Two variables to hold our scores
    public int _playerScore;
    public int _enemyScore;
    public float _roundStartDelay = 0f;
    public float _gameOverDelay = 3f;
    public float _roundOverDelay = 0f;
    public int _ballSpawnFromPlayer = -10;
    public int _ballSpawnFromEnemy = 10;
    public int _scoreToWin = 10;
	public bool _playerScoredLast = true;
    public int _startingScore;
    public Text _gameMessageText;
    public Text _playerScoreText;
    public Text _enemyScoreText;

    private Enums.GameStep _gameStep;
    private Vector3[] _playerStartingVectors = new Vector3[4];
    private Vector3[] _enemyStartingVectors = new Vector3[4];
    private BallManager _ballManager;
    private GameObject _ball;
    private float _timer = 0;
    private bool _winnerSet = false;

    // Use this for initialization
    public void Start()
    {
        _gameMessageText.text = "";
        _playerScoreText.text = _playerScore.ToString();
        _enemyScoreText.text = _enemyScore.ToString();
        _ball = GameObject.FindGameObjectWithTag("Ball");
        _ballManager = _ball.GetComponent<BallManager>();

        _playerStartingVectors[0] = new Vector3(3, 2, 0);
		_playerStartingVectors[1] = new Vector3(3, -2, 0);
		_playerStartingVectors[2] = new Vector3(4, 3, 0);
		_playerStartingVectors[3] = new Vector3(4, -3, 0);
		_enemyStartingVectors[0] = new Vector3(-3, 2, 0);
		_enemyStartingVectors[1] = new Vector3(-3, -2, 0);
		_enemyStartingVectors[2] = new Vector3(-4, 3, 0);
		_enemyStartingVectors[3] = new Vector3(-4, -3, 0);
        _gameStep = Enums.GameStep.RoundStarting;
    }

    public void Update()
    {
        if(_gameStep == Enums.GameStep.RoundStarting)
        {
            RoundStarting();
        }
        if(_gameStep == Enums.GameStep.RoundPlaying)
        {
            RoundPlaying();
        }
        if(_gameStep == Enums.GameStep.RoundEnding)
        {
            if (GameHasWinner())
            {
                _winnerSet = true;
            }
            RoundEnding();
        }
    }

    private void RoundStarting()
    {
        ResetBall();
        _gameStep = Enums.GameStep.RoundPlaying;
    }

    private void RoundPlaying()
    {
        if (!BallInPlay())
        {
            _gameStep = Enums.GameStep.RoundEnding;
        }
    }

    private void RoundEnding()
    {
        if (!_winnerSet)
        { 
            if (_ballManager._playerScoredLast && _ballManager._roundHadWinner)
            {
                _playerScore++;
                _playerScoreText.text = _playerScore.ToString();
                _playerScoredLast = true;


            }
            else if(_ballManager._roundHadWinner)
            {
                _enemyScore++;
                _enemyScoreText.text = _enemyScore.ToString();
                _playerScoredLast = false;
            }

            _ballManager._roundHadWinner = false;

            if (_enemyScore == _scoreToWin)
            {
                _gameMessageText.text = "You Lose!";
            }
            else if(_playerScore == _scoreToWin)
            {
                _gameMessageText.text = "You Win!";
            }
            else
            {
                _gameStep = Enums.GameStep.RoundStarting;
            }

        } else {
            _timer += Time.deltaTime;

            if (_timer > _gameOverDelay)
            {
                SceneManager.LoadScene(0);
            }
        }

    }


    private bool BallInPlay()
    {
        return _ball.activeSelf;
    }

    private bool GameHasWinner()
    {
        if(_playerScore == 10 || _enemyScore == 10)
        {
            return true;
        }
        return false;
    }

    public void ResetBall()
    {

        _ball.SetActive(true);

        Vector3 startPosition = _ball.GetComponent<Rigidbody>().position;
		Vector3 startingVector = new Vector3 ();

		if (_playerScoredLast == true) {
			startingVector = _playerStartingVectors[Random.Range(0, 4)];
            startPosition.x = _ballSpawnFromPlayer;
        }

		if (_playerScoredLast == false) {
			startingVector = _enemyStartingVectors[Random.Range(0, 4)];
            startPosition.x = _ballSpawnFromEnemy;
        }


        _ball.GetComponent<Rigidbody>().velocity = startingVector;
            
        startPosition.y = 0;
        _ball.GetComponent<Rigidbody>().position = startPosition;
        

    }


 

}

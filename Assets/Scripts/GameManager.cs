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
    private Vector3[] _playerStartingVectors = new Vector3[4];
	private Vector3[] _enemyStartingVectors = new Vector3[4];
    public int _ballSpawnFromPlayer = -10;
    public int _ballSpawnFromEnemy = 10;
    public int _scoreToWin = 10;
	public bool _playerScoredLast = true;
    public int _startingScore;
    public BallManager _ballManager;
    public GameObject _ball;
    public Text _gameMessageText;
    public Text _playerScoreText;
    public Text _enemyScoreText;
    

    private WaitForSeconds gameOverWait;
    private WaitForSeconds roundStartWait;

    // Use this for initialization
    public void Start()
    {
        gameOverWait = new WaitForSeconds(_gameOverDelay);
        roundStartWait = new WaitForSeconds(_roundStartDelay);


        _gameMessageText.text = "";
        _playerScore = _startingScore;
        _enemyScore = _startingScore;
        _playerScoreText.text = _playerScore.ToString();
        _enemyScoreText.text = _enemyScore.ToString();
        _ballManager.ballInstance = _ball;

        _playerStartingVectors[0] = new Vector3(3, 2, 0);
		_playerStartingVectors[1] = new Vector3(3, -2, 0);
		_playerStartingVectors[2] = new Vector3(4, 3, 0);
		_playerStartingVectors[3] = new Vector3(4, -3, 0);
		_enemyStartingVectors[0] = new Vector3(-3, 2, 0);
		_enemyStartingVectors[1] = new Vector3(-3, -2, 0);
		_enemyStartingVectors[2] = new Vector3(-4, 3, 0);
		_enemyStartingVectors[3] = new Vector3(-4, -3, 0);
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if (GameHasWinner())
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }

    private IEnumerator RoundStarting()
    {
        ResetBall();
        yield return null;
    }

    private IEnumerator RoundPlaying()
    {
        while (BallInPlay())
        {
            yield return null;
        }

    }

    private IEnumerator RoundEnding()
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
            yield return gameOverWait;
        }
        else if(_playerScore == _scoreToWin)
        {
            _gameMessageText.text = "You Win!";
            yield return gameOverWait;
        }
        else
        {
            yield return _roundOverDelay;
        }

        
    }


    private bool BallInPlay()
    {
        return _ballManager.ballInstance.activeSelf;
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

        _ballManager.ballInstance.SetActive(true);

        Vector3 startPosition = _ballManager.ballInstance.GetComponent<Rigidbody>().position;
		Vector3 startingVector = new Vector3 ();

		if (_playerScoredLast == true) {
			startingVector = _playerStartingVectors[Random.Range(0, 4)];
            startPosition.x = _ballSpawnFromPlayer;
        }

		if (_playerScoredLast == false) {
			startingVector = _enemyStartingVectors[Random.Range(0, 4)];
            startPosition.x = _ballSpawnFromEnemy;
        }

		_ballManager.ballInstance.GetComponent<Rigidbody>().velocity = startingVector;
            
        startPosition.y = 0;
        _ballManager.ballInstance.GetComponent<Rigidbody>().position = startPosition;
        

    }


 

}

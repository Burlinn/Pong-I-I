using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Pong
{
    public class PongManager : MonoBehaviour
    {

        public float _roundStartDelay = 0f;
        public float _gameOverDelay = 3f;
        public float _roundOverDelay = 0f;
        public int _ballSpawnFromPlayer = -10;
        public int _ballSpawnFromEnemy = 10;
        public Text _playerScoreText;
        public Text _enemyScoreText;
        public Text _gameMessageText;

        private static BallManager _ballManager;
        private static GameObject _ball;
        private float _timer = 0;
        private bool _winnerSet = false;
        private static System.Random random = new System.Random();

        // Use this for initialization
        public void Start()
        {
            _gameMessageText.text = "";
            _ball = GameObject.FindGameObjectWithTag("Ball");
            _ballManager = _ball.GetComponent<Pong.BallManager>();
            _playerScoreText.text = GameManager.GetPlayerScore().ToString();
            _enemyScoreText.text = GameManager.GetEnemyScore().ToString();
        }

        public void Update()
        {
            if (GameManager.GetGameStep() == Enums.GameStep.RoundStarting)
            {
                RoundStarting();
            }
            if (GameManager.GetGameStep() == Enums.GameStep.RoundPlaying)
            {
                RoundPlaying();
            }
            if (GameManager.GetGameStep() == Enums.GameStep.RoundEnding)
            {
                if (GameManager.GameHasWinner())
                {
                    _winnerSet = true;
                }
                RoundEnding();
            }
        }

        public static GameObject GetBall()
        {
            return _ball;
        }

        private void RoundStarting()
        {
            ResetBall();
            GameManager.SetGameStep(Enums.GameStep.RoundPlaying);
        }

        private void RoundPlaying()
        {
            if (!BallInPlay())
            {
                GameManager.SetGameStep(Enums.GameStep.RoundEnding);
            }
        }

        private void RoundEnding()
        {
            if (!_winnerSet)
            {
                if (_ballManager._playerScoredLast && _ballManager._roundHadWinner)
                {
                    GameManager.SetPlayerScore(GameManager.GetPlayerScore() + 1);
                    _playerScoreText.text = GameManager.GetPlayerScore().ToString();
                    GameManager.SetPlayerScoredLast(true);


                }
                else if (_ballManager._roundHadWinner)
                {
                    GameManager.SetEnemyScore(GameManager.GetEnemyScore() + 1);
                    _enemyScoreText.text = GameManager.GetEnemyScore().ToString();
                    GameManager.SetPlayerScoredLast(false);
                }

                _ballManager._roundHadWinner = false;

                if (GameManager.GetEnemyScore() == GameManager.GetScoreToWin())
                {
                    _gameMessageText.text = "You Lose!";
                }
                else if (GameManager.GetPlayerScore() == GameManager.GetScoreToWin())
                {
                    _gameMessageText.text = "You Win!";
                }
                else
                {
                    string scene = GameManager.GetNextScene();
                    SceneManager.LoadScene(scene);
                    GameManager.SetGameStep(Enums.GameStep.RoundStarting);
                }

            }
            else
            {
                _timer += Time.deltaTime;

                if (_timer > _gameOverDelay)
                {
                    GameManager.ResetScenes();
                    SceneManager.LoadScene("Pong");
                }
            }

        }

        private bool BallInPlay()
        {
            return _ball.activeSelf;
        }

        public void ResetBall()
        {

            _ball.SetActive(true);

            Vector3 startPosition = _ball.GetComponent<Rigidbody>().position;
            Vector3 startingVector = new Vector3();

            if (GameManager.GetPlayerScoredLast() == true)
            {
                startingVector = GameManager.GetPlayerStartingVectors()[Random.Range(0, 4)];
                startPosition.x = _ballSpawnFromPlayer;
            }

            if (GameManager.GetPlayerScoredLast() == false)
            {
                startingVector = GameManager.GetEnemyStartingVectors()[Random.Range(0, 4)];
                startPosition.x = _ballSpawnFromEnemy;
            }


            _ball.GetComponent<Rigidbody>().velocity = startingVector;

            startPosition.y = 0;
            _ball.GetComponent<Rigidbody>().position = startPosition;


        }
    }
}
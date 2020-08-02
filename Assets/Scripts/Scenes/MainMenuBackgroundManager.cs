using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MainMenuBackgroundManager : MonoBehaviour
    {

        public float _roundStartDelay = 0f;
        public float _gameOverDelay = 3f;
        public float _roundOverDelay = 0f;
        public int _ballSpawnFromPlayer = -10;
        public int _ballSpawnFromEnemy = 10;
        public Text _playerScoreText;
        public Text _enemyScoreText;
        public Text _gameMessageText;

        private static Generic.BallManager _ballManager;
        private static GameObject _ball;
        private float _timer = 0;
        private bool _winnerSet = false;
        private static System.Random random = new System.Random();
        private static int _drone1Score = 0;
        private static int _drone2Score = 0;
        private static bool _drone1ScoredLast = false;
        private static bool _gameHasWinner = false;

        // Use this for initialization
        public void Start()
        {
            _ball = GameObject.FindGameObjectWithTag("Ball");
            _ballManager = _ball.GetComponent<Generic.BallManager>();
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
                if (_gameHasWinner)
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
                    _drone1Score += 1;
                    _playerScoreText.text = _drone1Score.ToString();
                    _drone1ScoredLast = true;


                }
                else if (_ballManager._roundHadWinner)
                {
                    _drone2Score += 1;
                    _enemyScoreText.text = _drone2Score.ToString();
                    _drone1ScoredLast = false;
                }

                _ballManager._roundHadWinner = false;

                if (_drone2Score == GameManager.GetScoreToWin() || _drone1Score == GameManager.GetScoreToWin())
                {
                    _gameHasWinner = true;
                } else
                {
                    GameManager.SetGameStep(Enums.GameStep.RoundStarting);
                }

            }
            else
            {
                _timer += Time.deltaTime;

                if (_timer > _gameOverDelay)
                {
                    GameManager.ResetScenes();
                    SceneManager.LoadScene("MainMenu");
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

            if (_drone1ScoredLast == true)
            {
                startingVector = GameManager.GetPlayerStartingVectors()[Random.Range(0, 4)];
                startPosition.x = _ballSpawnFromPlayer;
            }

            if (_drone1ScoredLast == false)
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
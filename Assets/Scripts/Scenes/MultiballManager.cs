using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

namespace Multiball
{
    public class MultiballManager : MonoBehaviour
    {

        public float _roundStartDelay = 0f;
        public float _gameOverDelay = 3f;
        public float _roundOverDelay = 0f;
        public int _ballSpawnFromPlayer = -10;
        public int _ballSpawnFromEnemy = 10;
        public Text _playerScoreText;
        public Text _enemyScoreText;
        public Text _gameMessageText;
        public float _ballSpawnDelay = .03f;
        public float _multiballSpawnTimer;
        public GameObject _ballPrefab;

        private static BallManager _ballManager;
        private static List<GameObject> _balls;
        private float _timer = 0;
        private bool _winnerSet = false;
        private static System.Random random = new System.Random();
        private static Vector3 _nextBallSpawnPoint;
        private static bool _ballSpawnTimerStarted;
        private static bool _ballLastHitPlayer;

        // Use this for initialization
        public void Start()
        {
            _gameMessageText.text = "";
            _balls = new List<GameObject>();
            _balls.Add(GameObject.FindGameObjectWithTag("Ball"));
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

        public Vector3 GetNextBallSpawnPoint()
        {
            return _nextBallSpawnPoint;
        }

        public void SetNextBallSpawnPoint(Vector3 nextBallSpawnPoint)
        {
            _nextBallSpawnPoint = nextBallSpawnPoint;
        }

        public void StartBallSpawnTimer()
        {
            _ballSpawnTimerStarted = true;
        }

        public void SetBallLastHitPlayer(bool ballLastHitPlayer)
        {
            _ballLastHitPlayer = ballLastHitPlayer;
        }

        public List<GameObject> GetBalls()
        {
            return _balls;
        }

        private void RoundStarting()
        {
            ResetBall();
            GameManager.SetGameStep(Enums.GameStep.RoundPlaying);
        }

        private void RoundPlaying()
        {
            if (!AllBallsInPlay())
            {
                GameManager.SetGameStep(Enums.GameStep.RoundEnding);
            }

            if(_ballSpawnTimerStarted)
            {
                _multiballSpawnTimer += Time.deltaTime;

                if (_multiballSpawnTimer > _ballSpawnDelay)
                {
                    CreateNewBall();
                }
            }
        }

        private void RoundEnding()
        {
            if (!_winnerSet)
            {
                if (GameManager.GetPlayerScoredLast() && GameManager.GetRoundHadWinner())
                {
                    GameManager.SetPlayerScore(GameManager.GetPlayerScore() + 1);
                    _playerScoreText.text = GameManager.GetPlayerScore().ToString();
                    GameManager.SetPlayerScoredLast(true);


                }
                else if (GameManager.GetRoundHadWinner())
                {
                    GameManager.SetEnemyScore(GameManager.GetEnemyScore() + 1);
                    _enemyScoreText.text = GameManager.GetEnemyScore().ToString();
                    GameManager.SetPlayerScoredLast(false);
                }
                GameManager.SetRoundHadWinner(false);

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
                    GameManager.SetGameStep(Enums.GameStep.RoundStarting);
                    SceneManager.LoadScene(GameManager.GetSceneByIndex(GameManager.GetPlayerScore() + GameManager.GetEnemyScore()));
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

        private bool AllBallsInPlay()
        {
            return _balls.Where(x => !x.activeSelf).Count() == 0;
        }

        public void CreateNewBall()
        {
            Vector3 startingVector = new Vector3();
            Vector3 startPosition = new Vector3();

            GameObject newBall = Instantiate(_ballPrefab, _nextBallSpawnPoint, Quaternion.identity);
            _balls.Add(newBall);
            if (_ballLastHitPlayer == true)
            {
                startingVector = GameManager.GetPlayerStartingVectors()[Random.Range(0, 4)];
                startPosition.x = _ballSpawnFromPlayer;
            }

            if (_ballLastHitPlayer == false)
            {
                startingVector = GameManager.GetEnemyStartingVectors()[Random.Range(0, 4)];
                startPosition.x = _ballSpawnFromEnemy;
            }
            _ballSpawnTimerStarted = false;
            _multiballSpawnTimer = 0;
            newBall.GetComponent<Rigidbody>().velocity = startingVector;

            startPosition.y = 0;
            newBall.GetComponent<Rigidbody>().position = startPosition;
        }

        public void ResetBall()
        {
            GameObject startingBall = _balls.First();
            _balls.Clear();
            _balls.Add(startingBall);

            Vector3 startPosition = _balls.First().GetComponent<Rigidbody>().position;
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


            _balls.First().GetComponent<Rigidbody>().velocity = startingVector;

            startPosition.y = 0;
            _balls.First().GetComponent<Rigidbody>().position = startPosition;


        }
    }
}

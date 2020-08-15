using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

namespace Multiball
{
    public class MultiballManager : Generic.PongManager
    {
        public float _ballSpawnDelay = .03f;
        public float _multiballSpawnTimer;
        public GameObject _ballPrefab;

        private static List<GameObject> _balls;
        private static System.Random random = new System.Random();
        private static Vector3 _nextBallSpawnPoint;
        private static bool _ballSpawnTimerStarted;
        private static bool _ballLastHitPlayer;

        // Use this for initialization
        public override void Start()
        {
            _gameMessageText.text = "";
            _balls = new List<GameObject>();
            _balls.Add(GameObject.FindGameObjectWithTag(Constants.BALL));
            _playerScoreText.text = GameManager.GetPlayerScore().ToString();
            _enemyScoreText.text = GameManager.GetEnemyScore().ToString();
        }

        public override void Update()
        {
            if (Input.GetButton(Constants.MAIN_MENU))
            {
                SceneManager.LoadScene(Constants.MAIN_MENU);
            }
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
                    WinnerSet = true;
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

        public override void RoundPlaying()
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

        public override void ResetBall()
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

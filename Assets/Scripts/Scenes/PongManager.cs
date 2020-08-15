using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Generic
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

        private static Generic.Ball _ballManager;
        private static GameObject _ball;
        private float _timer = 0;
        private bool _winnerSet = false;
        private static System.Random random = new System.Random();

        public virtual GameObject SceneBall
        {
            get { return _ball; }
            set { _ball = value; }
        }

        public virtual bool WinnerSet
        {
            get { return _winnerSet; }
            set { _winnerSet = value; }
        }


        // Use this for initialization
        public virtual void Start()
        {
            _gameMessageText.text = "";
            SceneBall = GameObject.FindGameObjectWithTag(Constants.BALL);
            _ballManager = SceneBall.GetComponent<Generic.Ball>();
            _playerScoreText.text = GameManager.GetPlayerScore().ToString();
            _enemyScoreText.text = GameManager.GetEnemyScore().ToString();
        }

        public virtual void Update()
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

        public virtual void RoundStarting()
        {
            ResetBall();
            GameManager.SetGameStep(Enums.GameStep.RoundPlaying);
        }

        public virtual void RoundPlaying()
        {
            if (!BallInPlay())
            {
                GameManager.SetGameStep(Enums.GameStep.RoundEnding);
            }
        }

        public virtual void RoundEnding()
        {
            if (!WinnerSet)
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
                    SceneManager.LoadScene("MainMenu");
                }
            }

        }

        public virtual bool BallInPlay()
        {
            return SceneBall.activeSelf;
        }

        public virtual void ResetBall()
        {

            SceneBall.SetActive(true);

            Vector3 startPosition = SceneBall.GetComponent<Rigidbody>().position;
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


            SceneBall.GetComponent<Rigidbody>().velocity = startingVector;

            startPosition.y = 0;
            SceneBall.GetComponent<Rigidbody>().position = startPosition;


        }

    }
}
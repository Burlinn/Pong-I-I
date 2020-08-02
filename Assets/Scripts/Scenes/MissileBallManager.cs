using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Missile
{
    public class MissileBallManager : MonoBehaviour
    {

        public float _roundStartDelay = 0f;
        public float _gameOverDelay = 3f;
        public float _roundOverDelay = 0f;
        public int _ballSpawnFromPlayer = -10;
        public int _ballSpawnFromEnemy = 10;
        public Text _playerScoreText;
        public Text _enemyScoreText;
        public Text _gameMessageText;
        public Text _fireText;

        private static BallManager _ballManager;
        private static GameObject _ball;
        private float _timer = 0;
        private bool _winnerSet = false;
        private static System.Random random = new System.Random();
        private static bool _canFire = false;
        private static bool _playerHit = false;

        // Use this for initialization
        public void Start()
        {
            _gameMessageText.text = "";
            _ball = GameObject.FindGameObjectWithTag("Ball");
            _ballManager = _ball.GetComponent<Missile.BallManager>();
            _playerScoreText.text = GameManager.GetPlayerScore().ToString();
            _enemyScoreText.text = GameManager.GetEnemyScore().ToString();
        }

        public void Update()
        {

            //If the player has been hit, display this message. 
            if (_playerHit)
            {
                _fireText.text = "ERROR! HIT!";
            }
            //If the player is able to fire, display this message.
            else if (_canFire)
            {
                _fireText.text = "Press SPACE to Fire";
            }
            else
            {
                _fireText.text = string.Empty;
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
                    _winnerSet = true;
                }
                RoundEnding();
            }
        }

        public static GameObject GetBall()
        {
            return _ball;
        }

        //Can Fire indicates the player can fire a missile
        public void CanFire(bool canFire)
        {
            _canFire = canFire;
        }

        //Player hit indicates the player was hit by an enemy missile
        public void PlayerHit(bool playerHit)
        {
            _playerHit = playerHit;
        }

        //Reset the ball and give it a random vector depending on the winner of the last round.
        private void RoundStarting()
        {
            ResetBall();
            GameManager.SetGameStep(Enums.GameStep.RoundPlaying);
        }

        //While the ball is in play, the round isn't over yet.
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
                //Update the winners score, and keep track of who won the round
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

                //If the game is over, display the winner.
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
                    //If the game's not over, load a random unplayed scene.
                    GameManager.SetGameStep(Enums.GameStep.RoundStarting);
                    SceneManager.LoadScene(GameManager.GetSceneByIndex(GameManager.GetPlayerScore() + GameManager.GetEnemyScore()));
                }

            }
            else
            {
                //If a player has won enough games to be crown winner, reset the game.
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

        //Give the ball a random starting vector depending on who won the last round.
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
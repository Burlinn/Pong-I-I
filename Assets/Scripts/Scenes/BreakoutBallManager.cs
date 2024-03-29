﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BreakoutBall
{
    public class BreakoutBallManager : Generic.PongManager
    {

        public Text _playerBreakoutScoreText;
        public Text _enemyBreakoutScoreText;

        private static Generic.Ball _ballManager;
        private float _timer = 0;
        private bool _winnerSet = false;
        private static bool _ballLastHitPlayer = false;

        private int _playerBreakoutScore = 0;
        private int _enemyBreakoutScore = 0;
        private int _brickCount = 0;
        private bool _gameStarted = false;
        private bool _playersBall = false;

        // Use this for initialization
        public void Start()
        {
            _gameMessageText.text = "";
            SceneBall = GameObject.FindGameObjectWithTag(Constants.BALL);
            _ballManager = SceneBall.GetComponent<Generic.Ball>();
            _playerScoreText.text = GameManager.GetPlayerScore().ToString();
            _enemyScoreText.text = GameManager.GetEnemyScore().ToString();
            _ballLastHitPlayer = GameManager.GetPlayerScoredLast();
            _brickCount = GameObject.FindGameObjectsWithTag("Brick").Length;
            _playerBreakoutScoreText.text = _playerBreakoutScore.ToString();
            _enemyBreakoutScoreText.text = _enemyBreakoutScore.ToString();
            ResetBall();
        }

        public void Update()
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
                    _winnerSet = true;
                }
                RoundEnding();
            }
        }

        public bool GetBallLastHitPlayer()
        {
            return _ballLastHitPlayer;
        }

        public void SetBallLastHitPlayer(bool ballLastHitPlayer)
        {
            _ballLastHitPlayer = ballLastHitPlayer;
        }

        public int GetPlayerBreakoutScore()
        {
            return _playerBreakoutScore;
        }

        public void SetPlayerBreakoutScore(int playerBreakoutScore)
        {
            _playerBreakoutScore += playerBreakoutScore;
            _playerBreakoutScoreText.text = _playerBreakoutScore.ToString();
        }

        public int GetEnemyBreakoutScore()
        {
            return _enemyBreakoutScore;
        }

        public void SetEnemyBreakoutScore(int enemyBreakoutScore)
        {
            _enemyBreakoutScore += enemyBreakoutScore;
            _enemyBreakoutScoreText.text = _enemyBreakoutScore.ToString();
        }

        public int GetBrickCount()
        {
            return _brickCount;
        }

        public void RemoveBrick()
        {
            _brickCount--;
        }

        public void SetBrickCountToZero()
        {
            _brickCount = 0;
        }


        public void SetPlayersBall(bool playersBall)
        {
            _ballLastHitPlayer = playersBall;
            _playersBall = playersBall;
        }

        public override void RoundEnding()
        {
            if (!_winnerSet)
            {
                if(_brickCount == 0)
                {
                    if(_playerBreakoutScore > _enemyBreakoutScore)
                    {
                        GameManager.SetPlayerScore(GameManager.GetPlayerScore() + 1);
                        _playerScoreText.text = GameManager.GetPlayerScore().ToString();
                        GameManager.SetPlayerScoredLast(true);
                    } else if(_enemyBreakoutScore > _playerBreakoutScore)
                    {
                        GameManager.SetEnemyScore(GameManager.GetEnemyScore() + 1);
                        _enemyScoreText.text = GameManager.GetEnemyScore().ToString();
                        GameManager.SetPlayerScoredLast(false);
                    }
                    else
                    {
                        SceneManager.LoadScene("BreakoutBall");
                    }
                } 

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
                    //string scene = GameManager.GetNextScene();
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

        public override void ResetBall()
        {

            SceneBall.SetActive(true);

            Vector3 startPosition = SceneBall.GetComponent<Rigidbody>().position;
            Vector3 startingVector = new Vector3();
            bool playersBall = false;
            if(_gameStarted == true)
            {
                playersBall = _playersBall;
            } else
            {
                playersBall = GameManager.GetPlayerScoredLast();
                _gameStarted = true;
            }

            if (playersBall)
            {
                startPosition.x = _ballSpawnFromPlayer;
            } else
            {
                startPosition.x = _ballSpawnFromEnemy;
            }

            startPosition.y = 0;
            SceneBall.transform.position = startPosition;

            //On Spawn, make the ball target a random brick
            startingVector = GetRandomBrick().transform.position - startPosition;

            SceneBall.GetComponent<Rigidbody>().velocity = startingVector;
        }

        private GameObject GetRandomBrick()
        {
            GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
            return bricks[Random.Range(0, bricks.Length - 1)];
        }
    }
}
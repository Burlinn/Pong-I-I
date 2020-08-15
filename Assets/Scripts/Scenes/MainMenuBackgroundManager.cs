using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MainMenuBackgroundManager : Generic.PongManager
    {
        private static Generic.Ball _ballManager;
        private float _timer = 0;
        private bool _winnerSet = false;
        private static System.Random random = new System.Random();
        private static int _drone1Score = 0;
        private static int _drone2Score = 0;

        // Use this for initialization
        public void Start()
        {
            SceneBall = GameObject.FindGameObjectWithTag(Constants.BALL);
            _ballManager = SceneBall.GetComponent<Generic.Ball>();
            _playerScoreText.text = "0";
            _enemyScoreText.text = "0";
        }

        public override void RoundEnding()
        {
            if (!_winnerSet)
            {
                if (GameManager.GetPlayerScoredLast() && GameManager.GetRoundHadWinner())
                {
                    _drone1Score += 1;
                    _playerScoreText.text = _drone1Score.ToString();
                }
                else if (GameManager.GetRoundHadWinner())
                {
                    _drone2Score += 1;
                    _enemyScoreText.text = _drone2Score.ToString();
                }

                GameManager.SetRoundHadWinner(false);

                if (_drone2Score == GameManager.GetScoreToWin() || _drone1Score == GameManager.GetScoreToWin())
                {
                    _winnerSet = true;
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
                    _drone1Score = 0;
                    _drone2Score = 0;
                    SceneManager.LoadScene("MainMenu");
                }
            }

        }

    }
}
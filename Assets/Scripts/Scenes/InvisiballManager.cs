using UnityEngine;
using UnityEngine.SceneManagement;

namespace Invisiball
{
    public class InvisiballManager : Generic.PongManager
    {
        private static BallInvisible _ballManager;

        // Use this for initialization
        public override void Start()
        {
            _gameMessageText.text = "";
            SceneBall = GameObject.FindGameObjectWithTag(Constants.BALL);
            _ballManager = SceneBall.GetComponent<BallInvisible>();
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

    }
}
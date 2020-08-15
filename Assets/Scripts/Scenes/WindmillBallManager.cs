using UnityEngine;

namespace Windmill
{
    public class WindmillBallManager : Generic.PongManager
    {
        private static BallWindmill _ballManager;

        // Use this for initialization
        public override void Start()
        {
            _gameMessageText.text = "";
            SceneBall = GameObject.FindGameObjectWithTag(Constants.BALL);
            _ballManager = SceneBall.GetComponent<Windmill.BallWindmill>();
            _playerScoreText.text = GameManager.GetPlayerScore().ToString();
            _enemyScoreText.text = GameManager.GetEnemyScore().ToString();
        }

    }
}
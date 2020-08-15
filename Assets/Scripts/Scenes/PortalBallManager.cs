using UnityEngine;

namespace Portal
{
    public class PortalBallManager : Generic.PongManager
    {

        private static Generic.Ball _ballManager;

        // Use this for initialization
        public override void Start()
        {
            _gameMessageText.text = "";
            SceneBall = GameObject.FindGameObjectWithTag(Constants.BALL);
            _ballManager = SceneBall.GetComponent<Generic.Ball>();
            _playerScoreText.text = GameManager.GetPlayerScore().ToString();
            _enemyScoreText.text = GameManager.GetEnemyScore().ToString();
        }
    }
}
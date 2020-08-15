using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Missile
{
    public class MissileBallManager : Generic.PongManager
    {
        public Text _fireText;

        private static BallMissile _ballManager;
        private static bool _canFire = false;
        private static bool _playerHit = false;
        private float _flashTimer = 0;
        private float _flashOnTime = .5f;
        private float _flashOffTime = .2f;
        private bool _hitMessageFlashOn = true;

        // Use this for initialization
        public override void Start()
        {
            _gameMessageText.text = "";
            SceneBall = GameObject.FindGameObjectWithTag(Constants.BALL);
            _ballManager = SceneBall.GetComponent<BallMissile>();
            _playerScoreText.text = GameManager.GetPlayerScore().ToString();
            _enemyScoreText.text = GameManager.GetEnemyScore().ToString();
            _fireText.text = "Press SPACE to Fire";
        }

        public override void Update()
        {
            if (Input.GetButton(Constants.MAIN_MENU))
            {
                SceneManager.LoadScene(Constants.MAIN_MENU);
            }

            HandleFireText();

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

        private void HandleFireText()
        {
            //If the player has been hit, display this message in flashing red text
            if (_playerHit)
            {
                _flashTimer += Time.deltaTime;

                if (_hitMessageFlashOn)
                {
                    _fireText.text = "ERROR! HIT!";
                    _fireText.color = Color.red;
                }
                else
                {
                    _fireText.text = string.Empty;
                }

                if (_hitMessageFlashOn && _flashTimer > _flashOnTime)
                {
                    _hitMessageFlashOn = false;
                    _flashTimer = 0;
                }
                else if (!_hitMessageFlashOn && _flashTimer > _flashOffTime)
                {
                    _hitMessageFlashOn = true;
                    _flashTimer = 0;
                }

            }
            //If the player is able to fire, display this message.
            else if (_canFire)
            {

                _fireText.text = "Press SPACE to Fire";
                _fireText.color = Color.white;
            }
            //If the player can't fire yet, but isn't stunned, display nothing
            else
            {
                _fireText.text = string.Empty;
            }
        }

    }
}
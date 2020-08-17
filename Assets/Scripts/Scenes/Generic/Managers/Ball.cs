using UnityEngine;

namespace Generic { 
    public class Ball : MonoBehaviour
    {
        //Speed of the ball
        public float _constantBallSpeed = 25;
        
        //Used to manipulate delta time
        public float _gameSpeed = 10;
        public float _redirectThreshold = .6f;
        public float _largePaddleRedirectThreshold = .9f;

        //Two variables to hold our scores
        public bool _playerScoredLast = true;
        public bool _roundHadWinner = false;

        private float _playerRedirectThreshold;
        private float _enemyRedirectThreshold;

        private Rigidbody _rigidBody;

        public virtual float ConstantBallSpeed
        {
            get { return _constantBallSpeed; }
            set { _constantBallSpeed = value; }
        }

        public virtual float GameSpeed
        {
            get { return _gameSpeed; }
            set { _gameSpeed = value; }
        }

        public virtual Rigidbody BallRigidbody
        {
            get { return _rigidBody; }
            set { _rigidBody = value; }
        }

        public virtual float PlayerRedirectThreshold
        {
            get { return _playerRedirectThreshold; }
            set { _playerRedirectThreshold = value; }
        }

        public virtual float EnemyRedirectThreshold
        {
            get { return _enemyRedirectThreshold; }
            set { _enemyRedirectThreshold = value; }
        }

        public virtual void Start()
        {
            BallRigidbody = this.GetComponent<Rigidbody>();
            if (GameManager.GetLargerPaddle())
            {
                PlayerRedirectThreshold = _largePaddleRedirectThreshold;
            }
            else
            {
                PlayerRedirectThreshold = _redirectThreshold;
            }
            EnemyRedirectThreshold = _redirectThreshold;
        }

        // Update is called once per frame
        public virtual void Update()
        {
            Move();
        }

        public virtual void OnCollisionEnter(Collision collision)
        {
            HandleWallCollision(collision);
            HandlePlayersCollision(collision);
        }

        public virtual void Move()
        {
            Vector3 ballVelocity = _rigidBody.velocity;
            Vector3 ballSpeed = ballVelocity.normalized * ConstantBallSpeed;
            _rigidBody.velocity = Vector3.Lerp(ballVelocity, ballSpeed, Time.deltaTime * GameSpeed);

            //If we hit the top or the bottom, bounce off of them. 
            if (transform.position.y > 8 || transform.position.y < -8)
            {
                Vector3 currentVelocity = _rigidBody.velocity;
                _rigidBody.velocity.Set(currentVelocity.x, -currentVelocity.y, currentVelocity.z);
            }

            //If we somehow make it past the cieling or floor, despawn the ball
            if (transform.position.y > 8.1 || transform.position.y < -8.1)
            {
                GameManager.SetRoundHadWinner(false);
                this.gameObject.SetActive(false);
            }

            //If the ball somehow ends up in a state where it's going up and down, nudge it in the right direction.
            if (_rigidBody.velocity.x < 1 && _rigidBody.velocity.x > 0)
            {
                _rigidBody.velocity = new Vector3(2, -2, 0);
            }
            else if (_rigidBody.velocity.x > -1 && _rigidBody.velocity.x <= 0)
            {
                _rigidBody.velocity = new Vector3(-2, 2, 0);
            }
        }

        public virtual void HandleWallCollision(Collision collision)
        {
            //Set the round winner
            if (collision.gameObject.name == Constants.LEFT_WALL)
            {
                GameManager.SetPlayerScoredLast(false);
                GameManager.SetRoundHadWinner(true);
                this.gameObject.SetActive(false);
            }
            if (collision.gameObject.name == Constants.RIGHT_WALL)
            {
                GameManager.SetPlayerScoredLast(true);
                GameManager.SetRoundHadWinner(true);
                this.gameObject.SetActive(false);
            }
        }

        public virtual void HandlePlayersCollision(Collision collision)
        {
            //Bounce in a direction depending on where it hits the player's paddle.
            if (collision.gameObject.name == Constants.PLAYER)
            {
                if (transform.position.y <= collision.transform.position.y - PlayerRedirectThreshold)
                {
                    _rigidBody.velocity = new Vector3(4, -3, 0);
                }
                if (transform.position.y >= collision.transform.position.y + PlayerRedirectThreshold)
                {
                    _rigidBody.velocity = new Vector3(4, 3, 0);
                }

            }

            //Bounce in a direction depending on where it hits the enemy's paddle.
            if (collision.gameObject.name == Constants.ENEMY)
            {
                if (transform.position.y <= collision.transform.position.y - EnemyRedirectThreshold)
                {
                    _rigidBody.velocity = new Vector3(-4, -3, 0);
                }
                if (transform.position.y >= collision.transform.position.y + EnemyRedirectThreshold)
                {
                    _rigidBody.velocity = new Vector3(-4, 3, 0);
                }

            }
        }
    }

}
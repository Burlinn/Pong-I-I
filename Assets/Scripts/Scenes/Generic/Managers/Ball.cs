using UnityEngine;

namespace Generic { 
    public class Ball : MonoBehaviour
    {
        //Speed of the ball
        public float _constantBallSpeed = 25;
        
        //Used to manipulate delta time
        public float _gameSpeed = 10;

        //Two variables to hold our scores
	    public bool _playerScoredLast = true;
        public bool _roundHadWinner = false;

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

        public virtual bool PlayerScoredLast
        {
            get { return _playerScoredLast; }
            set { _playerScoredLast = value; }
        }

        public virtual bool RoundHadWinner
        {
            get { return _roundHadWinner; }
            set { _roundHadWinner = value; }
        }

        public virtual Rigidbody BallRigidbody
        {
            get { return _rigidBody; }
            set { _rigidBody = value; }
        }

        public void Start()
        {
            BallRigidbody = this.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        public void Update()
        {
            Move();
        }

        public void OnCollisionEnter(Collision collision)
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
                _roundHadWinner = false;
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
            if (collision.gameObject.name == "LeftWall")
            {
                _playerScoredLast = false;
                _roundHadWinner = true;
                this.gameObject.SetActive(false);
            }
            if (collision.gameObject.name == "RightWall")
            {
                _playerScoredLast = true;
                _roundHadWinner = true;
                this.gameObject.SetActive(false);
            }
        }

        public virtual void HandlePlayersCollision(Collision collision)
        {
            //Bounce in a direction depending on where it hits the player's paddle.
            if (collision.gameObject.name == "Player")
            {
                if (transform.position.y <= collision.transform.position.y - .3)
                {
                    _rigidBody.velocity = new Vector3(4, -3, 0);
                }
                if (transform.position.y >= collision.transform.position.y + .3)
                {
                    _rigidBody.velocity = new Vector3(4, 3, 0);
                }

            }

            //Bounce in a direction depending on where it hits the enemy's paddle.
            if (collision.gameObject.name == "Enemy")
            {
                if (transform.position.y <= collision.transform.position.y - .3)
                {
                    _rigidBody.velocity = new Vector3(-4, -3, 0);
                }
                if (transform.position.y >= collision.transform.position.y + .3)
                {
                    _rigidBody.velocity = new Vector3(-4, 3, 0);
                }

            }
        }
    }

}
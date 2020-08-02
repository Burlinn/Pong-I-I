using UnityEngine;

namespace BreakoutBall
{
    public class BallManager : MonoBehaviour
    {
        //Speed of the ball
        public float _constantBallSpeed = 20;
        //Used to manipulate delta time
        public float _gameSpeed = 10;

        //Two variables to hold our scores
        public bool _playerScoredLast = true;
        public bool _roundHadWinner = false;
        private GameObject _ballInstance;
        private BreakoutBallManager _scene;

        public void Start()
        {
            _ballInstance = this.gameObject;
            _scene = GameObject.Find("SceneManager").GetComponent<BreakoutBallManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_scene.GetBrickCount() == 0) {
                _ballInstance.SetActive(false);
            } else
            {
                Vector3 ballVelocity = GetComponent<Rigidbody>().velocity;
                Vector3 ballSpeed = ballVelocity.normalized * _constantBallSpeed;
                GetComponent<Rigidbody>().velocity = Vector3.Lerp(ballVelocity, ballSpeed, Time.deltaTime * _gameSpeed);

                //If we hit the top or the bottom, bounce off of them. 
                if (transform.position.y > 8 || transform.position.y < -8)
                {
                    Vector3 currentVelocity = GetComponent<Rigidbody>().velocity;
                    GetComponent<Rigidbody>().velocity.Set(currentVelocity.x, -currentVelocity.y, currentVelocity.z);
                }

                //If we somehow make it past the cieling or floor, despawn the ball
                if (transform.position.y > 8.1 || transform.position.y < -8.1)
                {
                    _roundHadWinner = false;
                    _ballInstance.SetActive(false);
                }

                //If the ball moves past where the wall used to be, the game is over.
                if (transform.position.x > 14 || transform.position.x < -14)
                {
                    _scene.SetBrickCountToZero();
                }

                //If the ball somehow ends up in a state where it's going up and down, nudge it in the right direction.
                if (GetComponent<Rigidbody>().velocity.x < 1 && GetComponent<Rigidbody>().velocity.x > 0)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(2, -2, 0);
                }
                else if (GetComponent<Rigidbody>().velocity.x > -1 && GetComponent<Rigidbody>().velocity.x <= 0)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(-2, 2, 0);
                }
            }

        }

        private GameObject getBall()
        {
            return _ballInstance;
        }

        void OnCollisionEnter(Collision collision)
        {
            //If the ball touches the wall, the ball respawns next to the opponent and it's their ball. 
            if (collision.gameObject.name == "LeftWall")
            {
                _scene.SetPlayersBall(false);
                _scene.ResetBall();
            }
            else if (collision.gameObject.name == "RightWall")
            {
                _scene.SetPlayersBall(true);
                _scene.ResetBall();
            }
            else if (collision.gameObject.name == "Player")
            {
                //Bounce in a direction depending on where it hits the player's paddle.
                if (transform.position.y <= collision.transform.position.y - .3)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(4, -3, 0);
                }
                if (transform.position.y >= collision.transform.position.y + .3)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(4, 3, 0);
                }
                _scene.SetBallLastHitPlayer(true);
            } else if (collision.gameObject.name == "Enemy")
            {
                //Bounce in a direction depending on where it hits the enemy's paddle.
                if (transform.position.y <= collision.transform.position.y - .3)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(-4, -3, 0);
                }
                if (transform.position.y >= collision.transform.position.y + .3)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(-4, 3, 0);
                }
                _scene.SetBallLastHitPlayer(false);

            }

        }
    }

}
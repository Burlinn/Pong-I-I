using UnityEngine;
using System.Collections;

namespace Missile
{
    public class BallManager : MonoBehaviour
    {
        //Speed of the ball
        public float _constantBallSpeed = 20;
        //Used to manipulate delta time
        public float _gameSpeed = 10;

        public float _shotVelocity = 6;

        //Two variables to hold our scores
        public bool _playerScoredLast = true;
        public bool _roundHadWinner = false;
        private GameObject _ballInstance;
        private bool _shotByPlayer = false;
        private bool _shotByEnemy = false;

        public void Start()
        {
            _ballInstance = this.gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 ballVelocity = GetComponent<Rigidbody>().velocity;
            if (_shotByPlayer)
            {
                ballVelocity = new Vector3(ballVelocity.x + _shotVelocity, ballVelocity.y, ballVelocity.z);
            }
            if (_shotByEnemy)
            {
                ballVelocity = new Vector3(ballVelocity.x - _shotVelocity, ballVelocity.y, ballVelocity.z);
            }
            Vector3 ballSpeed = ballVelocity.normalized * _constantBallSpeed;
            GetComponent<Rigidbody>().velocity = Vector3.Lerp(ballVelocity, ballSpeed, Time.deltaTime * _gameSpeed);

            //If we hit the top or the bottom, bounce off of them. s
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

            //If the ball somehow ends up in a state where it's going up and down, nudge it in the right direction.
            if (GetComponent<Rigidbody>().velocity.x < 1 && GetComponent<Rigidbody>().velocity.x > 0)
            {
                Vector3 currentVelocity = GetComponent<Rigidbody>().velocity;
                GetComponent<Rigidbody>().velocity = new Vector3(2, -2, 0);
            }
            else if (GetComponent<Rigidbody>().velocity.x > -1 && GetComponent<Rigidbody>().velocity.x <= 0)
            {
                Vector3 currentVelocity = GetComponent<Rigidbody>().velocity;
                GetComponent<Rigidbody>().velocity = new Vector3(-2, 2, 0);
            }

        }

        private GameObject getBall()
        {
            return _ballInstance;
        }

        public void IsShot(bool byPlayer)
        {
            if (byPlayer)
            {
                _shotByPlayer = true;
            }
            else
            {
                _shotByEnemy = true;
            }
        }


        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name == "LeftWall")
            {
                _playerScoredLast = false;
                _roundHadWinner = true;
                _ballInstance.SetActive(false);
            }
            if (collision.gameObject.name == "RightWall")
            {
                _playerScoredLast = true;
                _roundHadWinner = true;
                _ballInstance.SetActive(false);
            }

            //Bounce in a direction depending on where it hits the player's paddle.
            if (collision.gameObject.name == "Player")
            {
                _shotByEnemy = false;
                if (transform.position.y <= collision.transform.position.y - .3)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(4, -3, 0);
                }
                if (transform.position.y >= collision.transform.position.y + .3)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(4, 3, 0);
                }

            }

            if (collision.gameObject.name == "Enemy")
            {
                _shotByPlayer = false;
                if (transform.position.y <= collision.transform.position.y - .3)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(-4, -3, 0);
                }
                if (transform.position.y >= collision.transform.position.y + .3)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(-4, 3, 0);
                }

            }

        }
    }

}
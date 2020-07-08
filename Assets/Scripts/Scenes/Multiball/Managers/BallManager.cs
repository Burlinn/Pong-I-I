using UnityEngine;
using System.Collections;

namespace Multiball
{
    public class BallManager : MonoBehaviour
    {
        //Speed of the ball
        public float _constantBallSpeed = 20;
        //Used to manipulate delta time
        public float _gameSpeed = 10;

        public static MultiballManager _scene;

        private void Start()
        {
            _scene = GameObject.FindGameObjectWithTag("Scene").GetComponent<MultiballManager>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 ballVelocity = GetComponent<Rigidbody>().velocity;
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
                GameManager.SetRoundHadWinner(false);
                this.gameObject.SetActive(false);
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

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name == "LeftWall")
            {
                GameManager.SetPlayerScoredLast(false);
                GameManager.SetRoundHadWinner(true);
                this.gameObject.SetActive(false);
            }
            if (collision.gameObject.name == "RightWall")
            {
                GameManager.SetPlayerScoredLast(true);
                GameManager.SetRoundHadWinner(true);
                this.gameObject.SetActive(false);
            }

            //Bounce in a direction depending on where it hits the player's paddle.
            if (collision.gameObject.name == "Player")
            {
                if (transform.position.y <= collision.transform.position.y - .3)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(4, -3, 0);
                }
                if (transform.position.y >= collision.transform.position.y + .3)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(4, 3, 0);
                }
                _scene.SetNextBallSpawnPoint(this.GetComponent<Rigidbody>().position);
                _scene.StartBallSpawnTimer();
                _scene.SetBallLastHitPlayer(true);

            } else if (collision.gameObject.name == "Enemy")
            {
                _scene.SetNextBallSpawnPoint(this.GetComponent<Rigidbody>().position);
                _scene.StartBallSpawnTimer();
                _scene.SetBallLastHitPlayer(false);
            }

        }
    }
}
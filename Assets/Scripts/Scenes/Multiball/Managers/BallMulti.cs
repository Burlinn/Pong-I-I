using UnityEngine;
using System.Collections;

namespace Multiball
{
    public class BallMulti : Generic.Ball
    {
        //Speed of the ball
        public float _constantMultipleBallSpeed = 10;

        private MultiballManager _scene;

        private new void Start()
        {
            BallRigidbody = this.GetComponent<Rigidbody>();
            ConstantBallSpeed = _constantMultipleBallSpeed;
            _scene = GameObject.Find(Constants.SCENE_MANAGER).GetComponent<MultiballManager>();
        }

        // Update is called once per frame
        new void Update()
        {
            Move();
        }

        new void OnCollisionEnter(Collision collision)
        {
			//Set the round winner
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
                    BallRigidbody.velocity = new Vector3(4, -3, 0);
                }
                if (transform.position.y >= collision.transform.position.y + .3)
                {
                    BallRigidbody.velocity = new Vector3(4, 3, 0);
                }
                _scene.SetNextBallSpawnPoint(BallRigidbody.position);
                _scene.StartBallSpawnTimer();
                _scene.SetBallLastHitPlayer(true);

            } 
			
			//Bounce in a direction depending on where it hits the enemy's paddle.
			else if (collision.gameObject.name == "Enemy")
            {
				if (transform.position.y <= collision.transform.position.y - .3)
                {
                    BallRigidbody.velocity = new Vector3(-4, -3, 0);
                }
                if (transform.position.y >= collision.transform.position.y + .3)
                {
                    BallRigidbody.velocity = new Vector3(-4, 3, 0);
                }
                _scene.SetNextBallSpawnPoint(BallRigidbody.position);
                _scene.StartBallSpawnTimer();
                _scene.SetBallLastHitPlayer(false);
            }

        }
    }
}
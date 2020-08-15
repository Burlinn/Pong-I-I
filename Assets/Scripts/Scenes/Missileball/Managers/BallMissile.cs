using UnityEngine;
using System.Collections;

namespace Missile
{
    public class BallMissile : Generic.Ball
    {
        //Speed of the ball
        public float _constantMissileBallSpeed = 20;

        public float _shotVelocity = 6;

        private bool _shotByPlayer = false;
        private bool _shotByEnemy = false;


        private new void Start()
        {
            BallRigidbody = this.GetComponent<Rigidbody>();
            ConstantBallSpeed = _constantMissileBallSpeed;
        }

        // Update is called once per frame
        new void Update()
        {
            Vector3 ballVelocity = BallRigidbody.velocity;

            //If the ball has been hit by a missile, it shoots forward
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

        //Let the ball know who it was hit by so we know what direction to shoot it off in.
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

    }

}
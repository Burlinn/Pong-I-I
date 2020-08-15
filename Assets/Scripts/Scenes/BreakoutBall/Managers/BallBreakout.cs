using UnityEngine;

namespace BreakoutBall
{
    public class BallBreakout : Generic.Ball
    {
        public float _constantBreakoutBallSpeed = 20;

        private BreakoutBallManager _scene;

        public new void Start()
        {
            ConstantBallSpeed = _constantBreakoutBallSpeed;
            BallRigidbody = this.GetComponent<Rigidbody>();
            _scene = GameObject.Find(Constants.SCENE_MANAGER).GetComponent<BreakoutBallManager>();
        }

        // Update is called once per frame
        new void Update()
        {
            if (_scene.GetBrickCount() == 0)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                //If the ball moves past where the wall used to be, the game is over.
                if (transform.position.x > 14 || transform.position.x < -14)
                {
                    _scene.SetBrickCountToZero();
                }
                Move();
            }
        }

        public new void OnCollisionEnter(Collision collision)
        {
            HandleWallCollision(collision);
            HandlePlayersCollision(collision);
        }

        new void HandleWallCollision(Collision collision)
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
        }
    }
}
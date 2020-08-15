using UnityEngine;

namespace BreakoutBall
{
    public class EnemyBreakout : Generic.Enemy
    {

        private BreakoutBallManager _scene;

        private void Start()
        {
            _scene = GameObject.Find("SceneManager").GetComponent<BreakoutBallManager>();
        }

        // Update is called once per frame
        void Update()
        {
            //If the enemy is currently behind, it should try to score more points
            if(_scene.GetPlayerBreakoutScore() >= _scene.GetEnemyBreakoutScore()) {
                Move();
            }
            //Otherwise, the enemy should try to end the game.
            else
            {
                if (_ball.transform.position.y > transform.position.y && _ball.transform.position.x > 10)
                {
                    transform.Translate(new Vector3(0, -_speed, 0) * Time.deltaTime);
                }
                if (_ball.transform.position.y < transform.position.y && _ball.transform.position.x > 10)
                {
                    transform.Translate(new Vector3(0, _speed, 0) * Time.deltaTime);
                }
            }

            CheckBounds();
        }
    }
}
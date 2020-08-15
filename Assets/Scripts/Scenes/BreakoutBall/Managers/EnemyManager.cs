using UnityEngine;

namespace BreakoutBall
{
    public class EnemyManager : MonoBehaviour
    {

        public int _speed = 8;

        public GameObject _ball;

        private global::BreakoutBall.BreakoutBallManager _scene;

        private void Start()
        {
            _scene = GameObject.Find("SceneManager").GetComponent<BreakoutBallManager>();
        }

        // Update is called once per frame
        void Update()
        {
            //If the enemy is currently behind, it should try to score more points
            if(_scene.GetPlayerBreakoutScore() >= _scene.GetEnemyBreakoutScore()) { 
                if (_ball.transform.position.y > transform.position.y)
                {
                    transform.Translate(new Vector3(0, _speed, 0) * Time.deltaTime);
                }
                if (_ball.transform.position.y < transform.position.y)
                {
                    transform.Translate(new Vector3(0, -_speed, 0) * Time.deltaTime);
                }
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

            //Check top bounds
            if (transform.position.y > 6.75)
            {
                Vector3 holdAtTop = transform.position;
                holdAtTop.y = 6.75f;
                transform.position = holdAtTop;
            }
            if (transform.position.y < -6.75)
            {
                Vector3 holdAtBottom = transform.position;
                holdAtBottom.y = -6.75f;
                transform.position = holdAtBottom;
            }
        }
    }
}
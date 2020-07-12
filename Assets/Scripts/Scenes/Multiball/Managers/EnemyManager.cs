using UnityEngine;
using System.Collections.Generic;

namespace Multiball
{
    public class EnemyManager : MonoBehaviour
    {

        public int _speed = 8;
        public List<GameObject> _balls;
        
        private MultiballManager _scene;
        private GameObject _closestBall;

        private void Start()
        {
            _scene = GameObject.Find("SceneManager").GetComponent<MultiballManager>();
        }

        // Update is called once per frame
        void Update()
        {
            GetClosestBall();

            //Make our enemy tracks the nearest ball
            if (_closestBall.transform.position.y > transform.position.y)
            {
                transform.Translate(new Vector3(0, _speed, 0) * Time.deltaTime);
            }
            if (_closestBall.transform.position.y < transform.position.y)
            {
                transform.Translate(new Vector3(0, -_speed, 0) * Time.deltaTime);
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

        //Find the ball closest to the Enemy
        private void GetClosestBall()
        {
            _balls = _scene.GetBalls();
            float farthestX = -12.5f;
            foreach(GameObject ball in _balls)
            {
                if(ball.transform.position.x > farthestX)
                {
                    farthestX = ball.transform.position.x;
                    _closestBall = ball;
                }
            }
        }
    }
}
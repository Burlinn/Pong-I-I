using UnityEngine;

namespace Pong
{
    public class EnemyManager : MonoBehaviour
    {

        public int _speed = 8;

        public GameObject _ball;

        // Update is called once per frame
        void Update()
        {
            //Make our enemy track the ball
            if (_ball.transform.position.y > transform.position.y)
            {
                transform.Translate(new Vector3(0, _speed, 0) * Time.deltaTime);
            }
            if (_ball.transform.position.y < transform.position.y)
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
    }
}
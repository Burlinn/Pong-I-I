using UnityEngine;

namespace Generic
{
    public class Enemy : MonoBehaviour
    {

        public float _speed = 8;

        public GameObject _ball;

        // Update is called once per frame
        void Update()
        {
            Move();
            CheckBounds();
        }

        public virtual float EnemySpeed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public virtual void CheckBounds()
        {
            //Don't let the enemy move out of bounds.
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

        public virtual void Move()
        {
            //Make our enemy track the ball
            if (_ball.transform.position.y > transform.position.y)
            {
                transform.Translate(new Vector3(0, EnemySpeed, 0) * Time.deltaTime);
            }
            if (_ball.transform.position.y < transform.position.y)
            {
                transform.Translate(new Vector3(0, -EnemySpeed, 0) * Time.deltaTime);
            }
        }
    }
}
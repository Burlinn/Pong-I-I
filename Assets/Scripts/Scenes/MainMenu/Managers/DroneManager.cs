using UnityEngine;

namespace MainMenu
{
    public class DroneManager : MonoBehaviour
    {

        public int _speed = 8;
        public GameObject _ball;
        public bool _isLeftSide = false;

        private bool _ballHeadingTowardsDrone = false;
        private Vector3 _ballLastPosition;

        private void Start()
        {
            _ballLastPosition = _ball.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if((_isLeftSide && this.transform.position.x + _ball.transform.position.x < this.transform.position.x + _ballLastPosition.x) ||
                (!_isLeftSide && this.transform.position.x + _ball.transform.position.x > this.transform.position.x + _ballLastPosition.x)
            ) {
                _ballHeadingTowardsDrone = true;
            }
            else
            {
                _ballHeadingTowardsDrone = false;
            }

            if (_ballHeadingTowardsDrone) { 
                //Make our enemy track the ball
                if (_ball.transform.position.y > transform.position.y)
                {
                    transform.Translate(new Vector3(0, _speed, 0) * Time.deltaTime);
                }
                if (_ball.transform.position.y < transform.position.y)
                {
                    transform.Translate(new Vector3(0, -_speed, 0) * Time.deltaTime);
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
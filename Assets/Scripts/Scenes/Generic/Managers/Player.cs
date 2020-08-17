using UnityEngine;

namespace Generic
{
    public class Player : MonoBehaviour
    {

        public float _speed = 12;
        public float _fasterSpeed = 18;
        public float _largerPaddle = 1.5f;

        private void Start()
        {
            if (GameManager.GetFasterPaddle())
            {
                _speed = _fasterSpeed;
            }
            if (GameManager.GetLargerPaddle())
            {
                Vector3 scale = transform.localScale;
                scale.y = scale.y * _largerPaddle;
                transform.localScale = scale;
            }
        }

        // Update is called once per frame
        void Update()
        {
            Move();
            CheckBounds();
        }

        public virtual float PlayerSpeed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public virtual void CheckBounds()
        {
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

        public virtual void Move()
        {
            if (Input.GetButton("UP"))
            {
                Vector3 vec3 = new Vector3(0, PlayerSpeed, 0);
                transform.Translate(vec3 * Time.deltaTime);
            }
            if (Input.GetButton("DOWN"))
            {
                Vector3 vec3 = new Vector3(0, -PlayerSpeed, 0);
                transform.Translate(vec3 * Time.deltaTime);
            }
        }
    }
}
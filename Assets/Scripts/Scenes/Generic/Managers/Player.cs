using UnityEngine;

namespace Generic
{
    public class Player : MonoBehaviour
    {

        public int _speed = 12;

        // Update is called once per frame
        void Update()
        {
            Move();
        }

        public virtual void Move()
        {
            if (Input.GetButton("UP"))
            {
                Vector3 vec3 = new Vector3(0, _speed, 0);
                transform.Translate(vec3 * Time.deltaTime);
            }
            if (Input.GetButton("DOWN"))
            {
                Vector3 vec3 = new Vector3(0, -_speed, 0);
                transform.Translate(vec3 * Time.deltaTime);
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
using UnityEngine;

namespace Windmill
{
    public class WindmillManager : MonoBehaviour
    {

        public int _speed = 10;

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.right * Time.deltaTime * _speed);

        }
    }
}
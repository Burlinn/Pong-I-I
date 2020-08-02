using UnityEngine;

namespace BreakoutBall
{
    public class ExplosionManager : MonoBehaviour
    {

        public float _timer;
        public float _timeToDestory = 2f;

        private void Start()
        {
            this.gameObject.GetComponent<ParticleSystem>().Play();
        }

        // Update is called once per frame
        void Update()
        {
            //Once the explosion particle effect has finish, self destruct.
            _timer += Time.deltaTime;

            if (_timer > _timeToDestory)
            {
                Destroy(this.gameObject);
            }
        }


    }
}
using UnityEngine;

namespace Missile
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
            _timer += Time.deltaTime;

            if (_timer > _timeToDestory)
            {
                Destroy(this.gameObject);
            }
        }


    }
}
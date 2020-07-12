using UnityEngine;

namespace Missile
{
    public class MissileManager : MonoBehaviour
    {

        //Speed of the missile
        public float _constantMissileSpeed = 20;
        //Used to manipulate delta time
        public float _gameSpeed = 10;
        public GameObject _explosion;

        private bool _startingVectorSet = false;

        private bool _isPlayerMissile;
        private ParticleSystem _particleSystem;

        private void Start()
        {
            _particleSystem = this.gameObject.GetComponent<ParticleSystem>();
            _particleSystem.Play();
        }

        // Update is called once per frame
        void Update()
        {
            if (!_startingVectorSet)
            {
                int startingVectorX = 0;
                if (transform.rotation.eulerAngles.z == 270)
                {
                    startingVectorX = 3;
                    _isPlayerMissile = true;
                }
                else
                {
                    startingVectorX = -3;
                    _isPlayerMissile = false;
                }
                this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(startingVectorX, 0, 0);
                _startingVectorSet = true;
            }

            Vector3 ballVelocity = GetComponent<Rigidbody>().velocity;
            Vector3 ballSpeed = ballVelocity.normalized * _constantMissileSpeed;
            GetComponent<Rigidbody>().velocity = Vector3.Lerp(ballVelocity, ballSpeed, Time.deltaTime * _gameSpeed);

        }


        void OnCollisionEnter(Collision collision)
        {

            Instantiate(_explosion, this.transform.position, Quaternion.identity);

            if (collision.gameObject.name == "Ball")
            {
                if (_isPlayerMissile)
                {
                    collision.gameObject.GetComponent<BallManager>().IsShot(true);
                }
                else
                {
                    collision.gameObject.GetComponent<BallManager>().IsShot(false);
                }
            }

            if (collision.gameObject.name == "Player")
            {
                collision.gameObject.GetComponent<PlayerManager>().Shot();
            }

            if (collision.gameObject.name == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyManager>().Shot();
            }

            Destroy(this.gameObject);

        }

        public void SetIsPlayerMissile(bool isPlayerMissile)
        {
            _isPlayerMissile = isPlayerMissile;
        }
    }
}
using UnityEngine;

namespace Missile
{
    public class Missile : MonoBehaviour
    {

        //Speed of the missile
        public float _constantMissileSpeed = 20;
        //Used to manipulate delta time
        public float _gameSpeed = 10;
        public GameObject _explosion;

        private bool _startingVectorSet = false;

        private bool _isPlayerMissile;
        private ParticleSystem _particleSystem;
        private Rigidbody _rigidbody;

        private void Start()
        {
            //Create our smoke trail
            _particleSystem = this.gameObject.GetComponent<ParticleSystem>();
            _particleSystem.Play();
            _rigidbody = this.gameObject.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!_startingVectorSet)
            {
                int startingVectorX = 0;
                //Figure out if we're a player or an enemy missile.
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
                //Begin moving the missile in a straight line.
                _rigidbody.velocity = new Vector3(startingVectorX, 0, 0);
                _startingVectorSet = true;
            }

            Vector3 ballVelocity = _rigidbody.velocity;
            Vector3 ballSpeed = ballVelocity.normalized * _constantMissileSpeed;
            _rigidbody.velocity = Vector3.Lerp(ballVelocity, ballSpeed, Time.deltaTime * _gameSpeed);

        }


        void OnCollisionEnter(Collision collision)
        {

            Instantiate(_explosion, this.transform.position, Quaternion.identity);

            //If we hit a ball, then the ball should shoot off in the direction it was hit into.
            if (collision.gameObject.name == Constants.BALL)
            {
                if (_isPlayerMissile)
                {
                    collision.gameObject.GetComponent<BallMissile>().IsShot(true);
                }
                else
                {
                    collision.gameObject.GetComponent<BallMissile>().IsShot(false);
                }
            }

            //If we hit a player or enemy, they're stunned for some amount of time.
            if (collision.gameObject.name == Constants.PLAYER)
            {
                collision.gameObject.GetComponent<PlayerMissile>().Shot();
            }

            if (collision.gameObject.name == Constants.ENEMY)
            {
                collision.gameObject.GetComponent<EnemyMissile>().Shot();
            }

            Destroy(this.gameObject);

        }

    }
}
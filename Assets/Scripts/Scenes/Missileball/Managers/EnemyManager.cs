using UnityEngine;

namespace Missile
{
    public class EnemyManager : MonoBehaviour
    {

        public int _speed = 12;
        public GameObject _missilePrefab;
        public GameObject _ball;
        public float _shotVelocity = 10;
        public float _timeBetweenShots = 2f;
        public float _shotTimer = 0;
        public GameObject _explosion;
        public float _timeStuck = 4f;
        public float _stuckTimer = 0f;

        private Vector3 _missileSpawnPoint;
        private bool _isShot = false;
        private float _missileSpawnX = 11.8f;
        private float _missileSpawnRotationZ = 90;

        // Update is called once per frame
        void Update()
        {
            //If Enemy has been shot, don't let them move utnil the timer is finished.
            if (_isShot)
            {
                _stuckTimer += Time.deltaTime;
                if (_stuckTimer > _timeStuck)
                {
                    _isShot = false;
                    _stuckTimer = 0;
                }
            }
            else { 
                //Make our enemy track the ball
                if (_ball.transform.position.y > transform.position.y)
                {
                    transform.Translate(new Vector3(0, _speed, 0) * Time.deltaTime);
                }
                if (_ball.transform.position.y < transform.position.y)
                {
                    transform.Translate(new Vector3(0, -_speed, 0) * Time.deltaTime);
                }

                //If the Enemy is able to shoot, they do so immediatley, resetting the timer til they can shoot again.
                _shotTimer += Time.deltaTime;

                if (_shotTimer > _timeBetweenShots)
                {
                    _missileSpawnPoint = this.transform.position;
                    _missileSpawnPoint.x = _missileSpawnX;
                    GameObject newMissile = Instantiate(_missilePrefab, _missileSpawnPoint, Quaternion.identity);
                    newMissile.transform.Rotate(0, 0, _missileSpawnRotationZ);
                    _shotTimer = 0;
                }
            }

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

        //Set IsShot and instantiate an explosion.
        public void Shot()
        {
            _isShot = true;
            Instantiate(_explosion, this.transform.position, Quaternion.identity);
        }
    }
}
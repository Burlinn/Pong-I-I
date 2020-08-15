using UnityEngine;

namespace Missile
{
    public class EnemyMissile : Generic.Enemy
    {

        public GameObject _missilePrefab;
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
                Move();

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

            CheckBounds();
        }

        //Set IsShot and instantiate an explosion.
        public void Shot()
        {
            _isShot = true;
            Instantiate(_explosion, this.transform.position, Quaternion.identity);
        }
    }
}
using UnityEngine;
using System;

namespace Invisiball { 
    public class BallInvisible : Generic.Ball
    {
        private float _currentLocation;
        private float _transparency;
        private Renderer _renderer;
        private MeshRenderer _meshRenderer;


        public override void Start()
        {
            BallRigidbody = this.GetComponent<Rigidbody>();
            _renderer = this.gameObject.GetComponent<Renderer>();
            _meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
            GameManager.SetRoundHadWinner(false);
        }

        // Update is called once per frame
        public override void Update()
        {
            Move();
            SetVisibility();
        }

        void SetVisibility()
        {
            _currentLocation = Math.Abs(BallRigidbody.position.x);
            //On a white material, transparency takes affect when the alpha channel is down to .01. Dividing by 50 to make
            //it EXTRA invisible.
            _transparency = _currentLocation / 12.5f / 50;

            var color = _renderer.material.color;
            color.a = _transparency;
            _renderer.material.SetColor("_Color", color);

            //Make ball invisible as it gets close to players.
            if (this.transform.position.x > 6 || this.transform.position.x < -6)
            {
                _meshRenderer.enabled = false;
            }
            else
            {
                _meshRenderer.enabled = true;
            }
        }
    }

}
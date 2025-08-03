using UnityEngine;

namespace Orbitals
{
    public class Station : OrbitalBody
    {
        private GameObject _parent;
        private float _rotationalSpeed;
        private float _orbitRadius;
        private float _orbitAngle;
        
        // TODO - Make this a config value
        private const int RotationalMultiplier = 1000;

        public void InitializeStation(GameObject parent)
        {
            _parent = parent;

            _orbitRadius = Vector3.Distance(transform.position, parent.transform.position);

            // Sanity check that we're not going to divide by zero...
            if (_orbitRadius < 0.1f) _orbitRadius = 0.1f;

            // Generic rotational speed
            _rotationalSpeed = (1f / _orbitRadius) * RotationalMultiplier;

            // Start orbit at the current angle
            var offset = transform.position - _parent.transform.position;
            _orbitAngle = Mathf.Atan2(offset.z, offset.x);
        }

        private void Update()
        {
            if (!_parent) return;

            // Spin the station around its local axis
            transform.Rotate(Vector3.down, _rotationalSpeed * Time.deltaTime, Space.Self);

            // Update the orbit angle
            _orbitAngle += _rotationalSpeed * Mathf.Deg2Rad * Time.deltaTime;

            // Calculate new orbit position
            var x = Mathf.Cos(_orbitAngle) * _orbitRadius;
            var z = Mathf.Sin(_orbitAngle) * _orbitRadius;

            // Apply position in world space
            transform.position = _parent.transform.position + new Vector3(x, 0, z);
        }
    }
}
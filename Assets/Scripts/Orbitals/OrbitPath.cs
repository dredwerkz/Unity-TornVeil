using UnityEngine;

namespace Orbitals
{
    [RequireComponent(typeof(LineRenderer))]
    public class OrbitPath : MonoBehaviour
    {
        public Transform orbitCenter;  // Parent body transform
        public float orbitRadius; // Match the orbit path
        public int segments = 100;     // Path smoothness
        public Color lineColor = Color.white;
    
        private LineRenderer _lineRenderer;

        public void Initialize(Transform center, float radius, Color color)
        {
            orbitCenter = center;
            orbitRadius = radius;
            lineColor = color;

            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.useWorldSpace = true;
            _lineRenderer.loop = true;
            _lineRenderer.widthMultiplier = 0.5f;
            _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            _lineRenderer.startColor = lineColor;
            _lineRenderer.endColor = lineColor;

            DrawOrbit();
        }

        private void DrawOrbit()
        {
            if (!orbitCenter) return;
            
            var pointsCount = segments;
            _lineRenderer.positionCount = pointsCount;

            for (var i = 0; i < pointsCount; i++)
            {
                var angle = (float)i / segments * Mathf.PI * 2f;
                var x = Mathf.Cos(angle) * orbitRadius;
                var z = Mathf.Sin(angle) * orbitRadius;
                _lineRenderer.SetPosition(i, orbitCenter.position + new Vector3(x, 0, z));
            }
        }
    }
}
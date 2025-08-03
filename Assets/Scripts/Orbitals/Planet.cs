using Orbitals;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class Planet : OrbitalBody
{
    private Vector3 _parentLocation;
    private float _distanceToParent;
    private float _rotationalSpeed;
    
    // TODO - Make this a config value
    private const int RotationalMultiplier = 1000;


    private void Awake()
    {
        // Awake is the first thing this class does, so get all the info we'll need now.
        // The Star won't move so we don't need to update position
        _parentLocation = GameObject.Find("System_Star").transform.localPosition;
        _distanceToParent = Vector3.Distance(transform.position, _parentLocation);

        // Sanity check that we're not going to divide by zero...
        if (_distanceToParent < 0.1f) _distanceToParent = 0.1f;

        // Generic rotational speed, feels about right - not important!
        _rotationalSpeed = (1f / _distanceToParent) * RotationalMultiplier;
    }
    
    private void Start()
    {
        var orbitPath = new GameObject($"{name}_OrbitPath");
        orbitPath.AddComponent<LineRenderer>();
        var path = orbitPath.AddComponent<OrbitPath>();
    
        path.Initialize(
            GameObject.Find("System_Star").transform,
            _distanceToParent,
            Color.cyan
        );
    }

    private void Update()
    {
        // Rotation around local axis
        transform.RotateAround(transform.localPosition, Vector3.down, _rotationalSpeed * Time.deltaTime);

        // Rotation around parent axis
        transform.RotateAround(_parentLocation, Vector3.down, _rotationalSpeed * Time.deltaTime);
    }
}   
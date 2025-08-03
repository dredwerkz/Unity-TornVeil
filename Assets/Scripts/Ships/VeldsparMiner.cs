using TMPro;
using UnityEngine;

// TODO - Create base "Miner : Ship" classes! 

// ReSharper disable once CheckNamespace
public class VeldsparMiner : MonoBehaviour
{
    // Mining
    private float _veldsparOnBoard;
    private const float VeldsparPerSecond = 1f;
    private const float MineSpeedUpgrades = 1f;

    // Labels
    private TextMeshPro _veldsparLabel;
    private TextMeshPro _rateLabel;

    // Movement
    private Transform _factoryTarget;
    private Transform _asteroidTarget;
    private Transform _currentTarget;
    private const float Speed = 0.5f;
    private Vector3 _targetOffsetPosition; // Persistent per target

    public void Initialize()
    {
        // Init Mining
        _veldsparOnBoard = 0f;
        CreateLabels();

        // Find targets in the scene
        _factoryTarget = GameObject.Find("MinerFactory").transform;
        _asteroidTarget = GameObject.Find("Asteroids").transform;

        // Start by going to Asteroids
        _currentTarget = _asteroidTarget;
        _targetOffsetPosition = GetClosebyPosition(_currentTarget.position);
    }

    private void Update()
    {
        HandleMovementLoop();
    }

    public void Mine()
    {
        _veldsparOnBoard += VeldsparPerSecond * MineSpeedUpgrades;

        // Update the label only when we mine
        if (_veldsparLabel)
            _veldsparLabel.text = $"{_veldsparOnBoard:0.0}";
    }

    public float getVeldsparOnBoard()
    {
        return _veldsparOnBoard;
    }

    private void HandleMovementLoop()
    {
        if (!_currentTarget) return;
        
        var t = Speed * Time.deltaTime;
        t = 1f - Mathf.Pow(1f - t, 3f); // Ease out faster

        // Move towards the persistent offset target
        transform.position = Vector3.Lerp(transform.position, _targetOffsetPosition, t);

        // Check if reached
        var distance = Vector3.Distance(transform.position, _targetOffsetPosition);
        if (distance < 0.1f)
        {
            SwitchTarget();
        }
    }

    private void SwitchTarget()
    {
        // Switch between factory and asteroids
        // TODO - This shit ain't scale-able but I kinda get the gist
        if (_currentTarget == _asteroidTarget)
        {
            _currentTarget = _factoryTarget;
        }
        else
        {
            _currentTarget = _asteroidTarget;
        }

        // Calculate a single random offset *once* for this target
        _targetOffsetPosition = GetClosebyPosition(_currentTarget.position);
    }

    private Vector3 GetClosebyPosition(Vector3 targetPosition)
    {
        // Pick a random angle in radians
        var angle = Random.Range(0f, Mathf.PI * 2f);

        // Pick a random radius between inner and outer to create a ring
        const float radius = 30f;

        // Calculate position in a circle
        var xOffset = Mathf.Cos(angle) * radius;
        var yOffset = Mathf.Sin(angle) * radius;

        // Return position in world space
        return targetPosition + new Vector3(xOffset, yOffset, 0f);
    }

    private void CreateLabels()
    {
        // Create a parent for labels above the sphere
        var labelParent = new GameObject("MinerLabels");
        labelParent.transform.SetParent(transform);
        labelParent.transform.localPosition = new Vector3(0, 0, 0);

        // Veldspar onboard label
        var veldsparObj = new GameObject("VeldsparOnBoard");
        veldsparObj.transform.SetParent(labelParent.transform);
        veldsparObj.transform.localPosition = new Vector3(0, -10f, 0);
        
        _veldsparLabel = veldsparObj.AddComponent<TextMeshPro>();
        _veldsparLabel.fontSize = 45;
        _veldsparLabel.alignment = TextAlignmentOptions.Center;
        _veldsparLabel.color = Color.yellow;
        _veldsparLabel.text = "0";

        // Mining rate label
        var rateObj = new GameObject("MiningRate");
        rateObj.transform.SetParent(labelParent.transform);
        rateObj.transform.localPosition = new Vector3(0, -15f, 0);
        
        _rateLabel = rateObj.AddComponent<TextMeshPro>();
        _rateLabel.fontSize = 35f;
        _rateLabel.alignment = TextAlignmentOptions.Center;
        _rateLabel.color = Color.green;
        _rateLabel.text = $"{VeldsparPerSecond * MineSpeedUpgrades:0.0}/s";
    }
}
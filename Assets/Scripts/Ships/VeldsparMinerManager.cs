using System.Collections.Generic;
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class VeldsparMinerManager : MonoBehaviour
{
    public TextMeshProUGUI veldsparText;
    private readonly List<VeldsparMiner> _veldsparMiners = new();
    public float veldspar = 0f;
    private float _globalTimer;
    private float _nextMineTimer;
    private float _nextTotUpTimer;

    private void Update()
    {
        // Get a consistent time stamp for the function's runtime
        _globalTimer = Time.time;
        
        if (_globalTimer > _nextMineTimer)
        {
            foreach (var veldsparMiner in _veldsparMiners)
            {
                veldsparMiner.Mine();
            }

            _nextMineTimer = _globalTimer + 1f;
        }

        if (_globalTimer >= _nextTotUpTimer)
        {
            var newTotalVeldspar = 0f;
            
            foreach (var veldsparMiner in _veldsparMiners)
            {
                newTotalVeldspar += veldsparMiner.getVeldsparOnBoard();
                veldspar = newTotalVeldspar;
            }

            _nextTotUpTimer = _globalTimer + 5f;
        }

        veldsparText.text = "Veldspar: " + Mathf.FloorToInt(veldspar).ToString();
    }


    public void AddMiner()
    {
        Debug.Log("Adding a Veldspar miner");

        var minerFactory = GameObject.Find("MinerFactory");
        var factoryPosition = minerFactory.transform.position;

        // Create a sphere in the world
        var minerSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        
        minerSphere.transform.position = GetApproximateSpawnPosition(factoryPosition);
        minerSphere.transform.localScale = new Vector3(1, 1, 1) * 10f;

        // Attach the VeldsparMiner script to it
        var miner = minerSphere.AddComponent<VeldsparMiner>();

        // Initialize and start mining
        miner.Initialize();
        miner.Mine();

        // Keep track of it
        _veldsparMiners.Add(miner);
    }

    private Vector3 GetApproximateSpawnPosition(Vector3 factoryPosition)
    {
        // Pick a random angle in radians
        var angle = Random.Range(0f, Mathf.PI * 2f);

        // Pick a random radius between inner and outer to create a ring
        const float radius = 30f;

        // Calculate position in a circle
        var xOffset = Mathf.Cos(angle) * radius;
        var yOffset = Mathf.Sin(angle) * radius;

        // Return position in world space
        return factoryPosition + new Vector3(xOffset, yOffset, 0f);
    }
}
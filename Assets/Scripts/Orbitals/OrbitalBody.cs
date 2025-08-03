using UnityEngine;

// ReSharper disable once CheckNamespace
public class OrbitalBody : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.down, Time.deltaTime);
    }
}

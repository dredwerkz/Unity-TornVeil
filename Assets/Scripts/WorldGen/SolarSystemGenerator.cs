using System.Collections.Generic;
using Orbitals;
using UnityEngine;
using WorldGen;

// ReSharper disable once CheckNamespace
public class SolarSystemGenerator : MonoBehaviour
{
    private readonly PlanetGenerator _planetGenerator = new PlanetGenerator();
    private readonly OrbitalStationGenerator _orbitalStationGenerator = new OrbitalStationGenerator();
    private List<GameObject> _planets = new();

    public void GenerateStar()
    {
        // Called at worldgen
        var starSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        starSphere.name = "System_Star";

        // Set star to center, set size
        starSphere.transform.position = new Vector3(0, 0, 0);
        starSphere.transform.localScale = new Vector3(50, 50, 50);

        // Add color
        starSphere.GetComponent<Renderer>().material.color = Color.lightGoldenRodYellow;

        // Add the Star class to the GameObject
        _ = starSphere.AddComponent<Star>();

        // Add planets to the star!
        _planets = _planetGenerator.GeneratePlanets();

        // Seed solar system with default Orbital Stations
        _orbitalStationGenerator.GenerateOrbitalStations(1, _planets);
        
    }



}
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Orbitals;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class SolarSystemGenerator : MonoBehaviour
{
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
        GeneratePlanets();

        // Add a station to a random planet
        GenerateOrbitalStation(1);
        GenerateOrbitalStation(2);
        GenerateOrbitalStation(3);
    }

    private void GeneratePlanets()
    {
        // Placeholder hard coded generation logic, will eventually need to figure this out algorithmically
        const int bodyCount = 5;
        
        // Gets a list of floats for the planet apoapsides
        var randomDistance = Utilities.GetRandomApoapsides(bodyCount);

        // Sort them so the naming goes 1-X from inner to outer
        randomDistance.Sort();

        // For each of those floats, generate a world-space location, then create a planet component
        for (var i = 0; i < randomDistance.Count; i++)
        {
            var orbitalPosition = Utilities.GetRandomOrbitalDistanceVector(
                GameObject.Find("System_Star").transform.localPosition,
                randomDistance[i]
            );

            GeneratePlanetComponent(i, orbitalPosition);
        }
    }

    private void GeneratePlanetComponent(int id, Vector3 orbitalPosition)
    {
        // Create sphere
        var planet = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        // Give it a trackable name
        planet.name = $"OrbitalBody_{id}";

        // Use the injected position
        planet.transform.position = orbitalPosition;

        // Set its scale TODO - Randomise
        planet.transform.localScale = new Vector3(20, 20, 20);

        // Set colour and add the Planet class to GameObject
        planet.GetComponent<Renderer>().material.color = Color.forestGreen;
        planet.AddComponent<Planet>();
        
        // Add the planet to the list so we can do shit with it later
        _planets.Add(planet);
    }

    private void GenerateOrbitalStation(int id)
    {
        // Create Station Object
        
        var orbitalStation = GameObject.CreatePrimitive(PrimitiveType.Cube);
        
        orbitalStation.name = $"OrbitalStation_{id}";
        
        var randomIndex = Random.Range(0, _planets.Count);
        var randomPlanet = _planets[randomIndex];

        var vector = Utilities.GetRandomOrbitalDistanceVector(randomPlanet.transform.localPosition, 20f);

        orbitalStation.transform.localPosition = vector;
        orbitalStation.transform.localScale = new Vector3(5, 5, 5);

        orbitalStation.GetComponent<Renderer>().material.color = Color.indianRed;
        
        orbitalStation.AddComponent<Station>();
        orbitalStation.GetComponent<Station>().InitializeStation(randomPlanet);
    }

    
}
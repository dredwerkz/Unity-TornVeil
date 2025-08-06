using System.Collections.Generic;
using Orbitals;
using UnityEngine;

namespace WorldGen
{
    public class PlanetGenerator
    {
        public List<GameObject> GeneratePlanets()
        {
            // List of planets to eventually return
            List<GameObject> planets = new();
            
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

                var planet = GeneratePlanetComponent(i, orbitalPosition);
                planets.Add(planet);
            }

            return planets;
        }

        public GameObject GeneratePlanetComponent(int id, Vector3 orbitalPosition)
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
            return planet;
        }
    }
}
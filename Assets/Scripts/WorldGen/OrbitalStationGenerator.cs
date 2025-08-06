using System.Collections.Generic;
using Orbitals;
using UnityEngine;
using UtilityScripts;

namespace WorldGen
{
    public class OrbitalStationGenerator
    {
        public void GenerateOrbitalStations(int id, List<GameObject> planets)
        {
            // Set magic numbers (eek)
            const int stationsPerSystem = 4;
            const int planetsToPopulate = 3;
            var orbitalDistances = new[] { 15f, 25f, 20f, 20f };

            // Safety for planet count
            if (planets.Count < planetsToPopulate)
            {
                Debug.LogWarning("Not enough planets to populate orbital stations.");
                return;
            }

            // Pick planets to populate
            var populatedPlanets = PickRandomPlanets(planets, planetsToPopulate);

            // Generate station positions
            var stationPositions = GenerateStationPositions(populatedPlanets, orbitalDistances);

            // Assign stations to planets
            var stationTypes = new[] { "Shipyard", "Refinery", "Haulage_Depot", "Observation_Post" };
            
            for (var i = 0; i < stationsPerSystem; i++)
            {
                // Add two stations to the first planet then scatter one-by-one
                AddStation(stationTypes[i], stationPositions[i], populatedPlanets[i < 2 ? 0 : i - 1]);
            }
        }

        private static List<GameObject> PickRandomPlanets(List<GameObject> planets, int count)
        {
            // Init the list we'll return
            var chosen = new List<GameObject>();
            
            for (var i = 0; i < count; i++)
            {
                // Grab a planet then pop it
                var index = Random.Range(0, planets.Count);
                chosen.Add(planets[index]);
                planets.RemoveAt(index);
            }

            return chosen;
        }

        private static List<Vector3> GenerateStationPositions(List<GameObject> planets, float[] distances)
        {
            // Init list of positions
            var positions = new List<Vector3>();

            for (var i = 0; i < distances.Length; i++)
            {
                // Return the planet index against the iterator value..
                var planetIndex = i switch
                {
                    0 => 0,
                    1 => 0, // First planet gets 2 stations
                    2 => 1,
                    3 => 2,
                    _ => 0
                };

                // Get your positions here..
                var position = Utilities.GetRandomOrbitalDistanceVector(
                    planets[planetIndex].transform.localPosition,
                    distances[i]
                );

                // Avoid overlapping the first two stations..
                if (i == 1 && Vector3.Distance(position, positions[0]) < 10f)
                {
                    position = new Vector3(-position.x, -position.y, position.z);
                }

                // Add to list and run up again
                positions.Add(position);
            }

            // Should have all our positions now
            return positions;
        }

        private static void AddStation(string name, Vector3 position, GameObject planet)
        {
            // Create physical GameObject for the station
            var orbitalStation = GameObject.CreatePrimitive(PrimitiveType.Cube);
            orbitalStation.name = name;
            orbitalStation.transform.localPosition = position;
            orbitalStation.transform.localScale = Vector3.one * 5f;

            // Make it red (placeholder)
            var renderer = orbitalStation.GetComponent<Renderer>();
            if (renderer) renderer.material.color = Color.indianRed;

            // Add our MonoBehaviour to it and init!
            var stationComponent = orbitalStation.AddComponent<Station>();
            stationComponent.InitializeStation(planet);
        }
    }
}
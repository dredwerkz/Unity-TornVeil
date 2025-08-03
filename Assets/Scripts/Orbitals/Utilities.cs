using System.Collections.Generic;
using System.Linq;
using Config;
using UnityEngine;

namespace Orbitals
{
    public static class Utilities
    {

        /// <summary>
        /// Function to get a list of apoapsides for use when generating a series of objects around a parent stellar body
        /// </summary>
        /// <param name="count">The number of apoapsides to generate</param>
        /// <param name="multiplier">The multiplier to apply to the default lowerRange and upperRange of 100f/500f</param>
        /// <returns></returns>
        public static List<float> GetRandomApoapsides(int count, float multiplier = 1)
        {
            // TODO - At some point move these into a config
            var lowerRange = TornVeilGlobalConfig.StarSystemUpperRange * multiplier;
            var upperRange = TornVeilGlobalConfig.StarSystemLowerRange * multiplier;

            List<float> result = new();

            var maxAttempts = 1000; // safety to prevent infinite loops

            // While the result List is not as large as the 'count' value generate random apoapsides
            while (result.Count < count && maxAttempts-- > 0)
            {
                var candidate = Random.Range(lowerRange, upperRange);

                // Check distance from all existing numbers
                var tooClose = result.Any(r => Mathf.Abs(candidate - r) < 50f);

                if (!tooClose)
                {
                    result.Add(candidate);
                }
            }

            // If we've got all our valid candidates, return early, otherwise...
            if (result.Count >= count) return result;

            Debug.Log("Attempts expired. Forcing apoapsides outside of range.");

            // Sort them so they're in order since we'll be accessing based on that
            result.Sort();

            // Get how many we need to force
            var missingCount = count - result.Count;

            // Checks to see if 'result' actually has anything, if so get the last entry and add 50 to it (or just use upper range)
            var forcedValue = (result.Count > 0 ? result[^1] : upperRange) + 50f;

            // We now know the highest value so just fill out the rest of the list.
            for (var i = 0; i < missingCount; i++)
            {
                result.Add(forcedValue);
                forcedValue += 50f;
            }

            return result;
        }
        
        /// <summary>
        /// Returns a Vector3 position in random orbit relative to a parent GameObject's Vector.
        /// </summary>
        /// <param name="parent">The parent stellar body's Vector3 position, i.e. a Star would be 0,0,0</param>
        /// <param name="radius">The distance from the parent object the child position should be generated</param>
        /// <returns></returns>
        public static Vector3 GetRandomOrbitalDistanceVector(Vector3 parent, float radius)
        {
            // Pick a random angle in radians
            var angle = Random.Range(0f, Mathf.PI * 2f);

            // Calculate position in a circle
            var xOffset = Mathf.Cos(angle) * radius;
            var zOffset = Mathf.Sin(angle) * radius;

            // Create a Vector3 using the calculated position
            var orbitalPosition = new Vector3(xOffset, 0f, zOffset);

            // Return position in world space against the parent location
            return orbitalPosition + parent;
        }
    }
}
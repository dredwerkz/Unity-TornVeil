namespace Config
{
    public static class TornVeilGlobalConfig
    {
        /// <summary>
        ///  RotationalMultiplier for OrbitalBodies, orbiting around its parent and on its own axis.
        ///  A value of 500 is standard, while 50000 make rotation easily visible.
        /// </summary>
        public const int RotationalMultiplier = 500;

        /// <summary>
        /// Furthest distance bodies can naturally spawn from the star (before forced spawning)
        /// </summary>
        public const float StarSystemUpperRange = 1500f;
        
        /// <summary>
        /// Closest distance bodies can naturally spawn to the star
        /// </summary>
        public const float StarSystemLowerRange = 250f;
    }
}
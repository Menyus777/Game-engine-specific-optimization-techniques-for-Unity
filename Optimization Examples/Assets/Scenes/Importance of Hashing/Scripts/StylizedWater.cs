using UnityEngine;

namespace OptimizationExamples.ImportanceOfHashing.Shaders
{
    /// <summary>
    /// Property mappings for the StylizedWater shader
    /// </summary>
    public static class StylizedWater
    {
        public static readonly int _Color =
            Shader.PropertyToID(nameof(_Color));

        public static readonly int _FogColor =
            Shader.PropertyToID(nameof(_FogColor));

        public static readonly int _IntersectionColor =
            Shader.PropertyToID(nameof(_IntersectionColor));

        public static readonly int _FoamThreshold =
            Shader.PropertyToID(nameof(_FoamThreshold));
    }
}

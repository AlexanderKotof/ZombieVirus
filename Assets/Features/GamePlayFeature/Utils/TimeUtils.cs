using Features.GamePlayFeature.Systems;
using FeatureSystem.Systems;
using System.Collections;
using UnityEngine;

namespace Features.GamePlayFeature.Utils
{
    public static class TimeUtils
    {
        public static IEnumerator WaitForTime(float time)
        {
            return GameSystems.GetSystem<GameTimeSystem>().WaitForTime(time);
        }

        public static IEnumerator WaitForTimeIndependent(float time)
        {
            return GameSystems.GetSystem<GameTimeSystem>().WaitForTimeIndependent(time);
        }

        public static float GetDeltaTime()
        {
            return Time.deltaTime;
        }

        public static float GetIndependentDeltaTime()
        {
            return Time.unscaledDeltaTime;
        }

        public static float GetTimeSinceStart()
        {
            return GameSystems.GetSystem<GameTimeSystem>().TimeSinceStart;
        }
    }
}
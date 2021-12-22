using HarmonyLib;

namespace TooMuchPower
{
    public class Patch
    {
        [HarmonyPostfix, HarmonyPatch(typeof(UIDysonPanel), "_OnUpdate")]
        static void OnUpdatePatched(UIDysonPanel __instance)
        {
            __instance.genEnergyText.text = GeneratePowerValue(__instance.viewDysonSphere.energyGenCurrentTick * 60, TargetValue.MW);
        }

        private static string GeneratePowerValue(long totalEnergyGen, TargetValue targetValue)
        {
            // Remove trailing numbers according to the target power display value
            string value = totalEnergyGen.ToString();
            int length = value.Length;
            string sign = "W";
            if (totalEnergyGen != 0)
            {
                if (targetValue >= TargetValue.kW && length > 3)
                {
                    sign = " kW";
                    length -= 3;
                }
                if (targetValue >= TargetValue.MW && length > 3)
                {
                    sign = " MW";
                    length -= 3;
                }
                if (targetValue >= TargetValue.GW && length > 3)
                {
                    sign = " GW";
                    length -= 3;
                }
                if (targetValue >= TargetValue.TW && length > 3)
                {
                    sign = " TW";
                    length -= 3;
                }
                if (targetValue >= TargetValue.PW && length > 3)
                {
                    sign = " PW";
                    length -= 3;
                }
                value = value.Remove(length);
            }
            value += sign;
            return value;
        }

        enum TargetValue
        {
            kW = 3,
            MW = 6,
            GW = 9,
            TW = 12,
            PW = 15
        }
    }
}

using HarmonyLib;

namespace TooMuchPower
{
    public class Patch
    {
        private static string[] signs = new string[] { " kW", " MW", " GW", " TW", " PW" };

        [HarmonyPostfix, HarmonyPatch(typeof(UIDysonPanel), "_OnUpdate")]
        static void OnUpdatePatched(UIDysonPanel __instance)
        {
            string text = GeneratePowerValue(__instance.viewDysonSphere.energyGenCurrentTick * 60, TargetValue.MW);
            if (text.Length > 8)
            {
                __instance.genEnergyText.fontSize = 35;
            }
            __instance.genEnergyText.text = text;
        }

        private static string GeneratePowerValue(long totalEnergyGen, TargetValue targetValue)
        {
            // Remove trailing numbers according to the target value
            string value = totalEnergyGen.ToString();
            int length = value.Length;
            string sign = " W";
            if (totalEnergyGen != 0)
            {
                for (int i = 0; i <= (int)targetValue; i++)
                {
                    if (length > 3)
                    {
                        sign = signs[i];
                        length -= 3;
                    }
                }
                value = value.Remove(length);
            }
            value += sign;
            return value;
        }

        enum TargetValue
        {
            kW,
            MW,
            GW,
            TW,
            PW
        }
    }
}

using BepInEx;
using HarmonyLib;

namespace TooMuchPower
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class TooMuchPower : BaseUnityPlugin
    {
        public const string pluginGuid = "com.filadog.toomuchpower";
        public const string pluginName = "TooMuchPower";
        public const string pluginVersion = "1.0.0";
        
        public void Awake()
        {
            var harmony = new Harmony(pluginGuid);
            harmony.PatchAll(typeof(Patch));
        }
    }
}

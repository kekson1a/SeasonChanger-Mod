using HarmonyLib;
using SeasonChanger.ConsoleCommands;

using GameConsole;

namespace SeasonChanger.Patches
{
    [HarmonyPatch]
    public class ConsolePatch
    {
        [HarmonyPatch(typeof(Console), "Awake")]
        private static void Postfix(Console __instance)
        {
            __instance.RegisterCommand(new OpenSeasonMenu());
            var ignoreThis = SeasonChanger.UI.DateMenu.Instance; //if removed, "setdate" command will trigger a NullReferenceException
        }
    }
}

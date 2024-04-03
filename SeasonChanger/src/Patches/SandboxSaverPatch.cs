using HarmonyLib;
using SeasonChanger.UI;



namespace SeasonChanger.Patches
{
    [HarmonyPatch]
    public class SandboxSaverPatch
    {
        [HarmonyPatch(typeof(SandboxSaver), "QuickSave")]
        private static bool Prefix(ref SandboxSaver __instance)
        {

            if (DateMenu.Instance.SelectedSeason != DateMenu.SeasonalDate.None)
            {
                __instance.Save(string.Format("{0}-{1}-{2} {3}-{4}-{5}", new object[]
{
            RealDate.Now.Year,
            RealDate.Now.Month,
            RealDate.Now.Day,
            RealDate.Now.Hour,
            RealDate.Now.Minute,
            RealDate.Now.Second
}));
                return false;
            }
            return true;
        }
    }
}

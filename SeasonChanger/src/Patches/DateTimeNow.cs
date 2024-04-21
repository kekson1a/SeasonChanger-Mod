using System;
using HarmonyLib;
using SeasonChanger.UI;

namespace SeasonChanger.Patches
{
    [HarmonyPatch]
    public class DateTimeNow
    {
        [HarmonyPatch(typeof(DateTime), "get_Now")]
        private static bool Prefix(ref DateTime __result)
        {
            if (DateMenu.SelectedSeason == DateMenu.SeasonalDate.None)
                return true;

            __result = GetPatchedDate();
            return false;
        }

        public static DateTime GetPatchedDate()
        {
            var result = RealDate.Now;

            switch (DateMenu.SelectedSeason)
            {
                case DateMenu.SeasonalDate.Easter:
                    result = GetEaster(result.Year);
                    break;
                case DateMenu.SeasonalDate.Halloween:
                    result = new DateTime(result.Year, 10, 31);
                    break;
                case DateMenu.SeasonalDate.Christmas:
                    result = new DateTime(result.Year, 12, 25);
                    break;
                case DateMenu.SeasonalDate.AprilFools:
                    result = new DateTime(result.Year, 4, 1);
                    break;
            }

            return result;
        }

        /// <summary>Private "SeasonalHats.GetEaster()" method copied over for convenience.</summary>
        private static DateTime GetEaster(int year)
        {
            int num = year % 19;
            int num2 = year / 100;
            int num3 = (num2 - num2 / 4 - (8 * num2 + 13) / 25 + 19 * num + 15) % 30;
            int num4 = num3 - num3 / 28 * (1 - num3 / 28 * (29 / (num3 + 1)) * ((21 - num) / 11));
            int num5 = num4 - (year + year / 4 + num4 + 2 - num2 + num2 / 4) % 7;
            int num6 = 3 + (num5 + 40) / 44;
            int num7 = num5 + 28 - 31 * (num6 / 4);
            return new DateTime(year, num6, num7);
        }

    }
}
    
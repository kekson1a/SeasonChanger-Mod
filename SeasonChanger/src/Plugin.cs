using BepInEx;
using BepInEx.Configuration;
using SeasonChanger.Assets;
using SeasonChanger.UI;
using HarmonyLib;

using UnityEngine;

using static SeasonChanger.UI.DateMenu;
using System.Collections.Generic;

namespace SeasonChanger
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.NAME, PluginInfo.VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance;

        public ConfigEntry<bool> ConfigSaveSeasons;
        public ConfigEntry<int> ConfigLastUsedSeason;

        private void Awake()
        {
           //p gameObject.hideFlags = HideFlags.HideAndDontSave;
            // Plugin startup logic
            Logger.LogInfo($"hi, SeasonChanger of version {PluginInfo.VERSION} is loaded");
			Logger.LogInfo("Open your console with [F8] and type \"datemenu\" to open this mod's menu");
            var targetObject = new GameObject("SeasonChanger Components");
            targetObject.AddComponent<DateMenu>();
            targetObject.AddComponent<LoadBundle>();

            new Harmony(PluginInfo.GUID).PatchAll();

            Instance = this;

            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            ConfigSaveSeasons = Config.Bind("Options",
                                    "SaveSeasons",
                                    true,
                                    "Toggles saving the season you selected");

            ConfigLastUsedSeason = Config.Bind("Options.Season",
                        "SeasonOverride",
                        0,
                        "Saved season you selected (uses enum DateMenu.SeasonalDate)");
            DateMenu.Instance.SelectedSeason = (SeasonalDate)ConfigLastUsedSeason.Value;
            DateMenu.Instance.SaveSeason = ConfigSaveSeasons.Value;
        }

        public void PrintError(string message)
        {
            Logger.LogError(message);
        }
    }
	internal class PluginInfo
	{
        public const string GUID = "kekson1a." + NAME;
		public const string NAME = "SeasonChanger";
		public const string VERSION = "0.1.3";
	}
}

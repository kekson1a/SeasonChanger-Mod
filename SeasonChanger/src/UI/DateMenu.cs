using SeasonChanger.Assets;
using System.Collections;

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SeasonChanger.UI
{
    [ConfigureSingleton(SingletonFlags.PersistAutoInstance | SingletonFlags.DestroyDuplicates)]
    public class DateMenu : MonoSingleton<DateMenu>
    {
        //public static DateMenu Instance;
        public static SeasonalDate SelectedSeason;
        public static bool SaveSeason;

        private GameObject seasonMenu;
        private Canvas seasonCanvas;
        private Button reloadMenuButton;
        private TMP_Dropdown selectSeason;
        private Toggle saveSeasonToggle;
        
        protected override void Awake()
        {
            Instance = this;
            SceneManager.sceneLoaded += SceneLoaded;
            LoadBundle.Instance.OnBundleLoaded += OnBundleLoaded;
        }

        private void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (reloadMenuButton == null) return;

            if (SceneHelper.CurrentScene == "Main Menu")
                reloadMenuButton.gameObject.SetActive(true);
            else reloadMenuButton.gameObject.SetActive(false);
        }

        private void OnBundleLoaded(AssetBundle bundle)
        {
            StartCoroutine(Build(bundle));
        }

        private IEnumerator Build(AssetBundle bundle)
        {
            var assetLoadRequest = bundle.LoadAssetAsync<GameObject>("SeasonCanvas");
            yield return assetLoadRequest;
            var seasonCanvasObj = assetLoadRequest.asset as GameObject;
            seasonCanvas = seasonCanvasObj.GetComponent<Canvas>();
            seasonCanvas = Instantiate(seasonCanvas);
            seasonCanvas.sortingOrder = 99;
            seasonCanvas.gameObject.SetActive(false);
            DontDestroyOnLoad(seasonCanvas);

            seasonMenu = seasonCanvas.transform.Find("SeasonMenu").gameObject;
            seasonMenu.AddComponent<HudOpenEffect>();
            seasonMenu.transform.Find("Border/Close").GetComponent<Button>().onClick.AddListener(() => seasonCanvas.gameObject.SetActive(false));

            reloadMenuButton = seasonMenu.transform.Find("Layout/ReloadMenu").GetComponent<Button>(); 
            reloadMenuButton.onClick.AddListener(() => SceneHelper.LoadScene("Main Menu"));

            selectSeason = seasonMenu.transform.Find("Layout/SeasonOverrideDropdown/Dropdown").GetComponent<TMP_Dropdown>();
            selectSeason.onValueChanged.AddListener(SeasonDropdown);

            saveSeasonToggle = seasonMenu.transform.Find("Layout/SaveSeason").GetComponent<Toggle>();
            saveSeasonToggle.onValueChanged.AddListener(SaveSeasonToggle);

            TriggerLoadConfig();
        }

        private void SeasonDropdown(int seasonalDate)
        {
            SelectedSeason = (SeasonalDate)seasonalDate;
            if (SaveSeason) Plugin.Instance.ConfigLastUsedSeason.Value = seasonalDate;
        }

        private void SaveSeasonToggle(bool doSave)
        {
            SaveSeason = doSave;
            Plugin.Instance.ConfigSaveSeasons.Value = SaveSeason;
            if (!SaveSeason)
                Plugin.Instance.ConfigLastUsedSeason.Value = (int)SeasonalDate.None;

            if (SaveSeason) 
                Plugin.Instance.ConfigLastUsedSeason.Value = (int)SelectedSeason;

        }

        public void OpenSeasonMenu()
        {
            seasonCanvas.gameObject.SetActive(true);
        }

        public void TriggerLoadConfig()
        {
            saveSeasonToggle.isOn = SaveSeason;
            if (!SaveSeason) return;
            selectSeason.value = (int)SelectedSeason;
        }

        public enum SeasonalDate
        {
            None = 0,
            Easter = 1,
            Halloween = 2,
            Christmas = 3,
            AprilFools = 4
        }
    }
}

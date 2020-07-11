
using HarmonyLib;
using HugsLib;
using HugsLib.Settings;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Hotseat
{
    public class Hotseat : ModBase
    {

        private SettingHandle<bool> enableStorytellerSwitching;
        private SettingHandle<StorytellerUsageSetting> storytellerUsageDictionary;

        IEnumerable<StorytellerDef> storytellers;

        public override string ModIdentifier
        {
            get { return "Hotseat"; }
        }
        public static Hotseat Instance { get; private set; }
        public Hotseat()
        {
            FileLog.Log("Hotseat harmony log");
            Log.Message("Hotseat Loaded");
            Instance = this;
        }

        public override void DefsLoaded()
        {
            storytellers = DefDatabase<StorytellerDef>.AllDefs;

            enableStorytellerSwitching = Settings.GetHandle<bool>(
                "enableStorytellerSwitching",
                "enableStorytellerSwitching_title"/*.Translate()*/,
                "enableStorytellerSwitching_desc"/*.Translate()*/,
                false);

            storytellerUsageDictionary = Settings.GetHandle<StorytellerUsageSetting>("StorytellerDictionary", "StorytellerDictionary_Title"/*.Translate()*/, "StorytellerDictionary_Description"/*.Translate()*/, null);
            if (storytellerUsageDictionary.Value == null) storytellerUsageDictionary.Value = new StorytellerUsageSetting();

            Log.Message(storytellerUsageDictionary.Value.ToString());

            //storytellerUsageDictionary.CustomDrawer = rect => { return SettingsDrawUtility.StorytellerCheckboxList(rect, storytellerUsageDictionary, storytellers); };
            storytellerUsageDictionary.CustomDrawerFullWidth = rect => { return SettingsDrawUtility.StorytellerCheckboxList(rect, storytellerUsageDictionary, storytellers); };
        }



        //HotseatSettings settings;

        //public Hotseat(ModContentPack content)
        //{
        //    //settings = GetSettings<HotseatSettings>();

        //    //var harmony = new Harmony("com.arquebus.rimworld.mod.hotseat");
        //    //harmony.PatchAll(Assembly.GetExecutingAssembly());

        //    //Harmony.DEBUG = true;

        //    //FileLog.Log("Hotseat harmony log");
        //    //Log.Message("Hotseat Loaded");
        //}

        //public override void DoSettingsWindowContents(Rect inRect)
        //{
        //    settings.DoWindowContents(inRect);
        //}

        //public override string SettingsCategory()
        //{
        //    return "HotseatModSettings";
        //}

    }
}
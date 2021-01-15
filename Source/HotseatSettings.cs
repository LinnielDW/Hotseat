using System.Collections.Generic;
using System.Linq;
using Hotseat.SettingsDomain;
using RimWorld;
using UnityEngine;
using Verse;

namespace Hotseat
{
    [StaticConstructorOnStartup]
    public class HotseatStatics
    {
        public static readonly IEnumerable<StorytellerDef> AllVisibleStorytellers = DefDatabase<StorytellerDef>.AllDefs.Where(x => x.listVisible);
    }

    public class HotseatSettings : ModSettings
    {
        public static bool enableStorytellerSwitching = true;
        public static bool enableStorytellerSwitchNotification = true;
        public static bool enableStorytellerSwitchingWeighted = true;
        public static int changeOnEventChance = 10;
        public static int changeOnYearChance = 85;
        public static int changeOnQuadrumChance = 25;
        public static Dictionary<string, StorytellerHotseatStettings> storytellerSettingsDictionary = new Dictionary<string, StorytellerHotseatStettings>();

        public override void ExposeData()
        {
            Scribe_Values.Look(ref enableStorytellerSwitching, "enableStorytellerSwitching", true);
            Scribe_Values.Look(ref enableStorytellerSwitchNotification, "enableStorytellerSwitchNotification", true);
            Scribe_Values.Look(ref enableStorytellerSwitchingWeighted, "enableStorytellerSwitchingWeighted", true);
            Scribe_Values.Look(ref changeOnEventChance, "changeOnEventChance", 10);
            Scribe_Values.Look(ref changeOnYearChance, "changeOnYearChance", 85);
            Scribe_Values.Look(ref changeOnQuadrumChance, "changeOnQuadrumChance", 25);

            Scribe_Collections.Look(ref storytellerSettingsDictionary, "storyTellersEnabled", LookMode.Value, LookMode.Deep);
            if (Scribe.mode == LoadSaveMode.LoadingVars)
            {
                if(storytellerSettingsDictionary == null) storytellerSettingsDictionary = new Dictionary<string, StorytellerHotseatStettings>();
            }

            base.ExposeData();
        }

        public static IEnumerable<StorytellerDef> GetStorytellersFiltered()
        {
            return HotseatStatics.AllVisibleStorytellers.Where(storytellerDef => 
                                                                      storytellerSettingsDictionary.ContainsKey(storytellerDef.defName)                //storyteller exists in dictionary (all storytellers should default to true)
                                                                      && storytellerDef.defName != Current.Game.storyteller.def.defName                //storyteller does not equal current storyteller
                                                                      && storytellerSettingsDictionary[storytellerDef.defName].storytellerEnabledBool  //storyteller is allowed to be switched to
            );
        }

        public static StorytellerHotseatStettings GetOrCreateStorytellerEnabledSetting(string storytellerDefName)
        {
            var settingsValue = storytellerSettingsDictionary.TryGetValue(storytellerDefName);
            
            if (settingsValue != null) return settingsValue;
            
            Log.Warning("new settings made for storyteller: " + storytellerDefName);
            settingsValue = new StorytellerHotseatStettings();
            storytellerSettingsDictionary[storytellerDefName] = settingsValue;
            return settingsValue;
        }
    }

}

<<<<<<< HEAD
﻿//using RimWorld;
//using System.Collections.Generic;
//using UnityEngine;
//using Verse;

namespace Hotseat
{
    //public class storytellerEnabled : IExposable
    //{
    //    public bool storytellerEnabledBool = true;
    //    public storytellerEnabled(bool storytellerEnabled)
    //    {
    //        storytellerEnabledBool = storytellerEnabled;
    //    }

    //    public void ExposeData()
    //    {
    //        Scribe_Values.Look(ref storytellerEnabledBool, "storytellerEnabledBool");
    //    }
    //}

    //public class HotseatSettings : ModSettings
    //{
    //    public Dictionary<string, storytellerEnabled> storyTellersEnabled = new Dictionary<string, storytellerEnabled>();

    //    public override void ExposeData()
    //    {
    //        Scribe_Collections.Look(ref storyTellersEnabled, "storyTellersEnabled");
    //        base.ExposeData();
    //    }

    //    public void DoWindowContents(Rect inRect)
    //    {
    //        Listing_Standard listingStandard = new Listing_Standard();
    //        listingStandard.Begin(inRect);
    //        IEnumerable<StorytellerDef> storytellers = DefDatabase<StorytellerDef>.AllDefs;

    //        void getStorytellerEnabled(string storytellerDefName)
    //        {
    //            if (storyTellersEnabled.ContainsKey(storytellerDefName))
    //            {
    //                //return storyTellersEnabled[storytellerDefName].storytellerEnabledBool;
    //                //listingStandard.CheckboxLabeled(storytellerDefName, ref storyTellersEnabled[storytellerDefName].storytellerEnabledBool, "test");
    //            }
    //            else
    //            {
    //                //false;
    //            }
    //        }

    //        foreach (StorytellerDef storyteller in storytellers)
    //        {
    //            getStorytellerEnabled(storyteller.defName);
    //        }
    //        listingStandard.End();
    //    }
    //}
=======
﻿using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Hotseat
{
    public class StorytellerEnabled : IExposable
    {
        public bool storytellerEnabledBool = true;

        public void ExposeData()
        {
            Scribe_Values.Look(ref storytellerEnabledBool, "storytellerEnabledBool");
        }
    }

    [StaticConstructorOnStartup]
    public class HotseatStatics
    {
        public static IEnumerable<StorytellerDef> storytellers = DefDatabase<StorytellerDef>.AllDefs.Where(x => x.listVisible);
    }

    public class HotseatSettings : ModSettings
    {
        public bool exampleBool;
        public float exampleFloat = 200f;
        public Dictionary<string, StorytellerEnabled> storyTellersEnabledDictionary = new Dictionary<string, StorytellerEnabled>();

        public override void ExposeData()
        {
            Scribe_Values.Look(ref exampleBool, "exampleBool");
            Scribe_Values.Look(ref exampleFloat, "exampleFloat", 200f);

            Scribe_Collections.Look(ref storyTellersEnabledDictionary, "storyTellersEnabled", LookMode.Value, LookMode.Deep);
            if (Scribe.mode == LoadSaveMode.LoadingVars)
            {
                if (storyTellersEnabledDictionary == null) storyTellersEnabledDictionary = new Dictionary<string, StorytellerEnabled>();
            }

            base.ExposeData();
        }

        StorytellerEnabled GetOrCreateStorytellerEnabledSetting(string storytellerDefName)
        {
            StorytellerEnabled settingsValue = storyTellersEnabledDictionary.TryGetValue(storytellerDefName);
            if (settingsValue == null)
            {
                settingsValue = new StorytellerEnabled();
                storyTellersEnabledDictionary[storytellerDefName] = settingsValue;
            }

            return settingsValue;
        }

        public void DoWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);

            listingStandard.CheckboxLabeled("exampleBoolExplanation", ref exampleBool, "exampleBoolToolTip");
            
            listingStandard.Label("exampleFloatExplanation");
            exampleFloat = listingStandard.Slider(exampleFloat, 10f, 300f);

            Text.Font = GameFont.Medium;
            listingStandard.Label("Storytllers enabled for switching");
            Text.Font = GameFont.Small;

            foreach (StorytellerDef storyteller in HotseatStatics.storytellers)
            {
                StorytellerEnabled storytellerEnabledSetting = GetOrCreateStorytellerEnabledSetting(storyteller.defName);

                listingStandard.CheckboxLabeled(storyteller.label, ref storytellerEnabledSetting.storytellerEnabledBool, storyteller.description);
            }

            listingStandard.End();
        }
    }
>>>>>>> settings_no_hugs

}

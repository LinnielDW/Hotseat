//using RimWorld;
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

}

using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Hotseat
{
   /* public class storytellerEnabled : IExposable
    {
        public bool storytellerEnabledBool = true;
        public storytellerEnabled(bool storytellerEnabled)
        {
            storytellerEnabledBool = storytellerEnabled;
        }

        public void ExposeData()
        {
            Scribe_Values.Look(ref storytellerEnabledBool, "storytellerEnabledBool");
        }
    }

    public class HotseatSettings : ModSettings
    {
        [StaticConstructorOnStartup]
        public class hosteatsetup
        {
            public static IEnumerable<StorytellerDef> storytellers = DefDatabase<StorytellerDef>.AllDefs;
        }

        //public bool exampleBool;
        //public float exampleFloat = 200f;

        public Dictionary<string, storytellerEnabled> storyTellersEnabled = new Dictionary<string, storytellerEnabled>();

        public override void ExposeData()
        {
            //Scribe_Values.Look(ref exampleBool, "exampleBool");
            //Scribe_Values.Look(ref exampleFloat, "exampleFloat", 200f);

            Scribe_Collections.Look(ref storyTellersEnabled, "storyTellersEnabled");

            base.ExposeData();
        }

        public void DoWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);

            void getStorytellerEnabled(string storytellerDefName)
            {
                if (storyTellersEnabled.ContainsKey(storytellerDefName))
                {
                    //return storyTellersEnabled[storytellerDefName].storytellerEnabledBool;
                    //listingStandard.CheckboxLabeled(storytellerDefName, ref storyTellersEnabled[storytellerDefName].storytellerEnabledBool, "test");
                }
                else
                {
                    //false;
                }
            }

            //listingStandard.CheckboxLabeled("exampleBoolExplanation", ref exampleBool, "exampleBoolToolTip");
            //listingStandard.Label("exampleFloatExplanation");
            //exampleFloat = listingStandard.Slider(exampleFloat, 10f, 300f);

            foreach (StorytellerDef storyteller in hosteatsetup.storytellers)
            {
                //listingStandard.CheckboxLabeled(storyteller.defName, ref storyTellersEnabled[storytellerDefName].storytellerEnabledBool;, "test");
                getStorytellerEnabled(storyteller.defName);
            }
            listingStandard.End();
        }
    }*/

}

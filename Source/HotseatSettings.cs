using UnityEngine;
using Verse;

namespace Hotseat
{

    public class HotseatSettings : ModSettings
    {
        /// <summary>
        /// The three settings our mod has.
        /// </summary>
        public bool exampleBool;
        public float exampleFloat = 200f;

        /// <summary>
        /// The part that writes our settings to file. Note that saving is by ref.
        /// </summary>
        public override void ExposeData()
        {
            Scribe_Values.Look(ref exampleBool, "exampleBool");
            Scribe_Values.Look(ref exampleFloat, "exampleFloat", 200f);

            base.ExposeData();
        }


        public void DoWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.CheckboxLabeled("exampleBoolExplanation", ref exampleBool, "exampleBoolToolTip");
            listingStandard.Label("exampleFloatExplanation");
            exampleFloat = listingStandard.Slider(exampleFloat, 10f, 300f);
            listingStandard.End();
        }
    }

}

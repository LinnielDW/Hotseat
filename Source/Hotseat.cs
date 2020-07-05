using Verse;
using HarmonyLib;
using System.Reflection;
using UnityEngine;

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
    }
    public class Hotseat // : Mod
    {

        HotseatSettings settings;

        public Hotseat(ModContentPack content) // : base(content)
        {
            // this.settings = GetSettings<HotseatSettings>();

            var harmony = new Harmony("com.arquebus.rimworld.mod.hotseat");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            Harmony.DEBUG = true;

            FileLog.Log("Hotseat harmony log");
            Log.Message("Hotseat Loaded");
        }

        /*
        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.CheckboxLabeled("exampleBoolExplanation", ref settings.exampleBool, "exampleBoolToolTip");
            listingStandard.Label("exampleFloatExplanation");
            settings.exampleFloat = listingStandard.Slider(settings.exampleFloat, 100f, 300f);
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "HotseatModSettings".Translate();
        }
        */
    }
}
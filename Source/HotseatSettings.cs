using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Hotseat
{
    [StaticConstructorOnStartup]
    public class HotseatStatics
    {
        public static IEnumerable<StorytellerDef> storytellers = DefDatabase<StorytellerDef>.AllDefs.Where(x => x.listVisible);
    }

    public class HotseatSettings : ModSettings
    {
        public static bool enableStorytellerSwitching = true;
        public static int changeOnEventChance = 5;
        public static int changeOnYearChance = 80;
        public static int changeOnMonthChance = 15;
        public static Dictionary<string, StorytellerEnabled> storyTellersEnabledDictionary = new Dictionary<string, StorytellerEnabled>();

        public override void ExposeData()
        {
            Scribe_Values.Look(ref enableStorytellerSwitching, "enableStorytellerSwitching");
            Scribe_Values.Look(ref changeOnEventChance, "changeOnEventChance");
            Scribe_Values.Look(ref changeOnYearChance, "changeOnYearChance");
            Scribe_Values.Look(ref changeOnMonthChance, "changeOnMonthChance");

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
            Listing_Standard settingsList = new Listing_Standard();
            settingsList.ColumnWidth = inRect.width / 2f - 20f;
            settingsList.Begin(inRect);

            settingsList.CheckboxLabeled("EnableStorytellerSwitchingSetting".Translate(), ref enableStorytellerSwitching, "EnableStorytellerSwitchingSettingToolTip".Translate());

            DrawLabelledNumericSetting(settingsList, changeOnYearChance, nameof(changeOnYearChance));
            DrawLabelledNumericSetting(settingsList, changeOnMonthChance, nameof(changeOnMonthChance));
            DrawLabelledNumericSetting(settingsList, changeOnEventChance, nameof(changeOnEventChance));

            //listingStandard.Label("exampleFloatExplanation");
            //exampleFloat = listingStandard.Slider(exampleFloat, 10f, 300f);

            settingsList.NewColumn();
            DrawStorytellersEnabledSettingsDynamic(settingsList);

            settingsList.End();
        }

        private static void DrawLabelledNumericSetting(Listing_Standard settingsList, int settingValue, string settingName)
        {
            Rect numericSettingRect = settingsList.GetRect(24f);
            string settingValueStringBuffer = settingValue.ToString();

            Rect leftSide = numericSettingRect.LeftPart(0.8f).Rounded();

            Widgets.Label(leftSide, settingName.Translate());
            TooltipHandler.TipRegion(leftSide, (settingName + "Tooltip").Translate());

            Widgets.TextFieldNumeric(numericSettingRect.RightPart(0.2f).Rounded(), ref settingValue, ref settingValueStringBuffer, 0, 100);
        }

        private void DrawStorytellersEnabledSettingsDynamic(Listing_Standard listingStandard)
        {
            Text.Font = GameFont.Medium;
            listingStandard.Label("StorytellersArrayEnabledSettingTitle".Translate(), -1, "StorytellersArrayEnabledSettingTooltip".Translate());
            Text.Font = GameFont.Small;

            foreach (StorytellerDef storyteller in HotseatStatics.storytellers)
            {
                StorytellerEnabled storytellerEnabledSetting = GetOrCreateStorytellerEnabledSetting(storyteller.defName);

                listingStandard.CheckboxLabeled(storyteller.label, ref storytellerEnabledSetting.storytellerEnabledBool, storyteller.description);
            }
        }
    }

}

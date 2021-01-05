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
        public static bool enableStorytellerSwitchNotification = true;
        public static int changeOnEventChance = 10;
        public static int changeOnYearChance = 85;
        public static int changeOnQuadrumChance = 25;
        public static Dictionary<string, StorytellerEnabled> storyTellersEnabledDictionary = new Dictionary<string, StorytellerEnabled>();


        private static Vector2 scrollPosition = Vector2.zero;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref enableStorytellerSwitching, "enableStorytellerSwitching", true);
            Scribe_Values.Look(ref enableStorytellerSwitchNotification, "enableStorytellerSwitchNotification", true);
            Scribe_Values.Look(ref changeOnEventChance, "changeOnEventChance", 10);
            Scribe_Values.Look(ref changeOnYearChance, "changeOnYearChance", 85);
            Scribe_Values.Look(ref changeOnQuadrumChance, "changeOnQuadrumChance", 25);

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
            settingsList.ColumnWidth = (inRect.width / 2f) - 20f;
            settingsList.Begin(inRect);

            settingsList.CheckboxLabeled("EnableStorytellerSwitchingSetting".Translate(), ref enableStorytellerSwitching, "EnableStorytellerSwitchingSettingToolTip".Translate());
            settingsList.CheckboxLabeled("enableStorytellerSwitchNotificationSetting".Translate(), ref enableStorytellerSwitchNotification, "enableStorytellerSwitchNotificationSettingToolTip".Translate());

            DrawLabelledNumericSetting(settingsList, ref changeOnYearChance, nameof(changeOnYearChance));
            DrawLabelledNumericSetting(settingsList, ref changeOnQuadrumChance, nameof(changeOnQuadrumChance));
            DrawLabelledNumericSetting(settingsList, ref changeOnEventChance, nameof(changeOnEventChance));

            settingsList.NewColumn();
            DrawStorytellersEnabledSettingsDynamic(settingsList, inRect);

            settingsList.End();
        }

        private static void DrawLabelledNumericSetting(Listing_Standard settingsList, ref int settingValue, string settingName)
        {
            Rect numericSettingRect = settingsList.GetRect(24f);
            string settingValueStringBuffer = settingValue.ToString();

            Rect leftSide = numericSettingRect.LeftPart(0.8f).Rounded();

            Widgets.Label(leftSide, settingName.Translate());
            TooltipHandler.TipRegion(leftSide, (settingName + "Tooltip").Translate());

            Widgets.TextFieldNumeric(numericSettingRect.RightPart(0.2f).Rounded(), ref settingValue, ref settingValueStringBuffer, 0, 100);
        }

        private void DrawStorytellersEnabledSettingsDynamic(Listing_Standard listingStandard, Rect inRect)
        {
            Text.Font = GameFont.Medium;
            listingStandard.Label("StorytellersArrayEnabledSettingTitle".Translate(), -1, "StorytellersArrayEnabledSettingTooltip".Translate());
            Text.Font = GameFont.Small;
            
            int rowCount = HotseatStatics.storytellers.Count();

            Rect viewRect = new Rect(listingStandard.ColumnWidth + 20f, listingStandard.CurHeight, listingStandard.ColumnWidth, inRect.height - listingStandard.CurHeight);
            Rect scrollRect = new Rect(0f, 0f, viewRect.width - 16f, (Text.LineHeight + listingStandard.verticalSpacing) * rowCount);

            Widgets.BeginScrollView(viewRect, ref scrollPosition, scrollRect);
            listingStandard.ColumnWidth -= 16f; //This line is needed because the listingstandard and scrollrect are not synced. When one changes you need to change the other.

            listingStandard.Begin(scrollRect);
            foreach (StorytellerDef storyteller in HotseatStatics.storytellers)
            {
                StorytellerEnabled storytellerEnabledSetting = GetOrCreateStorytellerEnabledSetting(storyteller.defName);

                listingStandard.CheckboxLabeled(storyteller.label, ref storytellerEnabledSetting.storytellerEnabledBool, storyteller.description);
            }

            listingStandard.End();
            Widgets.EndScrollView();

        }
    }

}

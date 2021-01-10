using Hotseat.SettingsDomain;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;
using Verse.Sound;

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
        public static Dictionary<string, StorytellerHotseatStettings> storytellerSettingsDictionary = new Dictionary<string, StorytellerHotseatStettings>();

        private static Vector2 scrollPosition = Vector2.zero;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref enableStorytellerSwitching, "enableStorytellerSwitching", true);
            Scribe_Values.Look(ref enableStorytellerSwitchNotification, "enableStorytellerSwitchNotification", true);
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

        StorytellerHotseatStettings GetOrCreateStorytellerEnabledSetting(string storytellerDefName)
        {
            var settingsValue = storytellerSettingsDictionary.TryGetValue(storytellerDefName);
            if (settingsValue != null) return settingsValue;
            
            Log.Warning("new settings made for storyteller: " + storytellerDefName);
            settingsValue = new StorytellerHotseatStettings();
            Log.Warning(settingsValue.ToString());
            
            storytellerSettingsDictionary[storytellerDefName] = settingsValue;
            return settingsValue;
        }

        public void DoWindowContents(Rect inRect)
        {
            Listing_Standard settingsList = new Listing_Standard();
            settingsList.ColumnWidth = (inRect.width / 2f) - 20f;
            settingsList.Begin(inRect);

            settingsList.CheckboxLabeled("EnableStorytellerSwitchingSetting".Translate(), ref enableStorytellerSwitching, "EnableStorytellerSwitchingSettingToolTip".Translate());
            settingsList.CheckboxLabeled("EnableStorytellerSwitchNotificationSetting".Translate(), ref enableStorytellerSwitchNotification, "EnableStorytellerSwitchNotificationSettingToolTip".Translate());

            DrawLabelledNumericSetting(settingsList, ref changeOnYearChance, nameof(changeOnYearChance));
            DrawLabelledNumericSetting(settingsList, ref changeOnQuadrumChance, nameof(changeOnQuadrumChance));
            DrawLabelledNumericSetting(settingsList, ref changeOnEventChance, nameof(changeOnEventChance));

            settingsList.NewColumn();
            DrawStorytellerHotseatSettingsDynamic(settingsList, inRect);

            settingsList.End();
        }

        private static void DrawLabelledNumericSetting(Listing_Standard settingsList, ref int settingValue, string settingName)
        {
            var numericSettingRect = settingsList.GetRect(24f);
            var settingValueStringBuffer = settingValue.ToString();

            var leftSide = numericSettingRect.LeftPart(0.8f).Rounded();

            Widgets.Label(leftSide, settingName.Translate());
            TooltipHandler.TipRegion(leftSide, (settingName + "Tooltip").Translate());

            Widgets.TextFieldNumeric(numericSettingRect.RightPart(0.2f).Rounded(), ref settingValue, ref settingValueStringBuffer, 0, 100);
        }

        private void DrawStorytellerHotseatSettingsDynamic(Listing_Standard listingStandard, Rect inRect)
        {
            Text.Font = GameFont.Medium;
            listingStandard.Label("StorytellersArrayEnabledSettingTitle".Translate(), -1, "StorytellersArrayEnabledSettingTooltip".Translate());
            Text.Font = GameFont.Small;
            
            var rowCount = HotseatStatics.storytellers.Count();
            var viewRect = new Rect(listingStandard.ColumnWidth + 20f, listingStandard.CurHeight, listingStandard.ColumnWidth, inRect.height - listingStandard.CurHeight);
            var scrollRect = new Rect(0f, 0f, viewRect.width - 16f, CalculateScrollHeight(listingStandard, rowCount));

            Widgets.BeginScrollView(viewRect, ref scrollPosition, scrollRect);
            listingStandard.ColumnWidth -= 16f; //This line is needed because the listingstandard and scrollrect are not synced. When one changes you need to change the other.
            listingStandard.Begin(scrollRect);
            
            foreach (var storyteller in HotseatStatics.storytellers)
            {
                var storytellerEnabledSetting = GetOrCreateStorytellerEnabledSetting(storyteller.defName);
                listingStandard.CheckboxLabeled(storyteller.label, ref storytellerEnabledSetting.storytellerEnabledBool, storyteller.description);
                
                if (storytellerEnabledSetting.storytellerEnabledBool)
                {
                    DrawWeightSlider(listingStandard, storytellerEnabledSetting);
                }
                listingStandard.Gap(5f);
            }
            
            listingStandard.End();
            Widgets.EndScrollView();
        }

        private static void DrawWeightSlider(Listing_Standard listingStandard,
            StorytellerHotseatStettings storytellerEnabledSetting)
        {
            var rect = listingStandard.GetRect(24f);

            storytellerEnabledSetting.storytellerWeight =
                Widgets.HorizontalSlider(rect,
                    storytellerEnabledSetting.storytellerWeight,
                    1f, 100f, false,
                    storytellerEnabledSetting.storytellerWeight + "x likely",
                    "1", "100", 1f);
        }

        private static float CalculateScrollHeight(Listing_Standard listingStandard, int rowCount) {
            return (Text.LineHeight + listingStandard.verticalSpacing + 5f) * rowCount + 
                   24f * storytellerSettingsDictionary.Count(v => v.Value.storytellerEnabledBool);
        }
    }

}

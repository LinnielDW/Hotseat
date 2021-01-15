using System.Linq;
using Hotseat.SettingsDomain;
using UnityEngine;
using Verse;

namespace Hotseat.Utils
{
    public static class SettingsDrawUtil
    {
        private static Vector2 _scrollPosition = Vector2.zero;

        public static void DoWindowContents(Rect inRect)
        {
            var settingsList = new Listing_Standard {ColumnWidth = inRect.width / 2f - 20f};
            settingsList.Begin(inRect);

            settingsList.CheckboxLabeled("EnableStorytellerSwitchingSetting".Translate(), ref HotseatSettings.enableStorytellerSwitching, "EnableStorytellerSwitchingSettingToolTip".Translate());
            settingsList.CheckboxLabeled("EnableStorytellerSwitchNotificationSetting".Translate(), ref HotseatSettings.enableStorytellerSwitchNotification, "EnableStorytellerSwitchNotificationSettingToolTip".Translate());
            settingsList.CheckboxLabeled("EnableStorytellerSwitchingWeightedSetting".Translate(), ref HotseatSettings.enableStorytellerSwitchingWeighted, "EnableStorytellerSwitchingWeightedSettingToolTip".Translate());

            DrawLabelledNumericSetting(settingsList, ref HotseatSettings.changeOnYearChance, nameof(HotseatSettings.changeOnYearChance));
            DrawLabelledNumericSetting(settingsList, ref HotseatSettings.changeOnQuadrumChance, nameof(HotseatSettings.changeOnQuadrumChance));
            DrawLabelledNumericSetting(settingsList, ref HotseatSettings.changeOnEventChance, nameof(HotseatSettings.changeOnEventChance));

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

        private static void DrawStorytellerHotseatSettingsDynamic(Listing_Standard listingStandard, Rect inRect)
        {
            Text.Font = GameFont.Medium;
            listingStandard.Label("StorytellersArrayEnabledSettingTitle".Translate(), -1, "StorytellersArrayEnabledSettingTooltip".Translate());
            Text.Font = GameFont.Small;
            
            var rowCount = HotseatStatics.AllVisibleStorytellers.Count();
            var windowRect = new Rect(listingStandard.ColumnWidth + 20f, listingStandard.CurHeight, listingStandard.ColumnWidth, inRect.height - listingStandard.CurHeight);
            var scrollRect = new Rect(0f, 0f, windowRect.width - 16f, CalculateScrollHeight(listingStandard, rowCount));

            Widgets.BeginScrollView(windowRect, ref _scrollPosition, scrollRect);
            listingStandard.ColumnWidth -= 16f; //This line is needed because the listingstandard and scrollrect are not synced. When one changes you need to change the other.
            listingStandard.Begin(scrollRect);
            
            foreach (var storyteller in HotseatStatics.AllVisibleStorytellers)
            {
                var storytellerEnabledSetting = HotseatSettings.GetOrCreateStorytellerEnabledSetting(storyteller.defName);
                listingStandard.CheckboxLabeled(storyteller.label, ref storytellerEnabledSetting.storytellerEnabledBool, storyteller.description);
                
                if (storytellerEnabledSetting.storytellerEnabledBool && HotseatSettings.enableStorytellerSwitchingWeighted)
                {
                    DrawWeightSlider(listingStandard, storytellerEnabledSetting);
                }
                
                listingStandard.Gap(5f);
            }
            
            listingStandard.End();
            Widgets.EndScrollView();
        }

        private static void DrawWeightSlider(Listing_Standard listingStandard, StorytellerHotseatStettings storytellerEnabledSetting)
        {
            var rect = listingStandard.GetRect(24f);

            storytellerEnabledSetting.storytellerWeight =
                Widgets.HorizontalSlider(
                    rect,
                    storytellerEnabledSetting.storytellerWeight,
                    1f, 100f, false,
                    storytellerEnabledSetting.storytellerWeight + "xAsLikely".Translate(),
                    "1", "100", 1f
                );
        }

        private static float CalculateScrollHeight(Listing_Standard listingStandard, int rowCount)
        {
            var x = (Text.LineHeight + listingStandard.verticalSpacing + 5f) * rowCount;
            if (HotseatSettings.enableStorytellerSwitchingWeighted)
                x += 24f * HotseatSettings.storytellerSettingsDictionary.Count(v => v.Value.storytellerEnabledBool);
            
            return x;
        }
    }
}
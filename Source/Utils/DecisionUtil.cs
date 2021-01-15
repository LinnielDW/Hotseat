using System;
using System.Collections.Generic;
using System.Linq;
using Hotseat.SettingsDomain;
using RimWorld;
using Verse;

namespace Hotseat.Utils
{
    public static class DecisionUtil
    {
        public static void TryChangeStoryTeller(int chanceThreshold)
        {
            //if die does not reach threshold, then do not try to change the storyteller.
            if (Rand.RangeInclusive(1, 100) > chanceThreshold) return;

            StorytellerDef storytellerDef;
            try
            {
                storytellerDef = ChooseStoryTeller();
            }
            catch
            {
                Log.Warning("There were no storytellers that could be changed to. Keeping current storyteller.");
                return;
            }

            if (storytellerDef != null)
            {
                Current.Game.storyteller.def = storytellerDef;
                Current.Game.storyteller.Notify_DefChanged();

                if (HotseatSettings.enableStorytellerSwitchNotification) NotifierUtil.SendStorytellerChangeLetter();
                
                //Log.Message("Storyteller is now:" + Current.Game.storyteller.def.defName);
            }
            else Log.Error("null chosen as storyteller. This should not happen, if you see this, please tell the mod author(Arquebus).");
        }

        private static StorytellerDef ChooseStoryTeller()
        {
            var storytellerDefs = HotseatSettings.GetStorytellersFiltered().ToList();
            // StorytellerLogUtils.LogStorytellers(storytellerDefs);
            
            if (!storytellerDefs.Any()) throw new Exception("No valid storytellers");
            
            return HotseatSettings.enableStorytellerSwitchingWeighted ? DecidedByWeighted(storytellerDefs) : DecideByUniform(storytellerDefs);
        }

        private static StorytellerDef DecideByUniform(IEnumerable<StorytellerDef> storytellerDefs)
        {
            return storytellerDefs.RandomElement();
        }

        private static StorytellerDef DecidedByWeighted(IEnumerable<StorytellerDef> storytellerDefs)
        {
            RandomChoiceNumberSelect(storytellerDefs, out var max, out var storytellerListWeighted);
            
            var randomChoiceNumber = Rand.RangeInclusive(0, max - 1);
            // StorytellerLogUtils.LogStorytellerWeights(storytellerListWeighted, max, randomChoiceNumber);
            
            return storytellerListWeighted.First(st => randomChoiceNumber >= st.lft && randomChoiceNumber < st.rght
            ).storytellerDef;
        }

        private static void RandomChoiceNumberSelect(IEnumerable<StorytellerDef> storytellerDefs, out int max, out List<WeightedChoice> storytellerListWeighted)
        {
            max = 0;
            storytellerListWeighted = new List<WeightedChoice>();
            
            foreach (var storytellerDef in storytellerDefs)
            {
                var storytellerSetting = (int) HotseatSettings.storytellerSettingsDictionary[storytellerDef.defName].storytellerWeight;
                
                storytellerListWeighted.Add(
                    new WeightedChoice(storytellerDef, max, max + storytellerSetting)
                );
                max += storytellerSetting;
            }
        }
    }

    
}
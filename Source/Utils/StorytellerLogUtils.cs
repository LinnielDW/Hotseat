using System.Collections.Generic;
using Hotseat.SettingsDomain;
using RimWorld;
using Verse;

namespace Hotseat.Utils
{
    public static class StorytellerLogUtils
    {
        public static void LogStorytellers(IEnumerable<StorytellerDef> storytellers)
        {
            Log.Message("-----------------------------");
            Log.Message("storytellers filtered:");
            foreach (var storytellerDef in storytellers)
            {
                Log.Message(storytellerDef.defName);
            }
            Log.Message("-----------------------------");
        }

        public static void LogStorytellerWeights(IEnumerable<WeightedChoice> storytellerListWeighted, int max, int randomChoiceNumber)
        {
            Log.Message("-----------------------------");
            Log.Message("storyteller weights:");
            foreach (var weighted in storytellerListWeighted)
            {
                Log.Message(weighted.ToString());
            }

            Log.Message("max: " + max);
            Log.Message("rand: " + randomChoiceNumber);
            Log.Message("-----------------------------");
        }
    }
}
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Hotseat
{
    class HotseatUtils
    {
        public static string GetStorytellerChangeDescription()
        {
            return "The storyteller has changed!\n Now " + Current.Game.storyteller.def.label + " is in charge!";
        }

        public static void LogStorytellers(IEnumerable<StorytellerDef> storytellers)
        {
            Log.Message("-----------------------------");
            Log.Message("storytellers filtered:");
            foreach (StorytellerDef x in storytellers)
            {
                Log.Message(x.defName);
            }
            Log.Message("-----------------------------");
        }

        public static void SendStorytellerChangeLetter()
        {
            Find.LetterStack.ReceiveLetter("StorytellerChangeTitle", GetStorytellerChangeDescription(), LetterDefOf.NeutralEvent, null);
            Log.Message("Storyteller changed.");
        }
        public static IEnumerable<StorytellerDef> GetStorytellersFiltered()
        {
            return DefDatabase<StorytellerDef>.AllDefs.Where(x => x.listVisible
                            && HotseatSettings.storyTellersEnabledDictionary.ContainsKey(x.defName)
                            && x.defName != Current.Game.storyteller.def.defName
                            && HotseatSettings.storyTellersEnabledDictionary[x.defName].storytellerEnabledBool
                        );
        }
    }
}

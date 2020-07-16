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
            //TODO: change these strings to translation parts
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
            Find.LetterStack.ReceiveLetter("StorytellerChangeTitle".Translate(), GetStorytellerChangeDescription(), LetterDefOf.NeutralEvent, null);
            Log.Message("Storyteller changed.");
        }

        public static IEnumerable<StorytellerDef> GetStorytellersFiltered()
        {
            return DefDatabase<StorytellerDef>.AllDefs.Where(storytellerDef => storytellerDef.listVisible                    //storyteller is visible
                            && HotseatSettings.storyTellersEnabledDictionary.ContainsKey(storytellerDef.defName)             //storyteller exists in dictionary (all storytellers should default to true)
                            && storytellerDef.defName != Current.Game.storyteller.def.defName                                //storyteller does not equal current storyteller
                            && HotseatSettings.storyTellersEnabledDictionary[storytellerDef.defName].storytellerEnabledBool  //storyteller is allowed to be switched to
                        );
        }


        public static void TryChangeStoryTeller(int chanceTreshold)
        {
            if (Rand.RangeInclusive(0, 100) <= chanceTreshold)
            {
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

                    SendStorytellerChangeLetter();

                    Log.Message("Storyteller is now:" + Current.Game.storyteller.def.defName);
                }
                else Log.Error("null chosen as storyteller. This should not happen, if you see this, please tell Arquebus.");
            }
            else
            {
                Log.Message("Storyteller remains... for now");
            }
        }

        private static StorytellerDef ChooseStoryTeller()
        {
            IEnumerable<StorytellerDef> storytellersFiltered = GetStorytellersFiltered();

            LogStorytellers(storytellersFiltered);

            if (storytellersFiltered.Count() > 0)
            {
                StorytellerDef newStorytellerDef = storytellersFiltered.RandomElement();
                Log.Message("Storyteller chosen is: " + newStorytellerDef.defName);

                return newStorytellerDef;
            }
            else
            {
                throw new System.Exception("No valid storytellers");
            }
        }
    }
}

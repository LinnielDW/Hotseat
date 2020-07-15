using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Hotseat
{
    [HarmonyPatch(typeof(IncidentWorker))]
    [HarmonyPatch("TryExecute")]
    public static class PatchHStoryteller
    {
        static void Postfix()
        {
            if (HotseatSettings.enableStorytellerSwitching) {
                Log.Message("TryExecute Postfix: Storyteller before is:" + Current.Game.storyteller.def.defName);
                TryChangeStoryTeller(HotseatSettings.changeOnEventChance);
            }
        }

        private static void TryChangeStoryTeller(int chanceTreshold)
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

                    HotseatUtils.SendStorytellerChangeLetter();

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
            IEnumerable<StorytellerDef> storytellersFiltered = HotseatUtils.GetStorytellersFiltered();

            HotseatUtils.LogStorytellers(storytellersFiltered);

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

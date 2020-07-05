using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Hotseat
{
    [HarmonyPatch(typeof(IncidentWorker))]
    [HarmonyPatch("TryExecute")]
    public static class PatchHStoryteller
    {
        static void Postfix()
        {
            Log.Message("tryfire postfix");
            Log.Message(Current.Game.storyteller.def.defName);
            /*Log.Message(Find.Storyteller.storytellerComps.ToString());*/

            changeStoryTeller();

            Log.Message("Storyteller changed.");
            Log.Message(Current.Game.storyteller.def.defName);
        }

        private static void changeStoryTeller()
        {
            if (Rand.RangeInclusive(0, 100) < 5)
            {
                chooseStoryTeller();
            }
            else
            {
                Log.Message("Storyteller remains... for now");
            }
            //TODO: This method doesn't work.
        }


        private static void chooseStoryTeller()
        {
            IEnumerable<StorytellerDef> storytellers = DefDatabase<StorytellerDef>.AllDefs;

            StorytellerDef newStorytellerDef = storytellers.RandomElement();
            Log.Message("Storyteller chosen is: " + newStorytellerDef.defName);
            if (!newStorytellerDef.listVisible || newStorytellerDef == Current.Game.storyteller.def)
            {
                Log.Warning("new storyteller not suitable. Rechoosing.");
                chooseStoryTeller();
            }
            else
            {
                Current.Game.storyteller.def = newStorytellerDef;
                Current.Game.storyteller.Notify_DefChanged();
            }
        }
    }
}

using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Hotseat
{
    [HarmonyPatch(typeof(IncidentWorker))]
    [HarmonyPatch("TryExecute")]
    public static class PatchStoryteller
    {
        static void Postfix()
        {
            Log.Message("tryfire postfix");
            Log.Message(Current.Game.storyteller.def.defName);

            changeStoryTeller();

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

                Log.Message("Storyteller changed.");
            }
        }
    }
}

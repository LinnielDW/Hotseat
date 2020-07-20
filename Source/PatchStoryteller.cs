using HarmonyLib;
using RimWorld;
using Verse;

namespace Hotseat
{
    [HarmonyPatch(typeof(IncidentWorker))]
    [HarmonyPatch("TryExecute")]
    public static class PatchStoryteller
    {
        static void Postfix()
        {
            if (HotseatSettings.enableStorytellerSwitching) {
                Log.Message("TryExecute Postfix: Storyteller before is:" + Current.Game.storyteller.def.defName);
                HotseatUtils.TryChangeStoryTeller(HotseatSettings.changeOnEventChance);
            }
        }

    }
}

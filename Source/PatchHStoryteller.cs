﻿using HarmonyLib;
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
                HotseatUtils.TryChangeStoryTeller(HotseatSettings.changeOnEventChance);
            }
        }

    }
}

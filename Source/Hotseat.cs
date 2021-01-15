using HarmonyLib;
using System.Reflection;
using Hotseat.Utils;
using UnityEngine;
using Verse;

namespace Hotseat
{
    public class Hotseat : Mod
    {
        readonly HotseatSettings settings;

        public Hotseat(ModContentPack content) : base(content)
        {
            settings = GetSettings<HotseatSettings>();

            var harmony = new Harmony("com.arquebus.rimworld.mod.hotseat");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            SettingsDrawUtil.DoWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "HotseatSettingsTitle".Translate();
        }
    }
}
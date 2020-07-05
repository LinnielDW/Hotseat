using HarmonyLib;
using System.Reflection;
using UnityEngine;
using Verse;


namespace Hotseat
{
    public class Hotseat : Mod
    {

        HotseatSettings settings;

        public Hotseat(ModContentPack content) : base(content)
        {
            settings = GetSettings<HotseatSettings>();

            var harmony = new Harmony("com.arquebus.rimworld.mod.hotseat");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            Harmony.DEBUG = true;

            FileLog.Log("Hotseat harmony log");
            Log.Message("Hotseat Loaded");
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            settings.DoWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "HotseatModSettings";
        }

    }
}
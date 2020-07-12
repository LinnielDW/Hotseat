
using HarmonyLib;
using HugsLib;
using Verse;

namespace Hotseat
{
    public class Hotseat : ModBase
    {
<<<<<<< HEAD
        public override string ModIdentifier
        {
            get { return "Hotseat"; }
        }
        public static Hotseat Instance { get; private set; }
        public Hotseat()
        {
            FileLog.Log("Hotseat harmony log");
            Log.Message("Hotseat Loaded");
            Instance = this;
=======
        HotseatSettings settings;

        public Hotseat(ModContentPack content) : base(content)
        {
            settings = GetSettings<HotseatSettings>();

            var harmony = new Harmony("com.arquebus.rimworld.mod.hotseat");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            Log.Message("Hotseat Loaded");
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            settings.DoWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "HotseatSettingsTitle".Translate();
>>>>>>> settings_no_hugs
        }
    }
}
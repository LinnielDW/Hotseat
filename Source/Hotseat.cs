
using HarmonyLib;
using HugsLib;
using Verse;

namespace Hotseat
{
    public class Hotseat : ModBase
    {
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
        }
    }
}
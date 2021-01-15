using Verse;

namespace Hotseat.SettingsDomain
{
    public class StorytellerHotseatStettings : IExposable
    {
        public bool storytellerEnabledBool = true;
        public float storytellerWeight = 1f;

        public void ExposeData()
        {
            Scribe_Values.Look(ref storytellerEnabledBool, "storytellerEnabledBool", true);
            Scribe_Values.Look(ref storytellerWeight, "storytellerWeight", 1f);
        }
    }
}

using Verse;

namespace Hotseat
{
    public class StorytellerEnabled : IExposable
    {
        public bool storytellerEnabledBool = true;

        public void ExposeData()
        {
            Scribe_Values.Look(ref storytellerEnabledBool, "storytellerEnabledBool");
        }
    }
}

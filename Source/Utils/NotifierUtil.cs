using RimWorld;
using Verse;

namespace Hotseat.Utils
{
    public static class NotifierUtil
    {
        public static void SendStorytellerChangeLetter()
        {
            Find.LetterStack.ReceiveLetter("StorytellerChangeLetterTitle".Translate(), GetStorytellerChangeLetterDescription(), LetterDefOf.NeutralEvent, null);
        }

        private static string GetStorytellerChangeLetterDescription()
        {   
            return string.Format("StorytellerChangeLetterDescription".Translate(), Current.Game.storyteller.def.label);
        }
    }
}

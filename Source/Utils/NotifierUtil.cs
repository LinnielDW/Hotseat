using RimWorld;
using Verse;

namespace Hotseat.Utils
{
    public static class NotifierUtil
    {
        public static void SendStorytellerChangeLetter()
        {
            Find.LetterStack.ReceiveLetter("StorytellerChangeLetterTitle".Translate(), GetStorytellerChangeLetterDescription(), LetterDefOf.NeutralEvent);
        }

        private static TaggedString GetStorytellerChangeLetterDescription()
        {   
            return "StorytellerChangeLetterDescription".Translate(Current.Game.storyteller.def.label, Current.Game.storyteller.def.description);
        }
    }
}

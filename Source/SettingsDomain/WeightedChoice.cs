using RimWorld;

namespace Hotseat.SettingsDomain
{
    public class WeightedChoice
    {
        public StorytellerDef storytellerDef;
        public int lft;
        public int rght;

        public WeightedChoice(StorytellerDef storytellerDef, int i, float vStorytellerWeight)
        {
            this.storytellerDef = storytellerDef;
            lft = i;
            rght = (int) vStorytellerWeight;
        }

        public override string ToString()
        {
            return storytellerDef.defName + "{ lft: " + lft + " rght: "+ rght + " }";
        }
    }
}
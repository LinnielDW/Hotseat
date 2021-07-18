using Hotseat.Utils;
using Verse;

namespace Hotseat
{
    class HotseatGameComponent : GameComponent
    {
        public HotseatGameComponent(Game game)
        {
        }

        public override void GameComponentTick()
        {
            base.GameComponentTick();
            //TODO: hook into Rimworld.DateNotifier logic to determine month instead.
            if (!HotseatSettings.enableStorytellerSwitching) return;
            
            var currentTick = Find.TickManager.TicksGame;
            
            if (currentTick % 900000 == 0) //Quadrum
            {
                if (currentTick % 3600000 == 0) //Year
                {
                    //Log.Message("Year happened. Check if storyteller changed.");
                    DecisionUtil.TryChangeStoryTeller(HotseatSettings.changeOnYearChance);
                    return;
                }
                //Log.Message("Quadrum happened. Check if storyteller changed.");
                DecisionUtil.TryChangeStoryTeller(HotseatSettings.changeOnQuadrumChance);
            }
        }
    }
}

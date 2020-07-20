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

            int currentTick = Find.TickManager.TicksGame;
            if (currentTick % 900000 == 0) //Quadrum
            {
                if (currentTick % 3600000 == 0) //Year
                {
                    Log.Message("Year happened. Check if storyteller changed.");
                    HotseatUtils.TryChangeStoryTeller(HotseatSettings.changeOnYearChance);
                    return;
                }
                Log.Message("Quadrum happened. Check if storyteller changed.");
                HotseatUtils.TryChangeStoryTeller(HotseatSettings.changeOnQuadrumChance);
            }
        }
    }
}

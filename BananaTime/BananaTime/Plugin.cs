using BananaTime.BananaTime.Pages;
using BepInEx;
using Photon.Pun;
namespace BananaTime.BananaTime
{
    [BepInPlugin(Constants.GUID, Constants.Name, Constants.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static void TimeOfDay(int time)
        {
            BetterDayNightManager.instance.SetTimeOfDay(time);
        }

        public static void RainToggle(bool kaboom)
        {
            if (BetterDayNightManager.instance.weatherCycle != null)
            {
                for (int i = 1; i < BetterDayNightManager.instance.weatherCycle.Length; i++)
                {
                    BetterDayNightManager.instance.weatherCycle[i] = kaboom
                        ? BetterDayNightManager.WeatherType.Raining
                        : BetterDayNightManager.WeatherType.None;
                }
            }
        }
    }
}
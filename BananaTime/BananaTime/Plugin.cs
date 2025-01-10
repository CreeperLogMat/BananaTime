using System;
using System.ComponentModel;
using BananaTime.BananaTime.Pages;
using BepInEx;
using GorillaTag.CosmeticSystem;

namespace BananaTime.BananaTime
{
    [BepInPlugin(Constants.GUID, Constants.Name, Constants.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static bool IsModdedRoom;

        void Update()
        {
            if (!IsModdedRoom)
            {
                BetterDayNightManager.instance.currentSetting = TimeSettings.Normal;
                SwitchToPage(typeof(Page));
            }
        }

        private void SwitchToPage(Type type)
        {
            throw new NotImplementedException();
        }

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
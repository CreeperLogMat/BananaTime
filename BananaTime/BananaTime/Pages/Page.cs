using BananaOS;
using BananaOS.Pages;
using Photon.Pun;
using System.Text;
using UnityEngine;

namespace BananaTime.BananaTime.Pages
{
    public class Page : WatchPage
    {
        public override bool DisplayOnMainMenu => true;
        private const string PageTitle = "<color=yellow>Banana</color><color=yellow>Time</color>";
        public override string Title => PageTitle;
        private readonly Color notificationColor = Color.blue;
        private readonly Color textColor = Color.white;
        bool Raining;
        public static bool IsModdedRoom => NetworkSystem.Instance.InRoom && NetworkSystem.Instance.GameModeString.Contains("MODDED");
        void Start()
        {
            NetworkSystem.Instance.OnJoinedRoomEvent += OnJoinedRoom;
            NetworkSystem.Instance.OnReturnedToSinglePlayer += OnLeaveRoom;
        }

        void OnLeaveRoom()
        {
            BetterDayNightManager.instance.currentSetting = TimeSettings.Normal;
        }

        void OnJoinedRoom()
        {
            if (!IsModdedRoom)
            {
                BetterDayNightManager.instance.currentSetting = TimeSettings.Normal;
            }
        }
        public override void OnPostModSetup()
        {
            selectionHandler.maxIndex = 4;
        }

        public override string OnGetScreenContent()
        {
            var content = new StringBuilder();

            content.AppendLine($"<color=yellow>==</color> {PageTitle} <color=yellow>==</color>");
            content.AppendLine($"By <color=#9825F8>defaultuser0</color> and <color=#228B22>Cody</color>");
            content.AppendLine($"    ");
            content.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(0, "Morning"));
            content.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(1, "Day"));
            content.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(2, "Evening"));
            content.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(3, "Night"));
            content.AppendLine($"    ");
            content.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(4, "Toggle Rain"));

            return content.ToString();
        }

        public override void OnButtonPressed(WatchButtonType buttonType)
        {
            if (buttonType == WatchButtonType.Up)
                selectionHandler.MoveSelectionUp();

            else if (buttonType == WatchButtonType.Down)
                selectionHandler.MoveSelectionDown();

            else if (buttonType == WatchButtonType.Enter)
                HandleEnterButtonPress();

            else if (buttonType == WatchButtonType.Back)
                ReturnToMainMenu();
        }

        private void HandleEnterButtonPress()
        {
            switch (selectionHandler.currentIndex)
            {
                case 0:
                    if (IsModdedRoom)
                    {
                        Plugin.TimeOfDay(1);
                        SendNotification("<align=center><size=5>  Time Set To Morning!");
                    } 
                    else
                    {
                        SendErrorNoti("<align=center><size=5> NOT IN MODDED ROOM!");
                    }
 
                    break;
                case 1:
                    if (IsModdedRoom)
                    {
                        Plugin.TimeOfDay(3);
                        SendNotification("<align=center><size=5>  Time Set To Day!");
                    }
                    else
                    {
                        SendErrorNoti("<align=center><size=5> NOT IN MODDED ROOM!");
                    }
                    break;
                case 2:
                    if (IsModdedRoom)
                    {
                        Plugin.TimeOfDay(7);
                        SendNotification("<align=center><size=5>  Time Set To Evening!");
                    }
                    else
                    {
                        SendErrorNoti("<align=center><size=5> NOT IN MODDED ROOM!");
                    }
                    break;
                case 3:
                    if (IsModdedRoom)
                    {
                        Plugin.TimeOfDay(0);
                        SendNotification("<align=center><size=5>  Time Set To Night!");
                    }
                    else
                    {
                        SendErrorNoti("<align=center><size=5> NOT IN MODDED ROOM!");
                    }
                    break;
                case 4:
                    if (IsModdedRoom)
                    {
                        SendNotification("<align=center><size=5> Rain Has Been Toggled");
                        Raining = !Raining;
                        Plugin.RainToggle(Raining);                        
                    }
                    else
                    {
                        SendErrorNoti("<align=center><size=5> NOT IN MODDED ROOM!");
                    }
                    break;
            }
        }

        private void SendNotification(string message)
        {
            BananaNotifications.DisplayNotification(message, notificationColor, textColor, 0.8f);
        }
        
        private void SendErrorNoti(string message)
        {
            BananaNotifications.DisplayNotification(message, Color.red, Color.white, 0.8f);
        }
    }
}
using BananaOS;
using BananaOS.Pages;
using System.Text;
using UnityEngine;

namespace BananaTime.BananaTime.Pages
{
    public class Page : WatchPage
    {
        private const string PageTitle = "<color=yellow>Banana</color><color=yellow>Time</color>";

        private readonly Color notificationColor = Color.blue;
        private readonly Color textColor = Color.white;

        public override bool DisplayOnMainMenu => true;
        bool Raining;

        public override string Title => PageTitle;

        public override void OnPostModSetup()
        {
            selectionHandler.maxIndex = 4;
        }

        public override string OnGetScreenContent()
        {
            var content = new StringBuilder();

            content.AppendLine($"<color=yellow>==</color> {PageTitle} <color=yellow>==</color>");
            content.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(0, "Morning"));
            content.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(1, "Day"));
            content.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(2, "Evening"));
            content.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(3, "Night"));
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
            if (!Plugin.IsModdedRoom) return;

            switch (selectionHandler.currentIndex)
            {
                case 0:
                    SendNotification("<align=center><size=5>  Time Set To Morning!");
                    Plugin.TimeOfDay(1);
                    break;
                case 1:
                    SendNotification("<align=center><size=5>  Time Set To Day!");
                    Plugin.TimeOfDay(3);
                    break;
                case 2:
                    SendNotification("<align=center><size=5>  Time Set To Evening!");
                    Plugin.TimeOfDay(7);
                    break;
                case 3:
                    SendNotification("<align=center><size=5>  Time Set To Night!");
                    Plugin.TimeOfDay(0);
                    break;
                case 4:
                    SendNotification("<align=center><size=5> Rain Has Been Toggled");
                    Raining = !Raining;
                    Plugin.RainToggle(Raining);
                    break;
            }
        }

        private void SendNotification(string message)
        {
            BananaNotifications.DisplayNotification(message, notificationColor, textColor, 0.8f);
        }
    }
}
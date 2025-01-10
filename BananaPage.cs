using System;
using System.Collections.Generic;
using System.Text;
using BananaOS;
using BananaOS.Pages;
using UnityEngine;
using GorillaNetworking;

namespace BananaTime
{
    public class Page : WatchPage
    {
        private const string PageTitle = "<color=#FFEA00>BananaTime</color>";

        public override bool DisplayOnMainMenu => true;

        public override string Title => PageTitle;

        public override void OnPostModSetup()
        {
            selectionHandler.maxIndex = 3;
        }

        public override string OnGetScreenContent()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"<color=#FFEA00>BananaTime</color>");
            stringBuilder.AppendLine($"By <color=#9825F8>defaultuser0</color>");
            stringBuilder.AppendLine($"    ");
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(0, "Morning"));
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(1, "Day"));
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(2, "Evening"));
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(3, "Night"));

            return stringBuilder.ToString();
        }

        public override void OnButtonPressed(WatchButtonType buttonType)
        {
            switch (buttonType)
            {
                case WatchButtonType.Up:
                    selectionHandler.MoveSelectionUp();
                    break;

                case WatchButtonType.Down:
                    selectionHandler.MoveSelectionDown();
                    break;

                case WatchButtonType.Enter:
                    HandleEnterButtonPress();
                    break;

                case WatchButtonType.Back:
                    ReturnToMainMenu();
                    break;
            }
        }

        private void HandleEnterButtonPress()
        {
            switch (selectionHandler.currentIndex)
            {
                case 0:
                    BetterDayNightManager.instance.SetTimeOfDay(1);
                    break;

                case 1:
                    BetterDayNightManager.instance.SetTimeOfDay(3);
                    break;

                case 2:
                    BetterDayNightManager.instance.SetTimeOfDay(7);
                    break;

                case 3:
                    BetterDayNightManager.instance.SetTimeOfDay(0);
                    break;

                default:
                    break;
            }
        }
    }
}

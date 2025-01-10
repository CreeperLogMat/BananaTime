using System;
using HarmonyLib;
using Photon.Pun;
using UnityEngine;

namespace BananaTime.BananaTime.Patches
{
    public static class RoomPatches
    {
        public static bool IsModdedLobby()
        {
            if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("gameMode", out var gameMode))
            {
                return gameMode.ToString().Contains("MODDED", StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
    }

    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnJoinedRoom")]
    public class RoomJoinPatch
    {
        public static void Postfix()
        {
            Plugin.IsModdedRoom = RoomPatches.IsModdedLobby();
        }
    }

    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnLeftRoom")]
    public class RoomLeftPatch
    {
        public static void Postfix()
        {
            Plugin.IsModdedRoom = false;
        }
    }
}
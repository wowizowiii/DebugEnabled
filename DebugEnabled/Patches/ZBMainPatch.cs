using HarmonyLib;
using TMPro;

namespace AetharNet.Mods.ZumbiBlocks2.DebugEnabled.Patches;

[HarmonyPatch(typeof(ZBMain))]
public static class ZBMainPatch
{
    private static string OriginalVersionText;

    [HarmonyPostfix]
    [HarmonyPatch(nameof(ZBMain.Start))]
    [HarmonyPatch(nameof(ZBMain.OnEnteredMap))]
    [HarmonyPatch(nameof(ZBMain.CleanUp))]
    [HarmonyPriority(Priority.Last)]
    public static void ToggleDebugMode(ZBMain __instance)
    {
        // Save original version text
        // This patch runs last so other mods can modify the version text
        OriginalVersionText ??= __instance.versionText.text;
        // Only enable debug mode if not in-game, or if hosting a game
        __instance.debugEnabled = !__instance.mapIsLoaded || __instance.multiplayer.IsServer();
        // Add debug label if debug mode is enabled
        __instance.versionText.text = (DebugController.DebugEnabled ? "[Debug] " : string.Empty) + OriginalVersionText;
        // Disable word-wrapping to prevent the version label from overflowing
        __instance.versionText.textWrappingMode = TextWrappingModes.NoWrap;
    }
}

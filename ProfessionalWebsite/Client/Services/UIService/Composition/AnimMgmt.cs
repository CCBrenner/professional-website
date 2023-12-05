﻿namespace ProfessionalWebsite.Client.Services.UI;

public static class AnimMgmt
{
    private const string DISCONTINUE_BTN_ACTIVE_CLASS_NAME = "discontinue-button-on";
    private const int DISCONTINUE_BTN_PANEL_ID = 8;
    public static string ToggleAnimation(int animationIndex, string animateMain, List<bool> isContinuous, Dictionary<int, Panel> panels, List<PanelGroup> panelGroupsList)
    {
        if (animateMain == $"main{animationIndex + 1}-infinite" || animateMain == $"main{animationIndex + 1}")
        {
            SetDiscontinueButton(string.Empty, panels, panelGroupsList);
            return string.Empty;
        }
        else if (isContinuous[animationIndex])
        {
            SetDiscontinueButton(DISCONTINUE_BTN_ACTIVE_CLASS_NAME, panels, panelGroupsList);
            return $"main{animationIndex + 1}-infinite";
        }
        else
        {
            SetDiscontinueButton(string.Empty, panels, panelGroupsList);
            return $"main{animationIndex + 1}";
        }
    }
    public static string ToggleContinuousAnimation(int animationIndex, string animateMain, Dictionary<int, Panel> panels, List<PanelGroup> panelGroupsList)
    {
        if (animateMain == $"main{animationIndex + 1}-infinite" || animateMain == $"main{animationIndex + 1}")
        {
            SetDiscontinueButton(string.Empty, panels, panelGroupsList);
            return string.Empty;
        }
        else
        {
            SetDiscontinueButton(DISCONTINUE_BTN_ACTIVE_CLASS_NAME, panels, panelGroupsList);
            return $"main{animationIndex + 1}-infinite";
        }
    }
    public static string ToggleOnePlayAnimation(int animationIndex, string animateMain, Dictionary<int, Panel> panels, List<PanelGroup> panelGroupsList)
    {
        if (animateMain == $"main{animationIndex + 1}-infinite" || animateMain == $"main{animationIndex + 1}")
        {
            SetDiscontinueButton(string.Empty, panels, panelGroupsList);
            return string.Empty;
        }
        else
        {
            SetDiscontinueButton(string.Empty, panels, panelGroupsList);
            return $"main{animationIndex + 1}";
        }
    }
    public static string DiscontinueAnimation(Dictionary<int, Panel> panels, List<PanelGroup> panelGroupsList)
    {
        SetDiscontinueButton(string.Empty, panels, panelGroupsList);
        return string.Empty;
    }
    private static void SetDiscontinueButton(string discontinue, Dictionary<int, Panel> panels, List<PanelGroup> panelGroupsList)
    {
        if (discontinue == string.Empty)
            PanelMgmt.DeactivatePanel(DISCONTINUE_BTN_PANEL_ID, panels);
        else
            PanelMgmt.ActivatePanel(DISCONTINUE_BTN_PANEL_ID, panels, panelGroupsList);
    }
}

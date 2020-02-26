using UnityEngine;
using System.Collections;
using Doozy.Engine.UI;

public static class Popups {
    public const string UNLOCK_POPUP = "UnlockPopup";
    public const string CONTROLSTAR_POPUP = "ControlStarsPopup";
    public const string BOTERKROON_FAILEDSKILLTEST = "FailedSkillTest";
    public const string BOTERKROON_NEWSKILL = "NewSkill";
    public const string BOTERKROON_CONTROLWITHOUTTRAINING = "ControlWithoutTraining";

    public static void ShowPopup(string target) {
        UIPopup popup = UIPopup.GetPopup(target);
        popup.Show();
    }
}

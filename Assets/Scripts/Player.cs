using System.Collections.Generic;

[System.Serializable]
public struct Player
{
    public int id;
    public string name;
    public int levelMoney;
    public int allMoney;

    private List<string> _completedTutorials;
    private List<string> _viewedPopups;

    public Player(int tempId) {
        id = tempId;
        name = "Player";
        levelMoney = 0;
        allMoney = 0;
        _completedTutorials = new List<string>();
        _viewedPopups = new List<string>();
    }

    public void Addmoney(int moneyCount = 1) {
        levelMoney += moneyCount;
        allMoney += moneyCount;
    }

    public void ResetLevelMoney() {
        levelMoney = 0;
    }

    public void DeductMoney(int coinCount = 1) {
        allMoney = allMoney - coinCount < 0 ? 0 : allMoney - coinCount;
        levelMoney = levelMoney - coinCount < 0 ? 0 : levelMoney - coinCount;
    }

    public bool IsTutorialComplete(string tutorialType) {
        if (_completedTutorials != null)
            return _completedTutorials.Contains(tutorialType);
        return false;

    }

    public bool IsPopupViewed(string popupType) {
        if(_viewedPopups != null)
            return _viewedPopups.Contains(popupType);
        return false;
    }

    public void CompleteTutorial(string tutorialType) {
        _completedTutorials.Add(tutorialType);
    }

    public void SetPopupAsViewed(string popupType) {
        _viewedPopups.Add(popupType);
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Player {

    public int id;
    public string name;
    public int levelMoney;
    public int totalMoney;

    private List<RecordLine> _scoreList;
    private List<string> _completedTutorials;
    private List<string> _viewedPopups;

    public Player(int tempId) {
        id = tempId;
        name = "Player";
        levelMoney = 0;
        totalMoney = 0;
        _completedTutorials = new List<string>();
        _viewedPopups = new List<string>();
        _scoreList = new List<RecordLine>();
    }

    public void SetRecordTable() {
        RecordTable.GetPlayerRecord(_scoreList);
    }

    public void AddMoney(int moneyCount = 1) {
        levelMoney += moneyCount;
        totalMoney += moneyCount;
    }

    public void ClosePlayerData() {
        if (GameManager.instance.currentLevelType == Constants.LEVEL_TYPE_LINEAR) SetRecord();
        RecordTable.CleanRecordLineStats();
        ResetLevelMoney();
    }

    private void ResetLevelMoney() {
        levelMoney = 0;
    }

    public void ReduceTotalMoney(int coinCount = 1) {
        totalMoney = totalMoney - coinCount < 0 ? 0 : totalMoney - coinCount;
        levelMoney = levelMoney - coinCount < 0 ? 0 : levelMoney - coinCount;
    }

    public bool IsTutorialComplete(string tutorialType) {
        if (_completedTutorials != null)
            return _completedTutorials.Contains(tutorialType);
        return false;
    }

    public bool IsPopupViewed(string popupType) {
        if (_viewedPopups != null)
            return _viewedPopups.Contains(popupType);
        return false;
    }

    public void CompleteTutorial(string tutorialType) {
        if (!_completedTutorials.Contains(tutorialType))
            _completedTutorials.Add(tutorialType);
    }

    public void SetPopupAsViewed(string popupType) {
        if (!_viewedPopups.Contains(popupType))
            _viewedPopups.Add(popupType);
    }

    private void SetRecord() {
        RecordTable.SaveRecordInList();
        _scoreList = RecordTable.GetRecordTable();
    }
}


using System.Collections.Generic;

public static class RecordTable {

    private static int _groundPieceCost = 10;
    private static int _obstacleCost = 30;
    private static int _bonusCost = 50;
    private static int _coinCost = 1;

    private static List<RecordLine> _recordTable = new List<RecordLine>();
    private static RecordLine _recordLine = new RecordLine();

    public static void GetPlayerRecord(List<RecordLine> playerRecords) {
        _recordTable = playerRecords;
    }

    public static void AddObstacleCost(int costMultiplier = 1) {
        _recordLine.score += _obstacleCost * costMultiplier;
        _recordLine.obstacleCount++;
    }

    public static void AddGroundCost(int costMultiplier = 1) {
        _recordLine.score += _groundPieceCost * costMultiplier;
        _recordLine.groundCount++;
    }

    public static void AddBonusCost(int costMultiplier = 1) {
        _recordLine.score += _bonusCost * costMultiplier;
        _recordLine.bonusCount++;
    }

    public static void AddCoinCost(int costMultiplier = 1) {
        _recordLine.score += _coinCost * costMultiplier;
        _recordLine.coinCount++;
    }

    public static RecordLine GetRecord() {
        return _recordLine;
    }

    public static List<RecordLine> GetRecordTable() {
        return _recordTable;
    }

    public static void SaveRecordInList() {
        int linePosition = 0;
        for (int line = 0; line > _recordTable.Count; line++) {
            if (_recordLine.score < _recordTable[line].score) {
                linePosition = line;
                break;
            }
        }
        _recordTable.Insert(linePosition, _recordLine);
    }
}
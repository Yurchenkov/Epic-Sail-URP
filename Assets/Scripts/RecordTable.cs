
public static class RecordTable {

    private static int _groundPieceCost = 10;
    private static int _obstacleCost = 30;
    private static int _bonusCost = 50;
    private static int _coinCost = 1;

    private static int _playersScore = 0;
    private static int _groundPieceCount = 0;
    private static int _obstacleCount = 0;
    private static int _bonusCount = 0;

    public static void AddObstacleCost(int costMultiplier = 1) {
        _playersScore += _obstacleCost * costMultiplier;
        _obstacleCount++;
    }

    public static void AddGroundCost(int costMultiplier = 1) {
        _playersScore += _groundPieceCost * costMultiplier;
        _groundPieceCount++;
    }

    public static void AddBonusCost(int costMultiplier = 1) {
        _playersScore += _bonusCost * costMultiplier;
        _bonusCount++;
    }

    public static void AddCoinCost(int costMultiplier = 1) {
        _playersScore += _coinCost * costMultiplier;
    }

    public static int GetRecord() {
        return _playersScore;
    }

    public static string GetRecordStats() {
        return "Distance: " + _groundPieceCount + ". Bonuses: " + _bonusCount + ". Obstacle: " + _obstacleCount + ". Coins: " + GameManager.instance.playerData.levelMoney;
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    #region Variables
    private GameObject player;
    private Camera mainCamera;

    public int RewardScore { get; set; }
    public int DistanceScore { get; set; }

    private int inGameBounceCount = 0;
    private int inGameShieldCount = 0;
    private int inGameEraseCount = 0;
    private int inGameMagnetCount = 0;
    #endregion


    protected override void Awake() {
        base.Awake();
        player = GameObject.FindWithTag("Player");
        mainCamera = Camera.main;
    }

    private void Update() {
        if (IsPlayerInFrame()) {
            DistanceScore = Mathf.Max(DistanceScore, -Mathf.FloorToInt(player.transform.position.y/5));
            UiManager.Instance.UpdateScore(RewardScore + DistanceScore);
        }
    }

    private bool IsPlayerInFrame() {
        bool isInFrame = true;
        Vector3 screenPos = mainCamera.WorldToScreenPoint(player.transform.position);
        float xRatio = screenPos.x / mainCamera.pixelWidth; //horizontal check
        float yRatio = screenPos.y / mainCamera.pixelHeight; //vertical chack
        if(xRatio > 1.05f || xRatio < -.05f) {
            isInFrame = false;
            Ball.Instance.WentOutOfFrame();
        }
        if(yRatio > 1.05f || yRatio < -.05f) {
            isInFrame = false;
            UiManager.Instance.FadeIn();
        }
        return isInFrame;
    }

    public void GameOver() {
        int leapOfFaithScore = LineManager.Instance.IsLeapOfFaith ? GameManager.Instance.DistanceScore + GameManager.Instance.RewardScore : LineManager.Instance.LeapOfFaith;
        Player.Instance.ChangeHighScores(RewardScore + DistanceScore, DistanceScore, RewardScore, leapOfFaithScore);
        Player.Instance.ChangSuccesCount(inGameMagnetCount, inGameShieldCount, inGameEraseCount, inGameBounceCount);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IncreaseScore(int addedScore) {
        RewardScore += addedScore;
        UiManager.Instance.UpdateScore(RewardScore+ DistanceScore);
    }

    public void AddToInGameBounceCount() {
        inGameBounceCount++;
    }

    public void AddToInGameEraseCount() {
        inGameEraseCount++;
    }

    public void AddToInGameMagnetCount() {
        inGameMagnetCount++;
    }

    public void AddToInGameShieldCount() {
        inGameShieldCount++;
    }
}

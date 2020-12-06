using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUD : MonoBehaviour {
    private Text score;
    private void Start() {
        DependencyInjector.GetDependency<IScoreManager>().scoreChangedDelegate += updateScore;
        score = GetComponent<Text>();
    }
    private void OnDestroy(){
        DependencyInjector.GetDependency<IScoreManager>().scoreChangedDelegate -= updateScore;
    }
    public void updateScore(IScoreManager scoreManager) {
        score.text = scoreManager.getPoints().ToString("0");
    }
}

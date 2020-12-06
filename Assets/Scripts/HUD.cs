﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour {
    private TextMeshProUGUI score;
    private void Start() {
        DependencyInjector.GetDependency<IScoreManager>().scoreChangedDelegate += updateScore;
        score = GetComponent<TextMeshProUGUI>();
    }
    private void OnDestroy(){
        DependencyInjector.GetDependency<IScoreManager>().scoreChangedDelegate -= updateScore;
    }
    public void updateScore(IScoreManager scoreManager) {
        score.text = "Score: " + scoreManager.getPoints().ToString("0");
    }
}

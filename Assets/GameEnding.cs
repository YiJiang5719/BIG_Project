using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;

    //用于调节通关时alpha值的Canvasgroup
    public CanvasGroup exitBackgroundImageCanvasGroup;

    //玩家是否到触发点
    bool m_IsplayerAtExit;
    //计时器
    float m_Timer;

    private void OnTriggerEnter(Collider other)
    {
        //如果进入触发器范围的是玩家，改变布尔变量
        if (other.gameObject == player)
        {
            m_IsplayerAtExit = true;
        }
    }

    private void Update()
    {
        //如果玩家到达出口，执行Endlevel();
        if(m_IsplayerAtExit)
        {
            EndLevel();
        }
    }

    void EndLevel()
    {
        m_Timer = m_Timer + Time.deltaTime;
        //alpha的值随着百分比改变
        exitBackgroundImageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            Application.LoadLevel("LoadingBar");
        }
    }

}

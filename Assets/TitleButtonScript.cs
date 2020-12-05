using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtonScript : MonoBehaviour
{
    // 効果音の変数
    public AudioClip sound1;

    // AudioSourceの変数
    AudioSource audioSource;

    void Start()
    {
        // AudioSourceを取得
        audioSource = GetComponent<AudioSource>();
    }

    // ボタンが押されたとき
    public void OnClickStartButton()
    {
        // 効果音を再生
        this.audioSource.PlayOneShot(sound1);

        // ゲームシーンへ遷移する
        SceneManager.LoadScene("MainScene");
    }
}

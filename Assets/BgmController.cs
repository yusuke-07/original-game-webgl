using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmController : MonoBehaviour
{
    // 2つのBGMの変数
    public AudioClip[] bgm;

    // AudioSourceの変数
    AudioSource audioSource;

    // 時間計測用の変数
    private float delta = 0;

    // キャラクターが入る変数
    GameObject character;

    // CharacterControllerスクリプトが入る変数
    CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        // AudioSourceを取得
        audioSource = GetComponent<AudioSource>();

        // AudioClipに最初のBGMをセット
        audioSource.clip = bgm[0];
        // 再生
        audioSource.Play();

        // Characterオブジェクトを取得
        character = GameObject.Find("Character");
        // CharacterオブジェクトのCharacterControllerスクリプトを取得
        characterController = character.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // 時間経過の処理
        this.delta += Time.deltaTime;

        // CharacterControllerスクリプトのlp変数を代入
        int characterLp = characterController.lp;

        // 15秒が経過したらAudioClipに2番目のBGMをセットする（BGMが停止する）
        if (this.delta >= 15)
        {
            audioSource.clip = bgm[1];
        }

        // AudioClipが再生されていない場合、再生を開始する（ハードモード用のBGMを再生）
        if (audioSource.isPlaying  == false)
        {
            audioSource.Play();
        }

        // ライフポイントが0になった場合、BGMの再生を停止する
        if (characterLp == 0)
        {
            audioSource.Stop();
        }
    }
}

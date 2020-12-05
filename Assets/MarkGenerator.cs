using System.Collections;
using UnityEngine;

public class MarkGenerator : MonoBehaviour
{
    // マークのPrefab
    public GameObject[] markPrefabs;

    // マークのPrefab(ハードモード用)
    public GameObject[] markPrefabsHard;

    // マークの生成位置y座標
    private float genPosY = 6;

    // 時間計測用の変数
    private float delta = 0;

    // 時間計測用の変数（ハードモード用）
    public float deltaHard = 0;

    // Prefabの生成間隔
    private float span = 0.3f;

    // キャラクターが入る変数
    GameObject character;

    // CharacterControllerスクリプトが入る変数
    CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        // CharacterControllerスクリプトのlp変数を生成条件に使うため、スクリプトを取得
        // Characterオブジェクトを取得
        character = GameObject.Find("Character");
        // CharacterオブジェクトのCharacterControllerスクリプトを取得
        characterController = character.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // CharacterControllerスクリプトのlp変数を代入
        int characterLp = characterController.lp;

        // ライフポイントが残っている場合
        if (characterLp > 0)
        {
            // 時間経過の処理
            this.delta += Time.deltaTime;

            // 時間経過の処理（ハードモード移行用）
            this.deltaHard += Time.deltaTime;

            // マークをランダム生成する条件
            // 生成間隔の時間が経過した場合かつ15秒以内
            if (this.delta > this.span && this.deltaHard <= 15)
            {
                // 0に戻す
                this.delta = 0;

                // ランダムに生成するマークを選ぶ
                int markNo = Random.Range(0, 5);

                // ランダムにマークの生成位置X座標を選ぶ
                float genPosX = Random.Range(-9f, 9f);

                // マークを生成する
                GameObject go = Instantiate(markPrefabs[markNo]);
                go.transform.position = new Vector2(genPosX, genPosY);
            }
            // 15秒が経過したらハードモードに移行する
            else if (this.delta > this.span && this.deltaHard > 15)
            {
                // 生成間隔を早くする
                this.span = 0.1f;

                // 0に戻す
                this.delta = 0;

                // ランダムに生成するマークを選ぶ
                int markNoHard = Random.Range(0, 10);

                // ランダムにマークの生成位置X座標を選ぶ
                float genPosX = Random.Range(-9f, 9f);

                // マークを生成する
                GameObject go = Instantiate(markPrefabsHard[markNoHard]);
                go.transform.position = new Vector2(genPosX, genPosY);
            }

            // ハードモード移行時間計測用
            //Debug.Log(this.deltaHard);
        }

        // ライフポイントが0になった場合（メモ：指定したものはヒエラルキービューに生成されるオブジェクトではないのでこの処理はできない）
        //else if (characterLp == 0)
        //{
        // 生成済みのマークを消す
        //Destroy(markPrefabs[markNo]);
        //}
    }
}

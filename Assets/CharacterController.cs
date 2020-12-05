using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CharacterController : MonoBehaviour
{
    // ゲームオブジェクトの変数
    GameObject character;
    GameObject rightWall;
    GameObject leftWall;

    // 3つの効果音の変数
    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;

    // AudioSourceの変数
    AudioSource audioSource;

    // 移動速度
    private float speed = 14;

    // Rigidbody2Dの変数
    private Rigidbody2D rb;

    // スコアを表示するテキスト
    private GameObject scoreText;

    // GameOverを表示するテキスト
    private GameObject gameOverText;

    // ライフポイントを表示するテキスト
    private GameObject lifePointText;

    // 得点
    private int score;

    // ライフポイント
    public int lp = 3;

    void Start()
    {
        // AudioSourceを取得
        audioSource = GetComponent<AudioSource>();

        // Rigidbody2Dを取得
        rb = GetComponent<Rigidbody2D>();

        // ゲームオブジェクトを取得し宣言した変数に代入
        character = GameObject.Find("Character");
        rightWall = GameObject.Find("RightWall");
        leftWall = GameObject.Find("LeftWall");

        // シーン中のScoreTextオブジェクトを取得
        this.scoreText = GameObject.Find("ScoreText");
        
        // シーン中のGameOverTextオブジェクトを取得
        this.gameOverText = GameObject.Find("GameOverText");

        // シーン中のLifePointTextオブジェクトを取得
        this.lifePointText = GameObject.Find("LifePointText");
    }

    void FixedUpdate()
    {
        // キャラクターの移動範囲をゲーム画面内に制限する
        character.transform.position = new Vector2(Mathf.Clamp(character.transform.position.x, leftWall.transform.position.x, rightWall.transform.position.x), character.transform.position.y);

        // 左右のキー入力
        float horizontalKey = Input.GetAxis("Horizontal");

        //右入力で右向きに動く
        if (horizontalKey > 0)
        {
            rb.velocity = new Vector2(this.speed, rb.velocity.y);
        }
        //左入力で左向きに動く
        else if (horizontalKey < 0)
        {
            rb.velocity = new Vector2(-this.speed, rb.velocity.y);
        }
        //ボタンを離すと止まる
        else
        {
            rb.velocity = Vector2.zero;
        }


        // キャラクターの向きを移動方向に合わせて反転させる
        Vector2 lscale = gameObject.transform.localScale;

        if ((lscale.x > 0 && horizontalKey > 0) || (lscale.x < 0 && horizontalKey < 0))
        {
            lscale.x *= -1;
            gameObject.transform.localScale = lscale;
        }

    }    // Update is called once per frame
    void Update()
    {
        // ライフポイントが0になったらゲームオーバー画面になるのでキャラクターの位置(X軸)を中央に固定する
        if (this.lp == 0)
        {
            character.transform.position = new Vector2(Mathf.Clamp(character.transform.position.x, 0, 0), character.transform.position.y);

            // 左クリックしたらタイトルシーンへ遷移する
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("TitleScene");
            }

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
       // Niceマークに当たった場合かつライフポイントが残っている場合
       if (collision.gameObject.tag == "nicetag" && this.lp > 0)
        {
            // sound1を再生
            this.audioSource.PlayOneShot(sound1);

            // スコアを加算
            this.score += 10;
        }
        // Niceマーク2に当たった場合かつライフポイントが残っている場合
        else if (collision.gameObject.tag == "nice2tag" && this.lp > 0)
        {
            // sound2を再生
            this.audioSource.PlayOneShot(sound2);

            // スコアを加算
            this.score += 30;
        }
        // Badマークに当たった場合かつライフポイントが残っている場合
        else if (collision.gameObject.tag == "badtag" && this.lp > 0)
        {
            // sound3を再生
            this.audioSource.PlayOneShot(sound3);

            // ライフポイントを減らす
            this.lp -= 1;

            // ライフポイント表示を更新
            this.lifePointText.GetComponent<Text>().text = "LP:" + this.lp;
        }

        // スコアを表示する
        this.scoreText.GetComponent<Text>().text = this.score + "いいね";

        // ライフポイントが0になった場合
        if (this.lp == 0)
        {
            // GameOverを表示
            this.gameOverText.GetComponent<Text>().text = "GameOver";
        }
    }

}

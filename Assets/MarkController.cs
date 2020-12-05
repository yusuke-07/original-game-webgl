using UnityEngine;
using System.Collections;

public class MarkController : MonoBehaviour
{
    // Niceマークの移動速度
    private float niceSpeed = -7;

    // Niceマーク2の移動速度
    private float niceSpeed2 = -11;

    // Badマークの移動速度
    private float badSpeed = -13;

    // 消滅位置
    private float deadLine = -10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "nicetag")
        {
            // マークを移動させる
            transform.Translate(0, this.niceSpeed * Time.deltaTime, 0);
        }
        else if (gameObject.tag == "nice2tag")
        {
            // マークを移動させる
            transform.Translate(0, this.niceSpeed2 * Time.deltaTime, 0);
        }

        else if (gameObject.tag == "badtag")
        {
            // マークを移動させる
            transform.Translate(0, this.badSpeed * Time.deltaTime, 0);
        }

        // 画面外に出たら破棄する
        if (transform.position.y < this.deadLine)
        {
            Destroy(gameObject);
        }
    }

    // 衝突時に呼ばれる
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 衝突したマークを削除する
        Destroy(this.gameObject);
    }
}

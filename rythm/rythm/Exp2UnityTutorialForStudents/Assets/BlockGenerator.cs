using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockGenerator : MonoBehaviour {
    // ブロックのPrefabへの参照
    public GameObject BlockPrefab;
    // 生成したブロックインスタンスへの参照
    private GameObject _BlockInstance;
    // ブロックの最大生成数と残りブロックのカウント用変数
    // 今回は7列x8行を想定
    private static int _BlockMax = 56;
    private static int _BlockCount = 0;
    // ブロックの配置間隔(X方向とZ方向)
    private int _BlockStepX = 2;
    private int _BlockStepZ = -1;
    // 初期ブロックの生成位置(X軸とZ軸)
    private int _BlockInitX = -6;
    private int _BlockInitZ = 25;

    // ブロック生成のタイムスパンとタイムカウント用変数
    private float _TimeStep = 0.1f;
    private float _TimeCount;


    // Use this for initialization
    void Start () {
        // タイムカウントの初期化
        _TimeCount = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        // 設定したタイムスパンだけ時間が経過したら
        // ブロックを生成してタイムカウンタを再初期化
        if (_TimeCount + _TimeStep < Time.time) {
            GenerateBlock();
            _TimeCount = Time.time;
        }
	}

    void GenerateBlock()
    {
        // ブロックの数が最大数未満だったら新たなブロックを生成
        if (_BlockCount < _BlockMax) {
            // ブロックを生成する位置を計算してブロックを生成
            int xPos = _BlockCount % 7 * _BlockStepX + _BlockInitX;
            int zPos = _BlockCount / 7 * _BlockStepZ + _BlockInitZ;
            _BlockInstance = Instantiate(BlockPrefab, new Vector3(xPos, 0, zPos), Quaternion.identity);
            // ブロックインスタンスの名前を変更
            _BlockInstance.name = "Block" + _BlockCount;
            // ブロックインスタンスの親をBlockMasterに設定
            _BlockInstance.transform.SetParent(transform, false);
            //*********************************************//
            // ブロックインスタンスにBlockControllerを適用
            _BlockInstance.AddComponent<BlockController>();
            //*********************************************//
            // ブロックインスタンスの色をランダムに変更
            _BlockInstance.GetComponent<Renderer>().material.color
                = new Color(Random.value, Random.value, Random.value, 1.0f);
            // ブロックカウンタを増やす
            _BlockCount++;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    private NavigationScript ns;

    /// <summary>
    /// 設定多久切換路徑
    /// </summary>
    public float chgTimeDef;
    /// /// <summary>
    /// 設定多久切換路徑(計算用)
    /// </summary>
    [Header("勿動")]
    [SerializeField]
    private float chgTime;

    /// <summary>
    /// 設定多種路徑
    /// </summary>
    public Transform[] pathTrs;
    /// <summary>
    /// 路徑隨機取出(計算用)
    /// </summary>
    [Header("勿動")]
    [SerializeField]
    private List<Transform> listPathTr = new List<Transform>();
    [Header("勿動")]
    [SerializeField]
    private Transform pathTrsing;

    // Use this for initialization
    void Start () {
        ns = GetComponent<NavigationScript>();
        chgTime = chgTimeDef;

        Init();
        RandomPath();
    }
	
	// Update is called once per frame
	void Update () {
        chgTime -= Time.deltaTime;
        if (chgTime <= 0) {
            chgTime = chgTimeDef;
            RandomPath();
        }
    }

    /// <summary>
    /// 初始化(路徑列表)
    /// </summary>
    void Init() {
        for (int i = 0; i < pathTrs.Length; i++) {
            listPathTr.Add(pathTrs[i]);
        }
        pathTrs = null;
    }

    /// <summary>
    /// 隨機選取路徑
    /// </summary>
    void RandomPath() {
        //隨機取出本次路徑
        int r = Random.Range(0, listPathTr.Count);
        ns.target = listPathTr[r];

        //將上一次路徑加回去
        if (pathTrsing != null) {
            listPathTr.Add(pathTrsing);
        }

        //本次路徑暫存於pathTrsing，並且從listPathTr列表移除，避免下次再抽到
        pathTrsing = listPathTr[r];
        listPathTr.RemoveAt(r);
    }
}

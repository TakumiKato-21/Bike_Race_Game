using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BikeController : MonoBehaviour
{
    // 移動力
    [SerializeField] private float movePower = 15.0f;
    // 回転力
    [SerializeField] private float rotPower = 50.0f;
    // 前輪の座標
    [SerializeField] private Transform frontWheelPos;
    // 後輪の座標
    [SerializeField] private Transform rearWheelPos;
    // ハンドルの座標
    [SerializeField] private Transform handlePos;
    // 前輪のコライダー
    [SerializeField] private Transform frontWheelCol;
    // ホイールのパーティクル
    [SerializeField] private ParticleSystem wheelParticle;
    // マフラーのパーティクル
    [SerializeField] private GameObject mufflerParticle;

    // 移動力
    [SerializeField] private Rigidbody rb;

    public float MovePower { set { movePower = value; } get { return movePower; } }

    // ラップ数.
    public int LapCount = 0;
    // ゴール周回数.
    public int GoalLap = 2;
    // 逆走を判定するためのスイッチ.
    bool lapSwitch = false;
    // プレイステート.
    public GameController.PlayState CurrentState = GameController.PlayState.None;
    // ラップイベント.
    public UnityEvent LapEvent = new UnityEvent();
    // ゴール時イベント,
    public UnityEvent GoalEvent = new UnityEvent();

    void Start()
    {
        // 重心を変更
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0, -0.5f, -0.25f);
    }

    void FixedUpdate()
    {
        if (CurrentState != GameController.PlayState.Play) return;

        move();
    }

    void move()
    {
        // 移動速度
        var moveSpeed = movePower * Input.GetAxis("Vertical");
        // 回転速度
        var rotSpeed = rotPower * Input.GetAxis("Horizontal");
        // バイクを移動させる
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        // バイクを回転させる
        transform.Rotate(0, rotSpeed * Time.deltaTime, 0);
        // 前輪のメッシュを回転させる
        frontWheelPos.Rotate(moveSpeed, 0, 0);
        // 後輪のメッシュを回転させる
        rearWheelPos.Rotate(moveSpeed, 0, 0);
        // ハンドルのメッシュを回転させる
        handlePos.localEulerAngles = new Vector3(0, rotSpeed, 0);
        // 前輪のメッシュの角度を変える
        frontWheelPos.localEulerAngles = new Vector3(frontWheelPos.localEulerAngles.x, rotSpeed, frontWheelPos.localEulerAngles.z);
        // 前輪のコライダーの角度を変える
        frontWheelCol.localEulerAngles = new Vector3(frontWheelCol.localEulerAngles.x, rotSpeed, frontWheelCol.localEulerAngles.z);

        if (rotSpeed <= -rotPower || rotPower <= rotSpeed)
        {
            
            
        }
    }

    // ------------------------------------------------------------
    /// <summary>
    /// 前方ゲートコール.
    /// </summary>
    // ------------------------------------------------------------
    public void OnFrontGateCall()
    {
        // 通常のゲート通過.
        if (lapSwitch == true)
        {
            LapCount++;
            Debug.Log("Lap " + LapCount);
            lapSwitch = false;
            if (LapCount > GoalLap) OnGoal();
            else LapEvent?.Invoke();
        }
        // 逆走ゲート通過.
        else
        {
            LapCount--;
            if (LapCount < 0) LapCount = 0;
            Debug.Log("逆走 Lap " + LapCount);
            LapEvent?.Invoke();
        }
    }

    // ------------------------------------------------------------
    /// <summary>
    /// 後方ゲートコール.
    /// </summary>
    // ------------------------------------------------------------
    public void OnBackGateCall()
    {
        if (lapSwitch == false)
        {
            lapSwitch = true;
        }
    }

    // ------------------------------------------------------------
    /// <summary>
    /// ゴール時処理.
    /// </summary>
    // ------------------------------------------------------------
    public void OnGoal()
    {
        LapCount = 0;
        Debug.Log("Goal!!");
        CurrentState = GameController.PlayState.Finish;
        GoalEvent?.Invoke();
    }
}

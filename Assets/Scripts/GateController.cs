using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    // 前方のコライダーコール.
    [SerializeField] ColliderCallReceiver frontColliderCall = null;
    //後方のコライダーコール.
    [SerializeField] ColliderCallReceiver backColliderCall = null;

    void Start()
    {
        frontColliderCall.TriggerEnterEvent.AddListener(OnFrontTriggerEnter);
        backColliderCall.TriggerEnterEvent.AddListener(OnBackTriggerEnter);
    }

    // --------------------------------------------------------------------------
    /// <summary>
    /// 前方トリガーエンターコール.
    /// </summary>
    /// <param name="col"> 侵入してきたコライダー. </param>
    // --------------------------------------------------------------------------
    void OnFrontTriggerEnter(Collider col)
    {
        // 侵入したコライダーのゲームオブジェクトのタグがPlayer.
        if (col.gameObject.tag == "Player")
        {
            var bike = col.gameObject.GetComponent<BikeController>();
            bike.OnFrontGateCall();
        }
    }

    // --------------------------------------------------------------------------
    /// <summary>
    /// 後方トリガーエンターコール.
    /// </summary>
    /// <param name="col"> 侵入してきたコライダー. </param>
    // --------------------------------------------------------------------------
    void OnBackTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            var bike = col.gameObject.GetComponent<BikeController>();
            bike.OnBackGateCall();
        }
    }
}

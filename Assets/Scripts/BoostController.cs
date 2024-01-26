using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostController : MonoBehaviour
{
    // BikeControllerのスクリプト
    [SerializeField] private BikeController bikeController;

    // モータートグルのデフォルト出力
    private float defaultMotorTorque;

    // モータートグルの最小出力
    [SerializeField] private float minMotorTorque;

    // モータートグルのエンスト出力
    [SerializeField] private float engineStallMotorTorque;

    // ブースト中におけるモータートグルの出力
    [SerializeField] private float boostMotorTorque;

    // ブーストゲージの最大値
    [SerializeField] private float maxBoost;
    // ブーストゲージの現在値
    private float currentBoost;

    // エンストの終了時間
    [SerializeField] private float maxTime;
    // エンストの経過時間
    private float currentTime;

    // ブースト中かどうか判定
    private bool isBoost;

    // エンスト中かどうか判定
    private bool isEngineStall;

    // ブーストスライダー
    [SerializeField] private Slider boostSlider;

    void Start()
    {
        defaultMotorTorque = bikeController.MovePower;

        currentBoost = 0.0f;

        currentTime = 0.0f;

        isBoost = false;

        isEngineStall = false;

        boostSlider.value = 0.0f;
    }

    void Update()
    {
        Boost();
    }

    // ブーストを管理する関数
    void Boost()
    {
        // ブーストゲージの更新
        boostSlider.value = currentBoost / maxBoost;

        // エンスト時間中、かつ、エンスト中である
        if (currentTime <= maxTime && isEngineStall)
        {
            currentTime += Time.deltaTime;

            Debug.Log(currentTime);
        }
        // ブーストゲージがMAX以下である、かつ、ブースト中ではない (ブーストできない)
        else if (currentBoost <= maxBoost && !isBoost)
        {
            bikeController.MovePower = defaultMotorTorque;

            currentBoost += Time.deltaTime;

            currentTime = 0.0f;

            isEngineStall = false;

            // Debug.Log(currentBoost);
        }
        // Hキーを押したら、かつ、ブースト中ではない (ブースト開始)
        else if (Input.GetKeyDown(KeyCode.H) && !isBoost)
        {
            isBoost = true;

            // Debug.Log(isBoost);
        }
        // ブーストゲージが0より大きい、かつ、ブースト中である (ブーストできる)
        else if (0 < currentBoost && isBoost)
        {
            // (処理の追加) HキーとLキーの出力をマウスのホイールを出力に変更

            // Hキーを押したら
            if (Input.GetKey(KeyCode.H))
            {
                // モータートグルの最大出力を上げる
                bikeController.MovePower += boostMotorTorque * Time.deltaTime;
            }
            // Lキーを押したら
            else if (Input.GetKey(KeyCode.L))
            {
                // モータートグルの最大出力を下げる
                bikeController.MovePower -= boostMotorTorque * Time.deltaTime;
            }

            // エンストしたらブーストを終了する
            if (engineStallMotorTorque <= bikeController.MovePower)
            {
                currentBoost = 0.0f;

                bikeController.MovePower = minMotorTorque;

                isBoost = false;

                isEngineStall = true;

                // Debug.Log(isBoost);
                // Debug.Log(isEngineStall);
            }
            // ブースト中にブーストを終了する
            else if (bikeController.MovePower < defaultMotorTorque)
            {
                currentBoost = 0.0f;

                bikeController.MovePower = defaultMotorTorque;

                isBoost = false;

                // Debug.Log(isBoost);
            }

            currentBoost -= Time.deltaTime;

            // Debug.Log(currentBoost);
        }
        // ブーストゲージが0以下である、かつ、ブースト中である (ブースト終了)
        else if (currentBoost <= 0 && isBoost)
        {
            currentBoost = 0.0f;

            bikeController.MovePower = defaultMotorTorque;

            isBoost = false;

            // Debug.Log(isBoost);
        }
    }
}

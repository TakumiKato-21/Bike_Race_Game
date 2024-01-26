using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    // -------------------------------------------------------
    /// <summary>
    /// �Q�[���X�e�[�g.
    /// </summary>
    // -------------------------------------------------------
    public enum PlayState
    {
        None,
        Ready,
        Play,
        Finish,
    }

    // ���݂̃X�e�[�g.
    public PlayState CurrentState = PlayState.None;

    //! �J�E���g�_�E���X�^�[�g�^�C��.
    [SerializeField] int countStartTime = 5;

    //! �J�E���g�_�E���e�L�X�g.
    [SerializeField] TextMeshProUGUI countdownText = null;
    //! �^�C�}�[�e�L�X�g.
    [SerializeField] TextMeshProUGUI timerText = null;
    // �J�E���g�_�E���̌��ݒl.
    float currentCountDown = 0;
    // �Q�[���o�ߎ��Ԍ��ݒl.
    float timer = 0;
    //�v���C���[.
    [SerializeField] BikeController bike = null;

    void Start()
    {
        CountDownStart();
        bike.GoalEvent.AddListener(OnGoal);
    }

    void Update()
    {
        timerText.text = "Time : 000.0 s";
        // �X�e�[�g��Ready�̂Ƃ�.
        if (CurrentState == PlayState.Ready)
        {
            // ���Ԃ������Ă���.
            currentCountDown -= Time.deltaTime;

            int intNum = 0;
            // �J�E���g�_�E����.
            if (currentCountDown <= (float)countStartTime && currentCountDown > 0)
            {
                // int(����)��.
                intNum = (int)Mathf.Ceil(currentCountDown);
                countdownText.text = intNum.ToString();
            }
            else if (currentCountDown <= 0)
            {
                // �J�n.
                StartPlay();
                intNum = 0;
                countdownText.text = "Start!!";

                // Start�\�����������ď���.
                StartCoroutine(WaitErase());
            }
        }
        // �X�e�[�g��Play�̂Ƃ�.
        else if (CurrentState == PlayState.Play)
        {
            timer += Time.deltaTime;
            timerText.text = "Time : " + timer.ToString("000.0") + " s";
        }
        else
        {
            timer = 0;
            timerText.text = "Time : 000.0 s";
        }
    }

    // -------------------------------------------------------
    /// <summary>
    /// �J�E���g�_�E���X�^�[�g.
    /// </summary>
    // -------------------------------------------------------
    void CountDownStart()
    {
        currentCountDown = (float)countStartTime;
        SetPlayState(PlayState.Ready);
        countdownText.gameObject.SetActive(true);
    }

    // -------------------------------------------------------
    /// <summary>
    /// �Q�[���X�^�[�g.
    /// </summary>
    // -------------------------------------------------------
    void StartPlay()
    {
        Debug.Log("Start!!!");
        SetPlayState(PlayState.Play);
    }

    // -------------------------------------------------------
    /// <summary>
    /// �����҂��Ă���Start�\��������.
    /// </summary>
    // -------------------------------------------------------
    IEnumerator WaitErase()
    {
        yield return new WaitForSeconds(2f);
        countdownText.gameObject.SetActive(false);
    }

    // -------------------------------------------------------
    /// <summary>
    /// ���݂̃X�e�[�g�̐ݒ�.
    /// </summary>
    /// <param name="state"> �ݒ肷��X�e�[�g. </param>
    // -------------------------------------------------------
    void SetPlayState(PlayState state)
    {
        CurrentState = state;
        bike.CurrentState = state;
    }

    // -------------------------------------------------------
    /// <summary>
    /// �S�[��������.
    /// </summary>
    // -------------------------------------------------------
    void OnGoal()
    {
        CurrentState = PlayState.Finish;
        countdownText.text = "Goal";
        countdownText.gameObject.SetActive(true);
    }
}
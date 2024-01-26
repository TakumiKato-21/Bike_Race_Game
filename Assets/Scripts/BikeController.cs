using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BikeController : MonoBehaviour
{
    // �ړ���
    [SerializeField] private float movePower = 15.0f;
    // ��]��
    [SerializeField] private float rotPower = 50.0f;
    // �O�ւ̍��W
    [SerializeField] private Transform frontWheelPos;
    // ��ւ̍��W
    [SerializeField] private Transform rearWheelPos;
    // �n���h���̍��W
    [SerializeField] private Transform handlePos;
    // �O�ւ̃R���C�_�[
    [SerializeField] private Transform frontWheelCol;
    // �z�C�[���̃p�[�e�B�N��
    [SerializeField] private ParticleSystem wheelParticle;
    // �}�t���[�̃p�[�e�B�N��
    [SerializeField] private GameObject mufflerParticle;

    // �ړ���
    [SerializeField] private Rigidbody rb;

    public float MovePower { set { movePower = value; } get { return movePower; } }

    // ���b�v��.
    public int LapCount = 0;
    // �S�[������.
    public int GoalLap = 2;
    // �t���𔻒肷�邽�߂̃X�C�b�`.
    bool lapSwitch = false;
    // �v���C�X�e�[�g.
    public GameController.PlayState CurrentState = GameController.PlayState.None;
    // ���b�v�C�x���g.
    public UnityEvent LapEvent = new UnityEvent();
    // �S�[�����C�x���g,
    public UnityEvent GoalEvent = new UnityEvent();

    void Start()
    {
        // �d�S��ύX
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0, -0.5f, -0.25f);
    }

    void FixedUpdate()
    {
        if (CurrentState != GameController.PlayState.Play) return;

        move();
    }

    void move()
    {
        // �ړ����x
        var moveSpeed = movePower * Input.GetAxis("Vertical");
        // ��]���x
        var rotSpeed = rotPower * Input.GetAxis("Horizontal");
        // �o�C�N���ړ�������
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        // �o�C�N����]������
        transform.Rotate(0, rotSpeed * Time.deltaTime, 0);
        // �O�ւ̃��b�V������]������
        frontWheelPos.Rotate(moveSpeed, 0, 0);
        // ��ւ̃��b�V������]������
        rearWheelPos.Rotate(moveSpeed, 0, 0);
        // �n���h���̃��b�V������]������
        handlePos.localEulerAngles = new Vector3(0, rotSpeed, 0);
        // �O�ւ̃��b�V���̊p�x��ς���
        frontWheelPos.localEulerAngles = new Vector3(frontWheelPos.localEulerAngles.x, rotSpeed, frontWheelPos.localEulerAngles.z);
        // �O�ւ̃R���C�_�[�̊p�x��ς���
        frontWheelCol.localEulerAngles = new Vector3(frontWheelCol.localEulerAngles.x, rotSpeed, frontWheelCol.localEulerAngles.z);

        if (rotSpeed <= -rotPower || rotPower <= rotSpeed)
        {
            
            
        }
    }

    // ------------------------------------------------------------
    /// <summary>
    /// �O���Q�[�g�R�[��.
    /// </summary>
    // ------------------------------------------------------------
    public void OnFrontGateCall()
    {
        // �ʏ�̃Q�[�g�ʉ�.
        if (lapSwitch == true)
        {
            LapCount++;
            Debug.Log("Lap " + LapCount);
            lapSwitch = false;
            if (LapCount > GoalLap) OnGoal();
            else LapEvent?.Invoke();
        }
        // �t���Q�[�g�ʉ�.
        else
        {
            LapCount--;
            if (LapCount < 0) LapCount = 0;
            Debug.Log("�t�� Lap " + LapCount);
            LapEvent?.Invoke();
        }
    }

    // ------------------------------------------------------------
    /// <summary>
    /// ����Q�[�g�R�[��.
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
    /// �S�[��������.
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

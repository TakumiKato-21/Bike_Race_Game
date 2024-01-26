using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostController : MonoBehaviour
{
    // BikeController�̃X�N���v�g
    [SerializeField] private BikeController bikeController;

    // ���[�^�[�g�O���̃f�t�H���g�o��
    private float defaultMotorTorque;

    // ���[�^�[�g�O���̍ŏ��o��
    [SerializeField] private float minMotorTorque;

    // ���[�^�[�g�O���̃G���X�g�o��
    [SerializeField] private float engineStallMotorTorque;

    // �u�[�X�g���ɂ����郂�[�^�[�g�O���̏o��
    [SerializeField] private float boostMotorTorque;

    // �u�[�X�g�Q�[�W�̍ő�l
    [SerializeField] private float maxBoost;
    // �u�[�X�g�Q�[�W�̌��ݒl
    private float currentBoost;

    // �G���X�g�̏I������
    [SerializeField] private float maxTime;
    // �G���X�g�̌o�ߎ���
    private float currentTime;

    // �u�[�X�g�����ǂ�������
    private bool isBoost;

    // �G���X�g�����ǂ�������
    private bool isEngineStall;

    // �u�[�X�g�X���C�_�[
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

    // �u�[�X�g���Ǘ�����֐�
    void Boost()
    {
        // �u�[�X�g�Q�[�W�̍X�V
        boostSlider.value = currentBoost / maxBoost;

        // �G���X�g���Ԓ��A���A�G���X�g���ł���
        if (currentTime <= maxTime && isEngineStall)
        {
            currentTime += Time.deltaTime;

            Debug.Log(currentTime);
        }
        // �u�[�X�g�Q�[�W��MAX�ȉ��ł���A���A�u�[�X�g���ł͂Ȃ� (�u�[�X�g�ł��Ȃ�)
        else if (currentBoost <= maxBoost && !isBoost)
        {
            bikeController.MovePower = defaultMotorTorque;

            currentBoost += Time.deltaTime;

            currentTime = 0.0f;

            isEngineStall = false;

            // Debug.Log(currentBoost);
        }
        // H�L�[����������A���A�u�[�X�g���ł͂Ȃ� (�u�[�X�g�J�n)
        else if (Input.GetKeyDown(KeyCode.H) && !isBoost)
        {
            isBoost = true;

            // Debug.Log(isBoost);
        }
        // �u�[�X�g�Q�[�W��0���傫���A���A�u�[�X�g���ł��� (�u�[�X�g�ł���)
        else if (0 < currentBoost && isBoost)
        {
            // (�����̒ǉ�) H�L�[��L�L�[�̏o�͂��}�E�X�̃z�C�[�����o�͂ɕύX

            // H�L�[����������
            if (Input.GetKey(KeyCode.H))
            {
                // ���[�^�[�g�O���̍ő�o�͂��グ��
                bikeController.MovePower += boostMotorTorque * Time.deltaTime;
            }
            // L�L�[����������
            else if (Input.GetKey(KeyCode.L))
            {
                // ���[�^�[�g�O���̍ő�o�͂�������
                bikeController.MovePower -= boostMotorTorque * Time.deltaTime;
            }

            // �G���X�g������u�[�X�g���I������
            if (engineStallMotorTorque <= bikeController.MovePower)
            {
                currentBoost = 0.0f;

                bikeController.MovePower = minMotorTorque;

                isBoost = false;

                isEngineStall = true;

                // Debug.Log(isBoost);
                // Debug.Log(isEngineStall);
            }
            // �u�[�X�g���Ƀu�[�X�g���I������
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
        // �u�[�X�g�Q�[�W��0�ȉ��ł���A���A�u�[�X�g���ł��� (�u�[�X�g�I��)
        else if (currentBoost <= 0 && isBoost)
        {
            currentBoost = 0.0f;

            bikeController.MovePower = defaultMotorTorque;

            isBoost = false;

            // Debug.Log(isBoost);
        }
    }
}

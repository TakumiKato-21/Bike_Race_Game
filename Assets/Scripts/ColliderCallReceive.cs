using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// -------------------------------------------------------------------------
/// <summary>
/// �R���C�_�[�R�[���o�b�N�̎�M���[�e�B���e�B�N���X.
/// </summary>
// -------------------------------------------------------------------------
public class ColliderCallReceiver : MonoBehaviour
{
    // �R���C�_�[�C�x���g��`�N���X.
    public class CollisionEvent : UnityEvent<Collision> { }
    // �R���C�_�[�G���^�[�C�x���g.
    public CollisionEvent CollisionEnterEvent = new CollisionEvent();
    // �R���C�_�[�X�e�C�C�x���g.
    public CollisionEvent CollisionStayEvent = new CollisionEvent();
    // �R���C�_�[�C�O�W�b�g�C�x���g.
    public CollisionEvent CollisionExitEvent = new CollisionEvent();

    // �g���K�[�C�x���g��`�N���X.
    public class TriggerEvent : UnityEvent<Collider> { }
    // �g���K�[�G���^�[�C�x���g.
    public TriggerEvent TriggerEnterEvent = new TriggerEvent();
    // �g���K�[�X�e�C�C�x���g.
    public TriggerEvent TriggerStayEvent = new TriggerEvent();
    // �g���K�[�C�O�W�b�g�C�x���g.
    public TriggerEvent TriggerExitEvent = new TriggerEvent();

    void Start()
    {

    }

    // -------------------------------------------------------------------------
    /// <summary>
    /// �R���C�_�[�G���^�[�R�[���o�b�N.
    /// </summary>
    /// <param name="col"> �ڐG�����R���C�_�[. </param>
    // -------------------------------------------------------------------------
    void OnCollisionEnter(Collision col)
    {
        CollisionEnterEvent?.Invoke(col);
    }

    // -------------------------------------------------------------------------
    /// <summary>
    /// �R���C�_�[�X�e�C�R�[���o�b�N.
    /// </summary>
    /// <param name="col"> �ڐG�����R���C�_�[. </param>
    // -------------------------------------------------------------------------
    void OnCollisionStay(Collision col)
    {
        CollisionStayEvent?.Invoke(col);
    }

    // -------------------------------------------------------------------------
    /// <summary>
    /// �R���C�_�[�C�O�W�b�g�R�[���o�b�N.
    /// </summary>
    /// <param name="col"> �ڐG�����R���C�_�[. </param>
    // -------------------------------------------------------------------------
    void OnCollisionExit(Collision col)
    {
        CollisionExitEvent?.Invoke(col);
    }

    // -------------------------------------------------------------------------
    /// <summary>
    /// �g���K�[�G���^�[�R�[���o�b�N.
    /// </summary>
    /// <param name="other"> �ڐG�����R���C�_�[. </param>
    // -------------------------------------------------------------------------
    void OnTriggerEnter(Collider other)
    {
        TriggerEnterEvent?.Invoke(other);
    }

    // -------------------------------------------------------------------------
    /// <summary>
    /// �g���K�[�X�e�C�R�[���o�b�N.
    /// </summary>
    /// <param name="other"> �ڐG�����R���C�_�[. </param>
    // -------------------------------------------------------------------------
    void OnTriggerStay(Collider other)
    {
        TriggerStayEvent?.Invoke(other);
    }

    // -------------------------------------------------------------------------
    /// <summary>
    /// �g���K�[�C�O�W�b�g�R�[���o�b�N.
    /// </summary>
    /// <param name="other"> �ڐG�����R���C�_�[. </param>
    // -------------------------------------------------------------------------
    void OnTriggerExit(Collider other)
    {
        TriggerExitEvent?.Invoke(other);
    }
}
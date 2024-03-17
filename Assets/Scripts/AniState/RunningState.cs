using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : MoveMentBaseState
{
    // �������°ź��� ���ȭ�� ����, ���ڵ� ��������
    const string RUN = "Run";

    // abstract �Լ� ���ָ� ������ �׾����⿡ Ŭ���� �� abstract �߰�������Ѵ�.
    // �Ű������� PlayerŸ���� �� ������ Player���� ����ó���� �ϰ� �ֱ� �����̴�

    // ���� ���� �� ���� �� �޼���
    public override void EnterState(Player movement)
    {
        movement.SetAnimationState(RUN, true);
        Debug.Log("RUN, true");
    }

    // ���� ���� �� ��� ������Ʈ �� �޼���
    public override void UpdateState(Player movement)
    {
        // ����ƮŰ�� �� ���� -> �Ȱų�, ���̵��̰ų�
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ExitState(movement, (movement.Direction.magnitude < 0.01f) ? movement.Idle : movement.Walk);
        }
        // ����ƮŰ�� ���������� ����Ű�� �ȴ����ִ� ���� -> ���̵�
        if (Input.GetKey(KeyCode.LeftShift) && movement.Direction.magnitude < 0.01f)
        {
            ExitState(movement, movement.Idle);
        }
        else if (Input.GetButtonDown("Jump") && movement.IsGround())
        {
            ExitState(movement, movement.Jumping);
        }

        movement.UpdateSpeed(this);
    }
    /*    public override void UpdateState(Player movement)
        {
            // �ٴٰ� Ű ������  ��ũ���·� ���ư���
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                ExitState(movement, (Mathf.Abs(movement.Direction.magnitude) < 0.01f) ? movement.Idle : movement.Walk);
            }

            else if (Input.GetButtonDown("Jump") && GameManager.Instance.player.IsGround())
            {
                ExitState(movement, movement.Jumping);
            }

    *//*        else if (Mathf.Abs(movement.Direction.magnitude) < 0.01f) // �ӵ��� 0.1���� �������� ���̵� ����.
                ExitState(movement, movement.Idle);*//*
            // ���¿� ���� ���ǵ� ����
            movement.UpdateSpeed(this);
        }
    */
    // ���� �� �޼��� �� ���ְ�, ���ο� ���·� ���� �ϱ� ����, �÷��̾�� MoveMentBaseState -> currentState �� ���� �����߾���
    public override void ExitState(Player movement, MoveMentBaseState nextSate)
    {
        // ù��° �Ű������� ���� (��, �ʱ⿡ ���� �ִϸ��̼� ����)�� ���ش�
        movement.SetAnimationState(RUN, false);
        Debug.Log("RUN, false");
        // SwitchState�� �Ű������� MoveMentBaseState Ÿ�Ե�(��ӹ���) ���̵�, �ȱ�, �ٱ� �� �� �Ű������� ���� �� �ֵ�
        // Player�� �ִϸ��̼��� ����Ī�Ѵ�, �ι�° �Ű������� ��� �� �ִϸ��̼�����.
        movement.SwitchState(nextSate);

    }

}

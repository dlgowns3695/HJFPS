using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.FilterWindow;

public class JumpingState : MoveMentBaseState
{
    // �������°ź��� ���ȭ�� ����, ���ڵ� ��������
    const string JUMP = "Jump";

    // abstract �Լ� ���ָ� ������ �׾����⿡ Ŭ���� �� abstract �߰�������Ѵ�.
    // �Ű������� PlayerŸ���� �� ������ Player���� ����ó���� �ϰ� �ֱ� �����̴�

    // ���� ���� �� ���� �� �޼���
    public override void EnterState(Player movement)
    {
        movement.SetAnimationState(JUMP, true);
        Debug.Log("JUMP, true");

    }

    // ���� ���� �� ��� ������Ʈ �� �޼���
    public override void UpdateState(Player movement)
    {
        // ���߿� �� �ִٸ� �׳� ����������
        if (!movement.IsGround())
        {
            return;
        }
        // �ٴ��� ��
        if (movement.IsGround())
        {   // ���� �ִϸ��̼��� ���������� ���ο� �ִϸ��̼����� ����, ���� : �ӵ��� 0.1���� �۴�? �׷� ���̵�
            ExitState(movement, (movement.Direction.magnitude < 0.01f) ? movement.Idle : movement.Walk);
            
            Debug.Log($"movement.Direction.magnitude : {movement.Direction.magnitude}");
            
        }
        if (Input.GetKey(KeyCode.LeftShift))
            ExitState(movement, movement.Run);

        // ����(Ŭ������ ���� �����̴� �ӵ��� �����Ѵ�)
        movement.UpdateSpeed(this);
    }

    // ���� �� �޼��� �� ���ְ�, ���ο� ���·� ���� �ϱ� ����, �÷��̾�� MoveMentBaseState -> currentState �� ���� �����߾���
    public override void ExitState(Player movement, MoveMentBaseState nextSate)
    {
        // ù��° �Ű������� ���� (��, �ʱ⿡ ���� �ִϸ��̼� ����)�� ���ش�
        movement.SetAnimationState(JUMP, false);
        Debug.Log("JUMP, false");
        // SwitchState�� �Ű������� MoveMentBaseState Ÿ�Ե�(��ӹ���) ���̵�, �ȱ�, �ٱ� �� �� �Ű������� ���� �� �ֵ�
        // Player�� �ִϸ��̼��� ����Ī�Ѵ�, �ι�° �Ű������� ��� �� �ִϸ��̼�����.
        movement.SwitchState(nextSate); 
        
    }
}

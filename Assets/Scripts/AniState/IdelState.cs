using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdelState : MoveMentBaseState
{

    // abstract �Լ� ���ָ� ������ �׾����⿡ Ŭ���� �� abstract �߰�������Ѵ�. --> override �� ������ �������
    public override void EnterState(Player movement) {
        Debug.Log("���̵���·� �Խ��ϴ�.");
    }


    // �� ������ ����ġ������ �ִϸ��̼� ���������ϴ°ſ� ����ϴٰ� �����ϴ°� ���ذ� ������
    public override void UpdateState(Player movement)
    {   // �÷��̾ü�� ���Ͱ��� 0.1f���� Ŭ ���  , �� �� Direction�� ������Ƽ�� �����ؼ� �� �� �־��� ��
        if (movement.Direction.magnitude > 0.01f)
        {
            // Player Walk �Ǵ� Run ���·� ����
            
            movement.SwitchState(Input.GetKey(KeyCode.LeftShift) ? movement.Run : movement.Walk);
        }
        else if (Input.GetButtonDown("Jump"))
        {
            movement.SwitchState(movement.Jumping);
        }
    }

    public override void ExitState(Player movement, MoveMentBaseState nextSate)
    {

    }
}

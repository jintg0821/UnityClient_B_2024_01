using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParabolicTrajectroy : MonoBehaviour
{
    public LineRenderer lineRenderer;                   //Line Renderer ������Ʈ�� �Ҵ��� ����
    public int resloution = 30;                         //������ �׸� �� ����� ���� ����
    public float timeStep = 0.1f;                      //�ð� ����

    public Transform launchPoint;                       //�߻� ��ġ�� ��Ÿ���� Ʈ������
    public float myRotation;
    public float launchPower;                           //�߻� �ӵ�
    public float launchAngle;                           //�߻� ����
    public float launchDirection;                       //�߻� ����
    public float gravity = -9.8f;                       //�߷� ��
    public GameObject projectilePrefabs;                //�߻��� ��ü�� ������


    void Start()
    {
        lineRenderer.positionCount = resloution;                //Line Renderer�� �� ���� ����
    }

    // Update is called once per frame
    void Update()
    {
        RenderTrajectory();
    }

    void RenderTrajectory()                                 //������ ����ϰ� Line Renderer�� �����ϴ� �Լ�
    {
        lineRenderer.positionCount = resloution;            //Line Renderer�� �� ���� ����
        Vector3[] points = new Vector3[resloution];         //���� ������ ������ �迭

        for(int i = 0; i < resloution; i++)
        {
            float t = i * timeStep;                         //���� �ð� ���
            points[i] = CalculatePositionAtTime(t);         //���� �ð������� ��ġ ���
        }

        lineRenderer.SetPositions(points);                   //���� ������ Line Renderer�� ����
    }

    Vector3 CalculatePositionAtTime(float time)                 //�־��� �ð����� ��ü�� ��ġ�� ��� �ϴ� �Լ�
    {
        float launchAngleRad = Mathf.Deg2Rad * launchAngle;             //�߻� ������ �������� ��ȯ
        float launchDirectionRad = Mathf.Deg2Rad * launchDirection;

        //�ð� t������ x,y,z ��ǥ ���
        float x = launchPower * time * Mathf.Cos(launchAngleRad) * Mathf.Cos(launchDirectionRad);
        float z = launchPower * time * Mathf.Cos(launchAngleRad) * Mathf.Sin(launchDirectionRad);
        float y = launchPower * time * Mathf.Sin(launchAngleRad) + 0.5f * gravity * time * time;

        //�߻� ��ġ�� �������� ���� ��ġ ��ȯ
        return launchPoint.position + new Vector3(x, y, z);
    }
}
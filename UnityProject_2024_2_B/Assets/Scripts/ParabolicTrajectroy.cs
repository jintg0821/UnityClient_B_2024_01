using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParabolicTrajectroy : MonoBehaviour
{
    public LineRenderer lineRenderer;                   //Line Renderer 컴포넌트를 할당할 변수
    public int resloution = 30;                         //궤적을 그릴 때 사용할 점의 개수
    public float timeStep = 0.1f;                      //시간 간격

    public Transform launchPoint;                       //발사 위치를 나타내는 트랜스폼
    public float myRotation;
    public float launchPower;                           //발사 속도
    public float launchAngle;                           //발사 각도
    public float launchDirection;                       //발사 방향
    public float gravity = -9.8f;                       //중력 값
    public GameObject projectilePrefabs;                //발사할 물체의 프리팹


    void Start()
    {
        lineRenderer.positionCount = resloution;                //Line Renderer의 점 개수 설정
    }

    // Update is called once per frame
    void Update()
    {
        RenderTrajectory();
    }

    void RenderTrajectory()                                 //궤적을 계산하고 Line Renderer에 설정하는 함수
    {
        lineRenderer.positionCount = resloution;            //Line Renderer의 점 개수 설정
        Vector3[] points = new Vector3[resloution];         //궤적 점들을 저장할 배열

        for(int i = 0; i < resloution; i++)
        {
            float t = i * timeStep;                         //현재 시간 계산
            points[i] = CalculatePositionAtTime(t);         //현재 시간에서의 위치 계산
        }

        lineRenderer.SetPositions(points);                   //계산된 점들을 Line Renderer에 설정
    }

    Vector3 CalculatePositionAtTime(float time)                 //주어진 시간에서 물체의 위치를 계산 하는 함수
    {
        float launchAngleRad = Mathf.Deg2Rad * launchAngle;             //발사 각도를 라디안으로 변환
        float launchDirectionRad = Mathf.Deg2Rad * launchDirection;

        //시간 t에서의 x,y,z 좌표 계산
        float x = launchPower * time * Mathf.Cos(launchAngleRad) * Mathf.Cos(launchDirectionRad);
        float z = launchPower * time * Mathf.Cos(launchAngleRad) * Mathf.Sin(launchDirectionRad);
        float y = launchPower * time * Mathf.Sin(launchAngleRad) + 0.5f * gravity * time * time;

        //발사 위치를 기준으로 계산된 위치 반환
        return launchPoint.position + new Vector3(x, y, z);
    }
}

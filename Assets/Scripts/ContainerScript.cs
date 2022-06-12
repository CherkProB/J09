using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerScript : MonoBehaviour
{
    private ContainerRowScript rowScript;
    private ContainerSpotScript spotScript;

    private Vector3 startPosition;

    //Setters
    public void SetSpotScript(ContainerSpotScript css) { spotScript = css; }
    public void SetRowScript(ContainerRowScript crs) { rowScript = crs; }
    public void SetStartPosition() { startPosition = transform.position; }

    /// <summary>
    /// ����� ������ �������� ������ ����������
    /// </summary>
    public void CreateContainer() { rowScript.CreateContainer(spotScript); }

    /// <summary>
    /// ����� ������ �������� ����������
    /// </summary>
    public void RemoveContainer() { rowScript.RemoveContainer(spotScript); }

    /// <summary>
    /// ����� ������ �������� ���� �����������
    /// </summary>
    public void RaiseContainers(List<GameObject> containersRows) { rowScript.RaiseContainersRow(containersRows); }

    /// <summary>
    /// ����� ������ ��������� ���� �����������
    /// </summary>
    public void DropContainers() { rowScript.DropContainersRow(); }


    /// <summary>
    /// �������� �������� ����������
    /// </summary>
    /// <param name="easing">������ ��������</param>
    /// <param name="targetPosition">�����, �� ������� ���������� ������� ���������</param>
    /// <returns></returns>
    public IEnumerator Raise(AnimationCurve easing, float animationTime, Vector3 targetPosition) 
    {
        for (float i = 0; i < 1; i += Time.deltaTime / animationTime) 
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, easing.Evaluate(i));
            yield return null;
        }
        transform.position = targetPosition; 
    }

    /// <summary>
    /// �������� ��������� ����������
    /// </summary>
    /// <param name="easing">������ ��������</param>
    /// <param name="currentPos">�����, � ������� ��������� ��������� � ������ ������ ��������</param>
    /// <returns></returns>
    public IEnumerator Drop(AnimationCurve easing, float animationTime, Vector3 currentPos)
    {
        for (float i = 0; i < 1; i += Time.deltaTime / animationTime)
        {
            transform.position = Vector3.Lerp(currentPos, startPosition, easing.Evaluate(i));
            yield return null;
        }
        transform.position = startPosition;
    }

    /// <summary>
    /// ��������� ��������� �� ��������� �� ��������� �������
    /// </summary>
    /// <returns>���������� true ����� ��������� ��������� � ��������� �������</returns>
    public bool OnPlace() { return transform.position == startPosition; }

}

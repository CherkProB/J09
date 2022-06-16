using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerScript : MonoBehaviour
{
    private ContainerRowScript rowScript;
    private ContainerSpotScript spotScript;

    private Vector3 startPosition;
    private Vector3 positionInStack;

    //Setters
    public void SetSpotScript(ContainerSpotScript css) { spotScript = css; }
    public void SetRowScript(ContainerRowScript crs) { rowScript = crs; }
    public void SetStartPosition() { startPosition = transform.position; }
    public void SetPositionInStack(Vector3 pos) { positionInStack = pos; }

    //Gettrs
    public Vector3 GetPositionInStack() { return positionInStack; }

    /// <summary>
    /// ����� ������ �������� ������ ����������
    /// </summary>
    public void CreateContainer() { rowScript.CreateContainer(spotScript, positionInStack); }

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
    public IEnumerator Raise(AnimationCurve easing, float animationTime)
    {
        Vector3 targetPosition = transform.position;
        targetPosition.y = rowScript.GetContainerHeight() * (rowScript.GetMaxHeight() + 1) + rowScript.GetContainerHeight() * 2 * positionInStack.y;

        Vector3 activePosition = transform.position;

        for (float i = 0; i < 1; i += Time.deltaTime / animationTime) 
        {
            transform.position = Vector3.Lerp(activePosition, targetPosition, easing.Evaluate(i));
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
    /// <returns>���������� true, ����� ��������� ��������� � ��������� �������</returns>
    public bool OnPlace() { return transform.position == startPosition; }

    /// <summary>
    /// ��������� ��������� �� ������ ���������
    /// </summary>
    /// <returns>���������� true, ����� ��������� ������ ���������</returns>
    public bool isRaised() 
    {
        Vector3 raisedPos = transform.position;
        raisedPos.y = rowScript.GetContainerHeight() * (rowScript.GetMaxHeight() + 1) + rowScript.GetContainerHeight() * 2 * positionInStack.y;

        return transform.position == raisedPos;
    }
}

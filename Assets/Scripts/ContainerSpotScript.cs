using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerSpotScript : MonoBehaviour
{
    private ContainerRowScript rowScript;
    private List<GameObject> containers = new List<GameObject>();

    private Vector3 positionInStack;

    //Setters
    public void SetRowScript(ContainerRowScript crs) { rowScript = crs; }
    public void SetPositionInStack(Vector3 pos) { positionInStack = pos; }

    /// <summary>
    /// ��������� ������ �����������
    /// </summary>
    /// <returns>���������� ������ ��������</returns>
    public List<GameObject> GetContainers() { return containers; }

    /// <summary>
    /// ����� ������ �������� ������ ����������
    /// </summary>
    public void CreateContainer() { rowScript.CreateContainer(this, positionInStack); }

    /// <summary>
    /// ����� ������ �������� ����������
    /// </summary>
    public void RemoveContainer() { rowScript.RemoveContainer(this); }

    /// <summary>
    /// ����� ������ �������� ���� �����������
    /// </summary>
    public void RaiseContainers(List<GameObject> containersRows) { rowScript.RaiseContainersRow(containersRows); }

    /// <summary>
    /// ����� ������ ��������� ���� �����������
    /// </summary>
    public void DropContainers() { rowScript.DropContainersRow(); }
}

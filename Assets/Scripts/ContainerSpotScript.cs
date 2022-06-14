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
    /// Получение списка контейнеров
    /// </summary>
    /// <returns>Возвращает список объектов</returns>
    public List<GameObject> GetContainers() { return containers; }

    /// <summary>
    /// Вызов метода создания нового контейнера
    /// </summary>
    public void CreateContainer() { rowScript.CreateContainer(this, positionInStack); }

    /// <summary>
    /// Вызов метода удаления контейнера
    /// </summary>
    public void RemoveContainer() { rowScript.RemoveContainer(this); }

    /// <summary>
    /// Вызов метода поднятия ряда контйенеров
    /// </summary>
    public void RaiseContainers(List<GameObject> containersRows) { rowScript.RaiseContainersRow(containersRows); }

    /// <summary>
    /// Вызов метода опускания ряда контйенеров
    /// </summary>
    public void DropContainers() { rowScript.DropContainersRow(); }
}

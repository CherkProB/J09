using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerSpotScript : MonoBehaviour
{
    private ContainerRowScript rowScript;
    private List<GameObject> containers = new List<GameObject>();

    //Setters
    public void SetRowScript(ContainerRowScript crs) { rowScript = crs; }

    /// <summary>
    /// Получение списка контейнеров
    /// </summary>
    /// <returns>Возвращает список объектов</returns>
    public List<GameObject> GetContainers() { return containers; }

    /// <summary>
    /// Вызов метода создания нового контейнера
    /// </summary>
    public void CreateContainer() { rowScript.CreateContainer(this); }

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

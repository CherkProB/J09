using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerRow : MonoBehaviour
{
    //Точки появления контейнеров
    private List<ContainerSpot> containerSpots;

    //Свойства
    public List<ContainerSpot> ContainerSpots { get { return containerSpots; } }

    /// <summary>
    /// Инициализация основных полей
    /// </summary>
    private void Awake()
    {
        containerSpots = new List<ContainerSpot>();
    }

    /// <summary>
    /// Поднятие всего ряда контецнеров
    /// </summary>
    /// <param name="containerSize">Информация о размерах контейнеро. Для корутины</param>
    /// <param name="maxHeight">Информация о максимаольном количестве контейнеров. Для корутины</param>
    public void RaiseRow(Vector3Int containerSize, int maxHeight)
    {
        StopAllCoroutines();

        foreach (ContainerSpot spot in containerSpots)
            foreach (Container container in spot.Containers)
                StartCoroutine(container.Raise(containerSize, maxHeight));
    }

    /// <summary>
    /// Опускание всего ряда контейнеров
    /// </summary>
    public void DropRow()
    {
        StopAllCoroutines();

        foreach (ContainerSpot spot in containerSpots)
            foreach (Container container in spot.Containers)
                StartCoroutine(container.Drop());
    }

}

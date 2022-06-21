using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerSpot : MonoBehaviour
{
    //Контейнеры
    private List<Container> containers;
    //Позиция в ряду и секции
    private Vector3Int position;

    //Свойства
    public List<Container> Containers { get { return containers; } }
    public Vector3Int Position { get { return position; } set { position = value; } }

    /// <summary>
    /// Инициализация основных полей
    /// </summary>
    private void Awake()
    {
        containers = new List<Container>();
    }

}

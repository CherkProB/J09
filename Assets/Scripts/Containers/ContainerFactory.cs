using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerFactory : MonoBehaviour
{
    //Информация об анимации
    private AnimationCurve easing;
    private float animTime;
    //Информация о поле
    private FieldData fieldSize;
    //Информация о размере контейнеров
    private Vector3Int containerSize;

    //Свойства
    public AnimationCurve Easing { set { easing = value; } }
    public float AnimTime { set { animTime = value; } }
    public FieldData FieldSize { set { fieldSize = value; } }
    public Vector3Int ContainerSize { set { containerSize = value; } }

    /// <summary>
    /// Метод создания нового контейнера
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="parent"></param>
    /// <param name="senderPos"></param>
    /// <returns></returns>
    public Container CreateContainer(Container prefab, ContainerSpot parent, Vector3Int senderPos)
    {
        //Достигла ли точка, которая вызвала данный метод, максимально возможное число контейнеро в высоту
        if (parent.Containers.Count >= fieldSize.Height) return null;

        //Получение контейнера, который находится на самом верху
        Container lastContainerInStack = null;
        if (parent.Containers.Count > 0)
            lastContainerInStack = parent.Containers[parent.Containers.Count - 1];

        //Создание контйенера
        Container newContainer = Instantiate(prefab, Vector3.zero, Quaternion.identity, parent.transform);

        //Если контейнеры уже существуют
        if (lastContainerInStack)
        {
            newContainer.StartPosition = lastContainerInStack.transform.position + Vector3.up * containerSize.y;
            newContainer.Position = lastContainerInStack.Position + Vector3Int.up;
        }
        else //Если это первый контейнер
        {
            newContainer.StartPosition = parent.transform.position;
            newContainer.Position = senderPos + Vector3Int.up;
        }

        //Создание обводки
        Outline newContainerOutLine = newContainer.gameObject.AddComponent<Outline>();
        newContainerOutLine.OutlineColor = Color.cyan;
        newContainerOutLine.OutlineWidth = 10;
        newContainerOutLine.OutlineMode = Outline.Mode.OutlineAll;

        //Добавление всей необходимой информации
        newContainer.Outline = newContainerOutLine;
        newContainer.Outline.enabled = false;
        newContainer.Easing = easing;
        newContainer.AnimationTime = animTime;
        newContainer.transform.position = newContainer.StartPosition;

        return newContainer;
    }

    /// <summary>
    /// Удаление контейнера
    /// </summary>
    /// <param name="sender">Точка, в которой необходимо удалить контейнер</param>
    public void DeleteContainer(ContainerSpot sender)
    {
        //Если контейнеров нет
        //То нечего удалять
        if (sender.Containers.Count <= 0) return;
        //Кэширования контейнера, который необходимо удалить
        Container containerToDelete = sender.Containers[sender.Containers.Count - 1];
        //Удаление из списка и из сцены
        sender.Containers.Remove(containerToDelete);
        Destroy(containerToDelete.gameObject);
    }
}

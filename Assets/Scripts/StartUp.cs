using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUp : MonoBehaviour
{
    //SceneManager
    [SerializeField] private SceneManager sceneManager;

    //UI
    [SerializeField] private UI ui;
    [SerializeField] private StartScreen startScreen;
    [SerializeField] private InGameScreen gameScreen;

    //UI - Panel
    [SerializeField] private float appearanceTime;
    [SerializeField] private float idleTime;
    [SerializeField] private ShowInformationPanel showInfoPanel;
    [SerializeField] private Text showInfoPanelTitle;
    [SerializeField] private Text showInfoPanelDescription;

    //UI - Text
    [SerializeField] private Text modeText;

    //Camera
    [SerializeField] private CameraController cameraController;

    //InputManager
    [SerializeField] private InputManager inputManager;

    //ContainerFactory
    [SerializeField] private ContainerFactory containerFactory;

    //Field
    [SerializeField] private Container containerPrefab;
    [SerializeField] private ContainerSpot containerSpotPrefab;
    [SerializeField] private ContainerRow containerRowPrefab;
    [SerializeField] private Vector3Int containerSize;

    //SP
    [SerializeField] private Transform spawnPoint;

    //Anim
    [SerializeField] private AnimationCurve easing;
    [SerializeField] private float animTime;

    private void Awake()
    {
        sceneManager.UI = ui;
        sceneManager.InputManager = inputManager;
        sceneManager.ContainerFactory = containerFactory;
        sceneManager.ContainerSize = containerSize;
        sceneManager.SpawnPoint = spawnPoint;
        sceneManager.Camera = cameraController;
        sceneManager.ContainerPrefab = containerPrefab;
        sceneManager.ContainerSpotPrefab = containerSpotPrefab;
        sceneManager.ContainerRowPrefab = containerRowPrefab;

        ui.StartScreen = startScreen;
        ui.GameScreen = gameScreen;
        ui.GameScreen.Panel = showInfoPanel;
        ui.GameScreen.Panel.Title = showInfoPanelTitle;
        ui.GameScreen.Panel.Description= showInfoPanelDescription;
        ui.GameScreen.Panel.AppearanceTime = appearanceTime;
        ui.GameScreen.ModeText = modeText;

        inputManager.Camera = cameraController.gameObject.GetComponent<Camera>();
        inputManager.IdleTime = idleTime;

        containerFactory.Easing = easing;
        containerFactory.AnimTime = animTime;
        containerFactory.ContainerSize = containerSize;
    }

    private void Start()
    {
        sceneManager.ChangeModeText = ui.ChangeModeText;

        inputManager.ShowInfoPanel = ui.ShowPanelAnimation;
        inputManager.HideInfoPanel = ui.HidePanelAnimation;
    }
}

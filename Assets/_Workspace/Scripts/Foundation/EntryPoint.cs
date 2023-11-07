using System.Linq;

public class EntryPoint
{    
    private ResourcesLoader _resourcesLoader;

    private Player _player;
    private MainCamera _mainCamera;
    private MainCanvas _mainCanvas;
    private OrderSystem _orderSystem;
    private Landspace _landspace;

    public EntryPoint(ResourcesLoader resourcesLoader)
    {
        _resourcesLoader = resourcesLoader;

        LoadResources();
        CreateInjector();
        Init();
        Subs();
        Start();
    }

    private void LoadResources()
    {
        _player = _resourcesLoader.GetResource<Player>(ResourcesList.Player);
        _mainCamera = _resourcesLoader.GetResource<MainCamera>(ResourcesList.PlayerCamera);
        _mainCanvas = _resourcesLoader.GetResource<MainCanvas>(ResourcesList.MainCanvas);
        _orderSystem = _resourcesLoader.GetResource<OrderSystem>(ResourcesList.OrderSystem);
        _landspace = _resourcesLoader.GetResource<Landspace>(ResourcesList.Landspace);
    }

    private void CreateInjector()
    {
        TapInjector.AddMonoBehaviour(_player.Pallet);
        TapInjector.AddMonoBehaviour(_mainCamera);
    }

    private void Init()
    {
        _player.Init();
        _player.Movement.Init(_mainCanvas.GetPanel<GamePanel>().JoyStick, 4f);
        _mainCamera.Init(_player.transform);
        _orderSystem.Init(_mainCanvas.OrderCreator, _landspace.BagContainer, _player.Pallet);
        _mainCanvas.TapHandler.Init(_mainCamera.Camera);
        _mainCanvas.GetButton<UpDownButton>().Init();
        _mainCanvas.SmartphoneAnimation.Init();

        _landspace.ShipmentCar.Init();
    }

    private void Subs()
    {
        _mainCanvas.GetButton<StartButton>().OnClick += () => GameStart();
    }

    private void Start()
    {
        for (int s = 0; s < _landspace.Stands.ToList().Count; s++)
        {
            Stand stand = _landspace.Stands.ToList()[s];

            for (int i = 0; i < stand.Capacity; i++)
            {
                Food food = _landspace.ShipmentCar.GetBox(stand.FoodVariant).GetFood();
                stand.SetFood(food);
                food.ResetPosition(false);
            }
        }
    }

    private void GameStart()
    {
        _mainCanvas.SmartphoneAnimation.Close(() => 
        { 
            _mainCanvas.GetPanel<GameContent>().Enable();
            _mainCanvas.GetPanel<MenuContent>().Disable();
            _mainCanvas.GetButton<SmartphoneButton>().Enable();

            _player.Movement.Enable();

            _orderSystem.CreateOrder();
            _orderSystem.CreateOrder();
            _orderSystem.CreateOrder();
            _orderSystem.CreateOrder();
            _orderSystem.CreateOrder();
            _orderSystem.CreateOrder();
            _orderSystem.CreateOrder();
        });
    }
}
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "DataInstaller", menuName = "RoRo/DataInstaller")]
public class DataInstaller : ScriptableObjectInstaller<DataInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<JsonFileRepository<GamingData>>().AsSingle();
        Container.Bind<JsonFileRepository<EatingData>>().AsSingle();
        Container.Bind<JsonFileRepository<ExerciseData>>().AsSingle();
        Container.BindInterfacesTo<RewardParameters>().FromScriptableObjectResource("RewardParameters").AsSingle();
        Container.Bind<IRewardSystem>().To<RewardSystem>().AsSingle();
    }
}

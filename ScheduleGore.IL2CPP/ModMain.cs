using MelonLoader;
using ScheduleGore;
using ScheduleGore.Embedded;
using System.Reflection;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[assembly: MelonInfo(typeof(ModMain), "ScheduleGore", "1.0.0", "joeswanson.")]
namespace ScheduleGore
{
    public class ModMain : MelonMod
    {
        public override void OnInitializeMelon()
        {
            Assets.InitializeBundle();
            
        }
    }
}

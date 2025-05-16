using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ScheduleGore.Embedded
{
    internal class Assets
    {
        public const string GoreBundleName = "schedulegore.fx";


        public static Object<GameObject> Splat = new Object<GameObject>("BloodSplat.prefab");


        public static Il2CppAssetBundle? Bundle { get; private set; }
        public static Il2CppAssetBundle? InitializeBundle()
        {
            Bundle = EAB.LoadFromAssembly(Assembly.GetExecutingAssembly(), $"ScheduleGore.IL2CPP.Resources.{GoreBundleName}");
            if (Bundle == null)
            {
                Melon<ModMain>.Logger.Error($"Failed to load {GoreBundleName}. Many things will break");
            }
            else
            {
                ShaderFix.FixShaders(Bundle);
            }
            return Bundle;
        }
        public class Object<T> where T : UnityEngine.Object
        {
            public Object(string name)
            {
                this.name = name;
            }
            public string? name;
            T? _obj;
            public T? Instance
            {
                get
                {
                    if (_obj == null)
                        _obj = Bundle?.LoadAsset<T>(name);
                    else if( _obj is GameObject gameObject)
                        ShaderFix.FixShaders(gameObject);
                    return _obj;
                }
                set
                {
                    _obj = value;
                }
            }

            public T? Instantiate()
            {
                return Instance ? UnityEngine.Object.Instantiate(Instance) : null;
            }
        }
    }
}

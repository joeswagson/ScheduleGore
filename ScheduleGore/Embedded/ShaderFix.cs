using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace ScheduleGore.Embedded
{
    public class ShaderFix
    {
        // temp fix (hopefully)
        public static void FixShaders(AssetBundle obj)
        {
            foreach (var mat in obj.LoadAllAssets<Material>())
            {
                Shader found = Shader.Find(mat.shader.name);
                if (found != null) mat.shader = found;
            }
        }

        public static Material? bodySearchDecal;

        public static void FixShaders(GameObject obj)
        {
            if (bodySearchDecal == null)
                bodySearchDecal = UnityEngine.Object.FindObjectsOfType<UnityEngine.Rendering.Universal.DecalProjector>().Where(proj => proj.material?.name.Contains("Police arrest circle mat") ?? false).First().material;
            //ew ew ew ew ew ew ew ew ew
            foreach (DecalProjector renderer in obj.GetComponentsInChildren<DecalProjector>(true))
            {
                renderer.material.SetTexture("_Base_Map", renderer.material.GetTexture("Base_Map"));
                renderer.material.SetTexture("_Normal_Map", renderer.material.GetTexture("Normal_Map")); // guessing this one lol. hoping tylers custom decal shader has normals.
                renderer.material.shader = Shader.Find("Shader Graphs/Tyler Decal"); // always loaded? not complaining
            }
        }
    }
}

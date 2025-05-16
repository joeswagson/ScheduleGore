using ScheduleGore.Blood.Generics;
using Il2CppScheduleOne.Combat;
using Il2CppScheduleOne.NPCs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MelonLoader;

namespace ScheduleGore.Blood
{
    [RegisterTypeInIl2Cpp]
    internal class VisualDamageController : MonoBehaviour
    {
        public NPC NPC { get; private set; } = null!;
        void Start()
        {
            NPC? inParent = GetComponentInParent<NPC>();
            if (inParent)
                NPC = inParent;
            else
                Destroy(this);
        }
        public VisualDamageController(IntPtr ptr) : base(ptr) { }
        public void ImpactRecieved(NPC npc, Impact impact)
        {

        }
    }
}

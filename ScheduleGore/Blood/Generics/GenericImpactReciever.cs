using ScheduleOne.Combat;
using ScheduleOne.NPCs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ScheduleGore.Blood.Generics
{
    internal abstract class GenericImpactReciever : MonoBehaviour
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

        public abstract void ImpactRecieved(NPC npc, Impact impact);
    }
}

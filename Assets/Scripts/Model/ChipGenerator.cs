using System;
using Random = UnityEngine.Random;

namespace Model
{
    public class ChipGenerator
    {
        private Array _chipTypes;

        public ChipGenerator()
        {
            _chipTypes = Enum.GetValues(typeof(ChipType));
        }

        public Chip GetRandomChip()
        {
            return new Chip(GetRandomType());
        }

        private ChipType GetRandomType()
        {
            return (ChipType) Random.Range(0, _chipTypes.Length);
        }
    }
}
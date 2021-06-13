using System;

namespace Model
{
    [Serializable]
    public class Chip
    {
        private ChipType _chipType;

        public ChipType ChipType => _chipType;
        
        public Chip(ChipType chipType)
        {
            _chipType = chipType;
        }
    }
}
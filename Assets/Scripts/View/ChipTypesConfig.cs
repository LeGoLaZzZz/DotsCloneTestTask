using System;
using System.Collections.Generic;
using Model;
using UnityEngine;

namespace View
{
    [CreateAssetMenu(fileName = "ChipTypesConfig", menuName = "Chips/ChipTypesConfig", order = 0)]
    public class ChipTypesConfig : ScriptableObject
    {
        public ChipTypeConfig[] chipTypeConfigs;

        private Dictionary<ChipType, ChipTypeConfig> _dictionary;


        public ChipTypeConfig this[ChipType index] => GetChipTypeConfig(index);

        private ChipTypeConfig GetChipTypeConfig(ChipType chipType)
        {
            if (_dictionary == null) InitDictionary();
            return _dictionary[chipType];
        }

        private void InitDictionary()
        {
            _dictionary = new Dictionary<ChipType, ChipTypeConfig>();
            foreach (var chipTypeConfig in chipTypeConfigs)
            {
                _dictionary.Add(chipTypeConfig.chipType, chipTypeConfig);
            }
        }
    }

    [Serializable]
    public class ChipTypeConfig
    {
        public ChipType chipType;
        public Color color;
    }
}
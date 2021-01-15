using System;
using System.Collections.Generic;
using System.Text;

namespace PAMDomain.MaturityModels
{
    public class OptionsDomain
    {
        public string Name { get; private set; }

        public int Value { get; private set; }

        public int Level { get; private set; }

        public static OptionsDomain CreateOption(string Name, int value, int level)
        {
            return new OptionsDomain
            {
                Name = Name,
                Value = value,
                Level = level
            };
        }

        internal void SetName(string name)
        {
            this.Name = name;
        }

        internal void SetLevel(int level)
        {
            this.Level = level;
        }
    }
}

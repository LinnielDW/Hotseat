using HugsLib.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotseat
{
    class StorytellerUsageSetting : SettingHandleConvertible
    {
        public Dictionary<string, bool> dictionary = new Dictionary<string, bool>();

        public override bool ShouldBeSaved
        {
            get { return dictionary.Count > 0; }
        }


        public override void FromString(string settingValue)
        {
            dictionary = new Dictionary<String, bool>();
            if (!settingValue.Equals(string.Empty)) {
                foreach (string str in settingValue.Split('|')) {
                    string[] split = str.Split(',');

                    dictionary.Add(split[0], bool.Parse(split[1]));
                }
            }
        }

        public override string ToString()
        {
            List<String> strings = new List<string>();
            foreach (KeyValuePair<string, bool> item in dictionary)
            {
                strings.Add(item.Key + "," + item.Value.ToString());
            }

            return dictionary != null ? String.Join("|", strings.ToArray()) : "";
        }
    }
}

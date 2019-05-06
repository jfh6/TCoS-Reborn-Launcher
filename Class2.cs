using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Spellborn_Reborn_Luancher
{
    class GameInfo
    {
        public string version;
        public string file;
        public string checksum;

        public void LoadInfo()
        {
            using (StreamReader r = new StreamReader("http://files.spellborn.org/latest.json"))
            {
                string json = r.ReadToEnd();
                List<GameInfo> items = JsonConvert.DeserializeObject<List<GameInfo>>(json);
            }
        }

    }
}

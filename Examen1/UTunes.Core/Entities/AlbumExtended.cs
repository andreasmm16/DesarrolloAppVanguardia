using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTunes.Core.Entities
{
    public class AlbumExtended
    {
        public string Name { get; set; }

        public string Artist { get; set; }

        public string Review { get; set; }

        public ICollection<SongAlbumInfo> Songs { get; set; }
        
        public double AlbumPrice { get; set; }
        public double Score { get; set; }

    }
}

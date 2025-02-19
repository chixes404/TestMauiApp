using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAPP.Models
{
    public class UserGuildsResponse
    {

        public List<Guilds> YourGuild { get; set; }
        public List<Guilds> RecommendedGuild { get; set; }
    }

    public class GuildsListResponse
    {

        public List<Guilds> allGuilds { get; set; }
    }
}

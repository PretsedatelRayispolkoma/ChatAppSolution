using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CringeChat.DataBase
{
    public partial class Chatroom
    {
        public DateTime LastMessageDate => (DateTime)ChatMessage?.Max(x => x?.Date);
    }
}

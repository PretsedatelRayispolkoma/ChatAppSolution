using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CringeChat.DataBase
{
    public partial class ChatAppDBEntities
    {
        private static ChatAppDBEntities context;
        public static ChatAppDBEntities GetContext()
        {
            if (context == null)
                context = new ChatAppDBEntities();
            return context;
        }
    }
}

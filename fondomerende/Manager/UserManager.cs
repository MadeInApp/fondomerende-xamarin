using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Manager
{
    public sealed class UserManager
        {
    
        private static UserManager _instance;

    
        private UserManager() { }
       
        public static UserManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserManager();
                }
                return _instance;
            }
        }
    }
}

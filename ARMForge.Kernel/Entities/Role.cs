using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Kernel.Entities
{
    public class Role : BaseEntity
    {
        private int name;

        public int Name
        {
            get { return name; }
            set { name = value; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private bool isSystemRole;

        public bool IsSystemRole
        {
            get { return isSystemRole; }
            set { isSystemRole = value; }
        }

        private ICollection<User> users;

        public ICollection<User> Users
        {
            get { return users; }
            set { users = value; }
        }

        public Role()
        {
            isSystemRole = false;
            users = [];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Kernel.Entities
{
    public class User : BaseEntity 
    {
        private String firstname;

        public String Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }

        private String lastname;

        public String Lastname
        {
            get { return lastname; }
            set { lastname = value; }
        }

        private string passwordHash = string.Empty;

		public string PasswordHash
		{
			get { return passwordHash; }
			set { passwordHash = value; }
		}

        private String email;
        public String Email
        {
            get { return email; }
            set { email = value; }
        }

        private String phoneNumber;
        public String PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        private DateTime? lastLogin;
        public DateTime? LastLogin
        {
            get { return lastLogin; }
            set { lastLogin = value; }
        }

        private int roleId;
        public int RoleId
		{
			get { return roleId; }
			set { roleId = value; }
		}

		private Role role;
		public Role Role
		{
			get { return role; }
			set { role = value; }
		}

        private DateTime hireDate;

        public DateTime HireDate
        {
            get { return hireDate; }
            set { hireDate = value; }
        }

        private String department;

        public String Department
        {
            get { return department; }
            set { department = value; }
        }

        private int managerId;

        public int ManagerId
        {
            get { return managerId; }
            set { managerId = value; }
        }

        private int salary;

        public int Salary
        {
            get { return salary; }
            set { salary = value; }
        }

        private bool isActive;

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

    }
}

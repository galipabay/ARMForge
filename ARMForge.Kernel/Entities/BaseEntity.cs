using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Kernel.Entities
{
    public class BaseEntity
    {
		private int id;

		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		private DateTime createdAt;

		public DateTime CreatedAt
        {
			get { return createdAt; }
			set { createdAt = value; }
		}

        private DateTime? updatedAt;

        public DateTime? UpdatedAt
        {
            get { return updatedAt; }
            set { updatedAt = value; }
        }

		private bool isActive;

		public bool IsActive
        {
			get { return isActive; }
			set { isActive = value; }
		}

        protected BaseEntity()
        {
            createdAt = DateTime.UtcNow;
            isActive = true;
        }

    }
}

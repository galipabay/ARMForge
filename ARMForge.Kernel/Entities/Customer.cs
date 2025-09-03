using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Kernel.Entities
{
    public class Customer : BaseEntity
    {
		private String companyName;

		public String CompanyName
        {
			get { return companyName; }
			set { companyName = value; }
		}

		private String contactPerson;

		public String ContactPerson
        {
			get { return contactPerson; }
			set { contactPerson = value; }
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
		private String address;

		public String Address
        {
			get { return address; }
			set { address = value; }
		}
		private String taxId;

		public String TaxId
        {
			get { return taxId; }
			set { taxId = value; }
		}
		
	}
}

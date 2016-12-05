using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazine_Software
{
    public class ClientInfo : IAddress, IPersonal
    {
        public int ID { get; set; }
        string _lastName;
        string _firstName;
        string _phone;
        string _email;
        string _city;
        string _street;
        string _house;
        int _flat;
        public string LastName
        {
            get
            {
                return _lastName;
            }

            set
            {
                _lastName = value;
            }
        }

        public string FirstName
        {
            get
            {
                return _firstName;
            }

            set
            {
                _firstName = value;
            }
        }

        public string Phone
        {
            get
            {
                return _phone;
            }

            set
            {
                _phone = value;
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                _email = value;
            }
        }
        public string City
        {
            get
            {
                return _city;
            }

            set
            {
                _city = value;
            }
        }

        public string Street
        {
            get
            {
                return _street;
            }

            set
            {
                _street = value;
            }
        }

        public string House
        {
            get
            {
                return _house;
            }

            set
            {
                _house = value;
            }
        }

        public int Flat
        {
            get
            {
                return _flat;
            }

            set
            {
                _flat = value;
            }
        }

        public override string ToString()
        {
            return LastName + ' ' + FirstName;
        }
    }
}

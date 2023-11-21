using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testWorkIntellectSoft.API.Models.DTO;
using testWorkIntellectSoft.API.Models.Struct;

namespace testWorkIntellectSoft.API.Test.Mockdata
{
    internal class UserDTOMockData
    {
        public static List<UserDTO> GetUsers()
        {
            return new List<UserDTO>
            {
                new UserDTO
                {
                    ID = 1,
                    FirstName = "Test",
                    LastName = "1",
                    Birthyear = 1999,
                    Phones = new List<PhoneDTO>
                    {
                        new PhoneDTO
                        {
                            PhoneID = 1,
                            PhoneNumber = "+77777",
                        },
                        new PhoneDTO
                        {
                            PhoneID = 2,
                            PhoneNumber = "515515",
                        }
                    }
                },
                new UserDTO
                {
                    ID = 2,
                    FirstName = "Test",
                    LastName = "2",
                    Birthyear = 1988,
                    Phones = new List<PhoneDTO>
                    {
                        new PhoneDTO
                        {
                            PhoneID = 3,
                            PhoneNumber = "8800",
                        }
                    }
                },
                new UserDTO
                {
                    ID = 3,
                    FirstName = "Test",
                    LastName = "3",
                    Birthyear = 2003,
                    Phones = null
                }
            };
        }
        public static List<UserDTO> GetEmptyUsers()
        {
            return new List<UserDTO>();
        }
        public static UserDTO NewUser()
        {
            return new UserDTO
            {
                ID = 0,
                FirstName = "Test",
                LastName = "0",
                Birthyear = 2023,
                Phones = new List<PhoneDTO>
                {
                    new PhoneDTO
                    {
                        PhoneID = 0,
                        PhoneNumber = "0",
                    }
                }
            };
        }
    }
}

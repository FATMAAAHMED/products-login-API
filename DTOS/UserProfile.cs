using AutoMapper;
using ProductDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDTOS
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, LoginDTO>();
            CreateMap<LoginDTO, User>();
        }
    }
}

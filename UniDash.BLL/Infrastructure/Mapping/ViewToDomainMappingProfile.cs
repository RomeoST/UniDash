using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using UniDash.BLL.DTO;
using UniDash.Model.Models;

namespace UniDash.BLL.Infrastructure.Mapping
{
    public class ViewToDomainMappingProfile : Profile
    {
        public ViewToDomainMappingProfile()
        {
            CreateMap<RegisterDTO, DutUser>();
            CreateMap<LoginDTO, DutUser>();
        }
    }
}

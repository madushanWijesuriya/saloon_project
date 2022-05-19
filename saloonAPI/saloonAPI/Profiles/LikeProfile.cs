using AutoMapper;
using saloonAPI.Models;
using saloonAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Profiles
{
    public class LikeProfile : Profile
    {
        public LikeProfile()
        {
            CreateMap<Like, LikeDto>();
            CreateMap<LikeDto, Like>();
        }
    }
}

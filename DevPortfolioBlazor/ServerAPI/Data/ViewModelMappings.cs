using AutoMapper;
using Core.Models;

namespace ServerAPI.Data
{
    internal sealed class ViewModelMappings : Profile
    {
        public ViewModelMappings()
        {
            CreateMap<Post, PostViewModel>().ReverseMap();
        }
    }
}

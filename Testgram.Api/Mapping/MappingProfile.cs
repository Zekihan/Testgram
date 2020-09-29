using AutoMapper;
using Testgram.Api.ApiModels;
using Testgram.Api.InputModels;

namespace Testgram.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Customer Domain to Resource
            CreateMap<Core.Models.Profile, ProfileModel>();
            CreateMap<Core.Models.Post, PostModel>();
            CreateMap<Core.Models.Likes, LikesModel>();
            CreateMap<Core.Models.Follow, FollowModel>();
            CreateMap<Core.Models.Comment, CommentModel>();

            // Customer Model to Domain
            CreateMap<ProfileModel, Core.Models.Profile>();
            CreateMap<PostModel, Core.Models.Post>();
            CreateMap<LikesModel, Core.Models.Likes>();
            CreateMap<FollowModel, Core.Models.Follow>();
            CreateMap<CommentModel, Core.Models.Comment>();

            CreateMap<Core.Models.Follow, FollowInputModel>();
            CreateMap<FollowInputModel, Core.Models.Follow>();

            CreateMap<Core.Models.Likes, LikesInputModel>();
            CreateMap<LikesInputModel, Core.Models.Likes>();
        }
    }
}
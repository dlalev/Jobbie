using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Post.Microservice.Models;
using Post.Microservice.Context;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Post.Microservice.Features.PostFeatures.Queries
{
    public class GetPostByIdQuery : IRequest<Post.Microservice.Models.Post>
    {
        public Guid Id { get; set; }
        public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, Post.Microservice.Models.Post>
        {
            private readonly IApplicationContext _context;
            public GetPostByIdQueryHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<Post.Microservice.Models.Post> Handle(GetPostByIdQuery query, CancellationToken cancellationToken)
            {
                var post = await _context.Posts.Where(a => a.Id == query.Id).FirstOrDefaultAsync();
                if (post== null) return null;
                return post;
            }
        }
    }
}

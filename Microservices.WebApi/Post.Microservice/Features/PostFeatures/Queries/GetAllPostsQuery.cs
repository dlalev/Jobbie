using MediatR;
using Microsoft.EntityFrameworkCore;
using Post.Microservice.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Post.Microservice.Features.PostFeatures.Queries
{
    public class GetAllPostsQuery : IRequest<IEnumerable<Post.Microservice.Models.Post>>
    {
        public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, IEnumerable<Post.Microservice.Models.Post>>
        {
            private readonly IApplicationContext _context;
            public GetAllPostsQueryHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Post.Microservice.Models.Post>> Handle(GetAllPostsQuery query, CancellationToken cancellationToken)
            {
                var postslist = await _context.Posts.ToListAsync();
                if (postslist == null)
                {
                    return null;
                }
                return postslist.AsReadOnly();
            }
        }
    }
}

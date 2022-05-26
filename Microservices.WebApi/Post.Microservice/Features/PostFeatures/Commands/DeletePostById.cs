using MediatR;
using Microsoft.EntityFrameworkCore;
using Post.Microservice.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Post.Microservice.Features.PostFeatures.Commands
{
    public class DeletePostByIdCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public class DeletePostByIdCommandHandler : IRequestHandler<DeletePostByIdCommand, Guid>
        {
            private readonly IApplicationContext _context;
            public DeletePostByIdCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<Guid> Handle(DeletePostByIdCommand command, CancellationToken cancellationToken)
            {
                var post = await _context.Posts.Where(a => a.Id == command.Id).FirstOrDefaultAsync();
                if (post == null) return default;
                _context.Posts.Remove(post);
                await _context.SaveChanges();
                return post.Id;
            }
        }
    }
}
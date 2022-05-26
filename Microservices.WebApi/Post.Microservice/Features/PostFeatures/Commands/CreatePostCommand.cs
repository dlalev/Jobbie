using MediatR;
using Post.Microservice.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Post.Microservice.Features.PostFeatures.Commands
{
    public class CreatePostCommand : IRequest<Guid>
    {
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }

        public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid>
        {
            private readonly IApplicationContext _context;
            public CreatePostCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<Guid> Handle(CreatePostCommand command, CancellationToken cancellationToken)
            {
                var post = new Post.Microservice.Models.Post();
                post.Title = command.Title;
                post.DateCreated = command.DateCreated;
                post.Description = command.Description;
                post.Author = command.Author;
                _context.Posts.Add(post);
                await _context.SaveChanges();
                return post.Id;
            }
        }
    }
}

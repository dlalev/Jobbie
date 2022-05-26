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
    public class UpdatePostCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }

        public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, Guid>
        {
            private readonly IApplicationContext _context;
            public UpdatePostCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<Guid> Handle(UpdatePostCommand command, CancellationToken cancellationToken)
            {
                var post = _context.Posts.Where(a => a.Id == command.Id).FirstOrDefault();

                if (post == null)
                {
                    return default;
                }
                else
                {
                    post.Title = command.Title;
                    post.DateCreated = command.DateCreated;
                    post.Description = command.Description;
                    post.Author = command.Author;
                    await _context.SaveChanges();
                    return post.Id;
                }
            }
        }
    }
}
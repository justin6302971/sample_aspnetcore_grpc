using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcGreeter.DataContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GrpcGreeter
{
    public class PersonService : Person.PersonBase
    {
        private readonly ILogger<PersonService> _logger;
        private readonly SpDataContext _spDataContext;

        public PersonService(ILogger<PersonService> logger, SpDataContext spDataContext)
        {
            _logger = logger;
            _spDataContext = spDataContext;
        }

        public override async Task<Response> GetPerson(PersonRequest request, ServerCallContext context)
        {

            var personEntity = await _spDataContext.Person.FirstOrDefaultAsync(d => d.FirstName == request.FirstName);
            if (personEntity == null)
            {

                return new Response
                {
                    IsSuccess = false
                };
            }
            
            var result= new Response
            {
                IsSuccess = true
            };

            result.Items.Add(new PersonModel
            {
                Id = personEntity.Id.ToString(),
                FirstName = personEntity.FirstName,
                LastName = personEntity.LastName
            });
            return result;

        }
    }
}

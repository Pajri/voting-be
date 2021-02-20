using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingBackend.Models;

namespace VotingBackend.Application_Start
{
    public class ApplicationSieveProcessor : SieveProcessor
    {
        public ApplicationSieveProcessor(IOptions<SieveOptions> options) : base(options)
        {
        }

        protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            mapper.Property<Voting>(v => v.Name)
                .CanFilter()
                .CanSort();

            mapper.Property<Voting>(v => v.CategoryId)
                .CanFilter();

            mapper.Property<Voting>(v => v.Category.Name)
                .CanFilter();

            return mapper;
        }
    }
}

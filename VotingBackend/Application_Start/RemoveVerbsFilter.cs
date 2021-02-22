using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace VotingBackend.Application_Start
{
    public class RemoveVerbsFilter : Swashbuckle.Swagger.IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            foreach (PathItem path in swaggerDoc.paths.Values)
            {
                path.delete = null;
                path.head = null;
                path.options = null;
                path.patch = null;
                path.post = null;
                path.put = null;
            }
        }
    }
}

using System.Collections.Generic;
using Fabrik.Common;
using Fabrik.Common.WebAPI.Links;
using Newtonsoft.Json;

namespace HubFintech.ControleContas.Api.Domain.ViewModels.Response.Hypermedia
{
    public class Resource
    {
        private readonly List<Link> _links = new List<Link>();

        [JsonProperty(Order = 100)]
        public IEnumerable<Link> Links => _links;

        public void AddLink(Link link)
        {
            Ensure.Argument.NotNull(link, "link");
            _links.Add(link);
        }

        public void AddLinks(params Link[] links)
        {
            links.ForEach(AddLink);
        }
    }
}
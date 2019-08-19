using Microsoft.Graph;
using Microsoft.Graph.Auth;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Toolkit.Uwp.UI.Controls.Graph.Helpers
{
    public static class GraphExtensions
    {
        public static async Task<IUserPeopleCollectionPage> FindPersonAsync(this GraphServiceClient graph, string query)
        {
            try
            {
                return await graph
                    .Me
                    .People
                    .Request()
                    .Search(query)
                    .WithScopes(new string[] { "people.read" })
                    .GetAsync();
            }
            catch { }

            return new UserPeopleCollectionPage();
        }
        
        public static async Task<Stream> GetUserPhoto(this GraphServiceClient graph, string userid)
        {
            try
            {
                return await graph
                    .Users[userid]
                    .Photo
                    .Content
                    .Request()
                    .WithScopes(new string[] { "user.readbasic.all" })
                    .GetAsync();
            }
            catch { }

            return null;
        }

        public static T Search<T>(this T request, string query)
            where T : IBaseRequest
        {
            request.QueryOptions?.Add(new QueryOption("$search", query));

            return request;
        }
    }
}

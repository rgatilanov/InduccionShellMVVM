using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShellMVVM.Models;
namespace ShellMVVM.Services
{
    public class CloudData : IDataCloud<Cloud>
    {
        readonly List<Cloud> items;

        public CloudData()
        {
            items = new List<Cloud>()
            {
                new Cloud { Name = "Alibaba", Location= "Shangai",Details="Nube Tachidito",ImageUrl="https://s3.amazonaws.com/cdn.wp.m4ecmx/wp-content/uploads/2017/08/18161256/AlibabaGroup2.png",Type = "1" },
                 new Cloud { Name = "Takaka", Location= "Pekin",Details="Nube Sinsulancha",ImageUrl="https://s3.amazonaws.com/cdn.wp.m4ecmx/wp-content/uploads/2017/08/18161256/AlibabaGroup2.png",Type = "1" },
                 new Cloud { Name = "AWS", Location= "California",Details="Nube de Jeff Bezos",ImageUrl="http://fppchile.org/wp-content/uploads/2015/09/amazon-logo.png",Type = "0" },
                 new Cloud { Name = "Azure", Location= "Redmond, Washington",Details="Nube de Bill Gates",ImageUrl="https://www.centria.es/wp-content/uploads/2017/03/Microsoft-Azure_logo.png",Type = "0" },
                      new Cloud { Name = "Google", Location= "California",Details="Nube de Larry Page",ImageUrl="https://upload.wikimedia.org/wikipedia/commons/f/fb/Logo_google%2B_2015.png",Type = "0" },
            };
        }
        public async Task<IEnumerable<Cloud>> GetCloudAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public IList<Cloud> GetOneCloudAsync(string Name = "")
        {
            IList<Cloud> localItems = items.Where(x => x.Name == Name).ToList();
            return localItems;
        }

        public async Task<string> GetRandomCloudsAsync(string Type = "0")
        {
            Random rand = new Random();
            IList<Cloud> localItems = items.Where(x => x.Type == Type).ToList();
            var nameCloud = localItems.ElementAt(rand.Next(0, localItems.Count)).Name;
            return await Task.FromResult(nameCloud);
        }

        public IList<Cloud> GetClouds()
        {
            return items;
        }
    }
}

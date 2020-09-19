using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShellMVVM.Services
{
    public interface IDataCloud<T>
    {
        Task<IEnumerable<T>> GetCloudAsync(bool forceRefresh = false);
        IList<T> GetOneCloudAsync(string Name = "");
        Task<string> GetRandomCloudsAsync(string Type = "0");
        IList<T> GetClouds();
    }
}

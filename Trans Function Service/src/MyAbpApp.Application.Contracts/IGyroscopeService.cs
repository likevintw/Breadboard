

using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyAbpApp.IGyroscopeServices
{
    public interface IGyroscopeService
    {
        Task InsertDataAsync(string database, long timestamp, double value, string measure);
    }
}

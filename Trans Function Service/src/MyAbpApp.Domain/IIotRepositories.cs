using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace MyAbpApp.IIotRepositories
{
    public interface IIotRepository
    {
        Task InsertAsync(string database, long timestamp, List<object> values, List<string> measurements);
    }
}
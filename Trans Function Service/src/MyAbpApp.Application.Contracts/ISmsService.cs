using System;
using System.Threading.Tasks;
namespace MyAbpApp.ISmsServices
{
    public interface ISmsService
    {
        Task SendAsync(string phoneNumber, string message);
    }
}
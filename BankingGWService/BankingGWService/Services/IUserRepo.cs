using BankingGWService.Models;
using System.Threading.Tasks;

namespace BankingGWService.Services
{
    public interface IUserRepo<T>
    {
        public Task<T> RegisterAsync(T t);
        public Task<T> LoginAsync(LoginDTO t);
        public Task<T> Get(string email);
    }
}
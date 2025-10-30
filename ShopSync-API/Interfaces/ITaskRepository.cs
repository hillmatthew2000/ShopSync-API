using Microsoft.AspNetCore.Mvc;
using ShopSync_API.Models;

namespace ShopSync_API.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem?>> GetAllTaskItemsAsync();
        Task<TaskItem?> GetTaskItemAsync(int id);
        Task<bool> PostTaskItemAsync([FromForm] TaskItem taskItem);
        Task<bool> PutTaskItemAsync([FromForm] TaskItem taskItem, int id);
        Task<bool> DeleteTaskItemAsync(int id);
    }
}
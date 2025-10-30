using Microsoft.EntityFrameworkCore;
using ShopSync_API.Data;
using ShopSync_API.Interfaces;
using ShopSync_API.Models;

namespace ShopSync_API.Repositories;

public class TaskRepository : ITaskRepository
{
    private ApiDbContext dbContext;

    public TaskRepository(ApiDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<TaskItem?>> GetAllTaskItemsAsync()
    {
        return dbContext.TaskItems;
    }

    public async Task<TaskItem?> GetTaskItemAsync(int id)
    {
        var existingTaskItem = await dbContext.TaskItems.FindAsync(id);
        return existingTaskItem;
    }

    public async Task<bool> PostTaskItemAsync(TaskItem taskItem)
    {
        try
        {
            await dbContext.TaskItems.AddAsync(taskItem);
            await dbContext.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> PutTaskItemAsync(TaskItem taskItem, int id)
    {
        try
        {
            var existingTaskItem = await dbContext.TaskItems.FirstOrDefaultAsync(t => t.Id == id);
            if (existingTaskItem != null)
            {
                taskItem.Id = existingTaskItem.Id;
                dbContext.TaskItems.Entry(existingTaskItem).CurrentValues.SetValues(taskItem);
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteTaskItemAsync(int id)
    {
        try
        {
            var existingTaskItem = await dbContext.TaskItems.FirstOrDefaultAsync(t => t.Id == id);
            if (existingTaskItem != null)
            {
                dbContext.TaskItems.Remove(existingTaskItem);
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }

    }
}
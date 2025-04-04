using Application.Data.DataBaseContext;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Topics;

public class TopicService(IApplicationDbContext dbContext,
    ILogger<TopicService> logger) : ITopicService
{
    public Task<Topic> CreateTopicAsync(Topic topicRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task<Topic> DeleteTopicAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Topic> GetTopicAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Topic>> GetTopicsAsync()
    {
        return await dbContext.Topics.AsNoTracking<Topic>().ToListAsync();
    }

    public Task<Topic> UpdateTopicAsync(Guid id, Topic topicRequestDto)
    {
        throw new NotImplementedException();
    }
}

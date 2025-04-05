using Application.Data.DataBaseContext;
using Application.Dtos;
using Application.Extensions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Topics;

public class TopicService(IApplicationDbContext dbContext,
    ILogger<TopicService> logger) : ITopicService
{
    public Task<TopicResponseDto> CreateTopicAsync(CreateTopicRequestDto topicRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTopicAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<TopicResponseDto> GetTopicAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<TopicResponseDto>> GetTopicsAsync()
    {
        var topics = await dbContext.Topics.AsNoTracking<Topic>()
                                           .ToListAsync();

        return topics.ToTopicResponseDtoList();
    }

    public Task<TopicResponseDto> UpdateTopicAsync(Guid id, UpdateTopicRequestDto topicRequestDto)
    {
        throw new NotImplementedException();
    }
}

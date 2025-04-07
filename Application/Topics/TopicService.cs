using Application.Data.DataBaseContext;
using Application.Dtos;
using Application.Exceptions;
using Application.Extensions;
using Domain.Models;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Topics;

public class TopicService(IApplicationDbContext dbContext,
    ILogger<TopicService> logger) : ITopicService
{
    public async Task<TopicResponseDto> CreateTopicAsync(CreateTopicDto topicCreateDto)
    {
        var newTopic = Topic.Create(
            TopicId.Of(Guid.NewGuid()),
            topicCreateDto.Title,
            topicCreateDto.EventStart,
            topicCreateDto.Summary,
            topicCreateDto.TopicType,
            Location.Of(topicCreateDto.Location.City, topicCreateDto.Location.Street)
            );

        await dbContext.Topics.AddAsync(newTopic);
        await dbContext.SaveChangesAsync(CancellationToken.None);

        return newTopic.ToTopicResponseDto();
    }

    public Task DeleteTopicAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<TopicResponseDto> GetTopicAsync(Guid id)
    {
        var topicId = TopicId.Of(id);
        var result = await dbContext.Topics.FindAsync([topicId]);

        if (result is null)
        {
            throw new TopicNotFoundException(id);
        }

        return result.ToTopicResponseDto();
    }

    public async Task<List<TopicResponseDto>> GetTopicsAsync()
    {
        var topics = await dbContext.Topics.AsNoTracking<Topic>()
                                           .ToListAsync();

        return topics.ToTopicResponseDtoList();
    }

    public Task<TopicResponseDto> UpdateTopicAsync(Guid id, UpdateTopicDto topicRequestDto)
    {
        throw new NotImplementedException();
    }
}

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

    public async Task<TopicResponseDto> UpdateTopicAsync(Guid id, UpdateTopicDto topicUpdateDto)
    {
        var topicId = TopicId.Of(id);

        var topic = await dbContext.Topics.FindAsync([topicId]);

        if (topic is null)
        {
            throw new TopicNotFoundException(id);
        }

        topic.Title = topicUpdateDto.Title ?? topic.Title;
        topic.Summary = topicUpdateDto.Summary ?? topic.Summary;
        topic.TopicType = topicUpdateDto.TopicType ?? topic.TopicType;
        topic.EventStart = topicUpdateDto.EventStart;
        topic.Location = Location.Of(
            topicUpdateDto.Location.City,
            topicUpdateDto.Location.Street
            );

        await dbContext.SaveChangesAsync(CancellationToken.None);

        return topic.ToTopicResponseDto();
    }
}

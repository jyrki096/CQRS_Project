﻿using Domain.Models;

namespace Application.Topics;

public interface ITopicService
{
    Task<List<Topic>> GetTopicsAsync();
    Task<Topic> GetTopicAsync(Guid id);
    Task<Topic> CreateTopicAsync(Topic topicRequestDto);
    Task<Topic> UpdateTopicAsync(Guid id, Topic topicRequestDto);
    Task<Topic> DeleteTopicAsync(Guid id);
}

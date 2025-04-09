﻿global using Microsoft.EntityFrameworkCore;

global using MediatR;

global using Application;
global using Application.Exceptions;
global using Application.Dtos;
global using Application.Mapping;
global using Application.Topics.Queries.GetTopic;
global using Application.Topics.Queries.GetTopics;
global using Application.Topics.Commands.CreateTopic;
global using Application.Topics.Commands.DeleteTopic;
global using Application.Topics.Commands.UpdateTopic;


global using Infrastructure;
global using Infrastructure.Data.DataBaseContext;
global using Infrastructure.Data.Extensions;
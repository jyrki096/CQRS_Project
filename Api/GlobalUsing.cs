global using Microsoft.EntityFrameworkCore;

global using MediatR;

global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;

global using Application;

global using Application.Dtos;
global using Application.Dtos.Security;
global using Application.Mapping;
global using Application.Topics.Queries.GetTopic;
global using Application.Topics.Queries.GetTopics;
global using Application.Topics.Commands.CreateTopic;
global using Application.Topics.Commands.DeleteTopic;
global using Application.Topics.Commands.UpdateTopic;
global using Application.Security.Query;
global using Application.Security.Commands;

global using Domain.Security;
global using Domain.Exceptions;

global using Infrastructure;
global using Infrastructure.Data.DataBaseContext;
global using Infrastructure.Data.Extensions;
global using Microsoft.EntityFrameworkCore;

global using MediatR;

global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;

global using Application;
global using Application.Exceptions;
global using Application.Dtos;
global using Application.Mapping;
global using Application.Topics.Queries.GetTopic;
global using Application.Topics.Queries.GetTopics;
global using Application.Topics.Commands.CreateTopic;
global using Application.Topics.Commands.DeleteTopic;
global using Application.Topics.Commands.UpdateTopic;
global using Application.Auth.Commands;
global using Application.Auth.Query;
global using Application.Auth.Services;

global using Domain.Security;
global using Domain.Security.Dtos;

global using Infrastructure;
global using Infrastructure.Data.DataBaseContext;
global using Infrastructure.Data.Extensions;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Identity;

global using Shared.CQRS;
global using Domain.Enums;

global using Domain.Models;
global using Domain.ValueObjects;
global using Domain.Security;
global using Domain.Exceptions;

global using Application.Dtos;
global using Application.Dtos.Security;
global using Application.Security.Services;
global using Application.Data.DataBaseContext;
global using Application.Topics.Commands.CreateTopic;
global using Application.Extensions;

global using MediatR;
global using AutoMapper;
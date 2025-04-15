global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Identity;

global using Shared.CQRS;

global using Domain.Models;
global using Domain.ValueObjects;
global using Domain.Security;
global using Domain.Security.Dtos;

global using Application.Dtos;
global using Application.Data.DataBaseContext;
global using Application.Exceptions;
global using Application.Topics.Commands.CreateTopic;
global using Application.Extensions;
global using Application.Auth.Services;

global using MediatR;
global using AutoMapper;
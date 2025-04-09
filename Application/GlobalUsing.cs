global using Microsoft.EntityFrameworkCore;

global using Shared.CQRS;

global using Domain.Models;
global using Domain.ValueObjects;

global using Application.Dtos;
global using Application.Data.DataBaseContext;
global using Application.Exceptions;
global using Application.Topics.Commands.CreateTopic;
global using Application.Extensions;

global using MediatR;
global using AutoMapper;
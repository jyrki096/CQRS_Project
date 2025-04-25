global using Application.Data.DataBaseContext;
global using Application.Security.Services;

global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Configuration;
global using Microsoft.IdentityModel.Tokens;

global using Domain.Abstractions;
global using Domain.Enums;
global using Domain.Models;
global using Domain.Security;
global using Domain.ValueObjects;

global using Infrastructure.Data.DataBaseContext;
global using Infrastructure.Security.Auth;

global using System.Security.Claims;
global using System.Text;
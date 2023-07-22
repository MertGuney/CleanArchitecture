﻿global using CleanArchitecture.Application.DTOs.Tokens;
global using CleanArchitecture.Application.Interfaces.Services;
global using CleanArchitecture.Domain.Entities;
global using CleanArchitecture.Infrastructure.Services;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using System.Net;
global using System.Net.Mail;
global using System.Security.Claims;
global using System.Security.Cryptography;
global using System.Text;
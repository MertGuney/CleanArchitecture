﻿global using CleanArchitecture.Application.Common.Behaviours;
global using CleanArchitecture.Application.Common.Exceptions;
global using CleanArchitecture.Application.Common.Extensions;
global using CleanArchitecture.Application.Contracts.Responses;
global using CleanArchitecture.Application.Contracts.Responses.Externals;
global using CleanArchitecture.Application.Contracts.Responses.Externals.Facebook;
global using CleanArchitecture.Application.Interfaces.Services;
global using CleanArchitecture.Domain.Common;
global using CleanArchitecture.Domain.Common.Interfaces;
global using CleanArchitecture.Domain.Entities;
global using CleanArchitecture.Shared.Enums;
global using CleanArchitecture.Shared.Models;
global using FluentValidation;
global using FluentValidation.AspNetCore;
global using MediatR;
global using MediatR.Pipeline;
global using Microsoft.AspNetCore.Diagnostics;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.AspNetCore.WebUtilities;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using System.Diagnostics;
global using System.Globalization;
global using System.Reflection;
global using System.Security.Claims;
global using System.Text;
global using System.Text.Json;
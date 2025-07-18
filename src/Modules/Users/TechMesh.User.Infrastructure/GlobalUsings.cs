﻿global using Microsoft.EntityFrameworkCore;
global using TechMesh.User.Domain.Entities;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using TechMesh.User.Infrastructure.Mappings;
global using TechMesh.User.Domain.Interfaces.Repositories;
global using TechMesh.User.Infrastructure.Context;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using TechMesh.Domain.Interfaces.UnitOfWork;
global using TechMesh.Infrastructure.UnitOfWork;
global using TechMesh.User.Infrastructure.Persistence.Repositories;
global using MassTransit;
global using TechMesh.Infrastructure.MessageBus;
global using TechMesh.Application.Abstracts.MessageBus;
global using System.Text.Json.Serialization;
global using TechMesh.Infrastructure.Interceptors;
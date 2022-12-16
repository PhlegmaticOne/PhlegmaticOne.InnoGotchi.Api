﻿using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PhlegmaticOne.InnoGotchi.Application.Infrastructure.Helpers;
using PhlegmaticOne.InnoGotchi.Application.Infrastructure.MapperConfigurations;
using PhlegmaticOne.InnoGotchi.Application.Infrastructure.Validators;
using PhlegmaticOne.InnoGotchi.Application.Providers.Readable;
using PhlegmaticOne.InnoGotchi.Application.Providers.Writable;
using PhlegmaticOne.InnoGotchi.Application.Services;
using PhlegmaticOne.InnoGotchi.Data.EntityFramework.Context;
using PhlegmaticOne.InnoGotchi.Domain.Models;
using PhlegmaticOne.InnoGotchi.Domain.Providers.Readable;
using PhlegmaticOne.InnoGotchi.Domain.Providers.Writable;
using PhlegmaticOne.InnoGotchi.Domain.Services;
using PhlegmaticOne.UnitOfWork.Extensions;

namespace PhlegmaticOne.InnoGotchi.Application.ServicesRegistration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<RegisterProfileValidator>();
        services.AddAutoMapper(builder => { builder.AddMaps(typeof(FarmMapperConfiguration).Assembly); });
        services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);
        services.AddUnitOfWork<ApplicationDbContext>();

        services.AddScoped<IWritableCollaborationsProvider, WritableCollaborationsProvider>();
        services.AddScoped<IWritableFarmProvider, WritableFarmProvider>();
        services.AddScoped<IWritableInnoGotchiesProvider, WritableInnoGotchiProvider>();
        services.AddScoped<IWritableProfilesProvider, WritableProfileProvider>();
        services.AddScoped<IWritableFarmStatisticsProvider, WritableFarmStatisticsProvider>();
        services.AddScoped<IInnoGotchiesSynchronizationProvider, InnoGotchiesSynchronizationProvider>();

        services.AddScoped<IReadableInnoGotchiProvider, ReadableInnoGotchiProvider>();
        services.AddScoped<IInnoGotchiOwnChecker, InnoGotchiOwnChecker>();

        services.AddScoped<IJwtTokenGenerationService, JwtTokenGenerationService>();
        services.AddScoped<ITimeService, TimeService>();
        services.AddScoped<IDefaultAvatarService, DefaultAvatarService>();
        services.AddScoped<IAvatarProcessor, AvatarProcessor>();
        services.AddScoped<ISortingService<InnoGotchiModel>, SortingServiceBase<InnoGotchiModel>>(x =>
        {
            var sortByProperties = InnoGotchiSortingExpressions.GetSortingExpressions();
            return new SortingServiceBase<InnoGotchiModel>(sortByProperties);
        });
        services.AddScoped<IInnoGotchiSignsUpdateService>(x =>
        {
            var timeService = x.GetRequiredService<ITimeService>();
            return new InnoGotchiSignsUpdateService(timeService,
                TimeSpan.FromDays(1),
                TimeSpan.FromDays(1),
                TimeSpan.FromDays(7));
        });

        return services;
    }
}
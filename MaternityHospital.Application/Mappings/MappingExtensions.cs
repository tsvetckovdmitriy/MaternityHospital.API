﻿using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

namespace MaternityHospital.Application.Mappings;

public static class MappingExtensions
{
	public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration)
		=> queryable.ProjectTo<TDestination>(configuration).ToListAsync();
}

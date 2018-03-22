using AutoMapper;
using HatchlingCompany.Core.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HatchlingCompany.Core.Common.Implementations
{
    public class AutomapperConfig
    {
        public static void Initialize()
        {
            var types = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(x => !x.IsDynamic)
                .SelectMany(x => x.GetReferencedAssemblies())
                .Select(x => Assembly.Load(x))
                .SelectMany(x => x.GetTypes());

            Mapper.Initialize(cfg => Load(types, cfg));
        }

        private static void Load(IEnumerable<Type> types, IMapperConfigurationExpression cfg)
        {
            LoadStandardMappings(types, cfg);
            LoadCustomMappings(types, cfg);
        }

        private static void LoadStandardMappings(IEnumerable<Type> types, IMapperConfigurationExpression cfg)
        {
            LoadMapFrom(types, cfg);
            LoadMapTo(types, cfg);
        }

        private static void LoadMapTo(IEnumerable<Type> types, IMapperConfigurationExpression cfg)
        {
            var typesFoundForMapping = types
                  .Where(t => !t.IsInterface && !t.IsAbstract)
                  .Where(t => t.GetInterfaces()
                          .Any(i => i.IsGenericType &&
                               i.GetGenericTypeDefinition().Equals(typeof(IMapTo<>)))).ToList().Select(x => new
                               {
                                   source = x,
                                   dest = x.GetInterfaces().FirstOrDefault(y => y.GetGenericTypeDefinition() == typeof(IMapTo<>)).GetGenericArguments()[0]
                               });

            foreach (var type in typesFoundForMapping)
            {
                cfg.CreateMap(type.source, type.dest).ReverseMap();
            }
        }

        private static void LoadMapFrom(IEnumerable<Type> types, IMapperConfigurationExpression cfg)
        {
            var typesFoundForMapping = types
                  .Where(t => !t.IsInterface && !t.IsAbstract)
                  .Where(t => t.GetInterfaces()
                          .Any(i => i.IsGenericType &&
                               i.GetGenericTypeDefinition().Equals(typeof(IMapFrom<>)))).ToList().Select(x => new
                               {
                                   source = x,
                                   dest = x.GetInterfaces().FirstOrDefault(y => y.GetGenericTypeDefinition() == typeof(IMapFrom<>)).GetGenericArguments()[0]
                               });

            foreach (var type in typesFoundForMapping)
            {
                cfg.CreateMap(type.source, type.dest).ReverseMap();
            }
        }

        private static void LoadCustomMappings(IEnumerable<Type> types, IMapperConfigurationExpression cfg)
        {
            var typesFoundForMapping = types
                  .Where(t => !t.IsInterface && !t.IsAbstract)
                  .Where(t => t.GetInterfaces().Any(i => typeof(ICustomMapping).IsAssignableFrom(i)))
                          .Select(x => (ICustomMapping)Activator.CreateInstance(x)).ToArray();


            foreach (var type in typesFoundForMapping)
            {
                type.CreateMappings(cfg);
            }
        }

        #region my config
        //public AutoMapperProfile()
        //{
        //    var allTypes = AppDomain
        //        .CurrentDomain
        //        .GetAssemblies()
        //        .Where(a => a.GetName().Name.Contains("HatchlingCompany"))
        //        .SelectMany(a => a.GetTypes());

        //    allTypes
        //        .Where(t => t.IsClass && !t.IsAbstract && t
        //            .GetInterfaces()
        //            .Where(i => i.IsGenericType)
        //            .Select(i => i.GetGenericTypeDefinition())
        //            .Contains(typeof(IMapFrom<>)))
        //        .Select(t => new
        //        {
        //            Destination = t,
        //            Source = t
        //                .GetInterfaces()
        //                .Where(i => i.IsGenericType)
        //                .Select(i => new
        //                {
        //                    Definition = i.GetGenericTypeDefinition(),
        //                    Arguments = i.GetGenericArguments()
        //                })
        //                .Where(i => i.Definition == typeof(IMapFrom<>))
        //                .SelectMany(i => i.Arguments)
        //                .First(),
        //        })
        //        .ToList()
        //        .ForEach(mapping => this.CreateMap(mapping.Source, mapping.Destination));

        //    allTypes
        //        .Where(t => t.IsClass
        //            && !t.IsAbstract
        //            && typeof(ICustomMapping).IsAssignableFrom(t))
        //        .Select(Activator.CreateInstance)
        //        .Cast<ICustomMapping>()
        //        .ToList()
        //        .ForEach(mapping => mapping.ConfigureMapping(this));
        //}
        #endregion
    }
}

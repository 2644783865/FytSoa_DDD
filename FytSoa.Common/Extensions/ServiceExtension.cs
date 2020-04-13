using FytSoa.Common.AutofacManager;
using FytSoa.Common.Provider;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FytSoa.Common
{
    /// <summary>
    /// IServiceCollection
    /// </summary>
    public static class ServiceExtension
    {
        /// <summary>
        /// 自动注册服务——获取程序集中的实现类对应的多个接口
        /// </summary>
        /// <param name="service"></param>
        /// <param name="interfaceAssemblyName"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterAssembly(this IServiceCollection service, string interfaceAssemblyName)
        {
            Assembly assembly = Assembly.Load(interfaceAssemblyName);
            List<Type> types = assembly.GetTypes().Where(u => u.IsClass && !u.IsAbstract && !u.IsGenericType && u.Name.EndsWith("Service")).ToList();
            foreach (var item in types)
            {
                var interfaceType = item.GetInterfaces().FirstOrDefault();
                service.AddTransient(interfaceType, item);
            }
            return service;
        }

        /// <summary>
        /// 返回的路径后面不带/，拼接时需要自己加上/
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string MapPath(this string path)
        {
            return MapPath(path, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="rootPath">获取wwwroot路径</param>
        /// <returns></returns>
        public static string MapPath(this string path, bool rootPath)
        {
            return AutofacContainerModule.GetService<IPathProvider>().MapPath(path, rootPath);
        }
    }
}

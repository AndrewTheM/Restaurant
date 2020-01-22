using AutoMapper;
using eRestaurant.API.DTO;
using eRestaurant.API.Helpers;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace eRestaurant.API.Extensions
{
    public static class ExtensionMethods
    {
        public static byte[] ToByteArray(this Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        public static TDestination Map<TDestination>(this IMapper mapper, params object[] sources)
        {
            if (!sources.Any())
                return default;
            var destination = mapper.Map<TDestination>(sources.First());
            foreach (var source in sources.Skip(1))
                mapper.Map(source, destination);
            return destination;
        }

        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> enumerable, PagingParameters pars) => new PagedList<T>(enumerable, pars);
    }
}

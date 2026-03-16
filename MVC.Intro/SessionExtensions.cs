using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace MVC.Intro
{
    public static class SessionExtensions
    {
        public const string FavoritesKey = "FavoritesProductIds";

        public static List<Guid> GetFavoriteProductIds(this ISession session)
        {
            var json = session.GetString(FavoritesKey);
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Guid>();
            }

            try
            {
                var ids = JsonSerializer.Deserialize<List<Guid>>(json);
                return ids ?? new List<Guid>();
            }
            catch
            {
                return new List<Guid>();
            }
        }

        public static void SetFavoriteProductIds(this ISession session, List<Guid> ids)
        {
            var json = JsonSerializer.Serialize(ids);
            session.SetString(FavoritesKey, json);
        }

        public static bool ToggleFavoriteProductId(this ISession session, Guid id)
        {
            var ids = session.GetFavoriteProductIds();
            var removed = ids.Remove(id);
            if (!removed)
            {
                ids.Add(id);
            }

            session.SetFavoriteProductIds(ids);
            return !removed;
        }

        public static void RemoveFavoriteProductId(this ISession session, Guid id)
        {
            var ids = session.GetFavoriteProductIds();
            ids.Remove(id);
            session.SetFavoriteProductIds(ids);
        }

        public static void ClearFavoriteProductIds(this ISession session)
        {
            session.Remove(FavoritesKey);
        }
    }
}

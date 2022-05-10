using Newtonsoft.Json;

namespace MyBookStore.MvcApp.Infrastructure
{
    public static class SessionExtensions
    {
        /// <summary>
        /// Добавить JSON к состоянию сеанса.
        /// </summary>
        public static void SetJson(this ISession session,
            string key,
            object value,
            bool ignoreRefLoops)
        {
            if (ignoreRefLoops)
            {
                session.SetString(key, JsonConvert.SerializeObject(value,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }));
            }
            else
            {
                session.SetString(key, JsonConvert.SerializeObject(value));
            }
        }

        /// <summary>
        /// Извлечь JSON из состояния сеанса.
        /// </summary>
        public static T? GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null
                ? default
                : JsonConvert.DeserializeObject<T>(sessionData);
        }
    }
}
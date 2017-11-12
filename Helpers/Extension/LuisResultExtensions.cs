
using System.Collections.Generic;
using System.Linq;
using Microsoft.Bot.Builder.Luis.Models;

namespace LuisBot.Helpers.Extension
{
    public static class LuisResultExtensions
    {
        public static string GetField(this LuisResult result, string field)
        {
            return mapParameter(result.Entities, field);
        }

        private static string mapParameter(IList<EntityRecommendation> entities, string type)
        {
            return entities.FirstOrDefault(it => it.Type == type)?.Entity;
        }
    }
}
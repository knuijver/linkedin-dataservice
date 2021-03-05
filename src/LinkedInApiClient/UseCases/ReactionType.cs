using System;
using System.Linq;

namespace LinkedInApiClient.UseCases
{
    public enum ReactionType
    {
        LIKE, // "Like" in the UI
        PRAISE, // "Celebrate" in the UI
        MAYBE, // "Curious" in the UI
        EMPATHY, // "Love" in the UI
        INTEREST, // "Insightful" in the UI
        APPRECIATION, // "Support" in the UI
    }
}

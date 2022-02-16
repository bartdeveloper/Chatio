namespace Chatio.Services
{
    public static class EmojiService
    {
        public static string FillEmojiInText(this string text)
        {
            return text
                    .Replace(":cat", "😺")
                    .Replace(":ufo", "👽")
                    .Replace(":poo", "💩")
                    .Replace(":ok", "👍")
                    .Replace(":)", "😊")
                    .Replace(";)", "😉")
                    .Replace(":P", "😜").Replace(":p", "😜").Replace(";p", "😜")
                    .Replace(":D", "😃").Replace(":d", "😃")
                    .Replace("xD", "🤣").Replace("xd", "🤣").Replace("Xd", "🤣")
                    .Replace(":>", "😏").Replace(";>", "😏")
                    .Replace(":]", "😎").Replace(";]", "😎")
                    .Replace(":(", "😒").Replace(";(", "😒")
                    .Replace("<3", "😍");                 
        }
    }
}

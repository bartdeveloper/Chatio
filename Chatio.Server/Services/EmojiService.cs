using System.Text;

namespace Chatio.Services
{
    public static class EmojiService
    {
        
        static Dictionary<string, string> emojiMap = new Dictionary<string, string>(){
            {":cat", "😺"},
            {":ufo", "👽"},
            {":poo", "💩"},
            {":ok", "👍"},
            {":)", "😊"},
            {";)", "😉"},
            {":P", "😜"}, {":p", "😜"}, {";p", "😜"},
            {":D", "😃"}, {":d", "😃"},
            {"xD", "🤣"}, {"xd", "🤣"}, {"Xd", "🤣"},
            {":>", "😏"}, {";>", "😏"},
            {":]", "😎"}, {";]", "😎"},
            {":(", "😒"}, {";(", "😒"},
            {"<3", "😍"} };

        public static string FillEmojiInText(this string text)
        {

            var stringBuilder = new StringBuilder(text);

            foreach (var emoji in emojiMap)
            {
                stringBuilder.Replace(emoji.Key, emoji.Value);
            }

            return stringBuilder.ToString();                 
        }
    }
}

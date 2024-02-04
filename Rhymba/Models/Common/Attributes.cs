namespace Rhymba.Models.Common
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    internal class BodyContent : Attribute
    {
        public bool IsBodyContent { get; }

        internal BodyContent()
        {
            this.IsBodyContent = true;
        }

        internal BodyContent(bool isBodyContent)
        {
            this.IsBodyContent = isBodyContent;
        }
    }
}

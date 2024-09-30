using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp.Elements
{
    public abstract class AbstractNode
    {
        public AttrList Attributes { get; }
        public ElementType Context { get; init; }
        public Document? Document { get; private set; }
        public string Id
        {
            get
            {
                return GetAttribute("id", GenerateId());
            }
            set
            {
                SetAttribute("id", value);
            }
        }
        public string NodeName { get; init; }
        public Block? Parent { get; private set; }

        public AbstractNode(
            ElementType context,
            Block? parent = null,
            Dictionary<string, string>? attributes = null)
        {
            Parent = parent;
            Context = context;
            Attributes = new AttrList(attributes);
            if (context == ElementType.Document)
                Document = (Document)this;
            else if (parent != null)
                Document = parent.Document;
            Id ??= GenerateId();
            NodeName = context.ToString().ToLower();
        }

        public abstract bool IsBlock();
        public abstract bool IsInline();
        public abstract string GenerateId();

        public void SetParent(Block parent)
        {
            Parent = parent;
            Document = parent.Document;
        }

        public string? GetAttribute(string key) => Attributes.GetAttribute(key);
        public string GetAttribute(string key, string defaultValue, string? fallbackName = null)
            => Attributes.GetAttribute(key) ?? (fallbackName == null ? defaultValue : Document?.GetAttribute(fallbackName) ?? defaultValue);
        public bool SetAttribute(string key, string value = "", bool overwrite = true)
            => Attributes.SetAttribute(key, value, overwrite);
    }

    public class AttrList
    {
        private Dictionary<string, string> Attributes { get; }
        private HashSet<string> Options
        {
            get { return new HashSet<string>(Attributes.Keys.Where(k => k.EndsWith("-option")).Select(k => k.Replace("-option", ""))); }
            init
            {
                foreach (var option in value)
                {
                    Attributes[$"{option}-option"] = "";
                }
            }
        }
        private HashSet<string> Roles
        {
            get { return [.. Attributes["role"].Split(' ')]; }
            init { Attributes["role"] = String.Join(' ', value); }
        }

        public AttrList(Dictionary<string, string>? attributes = null, HashSet<string>? options = null, HashSet<string>? roles = null)
        {
            Attributes = attributes ?? [];
            Options = options ?? [];
            Roles = roles ?? [];
        }

        public string? GetAttribute(string key) => Attributes.TryGetValue(key, out string? value) ? value : null;
        public bool SetAttribute(string key, string value = "", bool overwrite = true)
        {
            if (!overwrite && Attributes.ContainsKey(key))
                return false;
            else
            {
                Attributes[key] = value;
                return true;
            }
        }
        public bool RemoveAttribute(string key) => Attributes.Remove(key);

        public HashSet<string> GetOptionsAsSet() => Options;
        public string GetOptionsAsString() => String.Join(' ', Options);
        public bool HasOption(string option) => Options.Contains(option);
        public bool SetOption(string option) => SetAttribute(option + "-option") && Options.Add(option);
        public bool RemoveOption(string option) => RemoveAttribute(option + "-option") && Options.Remove(option);

        public HashSet<string> GetRolesAsSet() => Roles;
        public string GetRolesAsString() => String.Join(' ', Roles);
        public bool HasRole(string role) => Roles.Contains(role);
        public bool SetRole(string role) => SetAttribute("role", GetRolesAsString() + ' ' + role) && Roles.Add(role);
        public bool RemoveRole(string role) => SetAttribute("role", GetRolesAsString().Replace(role, "")) && Roles.Remove(role);
    }
}

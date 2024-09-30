namespace AsciiDocSharp.Elements
{
	/// <summary>
	/// AbstractNode is the base class for all elements in the AsciiDoc document model.
	/// Universal features of all nodes, such as members and methods, are defined here.
	/// </summary>
	/// <remarks>
	/// <list type="bullet">
	/// <item>
	/// <term>Attributes</term>
	/// <description><inheritdoc cref="Attributes" path="/summary"/></description>
	/// </item>
	/// <item>
	/// <term>Context</term>
	/// <description><inheritdoc cref="Context" path="/summary"/></description>
	/// </item>
	/// <item>
	/// <term>Document</term>
	/// <description><inheritdoc cref="Document" path="/summary"/></description>
	/// </item>
	/// <item>
	/// <term>Id</term>
	/// <description><inheritdoc cref="Id" path="/summary"/></description>
	/// </item>
	/// <item>
	/// <term>Parent</term>
	/// <description><inheritdoc cref="Parent" path="/summary"/></description>
	/// </item>
	/// </list>
	/// This class is based on <see href="https://github.com/asciidoctor/asciidoctor/blob/main/lib/asciidoctor/abstract_node.rb">AbstractNode.rb</see> from the Asciidoctor project.
	/// </remarks>
	public abstract class AbstractNode
    {
		/// <summary>
		/// An <see cref="AttrList"/> that stores the <see href="https://docs.asciidoctor.org/asciidoc/latest/attributes/element-attributes/">attributes</see>,
		/// <see href="https://docs.asciidoctor.org/asciidoc/latest/attributes/role/">roles</see>,
		/// and <see href="https://docs.asciidoctor.org/asciidoc/latest/attributes/options/">options</see> of the element.
		/// </summary>
		public AttrList Attributes { get; }
		/// <summary>
		/// An <see cref="ElementType"/> that represents the element.
		/// </summary>
		public ElementType Context { get; init; }
		/// <summary>
		/// The <see cref="Elements.Document"/> that contains the element.
		/// </summary>
		public Document? Document { get; private set; }
		/// <summary>
		/// The unique <see langword="string"/> identifier of the element.
		/// </summary>
		public string? Id
        {
            get
            {
                return GetAttribute("id");
            }
            set
            {
                SetAttribute("id", value ?? "");
            }
        }
		/// <summary>
		/// The parent <see cref="Block"/> of the element.
		/// </summary>
		public Block? Parent { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="AbstractNode"/> class. 
		/// </summary>
		/// <param name="context"><inheritdoc cref="Context" path="/summary"/></param>
		/// <param name="parent"><inheritdoc cref="Parent" path="/summary"/></param>
		/// <param name="attributes">The <see cref="Dictionary{TKey, TValue}"/> that represents the unparsed attribute list.</param>
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
        }

		/// <summary>
		/// Determines if the element is a <see cref="Block"/>.
		/// </summary>
		/// <returns>true if the element is a <see cref="Block"/>; otherwise, false.</returns>
		public abstract bool IsBlock();
		/// <summary>
		/// Determines if the element is a <see cref="BaseInline"/>.
        /// </summary>
        /// <returns>true if the element is a <see cref="BaseInline"/>; otherwise, false.</returns>
		public abstract bool IsInline();

		/// <summary>
		/// Sets the parent <see cref="Block"/> of the element.
		/// </summary>
		/// <remarks>
		/// This method will also update the <see cref="Document"/> property of the element, if necessary.
		/// Note that this method does not update the parent's children collection.
		/// </remarks>
		/// <param name="parent">The new parent <see cref="Block"/> of the element.</param>
		public void SetParent(Block parent)
        {
            Parent = parent;
            Document = parent.Document;
        }

		/// <inheritdoc cref="AttrList.GetAttribute(string)"/>
		/// <remarks>
		/// Equivalent to <see cref="AttrList.GetAttribute(string)"/> on the <see cref="Attributes"/> property.
		/// </remarks>
		public string? GetAttribute(string key) => Attributes.GetAttribute(key);
		/// <inheritdoc cref="AttrList.GetAttribute(string, string, string?)"/>
		/// <remarks>
		/// Equivalent to <see cref="AttrList.GetAttribute(string, string, string?)"/> on the <see cref="Attributes"/> property.
		/// </remarks>
		public string GetAttribute(string key, string defaultValue, string? fallbackName = null)
            => Attributes.GetAttribute(key) ?? (fallbackName == null ? defaultValue : Document?.GetAttribute(fallbackName) ?? defaultValue);
		/// <inheritdoc cref="AttrList.SetAttribute(string, string, bool)"/>
		/// <remarks>
		/// Equivalent to <see cref="AttrList.SetAttribute(string, string, bool)"/> on the <see cref="Attributes"/> property.
		/// </remarks>
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

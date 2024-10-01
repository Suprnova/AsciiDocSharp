namespace AsciiDocSharp.Elements
{
	/// <summary>
	/// Represents the base class for all elements in the AsciiDoc document model.
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
		/// An <see cref="AttributeList"/> that stores the <see href="https://docs.asciidoctor.org/asciidoc/latest/attributes/element-attributes/">attributes</see>,
		/// <see href="https://docs.asciidoctor.org/asciidoc/latest/attributes/role/">roles</see>,
		/// and <see href="https://docs.asciidoctor.org/asciidoc/latest/attributes/options/">options</see> of the element.
		/// </summary>
		public AttributeList Attributes { get; }
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
            Attributes = new AttributeList(attributes);
            if (context == ElementType.Document)
                Document = (Document)this;
            else if (parent != null)
                Document = parent.Document;
        }

		/// <summary>
		/// Determines if the element is a <see cref="Block"/>.
		/// </summary>
		/// <returns><see langword="true"/> if the element is a <see cref="Block"/>; otherwise, <see langword="false"/>.</returns>
		public abstract bool IsBlock();
		/// <summary>
		/// Determines if the element is a <see cref="BaseInline"/>.
		/// </summary>
		/// <returns><see langword="true"/> if the element is a <see cref="BaseInline"/>; otherwise, <see langword="false"/>.</returns>
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

		/// <inheritdoc cref="AttributeList.GetAttribute(string)"/>
		/// <remarks>
		/// Equivalent to <see cref="AttributeList.GetAttribute(string)"/> on the <see cref="Attributes"/> property.
		/// </remarks>
		public string? GetAttribute(string key) => Attributes.GetAttribute(key);
		/// <inheritdoc cref="AttributeList.GetAttribute(string, string, string?)"/>
		/// <remarks>
		/// Equivalent to <see cref="AttributeList.GetAttribute(string, string, string?)"/> on the <see cref="Attributes"/> property.
		/// </remarks>
		public string GetAttribute(string key, string defaultValue, string? fallbackName = null)
            => Attributes.GetAttribute(key) ?? (fallbackName == null ? defaultValue : Document?.GetAttribute(fallbackName) ?? defaultValue);
		/// <inheritdoc cref="AttributeList.SetAttribute(string, string, bool)"/>
		/// <remarks>
		/// Equivalent to <see cref="AttributeList.SetAttribute(string, string, bool)"/> on the <see cref="Attributes"/> property.
		/// </remarks>
		public bool SetAttribute(string key, string value = "", bool overwrite = true)
            => Attributes.SetAttribute(key, value, overwrite);
    }

	/// <summary>
	/// Represents the element attributes that are present in all elements in the AsciiDoc document model.
	/// </summary>
	/// <remarks>
	/// <list type="bullet">
	/// <item>
	/// <term>Attributes</term>
	/// <description><inheritdoc cref="AttributeList.Attributes" path="/summary"/></description>
	/// </item>
	/// <item>
	/// <term>Options</term>
	/// <description><inheritdoc cref="AttributeList.Options" path="/summary"/></description>
	/// </item>
	/// <item>
	/// <term>Roles</term>
	/// <description><inheritdoc cref="AttributeList.Roles" path="/summary"/></description>
	/// </item>
	/// </list>
	/// </remarks>
	public class AttributeList
    {
		/// <summary>
		/// A list of the attributes applied to an element, with <see langword="string"/>s as the key and value.
		/// </summary>
		/// <remarks>
		/// Attributes typically store a string as the value to a key, but occasionally an attribute simply represents a <see langword="true"/>
		/// value if the key exists at all.
		/// <para>
		/// Attributes in AsciiDoctor are not technically key-value pairs of <see langword="string"/>s,
		/// and are instead more similar to a <see cref="Dictionary{TKey, TValue}"/> of <see langword="string"/>, <see langword="object"/>.
		/// This should be considered when further implementing this class.
		/// </para>
		/// </remarks>
		private Dictionary<string, string> Attributes { get; }
		/// <summary>
		/// A set of <see langword="bool"/>-like values that can configure certain aspects of elements.
		/// </summary>
		/// <remarks>
		/// Options in AsciiDoctor are implemented by appending <c>-option</c> to the option name, and storing it as an attribute.
		/// In the <see href="https://docs.asciidoctor.org/asciidoc/latest/attributes/options/">documentation</see>, however, options are set
		/// by modifying the "options" or "opts" attribute. This should be considered when further implementing this class.
		/// </remarks>
		private HashSet<string> Options
        {
			// Options are stored in Attributes, so the returning HashSet is generated from indexing the Attributes.
			get { return new HashSet<string>(Attributes.Keys.Where(k => k.EndsWith("-option")).Select(k => k.Replace("-option", ""))); }
			// This will automatically configure the corresponding attribute for each option to ensure referential integrity.
			init
			{
                foreach (var option in value)
                {
                    Attributes[$"{option}-option"] = "";
                }
            }
        }
		/// <summary>
		/// A set of <see langword="string"/>s that are applied to an element.
		/// </summary>
		/// <remarks>
		/// Roles are typically used to apply classes to the generated HTML output.
		/// </remarks>
		private HashSet<string> Roles
        {
			// Roles are stored in the "role" attribute, so we generate a HashSet from the split string.
			get { return [.. Attributes["role"].Split(' ')]; }
			// This will automatically configure the "role" attribute to ensure referential integrity.
			init { Attributes["role"] = String.Join(' ', value); }
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="AttributeList"/> class.
		/// </summary>
		/// <param name="attributes"><inheritdoc cref="Attributes" path="/summary"/></param>
		/// <param name="options"><inheritdoc cref="Options" path="/summary"/></param>
		/// <param name="roles"><inheritdoc cref="Roles" path="/summary"/></param>
		public AttributeList(Dictionary<string, string>? attributes = null, HashSet<string>? options = null, HashSet<string>? roles = null)
        {
            Attributes = attributes ?? [];
            Options = options ?? [];
            Roles = roles ?? [];
        }

		/// <summary>
		/// Retrieves the value of an attribute from the <see cref="Attributes"/> property.
		/// </summary>
		/// <param name="key">The key of the attribute.</param>
		/// <returns>the value of the provided key if found; <see langword="null"/> if no such attribute was found.</returns>
		public string? GetAttribute(string key) => Attributes.TryGetValue(key, out string? value) ? value : null;
		/// <summary>
		/// Retrieves the value of an attribute from the <see cref="Attributes"/> property. If the value is not found,
		/// the fallback name is used to retrieve the value of the fallback attribute. If the fallback attribute is not found,
		/// returns the default value.
		/// </summary>
		/// <param name="key">The key of the attribute.</param>
		/// <param name="defaultValue">The default value to return.</param>
		/// <param name="fallbackName">The key of the fallback attribute.</param>
		/// <returns>the value of <c>key</c> or <c>fallbackName</c>, or <c>defaultValue</c> if neither key is valid.</returns>
		public string GetAttribute(string key, string defaultValue, string? fallbackName = null)
			=> GetAttribute(key) ?? (fallbackName == null ? defaultValue : GetAttribute(fallbackName) ?? defaultValue);
		/// <summary>
		/// Sets the value of the specific attribute in the <see cref="Attributes"/> property.
		/// </summary>
		/// <remarks>
		/// By default, sets <c>value</c> to "", which will be interpreted as a <see langword="true"/> value for <see langword="bool"/>-like attributes.
		/// </remarks>
		/// <param name="key">The key of the attribute.</param>
		/// <param name="value">The new value to assign to the attribute.</param>
		/// <param name="overwrite">Whether or not to overwrite the attribute should it exist.</param>
		/// <returns><see langword="true"/> if the attribute was successfully changed; otherwise, <see langword="false"/>.</returns>
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
		/// <summary>
		/// Removes the specified attribute from the <see cref="Attributes"/> property.
		/// </summary>
		/// <param name="key">The key of the attribute.</param>
		/// <returns><see langword="true"/> if the attribute was removed successfully; otherwise, <see langword="false"/>.</returns>
		public bool RemoveAttribute(string key) => Attributes.Remove(key);

		/// <summary>
		/// Returns the options as a <see cref="HashSet{T}"/> of <see langword="string"/>s.
		/// </summary>
		/// <returns>a <see cref="HashSet{T}"/> of options as <see langword="string"/>s.</returns>
		public HashSet<string> GetOptionsAsSet() => Options;
		/// <summary>
		/// Returns the options as a space-delimited <see langword="string"/>.
		/// </summary>
		/// <returns>a space-delimited <see langword="string"/> of options.</returns>
		public string GetOptionsAsString() => String.Join(' ', Options);
		/// <summary>
		/// Determines if the specified option is present in the <see cref="Options"/> property.
		/// </summary>
		/// <param name="option">The name of the option.</param>
		/// <returns><see langword="true"/> if the option is present; otherwise, <see langword="false"/>.</returns>
		public bool HasOption(string option) => Options.Contains(option);
		/// <summary>
		/// Adds the specified option to the <see cref="Options"/> property.
		/// </summary>
		/// <param name="option">The name of the option.</param>
		public void SetOption(string option) => SetAttribute(option + "-option");
		/// <summary>
		/// Removes the specified option from the <see cref="Options"/> property.
		/// </summary>
		/// <param name="option">The name of the option.</param>
		/// <returns><see langword="true"/> if the option was removed successfully; otherwise, <see langword="false"/>.</returns>
		public bool RemoveOption(string option) => RemoveAttribute(option + "-option");

		/// <summary>
		/// Returns the roles as a <see cref="HashSet{T}"/> of <see langword="string"/>s.
		/// </summary>
		/// <returns>a <see cref="HashSet{T}"/> of options as <see langword="string"/>s.</returns>
		public HashSet<string> GetRolesAsSet() => Roles;
		/// <summary>
		/// Returns the roles as a space-delimited <see langword="string"/>.
		/// </summary>
		/// <returns>a space-delimited <see langword="string"/> of roles.</returns>
		public string GetRolesAsString() => String.Join(' ', Roles);
		/// <summary>
		/// Determines if the specified role is present in the <see cref="Roles"/> property.
		/// </summary>
		/// <param name="role">The name of the role.</param>
		/// <returns><see langword="true"/> if the role is present; otherwise, <see langword="false"/>.</returns>
		public bool HasRole(string role) => Roles.Contains(role);
		/// <summary>
		/// Adds the specified role to the <see cref="Roles"/> property.
		/// </summary>
		/// <param name="role">The name of the role.</param>
		/// <returns><see langword="true"/> if the role was added successfully; otherwise, <see langword="false"/>.</returns>
		public bool SetRole(string role) => SetAttribute("role", GetRolesAsString() + ' ' + role);
		/// <summary>
		/// Removes the specified role from the <see cref="Roles"/> property.
		/// </summary>
		/// <param name="role">The name of the role.</param>
		/// <returns><see langword="true"/> if the role was removed successfully; otherwise, <see langword="false"/>.</returns>
		public bool RemoveRole(string role) => GetRolesAsString().Contains(role) && SetAttribute("role", GetRolesAsString().Replace(role, ""));
    }
}

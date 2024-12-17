﻿namespace BlazorBootstrap;

public partial class CheckboxInput : BlazorBootstrapComponentBase
{
    #region Fields and Constants

    private FieldIdentifier fieldIdentifier;

    #endregion

    #region Methods

    protected override void OnInitialized()
    {
        AdditionalAttributes ??= new Dictionary<string, object>();

        fieldIdentifier = FieldIdentifier.Create(ValueExpression);

        base.OnInitialized();
    }

    /// <summary>
    /// Disables number input.
    /// </summary>
    public void Disable() => Disabled = true;

    /// <summary>
    /// Enables number input.
    /// </summary>
    public void Enable() => Disabled = false;

    private async Task OnChange(ChangeEventArgs e)
    {
        var oldValue = Value;
        var newValue = e.Value is not null && (bool)e.Value;

        await ValueChanged.InvokeAsync(newValue);

        EditContext?.NotifyFieldChanged(fieldIdentifier);
    }

    #endregion

    #region Properties, Indexers

    protected override string? ClassNames =>
        BuildClassNames(
            Class,
            (BootstrapClass.FormCheckInput, true),
            (EditContext?.FieldCssClass(fieldIdentifier) ?? string.Empty, true)
        );

    /// <summary>
    /// Gets or sets the disabled state.
    /// </summary>
    /// <remarks>
    /// Default value is false.
    /// </remarks>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets the associated <see cref="Microsoft.AspNetCore.Components.Forms.EditContext" />.
    /// </summary>
    [CascadingParameter]
    private EditContext? EditContext { get; set; } = default!;

    /// <summary>
    /// Gets or sets the label.
    /// </summary>
    [Parameter]
    public string? Label { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [Parameter]
    public bool Value { get; set; }

    /// <summary>
    /// This event fired on every user keystroke that changes the NumberInput value.
    /// </summary>
    [Parameter]
    public EventCallback<bool> ValueChanged { get; set; }

    [Parameter] public Expression<Func<bool>> ValueExpression { get; set; } = default!;

    #endregion
}
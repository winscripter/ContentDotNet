using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264;

/// <summary>
/// Represents the constraint set flags for an H.264 Sequence Parameter Set (SPS).
/// </summary>
public struct H264Constraints : IEquatable<H264Constraints>
{
    internal static readonly IReadOnlyDictionary<H264Profile, H264Constraints> s_profileToConstraintMapping =
        new Dictionary<H264Profile, H264Constraints>
        {
            [H264Profile.Baseline] = new H264Constraints(constraint0: true, constraint1: true, constraint2: false, constraint3: false, constraint4: false, constraint5: false),
            [H264Profile.Main] = new H264Constraints(constraint0: false, constraint1: true, constraint2: false, constraint3: false, constraint4: false, constraint5: false),
            [H264Profile.Extended] = new H264Constraints(constraint0: false, constraint1: false, constraint2: true, constraint3: false, constraint4: false, constraint5: false),
            [H264Profile.High] = new H264Constraints(constraint0: false, constraint1: false, constraint2: false, constraint3: false, constraint4: false, constraint5: false),
            [H264Profile.High10] = new H264Constraints(constraint0: false, constraint1: false, constraint2: false, constraint3: false, constraint4: true, constraint5: false),
            [H264Profile.High422] = new H264Constraints(constraint0: false, constraint1: false, constraint2: false, constraint3: false, constraint4: false, constraint5: true),
            [H264Profile.High444Predictive] = new H264Constraints(constraint0: false, constraint1: false, constraint2: false, constraint3: false, constraint4: false, constraint5: false),
            [H264Profile.Cavlc444Intra] = new H264Constraints(constraint0: false, constraint1: false, constraint2: false, constraint3: true, constraint4: false, constraint5: false),
            [H264Profile.ScalableBaseline] = new H264Constraints(constraint0: false, constraint1: false, constraint2: false, constraint3: false, constraint4: false, constraint5: false),
            [H264Profile.ScalableHigh] = new H264Constraints(constraint0: false, constraint1: false, constraint2: false, constraint3: false, constraint4: false, constraint5: false),
            [H264Profile.ScalableHighIntra] = new H264Constraints(constraint0: false, constraint1: false, constraint2: false, constraint3: true, constraint4: false, constraint5: false),
            [H264Profile.MultiviewHigh] = new H264Constraints(constraint0: false, constraint1: false, constraint2: false, constraint3: false, constraint4: false, constraint5: false),
            [H264Profile.StereoHigh] = new H264Constraints(constraint0: false, constraint1: false, constraint2: false, constraint3: false, constraint4: false, constraint5: false),
        };

    private bool _constraint0;
    private bool _constraint1;
    private bool _constraint2;
    private bool _constraint3;
    private bool _constraint4;
    private bool _constraint5;

    /// <summary>
    /// Initializes a new instance of the <see cref="H264Constraints"/> struct with all constraints set to <c>false</c>.
    /// </summary>
    public H264Constraints()
    {
        _constraint0 = false;
        _constraint1 = false;
        _constraint2 = false;
        _constraint3 = false;
        _constraint4 = false;
        _constraint5 = false;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="H264Constraints"/> struct with the specified constraint flags.
    /// </summary>
    /// <param name="constraint0">Constraint set 0 flag.</param>
    /// <param name="constraint1">Constraint set 1 flag.</param>
    /// <param name="constraint2">Constraint set 2 flag.</param>
    /// <param name="constraint3">Constraint set 3 flag.</param>
    /// <param name="constraint4">Constraint set 4 flag.</param>
    /// <param name="constraint5">Constraint set 5 flag.</param>
    public H264Constraints(bool constraint0, bool constraint1, bool constraint2, bool constraint3, bool constraint4, bool constraint5)
    {
        _constraint0 = constraint0;
        _constraint1 = constraint1;
        _constraint2 = constraint2;
        _constraint3 = constraint3;
        _constraint4 = constraint4;
        _constraint5 = constraint5;
    }

    /// <summary>
    /// Gets or sets the constraint set 0 flag.
    /// </summary>
    public bool Constraint0
    {
        readonly get => _constraint0;
        set => _constraint0 = value;
    }

    /// <summary>
    /// Gets or sets the constraint set 1 flag.
    /// </summary>
    public bool Constraint1
    {
        readonly get => _constraint1;
        set => _constraint1 = value;
    }

    /// <summary>
    /// Gets or sets the constraint set 2 flag.
    /// </summary>
    public bool Constraint2
    {
        readonly get => _constraint2;
        set => _constraint2 = value;
    }

    /// <summary>
    /// Gets or sets the constraint set 3 flag.
    /// </summary>
    public bool Constraint3
    {
        readonly get => _constraint3;
        set => _constraint3 = value;
    }

    /// <summary>
    /// Gets or sets the constraint set 4 flag.
    /// </summary>
    public bool Constraint4
    {
        readonly get => _constraint4;
        set => _constraint4 = value;
    }

    /// <summary>
    /// Gets or sets the constraint set 5 flag.
    /// </summary>
    public bool Constraint5
    {
        readonly get => _constraint5;
        set => _constraint5 = value;
    }

    /// <summary>
    /// Creates a <see cref="H264Constraints"/> instance from a <see cref="SequenceParameterSet"/>.
    /// </summary>
    /// <param name="sps">The sequence parameter set to extract constraint flags from.</param>
    /// <returns>A new <see cref="H264Constraints"/> instance with flags set from the SPS.</returns>
    public static H264Constraints FromSequenceParameterSet(SequenceParameterSet sps)
    {
        return new H264Constraints
        {
            Constraint0 = sps.ConstraintSet0Flag,
            Constraint1 = sps.ConstraintSet1Flag,
            Constraint2 = sps.ConstraintSet2Flag,
            Constraint3 = sps.ConstraintSet3Flag,
            Constraint4 = sps.ConstraintSet4Flag,
            Constraint5 = sps.ConstraintSet5Flag
        };
    }

    /// <summary>
    ///   Returns all 6 constraint flags for the specified H.264 profile.
    /// </summary>
    /// <param name="profile">The input H.264 profile.</param>
    /// <returns>Constraint flags for the given profile.</returns>
    public static H264Constraints FromProfile(H264Profile profile) =>
        s_profileToConstraintMapping.TryGetValue(profile, out var constraints)
            ? constraints
            : default;

    /// <inheritdoc/>
    public readonly override bool Equals(object? obj)
    {
        return obj is H264Constraints constraints && Equals(constraints);
    }

    /// <summary>
    /// Determines whether the specified <see cref="H264Constraints"/> is equal to the current instance.
    /// </summary>
    /// <param name="other">The other <see cref="H264Constraints"/> to compare.</param>
    /// <returns><c>true</c> if the specified instance is equal to the current instance; otherwise, <c>false</c>.</returns>
    public readonly bool Equals(H264Constraints other)
    {
        return _constraint0 == other._constraint0 &&
               _constraint1 == other._constraint1 &&
               _constraint2 == other._constraint2 &&
               _constraint3 == other._constraint3 &&
               _constraint4 == other._constraint4 &&
               _constraint5 == other._constraint5;
    }

    /// <inheritdoc/>
    public readonly override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(_constraint0);
        hash.Add(_constraint1);
        hash.Add(_constraint2);
        hash.Add(_constraint3);
        hash.Add(_constraint4);
        hash.Add(_constraint5);
        return hash.ToHashCode();
    }

    /// <summary>
    /// Determines whether two <see cref="H264Constraints"/> instances are equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(H264Constraints left, H264Constraints right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="H264Constraints"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(H264Constraints left, H264Constraints right)
    {
        return !(left == right);
    }
}

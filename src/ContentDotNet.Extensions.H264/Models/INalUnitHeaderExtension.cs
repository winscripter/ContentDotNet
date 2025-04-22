using ContentDotNet.Abstractions;

namespace ContentDotNet.Extensions.H264.Models;

/// <summary>
///   This is a marker interface that can represent one of the following types:
///   <list type="bullet">
///     <item><see cref="Avc3DNalUnitHeaderExtension"/></item>
///     <item><see cref="MvcNalUnitHeaderExtension"/></item>
///     <item><see cref="SvcNalUnitHeaderExtension"/></item>
///   </list>
/// </summary>
public interface INalUnitHeaderExtension
{
}
